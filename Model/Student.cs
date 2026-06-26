using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hostel_management_system_oop.Model
{
    public class Student 
    {
      
        public int StudentID { get; set; }
        public string RollNumber { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string CNIC { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

       
        public Student()
        {
            Gender = "Boy";
        }
    }
}