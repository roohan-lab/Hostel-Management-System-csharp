using hostel_management_system_oop.DL;
using hostel_management_system_oop.Model;
using System;
using System.Data;

namespace hostel_management_system_oop.BL
{
    public class FeeBL
    {
        private FeeDL feeDL = new FeeDL();

        private string ValidateFee(FeeModel fee)
        {
            if (string.IsNullOrEmpty(fee.RollNumber))
                return "Roll Number is required!";

            if (fee.AmountDue <= 0)
                return "Amount Due must be greater than zero!";

            if (fee.AmountPaid < 0)
                return "Amount Paid cannot be negative!";

            if (fee.AmountPaid > fee.AmountDue)
                return "Amount Paid cannot exceed Amount Due!";

            if (fee.DueDate < DateTime.Now.Date)
                return "Due Date cannot be in the past!";

            return "VALID";
        }

        private string CalculatePaymentStatus(int amountDue, int amountPaid)
        {
            if (amountPaid >= amountDue)
                return "Paid";
            else if (amountPaid > 0)
                return "Partial";
            else
                return "Pending";
        }

        public string SaveFee(FeeModel fee)
        {
            fee.PaymentStatus = CalculatePaymentStatus(fee.AmountDue, fee.AmountPaid);

            string validation = ValidateFee(fee);
            if (validation != "VALID")
                return validation;

            bool saved = feeDL.InsertFee(fee);
            return saved ? "SUCCESS" : "Failed to save fee!";
        }

        public string UpdateFee(FeeModel fee)
        {
            if (fee.FeeID <= 0)
                return "Invalid Fee ID!";

            fee.PaymentStatus = CalculatePaymentStatus(fee.AmountDue, fee.AmountPaid);

            string validation = ValidateFee(fee);
            if (validation != "VALID")
                return validation;

            bool updated = feeDL.UpdateFee(fee);
            return updated ? "SUCCESS" : "Failed to update fee!";
        }

        public string DeleteFee(int feeID)
        {
            if (feeID <= 0)
                return "Invalid Fee ID!";

            bool deleted = feeDL.DeleteFee(feeID);
            return deleted ? "SUCCESS" : "Failed to delete fee!";
        }

        public DataTable GetAllFees()
        {
            return feeDL.GetAllFees();
        }

        public DataTable GetFeeById(int feeID)
        {
            return feeDL.GetFeeById(feeID);
        }
    }
}