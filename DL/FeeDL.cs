using MySql.Data.MySqlClient;
using System;
using System.Data;
using hostel_management_system_oop.Model;

namespace hostel_management_system_oop.DL
{
    public class FeeDL
    {
        public DataTable GetAllFees()
        {
            string query = "SELECT FeeID, RollNumber, AmountDue, AmountPaid, DueDate, PaymentStatus FROM fees ORDER BY FeeID DESC";
            return DatabaseConnection.ExecuteQuery(query);
        }

        public DataTable GetFeeById(int feeID)
        {
            string query = "SELECT * FROM fees WHERE FeeID = @id";
            MySqlParameter[] parameters = { new MySqlParameter("@id", feeID) };
            return DatabaseConnection.ExecuteQuery(query, parameters);
        }

        public bool InsertFee(FeeModel fee)
        {
            string query = @"INSERT INTO fees (RollNumber, AmountDue, AmountPaid, DueDate, PaymentStatus) 
                             VALUES (@roll, @due, @paid, @date, @status)";

            MySqlParameter[] parameters = {
                new MySqlParameter("@roll", fee.RollNumber),
                new MySqlParameter("@due", fee.AmountDue),
                new MySqlParameter("@paid", fee.AmountPaid),
                new MySqlParameter("@date", fee.DueDate),
                new MySqlParameter("@status", fee.PaymentStatus)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateFee(FeeModel fee)
        {
            string query = @"UPDATE fees SET 
                             RollNumber = @roll,
                             AmountDue = @due,
                             AmountPaid = @paid,
                             DueDate = @date,
                             PaymentStatus = @status
                             WHERE FeeID = @id";

            MySqlParameter[] parameters = {
                new MySqlParameter("@roll", fee.RollNumber),
                new MySqlParameter("@due", fee.AmountDue),
                new MySqlParameter("@paid", fee.AmountPaid),
                new MySqlParameter("@date", fee.DueDate),
                new MySqlParameter("@status", fee.PaymentStatus),
                new MySqlParameter("@id", fee.FeeID)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteFee(int feeID)
        {
            string query = "DELETE FROM fees WHERE FeeID = @id";
            MySqlParameter[] parameters = { new MySqlParameter("@id", feeID) };
            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}