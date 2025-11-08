using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Environmental_Monitoring.Model;

namespace Environmental_Monitoring.Controller.Data
{
    internal class EnvironmentalSampleRepo
    {
        private readonly string _connectionString = "Server=taa37w.h.filess.io;Database=environment_setsobtain;Uid=environment_setsobtain;Pwd=2efaa3d12c08f988586f5b378f36d9e1c2a1a794;";

        /// <summary>
        /// Lấy tất cả mẫu môi trường từ bảng EnvironmentalSamples
        /// </summary>
        public List<EnvironmentalSample> GetAll()
        {
            var list = new List<EnvironmentalSample>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"SELECT SampleID, MaMau, ContractID, TemplateID, AssignedToHT, AssignedToPTN 
                               FROM EnvironmentalSamples";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new EnvironmentalSample
                    {
                        SampleID = Convert.ToInt32(reader["SampleID"]),
                        MaMau = reader["MaMau"].ToString(),
                        ContractID = Convert.ToInt32(reader["ContractID"]),
                        TemplateID = Convert.ToInt32(reader["TemplateID"]),
                        AssignedToHT = reader["AssignedToHT"] != DBNull.Value ? Convert.ToInt32(reader["AssignedToHT"]) : (int?)null,
                        AssignedToPTN = reader["AssignedToPTN"] != DBNull.Value ? Convert.ToInt32(reader["AssignedToPTN"]) : (int?)null
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// Thêm một mẫu môi trường mới
        /// </summary>
        public void Add(EnvironmentalSample sample)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO EnvironmentalSamples (MaMau, ContractID, TemplateID, AssignedToHT, AssignedToPTN)
                               VALUES (@MaMau, @ContractID, @TemplateID, @AssignedToHT, @AssignedToPTN)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@MaMau", sample.MaMau);
                cmd.Parameters.AddWithValue("@ContractID", sample.ContractID);
                cmd.Parameters.AddWithValue("@TemplateID", sample.TemplateID);
                cmd.Parameters.AddWithValue("@AssignedToHT", (object)sample.AssignedToHT ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@AssignedToPTN", (object)sample.AssignedToPTN ?? DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Cập nhật mẫu môi trường theo SampleID
        /// </summary>
        public void Update(EnvironmentalSample sample)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"UPDATE EnvironmentalSamples 
                               SET MaMau = @MaMau,
                                   ContractID = @ContractID,
                                   TemplateID = @TemplateID,
                                   AssignedToHT = @AssignedToHT,
                                   AssignedToPTN = @AssignedToPTN
                               WHERE SampleID = @SampleID";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@MaMau", sample.MaMau);
                cmd.Parameters.AddWithValue("@ContractID", sample.ContractID);
                cmd.Parameters.AddWithValue("@TemplateID", sample.TemplateID);
                cmd.Parameters.AddWithValue("@AssignedToHT", (object)sample.AssignedToHT ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@AssignedToPTN", (object)sample.AssignedToPTN ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SampleID", sample.SampleID);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Xóa mẫu môi trường theo SampleID
        /// </summary>
        public void Delete(int sampleID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM EnvironmentalSamples WHERE SampleID = @SampleID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SampleID", sampleID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
