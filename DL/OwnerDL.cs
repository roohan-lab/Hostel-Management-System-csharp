using hostel_management_system_oop.Model;
using MySql.Data.MySqlClient;
using System.Data;

namespace hostel_management_system_oop.DL
{
    public class OwnerProfileDL
    {
        public DataTable GetOwnerByUsername(string username)
        {
            string query = "SELECT * FROM owners WHERE Username = @user";
            MySqlParameter[] parameters = { new MySqlParameter("@user", username) };
            return DatabaseConnection.ExecuteQuery(query, parameters);
        }

        public bool UpdateOwner(Owner owner)
        {
            string query = @"UPDATE owners SET 
                             FullName = @name,
                             Password = @pass,
                             Email = @email,
                             PhoneNumber = @phone
                             WHERE Username = @user";

            MySqlParameter[] parameters = {
                new MySqlParameter("@name", owner.FullName),
                new MySqlParameter("@pass", owner.Password),
                new MySqlParameter("@email", owner.Email),
                new MySqlParameter("@phone", owner.PhoneNumber),
                new MySqlParameter("@user", owner.Username)
            };
            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}