using hostel_management_system_oop.DL;

namespace hostel_management_system_oop.BL
{
    public class AdminBL
    {
        private AdminDL adminDL = new AdminDL();

        public string ValidateLogin(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                return "Username is required!";

            if (string.IsNullOrEmpty(password))
                return "Password is required!";

            bool isValid = adminDL.ValidateLogin(username, password);
            return isValid ? "SUCCESS" : "Invalid username or password!";
        }
    }
}