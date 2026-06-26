using MySql.Data.MySqlClient;
using System.Data;
using hostel_management_system_oop.Model;

namespace hostel_management_system_oop.DL
{
    public class RoomDL
    {
        public DataTable GetAllRooms()
        {
            string query = "SELECT StudentName, RoomID, RoomNumber, HostelType, TotalCapacity, CurrentOccupancy, RoomStatus FROM rooms ORDER BY RoomID DESC";
            return DatabaseConnection.ExecuteQuery(query);
        }

        public DataTable GetRoomById(int roomID)
        {
            string query = "SELECT * FROM rooms WHERE RoomID = @id";
            MySqlParameter[] parameters = { new MySqlParameter("@id", roomID) };
            return DatabaseConnection.ExecuteQuery(query, parameters);
        }

        public bool InsertRoom(RoomModel room)
        {
            string query = @"INSERT INTO rooms (StudentName, RoomNumber, HostelType, TotalCapacity, CurrentOccupancy, RoomStatus) 
                             VALUES (@studentName, @roomNo, @type, @capacity, @occupancy, @status)";

            MySqlParameter[] parameters = {
                new MySqlParameter("@studentName", room.StudentName),
                new MySqlParameter("@roomNo", room.RoomNumber),
                new MySqlParameter("@type", room.HostelType),
                new MySqlParameter("@capacity", room.TotalCapacity),
                new MySqlParameter("@occupancy", room.CurrentOccupancy),
                new MySqlParameter("@status", room.RoomStatus)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateRoom(RoomModel room)
        {
            string query = @"UPDATE rooms SET 
                             StudentName = @studentName,
                             RoomNumber = @roomNo,
                             HostelType = @type,
                             TotalCapacity = @capacity,
                             CurrentOccupancy = @occupancy,
                             RoomStatus = @status
                             WHERE RoomID = @id";

            MySqlParameter[] parameters = {
                new MySqlParameter("@studentName", room.StudentName),
                new MySqlParameter("@roomNo", room.RoomNumber),
                new MySqlParameter("@type", room.HostelType),
                new MySqlParameter("@capacity", room.TotalCapacity),
                new MySqlParameter("@occupancy", room.CurrentOccupancy),
                new MySqlParameter("@status", room.RoomStatus),
                new MySqlParameter("@id", room.RoomID)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteRoom(int roomID)
        {
            string query = "DELETE FROM rooms WHERE RoomID = @id";
            MySqlParameter[] parameters = { new MySqlParameter("@id", roomID) };
            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}