using hostel_management_system_oop.DL;
using System.Data;

namespace hostel_management_system_oop.BL
{
    public class MessBL
    {
        private MessDL messDL = new MessDL();

        public string SaveMessData(string hallName, int capacity, string rollNo, string fullName, string department)
        {
            if (string.IsNullOrEmpty(hallName))
                return "Hall Name is required!";

            if (capacity <= 0)
                return "Capacity must be greater than zero!";

            if (string.IsNullOrEmpty(rollNo))
                return "Roll Number is required!";

            if (string.IsNullOrEmpty(fullName))
                return "Full Name is required!";

            bool saved = messDL.InsertMessData(hallName, capacity, rollNo, fullName, department);
            return saved ? "SUCCESS" : "Failed to save data!";
        }

        public string UpdateMessData(string oldHallName, string oldRollNo, string oldFullName,
                                     string hallName, int capacity, string rollNo, string fullName, string department)
        {
            if (string.IsNullOrEmpty(oldHallName) || string.IsNullOrEmpty(oldRollNo))
                return "Please select a record first!";

            bool updated = messDL.UpdateMessData(oldHallName, oldRollNo, oldFullName, hallName, capacity, rollNo, fullName, department);
            return updated ? "SUCCESS" : "Failed to update data!";
        }

        public string DeleteMessData(string hallName, string rollNo, string fullName)
        {
            if (string.IsNullOrEmpty(hallName) || string.IsNullOrEmpty(rollNo))
                return "Please select a record first!";

            bool deleted = messDL.DeleteMessData(hallName, rollNo, fullName);
            return deleted ? "SUCCESS" : "Failed to delete data!";
        }

        public DataTable GetAllMessData()
        {
            return messDL.GetAllMessData();
        }
    }
}