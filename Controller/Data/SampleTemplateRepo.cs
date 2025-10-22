using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Environmental_Monitoring.Model;

namespace Environmental_Monitoring.Controller.Data
{
    internal class SampleTemplateRepo
    {
        private readonly string _connectionString;

        public SampleTemplateRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Lấy tất cả mẫu chuẩn
        /// </summary>
        public List<SampleTemplate> GetAll()
        {
            var list = new List<SampleTemplate>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT TemplateID, TenMau FROM SampleTemplates";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new SampleTemplate
                    {
                        TemplateID = Convert.ToInt32(reader["TemplateID"]),
                        TenMau = reader["TenMau"].ToString()
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// Thêm mẫu chuẩn mới
        /// </summary>
        public void Add(SampleTemplate template)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO SampleTemplates (TenMau) VALUES (@TenMau)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenMau", template.TenMau);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Cập nhật mẫu chuẩn
        /// </summary>
        public void Update(SampleTemplate template)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "UPDATE SampleTemplates SET TenMau = @TenMau WHERE TemplateID = @TemplateID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenMau", template.TenMau);
                cmd.Parameters.AddWithValue("@TemplateID", template.TemplateID);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Xóa mẫu chuẩn
        /// </summary>
        public void Delete(int templateID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM SampleTemplates WHERE TemplateID = @TemplateID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TemplateID", templateID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}


