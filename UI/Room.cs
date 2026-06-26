using System;
using System.Data;
using System.Windows.Forms;
using hostel_management_system_oop.BL;
using hostel_management_system_oop.Model;
using hostel_management_system_oop.UI;

namespace hostel_management_system_oop
{
    public partial class Room : AbstractBaseForm
    {
        private RoomBL roomBL = new RoomBL();

        public Room()
        {
            // ✅ FIX: Check if NOT in design mode
            if (!DesignMode)
            {
                InitializeComponent();
                this.Text = FormTitle;
                SetupDataGridView();
                LoadData();
                LoadRoomStatus();
            }
            else
            {
                InitializeComponent();
            }
        }

        public override string FormTitle
        {
            get { return "Room Management"; }
        }

        public override void LoadData()
        {
            try
            {
                DataTable dt = roomBL.GetAllRooms();
                dgvR.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dgvR.Rows.Add(
                        row["StudentName"].ToString(),
                        row["RoomID"].ToString(),
                        row["RoomNumber"].ToString(),
                        row["HostelType"].ToString(),
                        row["TotalCapacity"].ToString(),
                        row["CurrentOccupancy"].ToString(),
                        row["RoomStatus"].ToString()
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading rooms: " + ex.Message);
            }
        }

        public override void ClearForm()
        {
            tbx1f10.Clear();
            tbx2f10.Clear();
            rb1f10.Checked = false;
            rb2f10.Checked = false;
            tbx3f10.Clear();
            tbx4f10.Clear();
            cbx1f10.SelectedIndex = 0;
            tbx2f10.Focus();
        }

        public override bool ValidateForm()
        {
            if (string.IsNullOrEmpty(tbx2f10.Text))
            {
                MessageBox.Show("Please enter Room Number!");
                tbx2f10.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(tbx3f10.Text))
            {
                MessageBox.Show("Please enter Total Capacity!");
                tbx3f10.Focus();
                return false;
            }

            int capacity;
            if (!int.TryParse(tbx3f10.Text, out capacity) || capacity <= 0)
            {
                MessageBox.Show("Please enter valid Total Capacity!");
                tbx3f10.Focus();
                return false;
            }

            return true;
        }

        private void SetupDataGridView()
        {
            dgvR.AutoGenerateColumns = false;
            dgvR.Columns.Clear();

            // Add columns
            dgvR.Columns.Add("StudentName", "Student Name");
            dgvR.Columns.Add("RoomID", "ID");
            dgvR.Columns.Add("RoomNumber", "Room No");
            dgvR.Columns.Add("HostelType", "Hostel Type");
            dgvR.Columns.Add("TotalCapacity", "Total Capacity");
            dgvR.Columns.Add("CurrentOccupancy", "Current Occupancy");
            dgvR.Columns.Add("RoomStatus", "Room Status");

            // Set widths
            dgvR.Columns["StudentName"].Width = 120;
            dgvR.Columns["RoomID"].Width = 50;
            dgvR.Columns["RoomNumber"].Width = 80;
            dgvR.Columns["HostelType"].Width = 90;
            dgvR.Columns["TotalCapacity"].Width = 100;
            dgvR.Columns["CurrentOccupancy"].Width = 110;
            dgvR.Columns["RoomStatus"].Width = 90;

            dgvR.CellClick += dgvR_CellClick;
        }

        private void LoadRoomStatus()
        {
            cbx1f10.Items.Clear();
            cbx1f10.Items.Add("Available");
            cbx1f10.Items.Add("Full");
            cbx1f10.Items.Add("Maintenance");
            cbx1f10.SelectedIndex = 0;
        }

        private void dgvR_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvR.Rows[e.RowIndex];

                tbx1f10.Text = row.Cells["StudentName"].Value.ToString();
                tbx2f10.Text = row.Cells["RoomNumber"].Value.ToString();

                string hostelType = row.Cells["HostelType"].Value.ToString();
                rb1f10.Checked = (hostelType == "Boy");
                rb2f10.Checked = (hostelType == "Girl");

                tbx3f10.Text = row.Cells["TotalCapacity"].Value.ToString();
                tbx4f10.Text = row.Cells["CurrentOccupancy"].Value.ToString();
                cbx1f10.Text = row.Cells["RoomStatus"].Value.ToString();
            }
        }

        // SAVE Button
        private void btns_Click_1(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            RoomModel room = new RoomModel();
            room.StudentName = tbx1f10.Text;
            room.RoomNumber = tbx2f10.Text;
            room.HostelType = rb1f10.Checked ? "Boy" : "Girl";

            if (!int.TryParse(tbx3f10.Text, out int capacity) || capacity <= 0)
            {
                MessageBox.Show("Please enter valid Total Capacity!");
                return;
            }
            room.TotalCapacity = capacity;

            int occupancy = 0;
            if (!string.IsNullOrEmpty(tbx4f10.Text))
                int.TryParse(tbx4f10.Text, out occupancy);
            room.CurrentOccupancy = occupancy;

            room.RoomStatus = cbx1f10.Text;

            string result = roomBL.SaveRoom(room);

            if (result == "SUCCESS")
            {
                MessageBox.Show("Room Saved Successfully!");
                ClearForm();
                LoadData();
            }
            else
            {
                MessageBox.Show("Error: " + result);
            }
        }

        // UPDATE Button
        private void btnup_Click_1(object sender, EventArgs e)
        {
            if (dgvR.CurrentRow == null)
            {
                MessageBox.Show("Please click on a row to select a room first!");
                return;
            }

            if (!ValidateForm())
                return;

            int roomID = Convert.ToInt32(dgvR.CurrentRow.Cells["RoomID"].Value);

            RoomModel room = new RoomModel();
            room.RoomID = roomID;
            room.StudentName = tbx1f10.Text;
            room.RoomNumber = tbx2f10.Text;
            room.HostelType = rb1f10.Checked ? "Boy" : "Girl";

            if (!int.TryParse(tbx3f10.Text, out int capacity) || capacity <= 0)
            {
                MessageBox.Show("Please enter valid Total Capacity!");
                return;
            }
            room.TotalCapacity = capacity;

            int occupancy = 0;
            if (!string.IsNullOrEmpty(tbx4f10.Text))
                int.TryParse(tbx4f10.Text, out occupancy);
            room.CurrentOccupancy = occupancy;

            room.RoomStatus = cbx1f10.Text;

            string result = roomBL.UpdateRoom(room);

            if (result == "SUCCESS")
            {
                MessageBox.Show("Room Updated Successfully!");
                ClearForm();
                LoadData();
            }
            else
            {
                MessageBox.Show("Error: " + result);
            }
        }

        // DELETE Button
        private void btndel_Click_1(object sender, EventArgs e)
        {
            if (dgvR.CurrentRow == null)
            {
                MessageBox.Show("Please click on a row to select a room first!");
                return;
            }

            int roomID = Convert.ToInt32(dgvR.CurrentRow.Cells["RoomID"].Value);

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this room?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                string result = roomBL.DeleteRoom(roomID);

                if (result == "SUCCESS")
                {
                    MessageBox.Show("Room Deleted Successfully!");
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