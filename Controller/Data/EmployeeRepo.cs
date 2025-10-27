using Environmental_Monitoring.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Environmental_Monitoring.Controller.Data
{
    public class EmployeeRepo
    {
        private static EmployeeRepo instance;
        public static EmployeeRepo Instance
        {
            get
            {
                if (instance == null)
                    instance = new EmployeeRepo();
                return instance;
            }
            private set => instance = value;
        }

        private EmployeeRepo()
        {
        }

        public Employee Login(string username)
        {
            string query = @"SELECT
 	                        e.EmployeeID,
 	                        e.MaNhanVien,
 	                        e.HoTen,
 	                        e.NamSinh,
 	                        e.PhongBan,
 	                        e.DiaChi,
 	                        e.SoDienThoai,
 	                        e.Email,
                            e.PasswordHash,
 	                        e.RoleID
                         FROM Employees e
                         WHERE e.Email = @username";
            var data = DataProvider.Instance.ExecuteQuery(query, new object[] { username });
            if (data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                return new Employee(row);
            }
            return null;
        }

        public DataTable GetAll()
        {
            string query = @"SELECT
 	                        e.EmployeeID,
 	                        e.MaNhanVien,
 	                        e.HoTen,
 	                        e.NamSinh,
 	                        e.PhongBan,
 	                        e.DiaChi,
 	                        e.SoDienThoai,
 	                        e.Email,
 	                        e.RoleID,
 	                        r.RoleName
                         FROM Employees e
                         INNER JOIN Roles r ON e.RoleID = r.RoleId";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public Employee GetById(int employeeId)
        {
            string query = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { employeeId });
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new Employee(row);
            }
            return null;
        }

        public void InsertEmployee(Employee employee)
        {
            string query = @"INSERT INTO Employees (MaNhanVien, HoTen, NamSinh, PhongBan, DiaChi, SoDienThoai, Email, PasswordHash, RoleID)
                             VALUES (@MaNhanVien, @HoTen, @NamSinh, @PhongBan, @DiaChi, @SoDienThoai, @Email, @PasswordHash, @RoleID)";
            DataProvider.Instance.ExecuteNonQuery(query, new object[]
            {
                employee.MaNhanVien,
                employee.HoTen,
                employee.NamSinh,
                employee.PhongBan,
                employee.DiaChi,
                employee.SoDienThoai,
                employee.Email,
                employee.PasswordHash,
                employee.RoleID
            });
        }

        public void UpdateEmployee(Employee employee)
        {
            string query = @"UPDATE Employees
                             SET HoTen = @HoTen,
                                 NamSinh = @NamSinh,
                                 PhongBan = @PhongBan,
                                 DiaChi = @DiaChi,
                                 SoDienThoai = @SoDienThoai,
                                 Email = @Email,
                                 PasswordHash = @PasswordHash,
                                 RoleID = @RoleID
                             WHERE EmployeeID = @EmployeeID";
            DataProvider.Instance.ExecuteNonQuery(query, new object[]
            {
                employee.HoTen,
                employee.NamSinh,
                employee.PhongBan,
                employee.DiaChi,
                employee.SoDienThoai,
                employee.Email,
                employee.PasswordHash,
                employee.RoleID,
                employee.EmployeeID
            });
        }

        public void DeleteEmployee(int employeeId)
        {
            string query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { employeeId });
        }

        public bool ExistsMaNhanVien(string code, int? id = 0)
        {
            code = code.Trim().ToUpper();
            return DataProvider.Instance.ExecuteScalar("SELECT EmployeeId FROM Employees WHERE UPPER(MaNhanVien) = @code AND EmployeeId != @id", new object[] { code, id }) != null;
        }

        public bool ExistsAnotherTable(int id)
        {
            return DataProvider.Instance
                    .ExecuteScalar(
                    "SELECT ContractID FROM Contracts WHERE EmployeeId = @id",
                    new object[] { id }) != null ||
                DataProvider.Instance
                    .ExecuteScalar(
                    "SELECT AssignedToHT FROM EnvironmentalSamples WHERE AssignedToHT = @id",
                    new object[] { id }) != null ||
                DataProvider.Instance
                    .ExecuteScalar(
                    "SELECT AssignedToPTN FROM EnvironmentalSamples WHERE AssignedToPTN = @id",
                    new object[] { id }) != null;
        }

        public DataTable Filter(string keySearch)
        {
            string searchPattern = "%" + keySearch.Trim() + "%";
            string query = @"SELECT
                        e.EmployeeID,
                        e.MaNhanVien,
                        e.HoTen,
                        e.NamSinh,
                        e.PhongBan,
                        e.DiaChi,
                        e.SoDienThoai,
                        e.Email,
                        e.RoleID,
                        r.RoleName
                     FROM Employees e
                     INNER JOIN Roles r ON e.RoleID = r.RoleId
                     WHERE UPPER(e.MaNhanVien) LIKE UPPER(@keySearch)
                        OR UPPER(e.HoTen) LIKE UPPER(@keySearch1)
                        OR UPPER(e.PhongBan) LIKE UPPER(@keySearch2)
                        OR UPPER(e.SoDienThoai) LIKE UPPER(@keySearch3)
                        OR UPPER(e.Email) LIKE UPPER(@keySearch4)";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { searchPattern, searchPattern, searchPattern, searchPattern, searchPattern });
        }

        public PagedResult GetEmployees(int pageNumber, int pageSize, string keySearch = "")
        {
            PagedResult result = new PagedResult();
            string baseQuery = @"FROM Employees e
                             INNER JOIN Roles r ON e.RoleID = r.RoleId";
            string whereClause = "";
            object[] parameters;

            // Xử lý tìm kiếm
            if (!string.IsNullOrWhiteSpace(keySearch))
            {
                string searchPattern = "%" + keySearch.Trim() + "%";
                whereClause = @"WHERE UPPER(e.MaNhanVien) LIKE UPPER(@keySearch1)
                             OR UPPER(e.HoTen) LIKE UPPER(@keySearch2)
                             OR UPPER(e.PhongBan) LIKE UPPER(@keySearch3)
                             OR UPPER(e.SoDienThoai) LIKE UPPER(@keySearch4)
                             OR UPPER(e.Email) LIKE UPPER(@keySearch5)";
                parameters = new object[] { searchPattern, searchPattern, searchPattern, searchPattern, searchPattern };
            }
            else
            {
                parameters = null;
            }

            // 1. Lấy TỔNG SỐ bản ghi
            string countQuery = "SELECT COUNT(e.EmployeeID) " + baseQuery + " " + whereClause;
            result.TotalCount = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(countQuery, parameters));

            // 2. Lấy DỮ LIỆU của trang
            int offset = (pageNumber - 1) * pageSize;
            string dataQuery = @"SELECT
                                e.EmployeeID, e.MaNhanVien, e.HoTen, e.NamSinh,
                                e.PhongBan, e.DiaChi, e.SoDienThoai, e.Email,
                                e.RoleID, r.RoleName " +
                                 baseQuery + " " + whereClause +
                                 " LIMIT @PageSize OFFSET @Offset";

            // Tạo mảng tham số MỚI cho query này
            object[] dataParameters;
            if (!string.IsNullOrWhiteSpace(keySearch))
            {
                // Gồm @keySearch, @PageSize, @Offset
                dataParameters = new object[] { parameters[0], parameters[0], parameters[0], parameters[0], parameters[0], pageSize, offset };
            }
            else
            {
                // Gồm @PageSize, @Offset
                dataParameters = new object[] { pageSize, offset };
            }

            result.Data = DataProvider.Instance.ExecuteQuery(dataQuery, dataParameters);

            return result;
        }
    }
}
