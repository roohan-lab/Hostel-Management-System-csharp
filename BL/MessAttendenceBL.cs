using hostel_management_system_oop.DL;
using hostel_management_system_oop.Model;
using System;
using System.Data;

namespace hostel_management_system_oop.BL
{
    public class MessAttendanceBL
    {
        private MessAttendanceDL attendanceDL = new MessAttendanceDL();

        private string ValidateAttendance(MessAttendanceModel attendance)
        {
            if (string.IsNullOrEmpty(attendance.RollNumber))
                return "Roll Number is required!";

            if (string.IsNullOrEmpty(attendance.MealType))
                return "Meal Type is required!";

            if (string.IsNullOrEmpty(attendance.Status))
                return "Status is required!";

            return "VALID";
        }

        public string SaveAttendance(MessAttendanceModel attendance)
        {
            string validation = ValidateAttendance(attendance);
            if (validation != "VALID")
                return validation;

            bool saved = attendanceDL.InsertAttendance(attendance);
            return saved ? "SUCCESS" : "Failed to save attendance!";
        }

        public string UpdateAttendance(MessAttendanceModel attendance)
        {
            if (attendance.MessAttendanceID <= 0)
                return "Invalid Attendance ID!";

            string validation = ValidateAttendance(attendance);
            if (validation != "VALID")
                return validation;

            bool updated = attendanceDL.UpdateAttendance(attendance);
            return updated ? "SUCCESS" : "Failed to update attendance!";
        }

        public string DeleteAttendance(int attendanceID)
        {
            if (attendanceID <= 0)
                return "Invalid Attendance ID!";

            bool deleted = attendanceDL.DeleteAttendance(attendanceID);
            return deleted ? "SUCCESS" : "Failed to delete attendance!";
        }

        public DataTable GetAllAttendance()
        {
            return attendanceDL.GetAllAttendance();
        }

        public DataTable GetAttendanceById(int attendanceID)
        {
            return attendanceDL.GetAttendanceById(attendanceID);
        }
    }
}