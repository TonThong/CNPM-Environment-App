using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Environmental_Monitoring.Controller;
using Environmental_Monitoring.Controller.Data;
using Environmental_Monitoring.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.Components
{
    public partial class FaceIDLoginForm : Form
    {
        private VideoCapture _capture;
        private CascadeClassifier _cascadeClassifier;
        private List<LBPHFaceRecognizer> _recognizers = new List<LBPHFaceRecognizer>();
        private Dictionary<int, int> _recognizerLabelMap = new Dictionary<int, int>();
        private int _recognizerIndex = 0;

        private Form _loginForm;

        private DateTime _lastRecognitionTime = DateTime.MinValue; 
        private const int RECOGNITION_DELAY_MS = 1000;

        public FaceIDLoginForm(Form loginForm)
        {
            InitializeComponent();
            _loginForm = loginForm;

            _capture = new VideoCapture(0);
            _cascadeClassifier = new CascadeClassifier("haarcascade_frontalface_default.xml");

            if (!LoadAllFaceData())
            {
                MessageBox.Show("Chưa có dữ liệu khuôn mặt nào được đăng ký trong hệ thống.");
                this.Close();
                return;
            }

            Application.Idle += ProcessFrame;
            this.Text = "Đang nhận diện...";
        }

        private bool LoadAllFaceData()
        {
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT EmployeeID, FaceIDData FROM Employees WHERE FaceIDData IS NOT NULL";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int employeeId = reader.GetInt32("EmployeeID");
                            byte[] modelData = (byte[])reader["FaceIDData"];

                            if (modelData != null && modelData.Length > 0)
                            {
                                LBPHFaceRecognizer recognizer = new LBPHFaceRecognizer();
                                string tempFile = Path.GetTempFileName();
                                try
                                {
                                    File.WriteAllBytes(tempFile, modelData);
                                    recognizer.Read(tempFile);
                                    _recognizers.Add(recognizer);
                                }
                                finally
                                {
                                    if (File.Exists(tempFile))
                                    {
                                        File.Delete(tempFile);
                                    }
                                }
                            }
                        }
                    }
                }
                return _recognizers.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu khuôn mặt: " + ex.Message);
                return false;
            }
        }

        private void ProcessFrame(object sender, EventArgs e)
        {
            using (Mat frame = _capture.QueryFrame())
            {
                if (frame != null)
                {
                    Image<Bgr, byte> image = frame.ToImage<Bgr, byte>();
                    Image<Gray, byte> grayImage = image.Convert<Gray, byte>();
                    Rectangle[] faces = _cascadeClassifier.DetectMultiScale(grayImage, 1.1, 4);

                    foreach (Rectangle face in faces)
                    {
                        image.Draw(face, new Bgr(Color.Red), 2);
                        Image<Gray, byte> testFace = grayImage.Copy(face).Resize(100, 100, Emgu.CV.CvEnum.Inter.Cubic);

                        if (DateTime.Now - _lastRecognitionTime > TimeSpan.FromMilliseconds(RECOGNITION_DELAY_MS))
                        {
                            _lastRecognitionTime = DateTime.Now;

                            foreach (var recognizer in _recognizers)
                            {
                                var result = recognizer.Predict(testFace);
                                if (result.Label > 0 && result.Distance < 90)
                                {
                                    Application.Idle -= ProcessFrame;
                                    _capture.Dispose();
                                    LoginSuccess(result.Label);
                                    return;
                                }
                            }
                        }
                    }
                    pictureBox1.Image = image.ToBitmap();
                }
            }
        }

        private void LoginSuccess(int employeeId)
        {
            try
            {
                Model.Employee user = EmployeeRepo.Instance.GetById(employeeId);

                if (user != null)
                {
                    if (user.RoleID.HasValue && user.RoleID > 0 && RoleRepo.Instance != null)
                    {
                        user.Role = RoleRepo.Instance.GetById(user.RoleID.Value);
                    }
                    UserSession.StartSession(user);

                    Mainlayout mainForm = new Mainlayout();

                    if (!UserSession.IsAdmin())
                    {
                        mainForm.LoadContractPageForEmployee();
                    }

                    mainForm.Show();

                    _loginForm.Hide();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng nhập: " + ex.Message);
                Application.Idle += ProcessFrame;
                _capture = new VideoCapture(0);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Idle -= ProcessFrame;
            _capture?.Dispose();
            _cascadeClassifier?.Dispose();
            base.OnFormClosing(e);
        }
    }
}