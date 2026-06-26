using hostel_management_system_oop.DL;
using hostel_management_system_oop.Model;
using System.Data;

namespace hostel_management_system_oop.BL
{
    public class OwnerProfileBL
    {
        private OwnerProfileDL ownerDL = new OwnerProfileDL();

        public DataTable GetOwnerByUsername(string username)
        {
            return ownerDL.GetOwnerByUsername(username);
        }

        public string UpdateOwner(Owner owner)
        {
            if (string.IsNullOrEmpty(owner.FullName))
                return "Full Name is required!";

            if (string.IsNullOrEmpty(owner.Email))
                return "Email is required!";

            if (!owner.Email.Contains("@"))
                return "Invalid Email format!";

            bool updated = ownerDL.UpdateOwner(owner);
            return updated ? "SUCCESS" : "Failed to update profile!";
        }
    }
}