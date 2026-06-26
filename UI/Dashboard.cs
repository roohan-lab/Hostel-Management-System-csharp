using System;
using System.Windows.Forms;
using hostel_management_system_oop.BL;
using hostel_management_system_oop.UI; 

namespace hostel_management_system_oop
{
    public partial class Dashboard : BaseForm  
    {
        public Dashboard()
        {
            InitializeComponent();
            LoadData(); 
        }

        
        public override void LoadData()
        {
            
        }

       
        public override void ClearForm()
        {
            
        }

       
        public override bool ValidateForm()
        {
            
            return true;
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            AdminLogin a = new AdminLogin();
            a.Show();
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            DailyAttendence da = new DailyAttendence();
            da.Show();
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            FeeForm f = new FeeForm();
            f.Show();
        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            MessAttendance da = new MessAttendance();
            da.ShowDialog();
        }

       
        private void button5_Click(object sender, EventArgs e)
        {
            MessMenu me = new MessMenu();
            me.Show();
        }

       
        private void button6_Click(object sender, EventArgs e)
        {
            Room r = new Room();
            r.Show();
        }

        
        private void button7_Click(object sender, EventArgs e)
        {
            Mess fac = new Mess();
            fac.Show();
        }

        
        private void button8_Click(object sender, EventArgs e)
        {
            OwnerProfile o = new OwnerProfile();
            o.Show();
        }

       
        private void button9_Click(object sender, EventArgs e)
        {
            Complaint complaint = new Complaint();
            complaint.Show();
        }

        
        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            StudentProfile s = new StudentProfile();
            s.ShowDialog();
        }
    }
}