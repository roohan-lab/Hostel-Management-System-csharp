using System;

namespace hostel_management_system_oop.Model
{
    public class MessAttendanceModel
    {
        public int MessAttendanceID { get; set; }
        public string RollNumber { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string MealType { get; set; }
        public string Status { get; set; }
    }
}