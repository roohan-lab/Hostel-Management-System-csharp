using System;
using System.Data;
using System.Windows.Forms;
using hostel_management_system_oop.BL;
using hostel_management_system_oop.Model;
using hostel_management_system_oop.UI;  // ← ADD THIS LINE

namespace hostel_management_system_oop
{
    public partial class MessAttendance : BaseForm 
    {
        private MessAttendanceBL attendanceBL = new MessAttendanceBL();
        private int selectedAttendanceID = -1;

        public MessAttendance()
        {
            InitializeComponent();
            SetupDataGridView();
            dtp1f8.Value = DateTime.Now;
            LoadStatus();
            LoadData();  
        }

       
        public override void LoadData()
        {
            try
            {
                DataTable dt = attendanceBL.GetAllAttendance();
                dgva.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                   
                    string status = row["Status"].ToString();
                    if (string.IsNullOrEmpty(status))
                    {
                        status = "Not Recorded";
                    }

                    dgva.Rows.Add(
                        row["MessAttendanceID"].ToString(),
                        row["RollNumber"].ToString(),
                        Convert.ToDateTime(row["AttendanceDate"]).ToString("dd/MM/yyyy"),
                        row["MealType"].ToString(),
                        status
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading attendance: " + ex.Message);
            }
        }

       
        public override void ClearForm()
        {
            tbx2f8.Clear();
            dtp1f8.Value = DateTime.Now;
            tbx3f8.Clear();
            cbx1f8.SelectedIndex = 0;
            tbx2f8.Focus();
            selectedAttendanceID = -1;
        }

       
        public override bool ValidateForm()
        {
            if (string.IsNullOrEmpty(tbx2f8.Text))
            {
                MessageBox.Show("Please enter Roll Number!");
                tbx2f8.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(tbx3f8.Text))
            {
                MessageBox.Show("Please enter Meal Type!");
                tbx3f8.Focus();
                return false;
            }

            return true;
        }

        private void SetupDataGridView()
        {
            dgva.AutoGenerateColumns = true;
            dgva.Columns.Clear();

            // Add columns
            dgva.Columns.Add("MessAttendanceID", "ID");
            dgva.Columns.Add("RollNumber", "Roll No");
            dgva.Columns.Add("AttendanceDate", "Date");
            dgva.Columns.Add("MealType", "Meal Type");
            dgva.Columns.Add("Status", "Status");

            // Set column widths
            dgva.Columns["MessAttendanceID"].Width = 60;
            dgva.Columns["RollNumber"].Width = 100;
            dgva.Columns["AttendanceDate"].Width = 120;
            dgva.Columns["MealType"].Width = 150;
            dgva.Columns["Status"].Width = 80;

            dgva.AllowUserToAddRows = false;
            dgva.ReadOnly = true;
            dgva.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgva.RowHeadersVisible = false;

            dgva.CellClick += dgva_CellClick;
        }

        private void LoadStatus()
        {
            cbx1f8.Items.Clear();
            cbx1f8.Items.Add("Eaten");
            cbx1f8.Items.Add("Skipped");
            cbx1f8.SelectedIndex = 0;
        }

        private void dgva_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgva.Rows[e.RowIndex];

                selectedAttendanceID = Convert.ToInt32(row.Cells["MessAttendanceID"].Value);

                tbx2f8.Text = row.Cells["RollNumber"].Value.ToString();

                // Handle date conversion safely
                try
                {
                    dtp1f8.Value = Convert.ToDateTime(row.Cells["AttendanceDate"].Value);
                }
                catch
                {
                    dtp1f8.Value = DateTime.Now;
                }

                tbx3f8.Text = row.Cells["MealType"].Value.ToString();

               
                string status = row.Cells["Status"].Value.ToString();
                if (status == "Not Recorded")
                {
                    cbx1f8.SelectedIndex = -1;
                }
                else
                {
                    cbx1f8.Text = status;
                }
            }
        }

       
        private void btn1f8_Click(object sender, EventArgs e)
        {
            
            if (!ValidateForm())
                return;

            string rollNo = tbx2f8.Text;
            DateTime attendanceDate = dtp1f8.Value.Date;
            string mealType = tbx3f8.Text;
            string status = cbx1f8.Text;

            MessAttendanceModel attendance = new MessAttendanceModel();
            attendance.RollNumber = rollNo;
            attendance.AttendanceDate = attendanceDate;
            attendance.MealType = mealType;
            attendance.Status = status;

            string result = attendanceBL.SaveAttendance(attendance);

            if (result == "SUCCESS")
            {
                MessageBox.Show("Attendance Saved Successfully!");
                ClearForm(); 
                LoadData();  
            }
            else
            {
                MessageBox.Show("Error: " + result);
            }
        }

       
        private void btn2f8_Click_1(object sender, EventArgs e)
        {
            if (selectedAttendanceID == -1)
            {
                MessageBox.Show("Please click on a row to select an attendance record first!");
                return;
            }

            
            if (!ValidateForm())
                return;

            string rollNo = tbx2f8.Text;
            DateTime attendanceDate = dtp1f8.Value.Date;
            string mealType = tbx3f8.Text;
            string status = cbx1f8.Text;

            MessAttendanceModel attendance = new MessAttendanceModel();
            attendance.MessAttendanceID = selectedAttendanceID;
            attendance.RollNumber = rollNo;
            attendance.AttendanceDate = attendanceDate;
            attendance.MealType = mealType;
            attendance.Status = status;

            string result = attendanceBL.UpdateAttendance(attendance);

            if (result == "SUCCESS")
            {
                MessageBox.Show("Attendance Updated Successfully!");
                ClearForm();
                LoadData();
                selectedAttendanceID = -1;
            }
            else
            {
                MessageBox.Show("Error: " + result);
            }
        }

       
        private void btn3f8_Click_1(object sender, EventArgs e)
        {
            if (selectedAttendanceID == -1)
            {
                MessageBox.Show("Please click on a row to select an attendance record first!");
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this attendance record?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                string result = attendanceBL.DeleteAttendance(selectedAttendanceID);

                if (result == "SUCCESS")
                {
                    MessageBox.Show("Attendance Deleted Successfully!");
                    ClearForm();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Error: " + result);
                }
            }
        }
    }
}