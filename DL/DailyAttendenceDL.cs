using MySql.Data.MySqlClient;
using System;
using System.Data;
using hostel_management_system_oop.Model;

namespace hostel_management_system_oop.DL
{
    public class DailyAttendenceDL
    {
        public DataTable GetAllAttendance()
        {
            string query = "SELECT AttendanceID, RollNumber, AttendanceDate, AttendanceStatus FROM dailyAttendance ORDER BY AttendanceID DESC";
            return DatabaseConnection.ExecuteQuery(query);
        }

        public DataTable GetAttendanceById(int attendanceID)
        {
            string query = "SELECT * FROM dailyAttendance WHERE AttendanceID = @id";
            MySqlParameter[] parameters = { new MySqlParameter("@id", attendanceID) };
            return DatabaseConnection.ExecuteQuery(query, parameters);
        }

        public bool InsertAttendance(DailyAttendenceModel attendance)
        {
            string query = @"INSERT INTO dailyAttendance (RollNumber, AttendanceDate, AttendanceStatus) 
                             VALUES (@roll, @date, @status)";

            MySqlParameter[] parameters = {
                new MySqlParameter("@roll", attendance.RollNumber),
                new MySqlParameter("@date", attendance.AttendanceDate),
                new MySqlParameter("@status", attendance.AttendanceStatus)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateAttendance(DailyAttendenceModel attendance)
        {
            string query = @"UPDATE dailyAttendance SET 
                             RollNumber = @roll,
                             AttendanceDate = @date,
                             AttendanceStatus = @status
                             WHERE AttendanceID = @id";

            MySqlParameter[] parameters = {
                new MySqlParameter("@roll", attendance.RollNumber),
                new MySqlParameter("@date", attendance.AttendanceDate),
                new MySqlParameter("@status", attendance.AttendanceStatus),
                new MySqlParameter("@id", attendance.AttendanceID)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteAttendance(int attendanceID)
        {
            string query = "DELETE FROM dailyAttendance WHERE AttendanceID = @id";
            MySqlParameter[] parameters = { new MySqlParameter("@id", attendanceID) };
            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}