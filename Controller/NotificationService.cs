using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Environmental_Monitoring.Controller.Data;

namespace Environmental_Monitoring.Controller
{
    internal class NotificationService
    {

        public static event Action OnNotificationCreated;

        /// <summary>
        /// Tạo một thông báo mới trong CSDL cho một vai trò cụ thể.
        /// (Đã sửa lại để khớp với schema và dùng DEFAULT values)
        /// </summary>
        public static void CreateNotification(string loai, string noiDung, int recipientRoleID, int? contractId = null, int? employeeId = null)
        {
            try
            {
                string query = @"INSERT INTO Notifications (LoaiThongBao, NoiDung, RecipientRoleID, ContractID, EmployeeID_LienQuan)
                                 VALUES (@loai, @noiDung, @recipientRoleID, @contractId, @employeeId)";

                DataProvider.Instance.ExecuteNonQuery(query, new object[] {
                    loai,               // @loai
                    noiDung,            // @noiDung
                    recipientRoleID,    // @recipientRoleID
                    contractId,         // @contractId
                    employeeId          // @employeeId
                });

                OnNotificationCreated?.Invoke();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tạo thông báo: " + ex.Message);
                throw;
            }
        }
    }
}