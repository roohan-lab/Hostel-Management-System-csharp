using System;

namespace hostel_management_system_oop.Model
{
    public class DailyAttendenceModel
    {
        public int AttendanceID { get; set; }
        public string RollNumber { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string AttendanceStatus { get; set; }  

        public DailyAttendenceModel()
        {
            AttendanceDate = DateTime.Now.Date;
            AttendanceStatus = "Absent";
        }
    }
}