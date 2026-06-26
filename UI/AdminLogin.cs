using System;
using System.Windows.Forms;
using hostel_management_system_oop.BL;
using hostel_management_system_oop.UI;  // ← ADD THIS LINE

namespace hostel_management_system_oop
{
    public partial class AdminLogin : BaseForm  // ← CHANGE Form TO BaseForm
    {
        // Static variable to store logged-in username
        public static string LoggedInUsername = "";

        private AdminBL adminBL = new AdminBL();

        public AdminLogin()
        {
            InitializeComponent();
            tbx2f2.PasswordChar = '*';
            LoadData();  // ← ADD THIS LINE
        }

        // ← ADD THIS OVERRIDE METHOD
        public override void LoadData()
        {
            tbx1f2.Clear();
            tbx2f2.Clear();
            tbx1f2.Focus();
        }

        // ← ADD THIS OVERRIDE METHOD
        public override bool ValidateForm()
        {
            if (string.IsNullOrEmpty(tbx1f2.Text))
            {
                MessageBox.Show("Please enter Username!");
                tbx1f2.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(tbx2f2.Text))
            {
                MessageBox.Show("Please enter Password!");
                tbx2f2.Focus();
                return false;
            }

            return true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // The actual login code is in btn1f2_Click
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.ShowDialog();
        }

        private void btn1f2_Click(object sender, EventArgs e)
        {
            // ← USING OVERRIDDEN ValidateForm
            if (!ValidateForm())
                return;

            string username = tbx1f2.Text.Trim();
            string password = tbx2f2.Text;

            string result = adminBL.ValidateLogin(username, password);

            if (result == "SUCCESS")
            {
                // Store the logged-in username
                LoggedInUsername = username;

                MessageBox.Show("Login Successful!");
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(result, "Login Failed");
            }
        }
    }
}