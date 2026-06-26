using MySql.Data.MySqlClient;
using System;
using System.Data;
using hostel_management_system_oop.Model;

namespace hostel_management_system_oop.DL
{
    public class ComplaintDL
    {
        public DataTable GetAllComplaints()
        {
            string query = "SELECT ComplaintID, RollNumber, ComplaintTitle, ComplaintDescription, SubmissionDate, ComplaintStatus, IsNewNotification FROM complaints ORDER BY ComplaintID DESC";
            return DatabaseConnection.ExecuteQuery(query);
        }

        public DataTable GetComplaintById(int complaintID)
        {
            string query = "SELECT * FROM complaints WHERE ComplaintID = @id";
            MySqlParameter[] parameters = { new MySqlParameter("@id", complaintID) };
            return DatabaseConnection.ExecuteQuery(query, parameters);
        }

        public bool InsertComplaint(ComplaintModel complaint)
        {
            string query = @"INSERT INTO complaints (RollNumber, ComplaintTitle, ComplaintDescription, SubmissionDate, ComplaintStatus, IsNewNotification) 
                             VALUES (@roll, @title, @desc, @date, @status, 1)";

            MySqlParameter[] parameters = {
                new MySqlParameter("@roll", complaint.RollNumber),
                new MySqlParameter("@title", complaint.ComplaintTitle),
                new MySqlParameter("@desc", complaint.ComplaintDescription),
                new MySqlParameter("@date", DateTime.Now.Date),
                new MySqlParameter("@status", complaint.ComplaintStatus)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateComplaint(ComplaintModel complaint)
        {
            string query = @"UPDATE complaints SET 
                             RollNumber = @roll,
                             ComplaintTitle = @title,
                             ComplaintDescription = @desc,
                             ComplaintStatus = @status
                             WHERE ComplaintID = @id";

            MySqlParameter[] parameters = {
                new MySqlParameter("@roll", complaint.RollNumber),
                new MySqlParameter("@title", complaint.ComplaintTitle),
                new MySqlParameter("@desc", complaint.ComplaintDescription),
                new MySqlParameter("@status", complaint.ComplaintStatus),
                new MySqlParameter("@id", complaint.ComplaintID)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool MarkAsRead(int complaintID)
        {
            string query = "UPDATE complaints SET IsNewNotification = 0 WHERE ComplaintID = @id";
            MySqlParameter[] parameters = { new MySqlParameter("@id", complaintID) };
            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteComplaint(int complaintID)
        {
            string query = "DELETE FROM complaints WHERE ComplaintID = @id";
            MySqlParameter[] parameters = { new MySqlParameter("@id", complaintID) };
            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public int GetNewComplaintsCount()
        {
            string query = "SELECT COUNT(*) FROM complaints WHERE IsNewNotification = 1";
            DataTable dt = DatabaseConnection.ExecuteQuery(query);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
    }
}