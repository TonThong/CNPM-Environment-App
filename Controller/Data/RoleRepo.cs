using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Environmental_Monitoring.Model; 
using MySql.Data.MySqlClient; 

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

        public Role GetById(int roleId)
        {
            Role role = null;
            using (MySqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "SELECT RoleID, RoleName FROM Roles WHERE RoleID = @ID";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", roleId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        role = new Role
                        {
                            RoleID = reader.GetInt32("RoleID"),
                            RoleName = reader.GetString("RoleName")
                        };
                    }
                }
            }
            return role;
        }
    }
}