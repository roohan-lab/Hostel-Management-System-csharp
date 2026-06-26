using hostel_management_system_oop.DL;
using hostel_management_system_oop.Model;
using System.Data;

namespace hostel_management_system_oop.BL
{
    public class RoomBL
    {
        private RoomDL roomDL = new RoomDL();

        private string ValidateRoom(RoomModel room)
        {
            if (string.IsNullOrEmpty(room.StudentName))
                return "Student Name is required!";

            if (string.IsNullOrEmpty(room.RoomNumber))
                return "Room Number is required!";

            if (string.IsNullOrEmpty(room.HostelType))
                return "Hostel Type is required!";

            if (room.TotalCapacity <= 0)
                return "Total Capacity must be greater than zero!";

            if (room.CurrentOccupancy < 0)
                return "Current Occupancy cannot be negative!";

            return "VALID";
        }

        public string SaveRoom(RoomModel room)
        {
            string validation = ValidateRoom(room);
            if (validation != "VALID")
                return validation;

            bool saved = roomDL.InsertRoom(room);
            return saved ? "SUCCESS" : "Failed to save room!";
        }

        public string UpdateRoom(RoomModel room)
        {
            if (room.RoomID <= 0)
                return "Invalid Room ID!";

            string validation = ValidateRoom(room);
            if (validation != "VALID")
                return validation;

            bool updated = roomDL.UpdateRoom(room);
            return updated ? "SUCCESS" : "Failed to update room!";
        }

        public string DeleteRoom(int roomID)
        {
            if (roomID <= 0)
                return "Invalid Room ID!";

            bool deleted = roomDL.DeleteRoom(roomID);
            return deleted ? "SUCCESS" : "Failed to delete room!";
        }

        public DataTable GetAllRooms()
        {
            return roomDL.GetAllRooms();
        }

        public DataTable GetRoomById(int roomID)
        {
            return roomDL.GetRoomById(roomID);
        }
    }
}