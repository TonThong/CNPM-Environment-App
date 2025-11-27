using Environmental_Monitoring.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Environmental_Monitoring.Controller.Data
{
    internal class ParameterRepo
    {
        private readonly string _connectionString;

        public ParameterRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Lấy tất cả các thông số môi trường
        /// </summary>
        public List<Parameter> GetAll()
        {
            var list = new List<Parameter>();
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"SELECT ParameterID, TenThongSo, DonVi, GioiHanMin, GioiHanMax, PhuTrach FROM Parameters";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Parameter
                        {
                            ParameterID = Convert.ToInt32(reader["ParameterID"]),
                            TenThongSo = reader["TenThongSo"] != DBNull.Value ? reader["TenThongSo"].ToString() : null,
                            DonVi = reader["DonVi"] != DBNull.Value ? reader["DonVi"].ToString() : null,
                            GioiHanMin = reader["GioiHanMin"] != DBNull.Value ? Convert.ToDecimal(reader["GioiHanMin"]) : (decimal?)null,
                            GioiHanMax = reader["GioiHanMax"] != DBNull.Value ? Convert.ToDecimal(reader["GioiHanMax"]) : (decimal?)null,
                            PhuTrach = reader["PhuTrach"] != DBNull.Value ? reader["PhuTrach"].ToString() : null
                        });
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Thêm thông số mới
        /// </summary>
        public void Add(Parameter parameter)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO Parameters (TenThongSo, DonVi, GioiHanMin, GioiHanMax, PhuTrach)
                               VALUES (@TenThongSo, @DonVi, @GioiHanMin, @GioiHanMax, @PhuTrach)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@TenThongSo", parameter.TenThongSo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DonVi", (object)parameter.DonVi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@GioiHanMin", (object)parameter.GioiHanMin ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@GioiHanMax", (object)parameter.GioiHanMax ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PhuTrach", (object)parameter.PhuTrach ?? DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Cập nhật thông số
        /// </summary>
        public void Update(Parameter parameter)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"UPDATE Parameters 
                               SET TenThongSo = @TenThongSo,
                                   DonVi = @DonVi,
                                   GioiHanMin = @GioiHanMin,
                                   GioiHanMax = @GioiHanMax,
                                   PhuTrach = @PhuTrach
                               WHERE ParameterID = @ParameterID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@TenThongSo", parameter.TenThongSo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@DonVi", (object)parameter.DonVi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@GioiHanMin", (object)parameter.GioiHanMin ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@GioiHanMax", (object)parameter.GioiHanMax ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PhuTrach", (object)parameter.PhuTrach ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ParameterID", parameter.ParameterID);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Xóa thông số theo ID
        /// </summary>
        public void Delete(int parameterID)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Parameters WHERE ParameterID = @ParameterID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ParameterID", parameterID);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Lấy 1 thông số theo ID
        /// </summary>
        public Parameter GetByID(int parameterID)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"SELECT ParameterID, TenThongSo, DonVi, GioiHanMin, GioiHanMax, PhuTrach 
                               FROM Parameters WHERE ParameterID = @ParameterID";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ParameterID", parameterID);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Parameter
                        {
                            ParameterID = Convert.ToInt32(reader["ParameterID"]),
                            TenThongSo = reader["TenThongSo"] != DBNull.Value ? reader["TenThongSo"].ToString() : null,
                            DonVi = reader["DonVi"] != DBNull.Value ? reader["DonVi"].ToString() : null,
                            GioiHanMin = reader["GioiHanMin"] != DBNull.Value ? Convert.ToDecimal(reader["GioiHanMin"]) : (decimal?)null,
                            GioiHanMax = reader["GioiHanMax"] != DBNull.Value ? Convert.ToDecimal(reader["GioiHanMax"]) : (decimal?)null,
                            PhuTrach = reader["PhuTrach"] != DBNull.Value ? reader["PhuTrach"].ToString() : null
                        };
                    }
                }
            }
            return null;
        }
    }
}