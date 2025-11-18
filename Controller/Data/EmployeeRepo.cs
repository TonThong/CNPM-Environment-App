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
    // --- LỚP MỚI ĐỂ CHỨA CHI TIẾT LỖI ---
    public class UsageDetails
    {
        public string ReasonKey { get; set; } // Ví dụ: "Usage_Contract"
        public string Value { get; set; }     // Ví dụ: "HD-011"
    }
    // ------------------------------------

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
                                e.TruongBoPhan, 
                                e.DiaChi,
                                e.SoDienThoai,
                                e.Email,
                                e.PasswordHash,
                                e.RoleID,
                                r.RoleName 
                             FROM Employees e
                             LEFT JOIN Roles r ON e.RoleID = r.RoleId 
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
                                e.TruongBoPhan, 
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
            string query = @"INSERT INTO Employees (MaNhanVien, HoTen, NamSinh, TruongBoPhan, DiaChi, SoDienThoai, Email, PasswordHash, RoleID)
                                     VALUES (@MaNhanVien, @HoTen, @NamSinh, @TruongBoPhan, @DiaChi, @SoDienThoai, @Email, @PasswordHash, @RoleID)";
            DataProvider.Instance.ExecuteNonQuery(query, new object[]
            {
                employee.MaNhanVien,
                employee.HoTen,
                employee.NamSinh,
                employee.TruongBoPhan,
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
                                   TruongBoPhan = @TruongBoPhan, 
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
                employee.TruongBoPhan,
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

        public bool ExistsEmail(string email, int? id = 0)
        {
            email = email.Trim().ToUpper();
            return DataProvider.Instance.ExecuteScalar("SELECT EmployeeId FROM Employees WHERE UPPER(Email) = @email AND EmployeeId != @id", new object[] { email, id }) != null;
        }

        // --- HÀM ĐÃ SỬA: Trả về đối tượng UsageDetails thay vì string ---
        public UsageDetails GetEmployeeUsageDetails(int employeeId)
        {
            // Kiểm tra Hợp đồng
            string contractQuery = "SELECT MaDon FROM Contracts WHERE EmployeeId = @id LIMIT 1";
            object contractResult = DataProvider.Instance.ExecuteScalar(contractQuery, new object[] { employeeId });
            if (contractResult != null && contractResult != DBNull.Value)
            {
                return new UsageDetails { ReasonKey = "Usage_Contract", Value = contractResult.ToString() };
            }

            // Kiểm tra Mẫu Hiện trường
            string sampleHTQuery = "SELECT MaMau FROM EnvironmentalSamples WHERE AssignedToHT = @id LIMIT 1";
            object sampleHTResult = DataProvider.Instance.ExecuteScalar(sampleHTQuery, new object[] { employeeId });
            if (sampleHTResult != null && sampleHTResult != DBNull.Value)
            {
                return new UsageDetails { ReasonKey = "Usage_SampleHT", Value = sampleHTResult.ToString() };
            }

            // Kiểm tra Mẫu Thí nghiệm
            string samplePTNQuery = "SELECT MaMau FROM EnvironmentalSamples WHERE AssignedToPTN = @id LIMIT 1";
            object samplePTNResult = DataProvider.Instance.ExecuteScalar(samplePTNQuery, new object[] { employeeId });
            if (samplePTNResult != null && samplePTNResult != DBNull.Value)
            {
                return new UsageDetails { ReasonKey = "Usage_SamplePTN", Value = samplePTNResult.ToString() };
            }

            // Không tìm thấy, trả về null
            return null;
        }


        // --- HÀM ĐÃ SỬA: Gọi hàm mới ---
        public bool ExistsAnotherTable(int id)
        {
            return GetEmployeeUsageDetails(id) != null;
        }

        public DataTable Filter(string keySearch)
        {
            string searchPattern = "%" + keySearch.Trim() + "%";
            string query = @"SELECT
                                e.EmployeeID,
                                e.MaNhanVien,
                                e.HoTen,
                                e.NamSinh,
                                e.TruongBoPhan, 
                                e.DiaChi,
                                e.SoDienThoai,
                                e.Email,
                                e.RoleID,
                                r.RoleName
                               FROM Employees e
                               INNER JOIN Roles r ON e.RoleID = r.RoleId
                               WHERE UPPER(e.MaNhanVien) LIKE UPPER(@keySearch1)
                                 OR UPPER(e.HoTen) LIKE UPPER(@keySearch2)
                                 OR UPPER(e.SoDienThoai) LIKE UPPER(@keySearch3)
                                 OR UPPER(e.Email) LIKE UPPER(@keySearch4)";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { searchPattern, searchPattern, searchPattern, searchPattern });
        }

        public PagedResult GetEmployees(int pageNumber, int pageSize, string keySearch = "")
        {
            PagedResult result = new PagedResult();
            string baseQuery = @"FROM Employees e
                               INNER JOIN Roles r ON e.RoleID = r.RoleId";
            string whereClause = "";
            object[] parameters;
            if (!string.IsNullOrWhiteSpace(keySearch))
            {
                string searchPattern = "%" + keySearch.Trim() + "%";
                whereClause = @"WHERE UPPER(e.MaNhanVien) LIKE UPPER(@keySearch1)
                                  OR UPPER(e.HoTen) LIKE UPPER(@keySearch2)
                                  OR UPPER(e.SoDienThoai) LIKE UPPER(@keySearch3)
                                  OR UPPER(e.Email) LIKE UPPER(@keySearch4)";
                parameters = new object[] { searchPattern, searchPattern, searchPattern, searchPattern };
            }
            else
            {
                parameters = null;
            }
            string countQuery = "SELECT COUNT(e.EmployeeID) " + baseQuery + " " + whereClause;
            result.TotalCount = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(countQuery, parameters));
            int offset = (pageNumber - 1) * pageSize;
            string dataQuery = @"SELECT
                                       e.EmployeeID, e.MaNhanVien, e.HoTen, e.NamSinh,
                                       e.TruongBoPhan, e.DiaChi, e.SoDienThoai, e.Email, 
                                       e.RoleID, r.RoleName " +
                                       baseQuery + " " + whereClause +
                                       " ORDER BY e.EmployeeID LIMIT @PageSize OFFSET @Offset";
            object[] dataParameters;
            if (!string.IsNullOrWhiteSpace(keySearch))
            {
                dataParameters = new object[] { parameters[0], parameters[0], parameters[0], parameters[0], pageSize, offset };
            }
            else
            {
                dataParameters = new object[] { pageSize, offset };
            }

            result.Data = DataProvider.Instance.ExecuteQuery(dataQuery, dataParameters);

            return result;
        }

        // --- CÁC HÀM MỚI CHO CHỨC NĂNG QUÊN MẬT KHẨU ---

        public bool PhoneNumberExists(string phoneNumber)
        {
            string query = "SELECT COUNT(*) FROM Employees WHERE SoDienThoai = @phone";
            int count = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(query, new object[] { phoneNumber }));
            return count > 0;
        }

        public bool SetResetCode(string phoneNumber, string code, DateTime expiry)
        {
            string query = "UPDATE Employees SET ResetCode = @code , CodeExpiry = @expiry WHERE SoDienThoai = @phone";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { code, expiry, phoneNumber });
            return result > 0;
        }

        public DataRow GetResetCodeData(string phoneNumber)
        {
            string query = "SELECT ResetCode, CodeExpiry FROM Employees WHERE SoDienThoai = @phone";
            DataTable table = DataProvider.Instance.ExecuteQuery(query, new object[] { phoneNumber });
            if (table.Rows.Count > 0)
            {
                return table.Rows[0];
            }
            return null;
        }

        public bool UpdatePasswordAndClearCode(string phoneNumber, string hashedPassword)
        {
            string query = "UPDATE Employees SET PasswordHash = @pass , ResetCode = NULL, CodeExpiry = NULL WHERE SoDienThoai = @phone";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { hashedPassword, phoneNumber });
            return result > 0;
        }
    }
}