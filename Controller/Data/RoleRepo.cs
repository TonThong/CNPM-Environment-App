using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Environmental_Monitoring.Controller.Data
{
    public class RoleRepo
    {
        private static RoleRepo instance;
        public static RoleRepo Instance
        {
            get
            {
                if (instance == null)
                    instance = new RoleRepo();
                return instance;
            }
            private set => instance = value;
        }

        private RoleRepo()
        {
        }
        public DataTable GetAll()
        {
            string query = "SELECT RoleID, RoleName FROM Roles";
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
