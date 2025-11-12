using MySql.Data.MySqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Configuration;

namespace Environmental_Monitoring.Controller
{
    public class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataProvider();
                return instance;
            }
            private set => instance = value;
        }

        private DataProvider()
        {

        }

        string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;

        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    if (parameter != null)
                    {
                        var matches = Regex.Matches(query, @"@\w+");

                        if (matches.Count != parameter.Length)
                        {
                            throw new ArgumentException(
                                $"Query tìm thấy {matches.Count} tham số nhưng chỉ nhận được {parameter.Length} giá trị."
                            );
                        }

                        for (int i = 0; i < matches.Count; i++)
                        {
                            command.Parameters.AddWithValue(matches[i].Value, parameter[i]);
                        }
                    }

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        return table;
                    }
                }
            }
        }

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    if (parameter != null)
                    {
                        var matches = Regex.Matches(query, @"@\w+");

                        if (matches.Count != parameter.Length)
                        {
                            throw new ArgumentException(
                                $"Query tìm thấy {matches.Count} tham số nhưng chỉ nhận được {parameter.Length} giá trị."
                            );
                        }

                        for (int i = 0; i < matches.Count; i++)
                        {
                            command.Parameters.AddWithValue(matches[i].Value, parameter[i]);
                        }
                    }

                    int result = command.ExecuteNonQuery();
                    connection.Close();

                    return result;
                }
            }
        }

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open();

                    if (parameter != null)
                    {
                        var matches = Regex.Matches(query, @"@\w+");

                        if (matches.Count != parameter.Length)
                        {
                            throw new ArgumentException(
                                $"Query tìm thấy {matches.Count} tham số nhưng chỉ nhận được {parameter.Length} giá trị."
                            );
                        }

                        for (int i = 0; i < matches.Count; i++)
                        {
                            command.Parameters.AddWithValue(matches[i].Value, parameter[i]);
                        }
                    }

                    var result = command.ExecuteScalar();
                    connection.Close();

                    return result;
                }
            }
        }

        public string GetConnectionString()
        {
            return connectionString;
        }
    }
}
