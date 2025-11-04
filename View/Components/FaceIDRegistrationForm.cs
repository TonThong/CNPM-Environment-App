using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Environmental_Monitoring.Controller;
using Environmental_Monitoring.Controller.Data;
using Environmental_Monitoring.Model;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Environmental_Monitoring.View.Components
{
    public partial class FaceIDRegistrationForm : Form
    {
        private VideoCapture _capture;
        private CascadeClassifier _cascadeClassifier;
        private List<Image<Gray, byte>> _faceImages = new List<Image<Gray, byte>>();
        private int _captureCount = 0;
        private const int TOTAL_IMAGES_TO_CAPTURE = 10;
        private int _currentUserId;

        public FaceIDRegistrationForm()
        {
            InitializeComponent();
            _capture = new VideoCapture(0); 
            _cascadeClassifier = new CascadeClassifier("haarcascade_frontalface_default.xml");
            _currentUserId = UserSession.CurrentUser.EmployeeID; 

            Application.Idle += ProcessFrame;
            this.Text = "Đăng ký khuôn mặt";
            lblStatus.Text = $"Chuẩn bị chụp {TOTAL_IMAGES_TO_CAPTURE} ảnh...";
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
                        image.Draw(face, new Bgr(Color.Green), 2);

                        if (_captureCount < TOTAL_IMAGES_TO_CAPTURE)
                        {
                            Image<Gray, byte> capturedFace = grayImage.Copy(face).Resize(100, 100, Emgu.CV.CvEnum.Inter.Cubic);
                            _faceImages.Add(capturedFace);
                            _captureCount++;
                            lblStatus.Text = $"Đã chụp: {_captureCount}/{TOTAL_IMAGES_TO_CAPTURE} ảnh";
                        }
                    }

                    pictureBox1.Image = image.ToBitmap(); 

                    if (_captureCount >= TOTAL_IMAGES_TO_CAPTURE)
                    {
                        Application.Idle -= ProcessFrame; 
                        _capture.Dispose();
                        TrainAndSaveFace();
                    }
                }
            }
        }

        private void TrainAndSaveFace()
        {
            if (_faceImages.Count < TOTAL_IMAGES_TO_CAPTURE) return;

            LBPHFaceRecognizer recognizer = new LBPHFaceRecognizer();

            int[] labels = Enumerable.Repeat(_currentUserId, TOTAL_IMAGES_TO_CAPTURE).ToArray();

            var vectorOfImages = new Emgu.CV.Util.VectorOfMat();
            foreach (var img in _faceImages)
            {
                vectorOfImages.Push(img.Mat); 
            }

            var vectorOfLabels = new Emgu.CV.Util.VectorOfInt(labels);

            recognizer.Train(vectorOfImages, vectorOfLabels);

            byte[] modelData;
            string tempFile = Path.GetTempFileName();
            try
            {
                recognizer.Write(tempFile);

                modelData = File.ReadAllBytes(tempFile);
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
            try
            {
                using (MySqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Employees SET FaceIDData = @FaceData WHERE EmployeeID = @ID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FaceData", modelData);
                    cmd.Parameters.AddWithValue("@ID", _currentUserId);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Đăng ký khuôn mặt thành công!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu khuôn mặt: " + ex.Message);
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
