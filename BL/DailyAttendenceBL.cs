using hostel_management_system_oop.DL;
using hostel_management_system_oop.Model;
using System;
using System.Data;

namespace hostel_management_system_oop.BL
{
    public class DailyAttendenceBL
    {
        private DailyAttendenceDL attendanceDL = new DailyAttendenceDL();

        private string ValidateAttendance(DailyAttendenceModel attendance)
        {
            if (string.IsNullOrEmpty(attendance.RollNumber))
                return "Roll Number is required!";

            if (string.IsNullOrEmpty(attendance.AttendanceStatus))
                return "Status is required!";

            if (attendance.AttendanceStatus != "Present" && attendance.AttendanceStatus != "Absent")
                return "Status must be 'Present' or 'Absent'!";

            return "VALID";
        }

        public string SaveAttendance(DailyAttendenceModel attendance)
        {
            string validation = ValidateAttendance(attendance);
            if (validation != "VALID")
                return validation;

            bool saved = attendanceDL.InsertAttendance(attendance);
            return saved ? "SUCCESS" : "Failed to save attendance!";
        }

        public string UpdateAttendance(DailyAttendenceModel attendance)
        {
            if (attendance.AttendanceID <= 0)
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