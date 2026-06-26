using MySql.Data.MySqlClient;
using System.Data;

namespace hostel_management_system_oop.DL
{
    public class MessDL
    {
        public DataTable GetAllMessData()
        {
            string query = "SELECT * FROM mess";
            return DatabaseConnection.ExecuteQuery(query);
        }

        public bool InsertMessData(string hallName, int capacity, string rollNo, string fullName, string department)
        {
            string query = @"INSERT INTO mess (HallName, TotalSittingCapacity, RollNumber, FullName, Department) 
                             VALUES (@hall, @capacity, @roll, @name, @dept)";

            MySqlParameter[] parameters = {
                new MySqlParameter("@hall", hallName),
                new MySqlParameter("@capacity", capacity),
                new MySqlParameter("@roll", rollNo),
                new MySqlParameter("@name", fullName),
                new MySqlParameter("@dept", department)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateMessData(string oldHallName, string oldRollNo, string oldFullName,
                                   string hallName, int capacity, string rollNo, string fullName, string department)
        {
            string query = @"UPDATE mess SET 
                             HallName = @newHall,
                             TotalSittingCapacity = @capacity,
                             RollNumber = @newRoll,
                             FullName = @newName,
                             Department = @dept
                             WHERE HallName = @oldHall AND RollNumber = @oldRoll AND FullName = @oldName";

            MySqlParameter[] parameters = {
                new MySqlParameter("@newHall", hallName),
                new MySqlParameter("@capacity", capacity),
                new MySqlParameter("@newRoll", rollNo),
                new MySqlParameter("@newName", fullName),
                new MySqlParameter("@dept", department),
                new MySqlParameter("@oldHall", oldHallName),
                new MySqlParameter("@oldRoll", oldRollNo),
                new MySqlParameter("@oldName", oldFullName)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteMessData(string hallName, string rollNo, string fullName)
        {
            string query = "DELETE FROM mess WHERE HallName = @hall AND RollNumber = @roll AND FullName = @name";
            MySqlParameter[] parameters = {
                new MySqlParameter("@hall", hallName),
                new MySqlParameter("@roll", rollNo),
                new MySqlParameter("@name", fullName)
            };
            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}