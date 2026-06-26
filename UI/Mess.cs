using System;
using System.Data;
using System.Windows.Forms;
using hostel_management_system_oop.BL;
using hostel_management_system_oop.UI;  // ← ADD THIS LINE

namespace hostel_management_system_oop
{
    public partial class Mess : BaseForm  // ← CHANGE Form TO BaseForm
    {
        private MessBL messBL = new MessBL();
        private string selectedHallName = "";
        private string selectedRollNo = "";
        private string selectedFullName = "";

        public Mess()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadDepartments();
            LoadData(); 
        }

        
        public override void LoadData()
        {
            try
            {
                DataTable dt = messBL.GetAllMessData();
                dgvf.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dgvf.Rows.Add(
                        row["HallName"].ToString(),
                        row["TotalSittingCapacity"].ToString(),
                        row["RollNumber"].ToString(),
                        row["FullName"].ToString(),
                        row["Department"].ToString()
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

       
        public override void ClearForm()
        {
            tbx1f7.Clear();
            tbx2f7.Clear();
            tbxrf7.Clear();
            tbx4f7.Clear();
            cbx1f7.SelectedIndex = 0;
            tbx1f7.Focus();
            selectedHallName = "";
            selectedRollNo = "";
            selectedFullName = "";
        }

       
        public override bool ValidateForm()
        {
            if (string.IsNullOrEmpty(tbx1f7.Text))
            {
                MessageBox.Show("Please enter Hall Name!");
                tbx1f7.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(tbx2f7.Text))
            {
                MessageBox.Show("Please enter Seating Capacity!");
                tbx2f7.Focus();
                return false;
            }

            int capacity;
            if (!int.TryParse(tbx2f7.Text, out capacity) || capacity <= 0)
            {
                MessageBox.Show("Please enter valid Capacity!");
                tbx2f7.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(tbxrf7.Text))
            {
                MessageBox.Show("Please enter Roll Number!");
                tbxrf7.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(tbx4f7.Text))
            {
                MessageBox.Show("Please enter Full Name!");
                tbx4f7.Focus();
                return false;
            }

            return true;
        }

        private void SetupDataGridView()
        {
            dgvf.AutoGenerateColumns = false;
            dgvf.Columns.Clear();

            dgvf.Columns.Add("HallName", "Hall Name");
            dgvf.Columns.Add("TotalSittingCapacity", "Capacity");
            dgvf.Columns.Add("RollNumber", "Roll No");
            dgvf.Columns.Add("FullName", "Full Name");
            dgvf.Columns.Add("Department", "Department");

            dgvf.Columns["HallName"].Width = 120;
            dgvf.Columns["TotalSittingCapacity"].Width = 70;
            dgvf.Columns["RollNumber"].Width = 90;
            dgvf.Columns["FullName"].Width = 130;
            dgvf.Columns["Department"].Width = 160;

            dgvf.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvf.AllowUserToAddRows = false;
            dgvf.ReadOnly = true;
            dgvf.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvf.RowHeadersVisible = false;

            dgvf.CellClick += dgvf_CellClick;
        }

        private void LoadDepartments()
        {
            cbx1f7.Items.Clear();
            cbx1f7.Items.Add("Computer Science (CS)");
            cbx1f7.Items.Add("Electrical Engineering (EE)");
            cbx1f7.Items.Add("Mechanical Engineering (ME)");
            cbx1f7.Items.Add("Civil Engineering (CE)");
            cbx1f7.Items.Add("Software Engineering (SE)");
            cbx1f7.Items.Add("Information Technology (IT)");
            cbx1f7.Items.Add("Business Administration (BBA)");
            cbx1f7.SelectedIndex = 0;
        }

        private void dgvf_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvf.Rows[e.RowIndex];

                selectedHallName = row.Cells["HallName"].Value.ToString();
                selectedRollNo = row.Cells["RollNumber"].Value.ToString();
                selectedFullName = row.Cells["FullName"].Value.ToString();

                tbx1f7.Text = row.Cells["HallName"].Value.ToString();
                tbx2f7.Text = row.Cells["TotalSittingCapacity"].Value.ToString();
                tbxrf7.Text = row.Cells["RollNumber"].Value.ToString();
                tbx4f7.Text = row.Cells["FullName"].Value.ToString();
                cbx1f7.Text = row.Cells["Department"].Value.ToString();
            }
        }

       
        private void btn1f7_Click_1(object sender, EventArgs e)
        {
           
            if (!ValidateForm())
                return;

            string hallName = tbx1f7.Text;
            string capacity = tbx2f7.Text;
            string rollNo = tbxrf7.Text;
            string fullName = tbx4f7.Text;
            string department = cbx1f7.Text;

            int cap;
            if (!int.TryParse(capacity, out cap) || cap <= 0)
            {
                MessageBox.Show("Please enter valid Capacity!");
                return;
            }

            string result = messBL.SaveMessData(hallName, cap, rollNo, fullName, department);

            if (result == "SUCCESS")
            {
                MessageBox.Show("Data Saved Successfully!");
                ClearForm(); 
                LoadData();  
            }
            else
            {
                MessageBox.Show("Error: " + result);
            }
        }

        // UPDATE Button
        private void btn2f7_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedHallName) || string.IsNullOrEmpty(selectedRollNo))
            {
                MessageBox.Show("Please click on a row to select a record first!");
                return;
            }

            
            if (!ValidateForm())
                return;

            string hallName = tbx1f7.Text;
            string capacity = tbx2f7.Text;
            string rollNo = tbxrf7.Text;
            string fullName = tbx4f7.Text;
            string department = cbx1f7.Text;

            int cap;
            if (!int.TryParse(capacity, out cap) || cap <= 0)
            {
                MessageBox.Show("Please enter valid Capacity!");
                return;
            }

            string result = messBL.UpdateMessData(selectedHallName, selectedRollNo, selectedFullName,
                                                  hallName, cap, rollNo, fullName, department);

            if (result == "SUCCESS")
            {
                MessageBox.Show("Data Updated Successfully!");
                ClearForm();
                LoadData();
            }
            else
            {
                MessageBox.Show("Error: " + result);
            }
        }

       
        private void btn3f7_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedHallName) || string.IsNullOrEmpty(selectedRollNo))
            {
                MessageBox.Show("Please click on a row to select a record first!");
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this record?\n\n" +
                "Hall: " + selectedHallName + "\nRoll No: " + selectedRollNo + "\nName: " + selectedFullName,
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                string result = messBL.DeleteMessData(selectedHallName, selectedRollNo, selectedFullName);

                if (result == "SUCCESS")
                {
                    MessageBox.Show("Data Deleted Successfully!");
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