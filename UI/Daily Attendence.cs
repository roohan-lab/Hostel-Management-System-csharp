using hostel_management_system_oop.BL;
using hostel_management_system_oop.DL;
using hostel_management_system_oop.Model;
using hostel_management_system_oop.UI;  // ← ADD THIS LINE
using System;
using System.Data;
using System.Windows.Forms;

namespace hostel_management_system_oop
{
    public partial class DailyAttendence : BaseForm
    {
        private DailyAttendenceBL attendanceBL = new DailyAttendenceBL();
        private int selectedAttendanceID = -1;

        public DailyAttendence()
        {
            InitializeComponent();
            SetupDataGridView();
            dtp1.Value = DateTime.Now;
            LoadData(); 
        }

      
        public override void LoadData()
        {
            try
            {
                DataTable dt = attendanceBL.GetAllAttendance();
                dgv1.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dgv1.Rows.Add(
                        row["AttendanceID"].ToString(),
                        row["RollNumber"].ToString(),
                        Convert.ToDateTime(row["AttendanceDate"]).ToString("dd/MM/yyyy"),
                        row["AttendanceStatus"].ToString()
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        // ← ADD THIS OVERRIDE METHOD (Replaces ClearForm)
        public override void ClearForm()
        {
            tbx1f5.Clear();
            dtp1.Value = DateTime.Now;
            rbnpresent.Checked = false;
            rbnabsent.Checked = false;
            tbx1f5.Focus();
            selectedAttendanceID = -1;
        }

       
        public override bool ValidateForm()
        {
            if (string.IsNullOrEmpty(tbx1f5.Text))
            {
                MessageBox.Show("Please enter Roll Number!");
                tbx1f5.Focus();
                return false;
            }

            if (!rbnpresent.Checked && !rbnabsent.Checked)
            {
                MessageBox.Show("Please select Present or Absent!");
                return false;
            }

            return true;
        }

        private void SetupDataGridView()
        {
            dgv1.AutoGenerateColumns = false;
            dgv1.Columns.Clear();

            dgv1.Columns.Add("AttendanceID", "ID");
            dgv1.Columns.Add("RollNumber", "Roll No");
            dgv1.Columns.Add("AttendanceDate", "Date");
            dgv1.Columns.Add("AttendanceStatus", "Status");

            dgv1.Columns["AttendanceID"].Width = 60;
            dgv1.Columns["RollNumber"].Width = 120;
            dgv1.Columns["AttendanceDate"].Width = 130;
            dgv1.Columns["AttendanceStatus"].Width = 100;

            dgv1.AllowUserToAddRows = false;
            dgv1.ReadOnly = true;
            dgv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv1.RowHeadersVisible = false;

            dgv1.CellClick += dgv1_CellClick;
        }

        private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv1.Rows[e.RowIndex];

                selectedAttendanceID = Convert.ToInt32(row.Cells["AttendanceID"].Value);

                tbx1f5.Text = row.Cells["RollNumber"].Value.ToString();
                dtp1.Value = Convert.ToDateTime(row.Cells["AttendanceDate"].Value);

                string status = row.Cells["AttendanceStatus"].Value.ToString();
                if (status == "Present")
                    rbnpresent.Checked = true;
                else if (status == "Absent")
                    rbnabsent.Checked = true;
            }
        }

        // SAVE Button
        private void btn1f5_Click(object sender, EventArgs e)
        {
           
            if (!ValidateForm())
                return;

            string rollNo = tbx1f5.Text;
            DateTime attendanceDate = dtp1.Value.Date;
            string status = "";

            if (rbnpresent.Checked)
                status = "Present";
            else if (rbnabsent.Checked)
                status = "Absent";

            DailyAttendenceModel attendance = new DailyAttendenceModel();
            attendance.RollNumber = rollNo;
            attendance.AttendanceDate = attendanceDate;
            attendance.AttendanceStatus = status;

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

        // UPDATE Button
        private void btnup_Click(object sender, EventArgs e)
        {
            if (selectedAttendanceID == -1)
            {
                MessageBox.Show("Please click on a row to select an attendance record first!");
                return;
            }

            
            if (!ValidateForm())
                return;

            string rollNo = tbx1f5.Text;
            DateTime attendanceDate = dtp1.Value.Date;
            string status = "";

            if (rbnpresent.Checked)
                status = "Present";
            else if (rbnabsent.Checked)
                status = "Absent";

            DailyAttendenceModel attendance = new DailyAttendenceModel();
            attendance.AttendanceID = selectedAttendanceID;
            attendance.RollNumber = rollNo;
            attendance.AttendanceDate = attendanceDate;
            attendance.AttendanceStatus = status;

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

        // DELETE Button
        private void btnd_Click(object sender, EventArgs e)
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