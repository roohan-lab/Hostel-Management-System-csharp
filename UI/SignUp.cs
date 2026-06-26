using System;
using System.Windows.Forms;
using hostel_management_system_oop.BL;
using hostel_management_system_oop.UI;  

namespace hostel_management_system_oop
{
    public partial class SignUp : BaseForm  
    {
        private SignUpBL signUpBL = new SignUpBL();

        public SignUp()
        {
            InitializeComponent();
            LoadData(); 
        }

       
        public override void LoadData()
        {
            tbx1R.Clear();
            tbx2R.Clear();
            tbx1R.Focus();
        }

       
        public override void ClearForm()
        {
            tbx1R.Clear();
            tbx2R.Clear();
            tbx1R.Focus();
        }

       
        public override bool ValidateForm()
        {
            string username = tbx1R.Text.Trim();
            string password = tbx2R.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter Username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbx1R.Focus();
                return false;
            }

            if (username.Length < 3)
            {
                MessageBox.Show("Username must be at least 3 characters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbx1R.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter Password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbx2R.Focus();
                return false;
            }

            if (password.Length < 4)
            {
                MessageBox.Show("Password must be at least 4 characters!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbx2R.Focus();
                return false;
            }

            return true;
        }

        private void btn1R_Click(object sender, EventArgs e)
        {
           
            if (!ValidateForm())
                return;

            string username = tbx1R.Text.Trim();
            string password = tbx2R.Text;

            string result = signUpBL.RegisterUser(username, password);

            if (result == "SUCCESS")
            {
                MessageBox.Show("Registration Successful! You can now login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm(); 
                this.Close();
            }
            else
            {
                MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn1R_Click_1(object sender, EventArgs e)
        {
           
            btn1R_Click(sender, e);
        }
    }
}