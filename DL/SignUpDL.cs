using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace hostel_management_system_oop.DL
{
    public class SignUpDL
    {
        public bool IsUsernameExists(string username)
        {
            string query = "SELECT COUNT(*) FROM owners WHERE Username = @user";
            MySqlParameter[] parameters = { new MySqlParameter("@user", username) };
            DataTable dt = DatabaseConnection.ExecuteQuery(query, parameters);
            return Convert.ToInt32(dt.Rows[0][0]) > 0;
        }

        public bool InsertOwner(string username, string password)
        {
            string query = "INSERT INTO owners (Username, Password) VALUES (@user, @pass)";
            MySqlParameter[] parameters = {
                new MySqlParameter("@user", username),
                new MySqlParameter("@pass", password)
            };
            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}