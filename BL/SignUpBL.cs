using hostel_management_system_oop.DL;

namespace hostel_management_system_oop.BL
{
    public class SignUpBL
    {
        private SignUpDL signUpDL = new SignUpDL();

        public string RegisterUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                return "Username is required!";

            if (username.Length < 3)
                return "Username must be at least 3 characters!";

            if (string.IsNullOrEmpty(password))
                return "Password is required!";

            if (password.Length < 4)
                return "Password must be at least 4 characters!";

            if (signUpDL.IsUsernameExists(username))
                return "Username already exists!";

            bool inserted = signUpDL.InsertOwner(username, password);
            return inserted ? "SUCCESS" : "Failed to register!";
        }
    }
}