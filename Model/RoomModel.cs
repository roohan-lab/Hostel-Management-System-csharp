namespace hostel_management_system_oop.Model
{
    public class RoomModel
    {
        public int RoomID { get; set; }
        public string StudentName { get; set; }  // Add this
        public string RoomNumber { get; set; }
        public string HostelType { get; set; }
        public int TotalCapacity { get; set; }
        public int CurrentOccupancy { get; set; }
        public string RoomStatus { get; set; }
    }
}