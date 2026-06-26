using System;

namespace hostel_management_system_oop.Model
{
    public class FeeModel
    {
        public int FeeID { get; set; }
        public string RollNumber { get; set; }
        public int AmountDue { get; set; }
        public int AmountPaid { get; set; }
        public DateTime DueDate { get; set; }
        public string PaymentStatus { get; set; }
    }
}