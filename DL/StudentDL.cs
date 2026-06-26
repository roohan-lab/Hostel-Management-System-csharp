using MySql.Data.MySqlClient;
using System.Data;
using hostel_management_system_oop.Model;

namespace hostel_management_system_oop.DL
{
    public class StudentDL
    {
        public DataTable GetAllStudents()
        {
            string query = "SELECT StudentID, RollNumber, FullName, CNIC, Gender, PhoneNumber, Email FROM students ORDER BY StudentID DESC";
            return DatabaseConnection.ExecuteQuery(query);
        }

        public DataTable GetStudentById(int studentID)
        {
            string query = "SELECT * FROM students WHERE StudentID = @id";
            MySqlParameter[] parameters = { new MySqlParameter("@id", studentID) };
            return DatabaseConnection.ExecuteQuery(query, parameters);
        }

        public bool InsertStudent(Student student)
        {
            string query = @"INSERT INTO students (RollNumber, FullName, CNIC, Gender, PhoneNumber, Email) 
                             VALUES (@roll, @name, @cnic, @gender, @phone, @email)";

            MySqlParameter[] parameters = {
                new MySqlParameter("@roll", student.RollNumber),
                new MySqlParameter("@name", student.FullName),
                new MySqlParameter("@cnic", student.CNIC),
                new MySqlParameter("@gender", student.Gender),
                new MySqlParameter("@phone", student.PhoneNumber),
                new MySqlParameter("@email", student.Email)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool UpdateStudent(Student student)
        {
            string query = @"UPDATE students SET 
                             RollNumber = @roll,
                             FullName = @name,
                             CNIC = @cnic,
                             Gender = @gender,
                             PhoneNumber = @phone,
                             Email = @email
                             WHERE StudentID = @id";

            MySqlParameter[] parameters = {
                new MySqlParameter("@roll", student.RollNumber),
                new MySqlParameter("@name", student.FullName),
                new MySqlParameter("@cnic", student.CNIC),
                new MySqlParameter("@gender", student.Gender),
                new MySqlParameter("@phone", student.PhoneNumber),
                new MySqlParameter("@email", student.Email),
                new MySqlParameter("@id", student.StudentID)
            };

            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }

        public bool DeleteStudent(int studentID)
        {
            string query = "DELETE FROM students WHERE StudentID = @id";
            MySqlParameter[] parameters = { new MySqlParameter("@id", studentID) };
            return DatabaseConnection.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}