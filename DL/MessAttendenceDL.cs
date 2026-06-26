using MySql.Data.MySqlClient;
using System;
using System.Data;
using hostel_management_system_oop.Model;

namespace hostel_management_system_oop.DL
{
    public class MessAttendanceDL
    {
        public DataTable GetAllAttendance()
        {
            string query = "SELECT MessAttendanceID, RollNumber, AttendanceDate, MealType, Status FROM messattendance ORDER BY MessAttendanceID DESC";
            return DatabaseConnection.ExecuteQuery(query);
        }

        public DataTable GetAttendanceById(int attendanceID)
        {
            string query = "SELECT * FROM messattendance WHERE MessAttendanceID = @id";
            MySqlParameter[] parameters = { new MySqlParameter("@id", attendanceID) };
            return DatabaseConnection.ExecuteQuery(query, parameters);
        }

        public bool InsertAttendance(MessAttendanceModel attendance)
        {
            string query = @"INSERT INTO messattendance (RollNumber, AttendanceDate, MealType, Status) 
                             VALUES (@roll, @date, @meal, @status)";

            MySqlParameter[] parameters = {
                new MySqlParameter("@roll", attendance.RollNumber),
                new MySqlParameter("@date", attendance.AttendanceDate),
                new MySqlParameter("@meal", attendance.MealType),
                new MySqlParameter("@status", attendance.Status)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateAttendance(MessAttendanceModel attendance)
        {
            string query = @"UPDATE messattendance SET 
                             RollNumber = @roll,
                             AttendanceDate = @date,
                             MealType = @meal,
                             Status = @status
                             WHERE MessAttendanceID = @id";

            MySqlParameter[] parameters = {
                new MySqlParameter("@roll", attendance.RollNumber),
                new MySqlParameter("@date", attendance.AttendanceDate),
                new MySqlParameter("@meal", attendance.MealType),
                new MySqlParameter("@status", attendance.Status),
                new MySqlParameter("@id", attendance.MessAttendanceID)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteAttendance(int attendanceID)
        {
            string query = "DELETE FROM messattendance WHERE MessAttendanceID = @id";
            MySqlParameter[] parameters = { new MySqlParameter("@id", attendanceID) };
            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}