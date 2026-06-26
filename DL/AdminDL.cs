using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace hostel_management_system_oop.DL
{
    public class AdminDL
    {
        public bool ValidateLogin(string username, string password)
        {
            string query = "SELECT COUNT(*) FROM owners WHERE Username = @user AND Password = @pass";
            MySqlParameter[] parameters = {
                new MySqlParameter("@user", username),
                new MySqlParameter("@pass", password)
            };
            DataTable dt = DatabaseConnection.ExecuteQuery(query, parameters);
            return Convert.ToInt32(dt.Rows[0][0]) > 0;
        }
    }
}