using hostel_management_system_oop.DL;
using hostel_management_system_oop.Model;
using System.Data;

namespace hostel_management_system_oop.BL
{
    public class StudentBL
    {
        private StudentDL studentDL = new StudentDL();

        private string ValidateStudent(Student student)
        {
            if (string.IsNullOrEmpty(student.RollNumber))
                return "Roll Number is required!";

            if (string.IsNullOrEmpty(student.FullName))
                return "Full Name is required!";

            if (string.IsNullOrEmpty(student.CNIC))
                return "CNIC is required!";

            return "VALID";
        }

        public string SaveStudent(Student student)
        {
            string validation = ValidateStudent(student);
            if (validation != "VALID")
                return validation;

            bool saved = studentDL.InsertStudent(student);
            return saved ? "SUCCESS" : "Failed to save student!";
        }

        public string UpdateStudent(Student student)
        {
            if (student.StudentID <= 0)
                return "Invalid Student ID!";

            string validation = ValidateStudent(student);
            if (validation != "VALID")
                return validation;

            bool updated = studentDL.UpdateStudent(student);
            return updated ? "SUCCESS" : "Failed to update student!";
        }

        public string DeleteStudent(int studentID)
        {
            if (studentID <= 0)
                return "Invalid Student ID!";

            bool deleted = studentDL.DeleteStudent(studentID);
            return deleted ? "SUCCESS" : "Failed to delete student!";
        }

        public DataTable GetAllStudents()
        {
            return studentDL.GetAllStudents();
        }

        public DataTable GetStudentById(int studentID)
        {
            return studentDL.GetStudentById(studentID);
        }
    }
}