using hostel_management_system_oop.BL;
using hostel_management_system_oop.Model;
using hostel_management_system_oop.UI;  // ← ADD THIS LINE
using System;
using System.Data;
using System.Windows.Forms;

namespace hostel_management_system_oop
{
    public partial class OwnerProfile : BaseForm  // ← CHANGE Form TO BaseForm
    {
        private OwnerProfileBL ownerBL = new OwnerProfileBL();
        private string currentUsername;

        public OwnerProfile()
        {
            InitializeComponent();
            currentUsername = AdminLogin.LoggedInUsername;
            LoadData();  
        }

       
        public override void LoadData()
        {
            try
            {
                DataTable dt = ownerBL.GetOwnerByUsername(currentUsername);

                if (dt.Rows.Count > 0)
                {
                    
                    lbl8f4.Text = dt.Rows[0]["OwnerID"].ToString();
                    lbl9f4.Text = dt.Rows[0]["FullName"].ToString();
                    lbl10f4.Text = dt.Rows[0]["Username"].ToString();
                    lbl11f4.Text = dt.Rows[0]["Password"].ToString();
                    lbl12f4.Text = dt.Rows[0]["Email"].ToString();
                    lbl13f4.Text = dt.Rows[0]["PhoneNumber"].ToString();
                }
                else
                {
                    SetDefaultProfile();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading profile: " + ex.Message);
                SetDefaultProfile();
            }
        }

        
        public override void ClearForm()
        {
            
        }

      
        public override bool ValidateForm()
        {
            
            return true;
        }

        private void SetDefaultProfile()
        {
            lbl8f4.Text = "Auto";
            lbl9f4.Text = currentUsername;
            lbl10f4.Text = currentUsername;
            lbl11f4.Text = "****";
            lbl12f4.Text = "Not provided";
            lbl13f4.Text = "Not provided";
        }
    }
}