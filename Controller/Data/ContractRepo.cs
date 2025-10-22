using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Environmental_Monitoring.Model;

namespace Environmental_Monitoring.Controller.Data
{
    internal class ContractRepo
    {
        private readonly string _connectionString;

        public ContractRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Contract> GetAll()
        {
            var list = new List<Contract>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                // Lấy tất cả các cột từ bảng Contracts
                string sql = "SELECT ContractID, MaDon, CustomerID, EmployeeID, NgayKy, NgayTraKetQua, ContractType, Status FROM Contracts";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Contract
                    {
                        ContractID = Convert.ToInt32(reader["ContractID"]),
                        MaDon = reader["MaDon"].ToString(),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        EmployeeID = Convert.ToInt32(reader["EmployeeID"]),

                        // Kiểm tra DBNull cho các trường DATE (có thể null)
                        NgayKy = reader["NgayKy"] != DBNull.Value ? Convert.ToDateTime(reader["NgayKy"]) : (DateTime?)null,
                        NgayTraKetQua = reader["NgayTraKetQua"] != DBNull.Value ? Convert.ToDateTime(reader["NgayTraKetQua"]) : (DateTime?)null,

                        ContractType = reader["ContractType"].ToString(),
                        Status = reader["Status"].ToString()
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// Thêm một hợp đồng mới
        /// </summary>
        public void Add(Contract contract)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                // Không cần chèn ContractID vì nó là AUTO_INCREMENT
                string sql = "INSERT INTO Contracts (MaDon, CustomerID, EmployeeID, NgayKy, NgayTraKetQua, ContractType, Status) " +
                             "VALUES (@MaDon, @CustomerID, @EmployeeID, @NgayKy, @NgayTraKetQua, @ContractType, @Status)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Thêm các tham số
                cmd.Parameters.AddWithValue("@MaDon", contract.MaDon);
                cmd.Parameters.AddWithValue("@CustomerID", contract.CustomerID);
                cmd.Parameters.AddWithValue("@EmployeeID", contract.EmployeeID);

                // Xử lý giá trị NULL cho kiểu nullable DateTime?
                cmd.Parameters.AddWithValue("@NgayKy", (object)contract.NgayKy ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayTraKetQua", (object)contract.NgayTraKetQua ?? DBNull.Value);

                cmd.Parameters.AddWithValue("@ContractType", contract.ContractType);

                // Nếu Status có DEFAULT 'Active', bạn có thể chọn bỏ qua nếu nó null
                // Ở đây ta mặc định là gán giá trị từ object
                cmd.Parameters.AddWithValue("@Status", (object)contract.Status ?? "Active"); // Gán 'Active' nếu null

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Contract contract)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                // Cập nhật dựa trên khóa chính (PK) là ContractID
                string sql = "UPDATE Contracts SET " +
                             "MaDon = @MaDon, " +
                             "CustomerID = @CustomerID, " +
                             "EmployeeID = @EmployeeID, " +
                             "NgayKy = @NgayKy, " +
                             "NgayTraKetQua = @NgayTraKetQua, " +
                             "ContractType = @ContractType, " +
                             "Status = @Status " +
                             "WHERE ContractID = @ContractID";
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Thêm các tham số
                cmd.Parameters.AddWithValue("@MaDon", contract.MaDon);
                cmd.Parameters.AddWithValue("@CustomerID", contract.CustomerID);
                cmd.Parameters.AddWithValue("@EmployeeID", contract.EmployeeID);
                cmd.Parameters.AddWithValue("@NgayKy", (object)contract.NgayKy ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayTraKetQua", (object)contract.NgayTraKetQua ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ContractType", contract.ContractType);
                cmd.Parameters.AddWithValue("@Status", contract.Status);

                // Tham số cho mệnh đề WHERE
                cmd.Parameters.AddWithValue("@ContractID", contract.ContractID);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Xóa hợp đồng dựa trên ContractID (Khóa chính)
        /// </summary>
        public void Delete(int contractID) // Sửa thành int vì ContractID là INT
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Contracts WHERE ContractID = @ContractID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContractID", contractID);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
