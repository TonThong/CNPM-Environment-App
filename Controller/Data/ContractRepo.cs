using System;
using System.Collections.Generic;
using System.Data;
using Environmental_Monitoring.Model;
using Environmental_Monitoring.Controller;

namespace Environmental_Monitoring.Controller.Data
{
    internal class ContractRepo
    {
        private static ContractRepo instance;
        public static ContractRepo Instance
        {
            get => instance ?? (instance = new ContractRepo());
        }

        private ContractRepo() { }

        public List<Contract> GetAll()
        {
            var list = new List<Contract>();
            string sql = "SELECT ContractID, MaDon, CustomerID, EmployeeID, NgayKy, NgayTraKetQua, ContractType, Status, TienTrinh FROM Contracts";

            DataTable data = DataProvider.Instance.ExecuteQuery(sql);

            foreach (DataRow row in data.Rows)
            {
                list.Add(new Contract(row)); 
            }

            return list;
        }

        /// <summary>
        /// Thêm một hợp đồng mới
        /// </summary>
        public void Add(Contract contract)
        {
            string sql = @"INSERT INTO Contracts (MaDon, CustomerID, EmployeeID, NgayKy, NgayTraKetQua, ContractType, Status, TienTrinh) 
                            VALUES (@MaDon, @CustomerID, @EmployeeID, @NgayKy, @NgayTraKetQua, @ContractType, @Status, @TienTrinh)";

            object[] parameters = new object[]
            {
                contract.MaDon,
                contract.CustomerID,
                contract.EmployeeID,
                (object)contract.NgayKy ?? DBNull.Value,
                (object)contract.NgayTraKetQua ?? DBNull.Value,
                contract.ContractType,
                (object)contract.Status ?? "Active",
                (object)contract.TienTrinh ?? 1
            };

            DataProvider.Instance.ExecuteNonQuery(sql, parameters);
        }

        public void Update(Contract contract)
        {
            string sql = @"UPDATE Contracts SET 
                            MaDon = @MaDon, 
                            CustomerID = @CustomerID, 
                            EmployeeID = @EmployeeID, 
                            NgayKy = @NgayKy, 
                            NgayTraKetQua = @NgayTraKetQua, 
                            ContractType = @ContractType, 
                            Status = @Status,
                            TienTrinh = @TienTrinh
                            WHERE ContractID = @ContractID";

            object[] parameters = new object[]
            {
                contract.MaDon,
                contract.CustomerID,
                contract.EmployeeID,
                (object)contract.NgayKy ?? DBNull.Value,
                (object)contract.NgayTraKetQua ?? DBNull.Value,
                contract.ContractType,
                contract.Status,
                contract.TienTrinh,
                contract.ContractID
            };

            DataProvider.Instance.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Xóa hợp đồng dựa trên ContractID (Khóa chính)
        /// </summary>
        public void Delete(int contractID)
        {
            string sql = "DELETE FROM Contracts WHERE ContractID = @ContractID";
            DataProvider.Instance.ExecuteNonQuery(sql, new object[] { contractID });
        }
    }
}