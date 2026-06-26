using System;

namespace hostel_management_system_oop.Model
{
    public class ComplaintModel
    {
        public int ComplaintID { get; set; }
        public string RollNumber { get; set; }
        public string ComplaintTitle { get; set; }
        public string ComplaintDescription { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string ComplaintStatus { get; set; }  
        public int IsNewNotification { get; set; }  

        public ComplaintModel()
        {
            SubmissionDate = DateTime.Now;
            ComplaintStatus = "Pending";
            IsNewNotification = 1;
        }
    }
}