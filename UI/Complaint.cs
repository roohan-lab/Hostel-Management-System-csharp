using System;
using System.Data;
using System.Windows.Forms;
using hostel_management_system_oop.BL;
using hostel_management_system_oop.Model;
using hostel_management_system_oop.UI;

namespace hostel_management_system_oop
{
    public partial class Complaint : AbstractBaseForm
    {
        private ComplaintBL complaintBL = new ComplaintBL();
        private int selectedComplaintID = -1;

        public Complaint()
        {
            // ✅ FIX: Check if NOT in design mode
            if (!DesignMode)
            {
                InitializeComponent();
                this.Text = FormTitle;
                SetupDataGridView();
                LoadData();
            }
            else
            {
                InitializeComponent();
            }
        }

        public override string FormTitle
        {
            get { return "Complaint System"; }
        }

        public override void LoadData()
        {
            try
            {
                DataTable dt = complaintBL.GetAllComplaints();
                dgv1c.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    int isNew = Convert.ToInt32(row["IsNewNotification"]);
                    string notification = isNew == 1 ? "🔴 New" : "✓ Read";

                    dgv1c.Rows.Add(
                        row["ComplaintID"].ToString(),
                        row["RollNumber"].ToString(),
                        row["ComplaintTitle"].ToString(),
                        row["ComplaintDescription"].ToString(),
                        Convert.ToDateTime(row["SubmissionDate"]).ToString("dd/MM/yyyy"),
                        row["ComplaintStatus"].ToString(),
                        notification
                    );
                }
                UpdateNotificationBadge();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading complaints: " + ex.Message);
            }
        }

        public override void ClearForm()
        {
            tbx1f11.Clear();
            tbx2f11.Clear();
            tbx3f11.Clear();
            rb1f11.Checked = false;
            rb2f11.Checked = false;
            tbx1f11.Focus();
            selectedComplaintID = -1;
        }

        public override bool ValidateForm()
        {
            if (string.IsNullOrEmpty(tbx1f11.Text))
            {
                MessageBox.Show("Please enter Roll Number!");
                tbx1f11.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(tbx2f11.Text))
            {
                MessageBox.Show("Please enter Complaint Title!");
                tbx2f11.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(tbx3f11.Text))
            {
                MessageBox.Show("Please enter Description!");
                tbx3f11.Focus();
                return false;
            }

            return true;
        }

        private void SetupDataGridView()
        {
            dgv1c.AutoGenerateColumns = false;
            dgv1c.Columns.Clear();

            // Add columns in order
            dgv1c.Columns.Add("ComplaintID", "ID");
            dgv1c.Columns.Add("RollNumber", "Roll No");
            dgv1c.Columns.Add("ComplaintTitle", "Title");
            dgv1c.Columns.Add("ComplaintDescription", "Description");
            dgv1c.Columns.Add("SubmissionDate", "Date");
            dgv1c.Columns.Add("ComplaintStatus", "Status");
            dgv1c.Columns.Add("Notification", "Notification");

            // Set column widths
            dgv1c.Columns["ComplaintID"].Width = 50;
            dgv1c.Columns["RollNumber"].Width = 80;
            dgv1c.Columns["ComplaintTitle"].Width = 120;
            dgv1c.Columns["ComplaintDescription"].Width = 150;
            dgv1c.Columns["SubmissionDate"].Width = 90;
            dgv1c.Columns["ComplaintStatus"].Width = 80;
            dgv1c.Columns["Notification"].Width = 80;

            dgv1c.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv1c.AllowUserToAddRows = false;
            dgv1c.ReadOnly = true;
            dgv1c.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv1c.RowHeadersVisible = false;

            dgv1c.CellClick += dgv1c_CellClick;
        }

        private void UpdateNotificationBadge()
        {
            int newCount = complaintBL.GetNewComplaintsCount();
            if (newCount > 0)
            {
                this.Text = $"Complaint System ({newCount} New)";
            }
            else
            {
                this.Text = "Complaint System";
            }
        }

        private void dgv1c_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv1c.Rows[e.RowIndex];

                selectedComplaintID = Convert.ToInt32(row.Cells[0].Value);

                string notification = row.Cells[6].Value.ToString();
                if (notification == "🔴 New")
                {
                    complaintBL.MarkAsRead(selectedComplaintID);
                    LoadData();
                    return;
                }

                tbx1f11.Text = row.Cells[1].Value.ToString();
                tbx2f11.Text = row.Cells[2].Value.ToString();
                tbx3f11.Text = row.Cells[3].Value.ToString();

                string status = row.Cells[5].Value.ToString();
                if (status == "Pending")
                    rb1f11.Checked = true;
                else if (status == "Resolved")
                    rb2f11.Checked = true;
            }
        }

        // SAVE Button
        private void btn1f11_Click_1(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            string rollNo = tbx1f11.Text;
            string title = tbx2f11.Text;
            string description = tbx3f11.Text;
            string status = "";

            if (rb1f11.Checked)
                status = "Pending";
            else if (rb2f11.Checked)
                status = "Resolved";

            ComplaintModel complaint = new ComplaintModel();
            complaint.RollNumber = rollNo;
            complaint.ComplaintTitle = title;
            complaint.ComplaintDescription = description;
            complaint.ComplaintStatus = status;

            string result = complaintBL.SaveComplaint(complaint);

            if (result == "SUCCESS")
            {
                MessageBox.Show("Complaint Saved Successfully!");
                ClearForm();
                LoadData();
            }
            else
            {
                MessageBox.Show("Error: " + result);
            }
        }

        // UPDATE Button
        private void btn2f11_Click(object sender, EventArgs e)
        {
            if (selectedComplaintID == -1)
            {
                MessageBox.Show("Please click on a row to select a complaint first!");
                return;
            }

            if (!ValidateForm())
                return;

            string rollNo = tbx1f11.Text;
            string title = tbx2f11.Text;
            string description = tbx3f11.Text;
            string status = "";

            if (rb1f11.Checked)
                status = "Pending";
            else if (rb2f11.Checked)
                status = "Resolved";

            ComplaintModel complaint = new ComplaintModel();
            complaint.ComplaintID = selectedComplaintID;
            complaint.RollNumber = rollNo;
            complaint.ComplaintTitle = title;
            complaint.ComplaintDescription = description;
            complaint.ComplaintStatus = status;

            string result = complaintBL.UpdateComplaint(complaint);

            if (result == "SUCCESS")
            {
                MessageBox.Show("Complaint Updated Successfully!");
                ClearForm();
                LoadData();
                selectedComplaintID = -1;
            }
            else
            {
                MessageBox.Show("Error: " + result);
            }
        }

        // DELETE Button
        private void btn3f11_Click(object sender, EventArgs e)
        {
            if (selectedComplaintID == -1)
            {
                MessageBox.Show("Please click on a row to select a complaint first!");
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this complaint?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                string result = complaintBL.DeleteComplaint(selectedComplaintID);

                if (result == "SUCCESS")
                {
                    MessageBox.Show("Complaint Deleted Successfully!");
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