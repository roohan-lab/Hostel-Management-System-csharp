using hostel_management_system_oop.DL;
using hostel_management_system_oop.Model;
using System;
using System.Data;

namespace hostel_management_system_oop.BL
{
    public class ComplaintBL
    {
        private ComplaintDL complaintDL = new ComplaintDL();

        private string ValidateComplaint(ComplaintModel complaint)
        {
            if (string.IsNullOrEmpty(complaint.RollNumber))
                return "Roll Number is required!";

            if (string.IsNullOrEmpty(complaint.ComplaintTitle))
                return "Complaint Title is required!";

            if (string.IsNullOrEmpty(complaint.ComplaintDescription))
                return "Description is required!";

            if (string.IsNullOrEmpty(complaint.ComplaintStatus))
                return "Status is required!";

            return "VALID";
        }

        public string SaveComplaint(ComplaintModel complaint)
        {
            string validation = ValidateComplaint(complaint);
            if (validation != "VALID")
                return validation;

            bool saved = complaintDL.InsertComplaint(complaint);
            return saved ? "SUCCESS" : "Failed to save complaint!";
        }

        public string UpdateComplaint(ComplaintModel complaint)
        {
            if (complaint.ComplaintID <= 0)
                return "Invalid Complaint ID!";

            string validation = ValidateComplaint(complaint);
            if (validation != "VALID")
                return validation;

            bool updated = complaintDL.UpdateComplaint(complaint);
            return updated ? "SUCCESS" : "Failed to update complaint!";
        }

        public string DeleteComplaint(int complaintID)
        {
            if (complaintID <= 0)
                return "Invalid Complaint ID!";

            bool deleted = complaintDL.DeleteComplaint(complaintID);
            return deleted ? "SUCCESS" : "Failed to delete complaint!";
        }

        public string MarkAsRead(int complaintID)
        {
            if (complaintID <= 0)
                return "Invalid Complaint ID!";

            bool marked = complaintDL.MarkAsRead(complaintID);
            return marked ? "SUCCESS" : "Failed to mark as read!";
        }

        public DataTable GetAllComplaints()
        {
            return complaintDL.GetAllComplaints();
        }

        public DataTable GetComplaintById(int complaintID)
        {
            return complaintDL.GetComplaintById(complaintID);
        }

        public int GetNewComplaintsCount()
        {
            return complaintDL.GetNewComplaintsCount();
        }
    }
}