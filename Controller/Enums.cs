using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Environmental_Monitoring.Controller
{
    // Dùng để quản lý vai trò người dùng
    public enum UserRole
    {
        Admin = 5,
        Lab = 7,
        Planning = 10
    }

    // Dùng để quản lý các bước tiến trình
    public enum ContractProcess
    {
        New = 1,
        Sampling = 2,
        Testing = 3,
        Approval = 4,
        Completed = 5
    }
}
