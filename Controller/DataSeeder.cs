using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Environmental_Monitoring.Controller
{
    public class DataSeeder
    {
        // Đường dẫn file CSV - Bạn sửa lại cho đúng máy bạn nếu cần
        private const string TrainingDataPath = @"C:\Github\resign_training.csv";

        public static void GenerateTrainingData()
        {
            var csvContent = new StringBuilder();
            // Header cập nhật thêm cột CorrectionCount và DaysOverdue
            csvContent.AppendLine("ContractType,WasOnTime,TotalContracts,CorrectionCount,DaysOverdue,Label");

            Random rand = new Random();
            int numberOfRecords = 2000; // 2000 mẫu để học kỹ

            for (int i = 0; i < numberOfRecords; i++)
            {
                // 1. Sinh dữ liệu ngẫu nhiên cơ bản
                string contractType = rand.Next(0, 2) == 0 ? "Quarterly" : "Semiannual";
                float totalContracts = rand.Next(1, 16); // 1 đến 15 hợp đồng

                // 2. Sinh dữ liệu trễ hạn (DaysOverdue)
                // 30% đơn hàng bị trễ (Expired/Expired Complete)
                bool isLate = rand.Next(0, 100) < 30;
                string wasOnTime = isLate ? "Late" : "OnTime";

                // Nếu Late thì trễ từ 1-15 ngày, nếu OnTime thì 0 ngày
                float daysOverdue = isLate ? rand.Next(1, 16) : 0;

                // 3. Sinh dữ liệu yêu cầu chỉnh sửa (CorrectionCount)
                // 70% không sửa, 20% sửa 1 lần, 10% sửa nhiều (>1 lần)
                float correctionCount = 0;
                int r = rand.Next(0, 100);
                if (r > 70 && r <= 90) correctionCount = 1;
                else if (r > 90) correctionCount = rand.Next(2, 6);

                // 4. LOGIC QUYẾT ĐỊNH NHÃN (LABEL) - CÔNG THỨC CHẤM ĐIỂM
                bool label;
                int penaltyScore = 0; // Điểm phạt

                // - Phạt vì trễ hạn
                if (daysOverdue > 0 && daysOverdue <= 3) penaltyScore += 20; // Trễ ít
                else if (daysOverdue > 3 && daysOverdue <= 7) penaltyScore += 50; // Trễ vừa
                else if (daysOverdue > 7) penaltyScore += 100; // Trễ quá lâu -> Chắc chắn bỏ

                // - Phạt vì chất lượng (phải sửa nhiều)
                if (correctionCount == 1) penaltyScore += 10;
                else if (correctionCount >= 2) penaltyScore += 60; // Khách cực khó chịu

                // - Điểm thưởng (Bonus) vì sự trung thành
                if (totalContracts > 5) penaltyScore -= 30; // Khách quen dễ bỏ qua lỗi
                if (totalContracts > 10) penaltyScore -= 50; // Khách VIP

                // Chuẩn hóa điểm phạt 0-100
                if (penaltyScore < 0) penaltyScore = 0;
                if (penaltyScore > 100) penaltyScore = 100;

                // Random quyết định dựa trên điểm phạt
                // Nếu Random < Điểm phạt -> Khách bỏ (False)
                if (rand.Next(0, 100) < penaltyScore)
                    label = false;
                else
                    label = true;

                // Ghi vào CSV
                csvContent.AppendLine($"{contractType},{wasOnTime},{totalContracts},{correctionCount},{daysOverdue},{label}");
            }

            try
            {
                File.WriteAllText(TrainingDataPath, csvContent.ToString());
                MessageBox.Show($"Đã tạo xong dữ liệu huấn luyện tại:\n{TrainingDataPath}\nSố lượng: {numberOfRecords} dòng.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo file: " + ex.Message);
            }
        }
    }
}