using System;
using System.Data;
using System.Windows.Forms;
using hostel_management_system_oop.BL;
using hostel_management_system_oop.Model;
using hostel_management_system_oop.UI;  
namespace hostel_management_system_oop
{
    public partial class FeeForm : BaseForm  
    {
        private FeeBL feeBL = new FeeBL();
        private int selectedFeeID = -1;

        public FeeForm()
        {
            InitializeComponent();
            SetupDataGridView();
            dtpf.Value = DateTime.Now.AddMonths(1);
            LoadData(); 
        }

       
        public override void LoadData()
        {
            try
            {
                DataTable dt = feeBL.GetAllFees();
                dgv1.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dgv1.Rows.Add(
                        row["FeeID"].ToString(),
                        row["RollNumber"].ToString(),
                        row["AmountDue"].ToString(),
                        row["AmountPaid"].ToString(),
                        Convert.ToDateTime(row["DueDate"]).ToString("dd/MM/yyyy"),
                        row["PaymentStatus"].ToString()
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading fees: " + ex.Message);
            }
        }

       
        public override void ClearForm()
        {
            tbx1f.Clear();
            tbx2f.Clear();
            tbx3f.Clear();
            dtpf.Value = DateTime.Now.AddMonths(1);
            tbx1f.Focus();
            selectedFeeID = -1;
        }

       
        public override bool ValidateForm()
        {
            if (string.IsNullOrEmpty(tbx1f.Text))
            {
                MessageBox.Show("Please enter Roll Number!");
                tbx1f.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(tbx2f.Text))
            {
                MessageBox.Show("Please enter Amount Due!");
                tbx2f.Focus();
                return false;
            }

            int amountDue;
            if (!int.TryParse(tbx2f.Text, out amountDue) || amountDue <= 0)
            {
                MessageBox.Show("Amount Due must be greater than zero!");
                tbx2f.Focus();
                return false;
            }

            return true;
        }

        private void SetupDataGridView()
        {
            dgv1.AutoGenerateColumns = false;
            dgv1.Columns.Clear();

            dgv1.Columns.Add("FeeID", "ID");
            dgv1.Columns.Add("RollNumber", "Roll No");
            dgv1.Columns.Add("AmountDue", "Amount Due");
            dgv1.Columns.Add("AmountPaid", "Amount Paid");
            dgv1.Columns.Add("DueDate", "Due Date");
            dgv1.Columns.Add("PaymentStatus", "Status");

            dgv1.Columns["FeeID"].Width = 50;
            dgv1.Columns["RollNumber"].Width = 100;
            dgv1.Columns["AmountDue"].Width = 100;
            dgv1.Columns["AmountPaid"].Width = 100;
            dgv1.Columns["DueDate"].Width = 120;
            dgv1.Columns["PaymentStatus"].Width = 80;

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

                selectedFeeID = Convert.ToInt32(row.Cells["FeeID"].Value);

                tbx1f.Text = row.Cells["RollNumber"].Value.ToString();
                tbx2f.Text = row.Cells["AmountDue"].Value.ToString();
                tbx3f.Text = row.Cells["AmountPaid"].Value.ToString();
                dtpf.Value = Convert.ToDateTime(row.Cells["DueDate"].Value);
            }
        }

       
        private string CalculateStatus(int amountDue, int amountPaid)
        {
           
            if (amountDue <= 0)
                return "Invalid (Due > 0)";

            
            if (amountPaid > amountDue)
                return "Invalid (Paid > Due)";

            // Logic
            if (amountPaid == 0)
                return "Pending";
            else if (amountPaid < amountDue)
                return "Partial";
            else
                return "Paid";
        }

       
        private bool ValidateAmounts(int amountDue, int amountPaid)
        {
            if (amountDue <= 0)
            {
                MessageBox.Show("Amount Due must be greater than zero!");
                return false;
            }

            if (amountPaid > amountDue)
            {
                MessageBox.Show("Amount Paid cannot be greater than Amount Due!");
                return false;
            }

            return true;
        }

        // SAVE Button
        private void btn1f_Click(object sender, EventArgs e)
        {
           
            if (!ValidateForm())
                return;

            int amountDue, amountPaid = 0;
            if (!int.TryParse(tbx2f.Text, out amountDue) || amountDue <= 0)
            {
                MessageBox.Show("Amount Due must be greater than zero!");
                return;
            }

            if (!string.IsNullOrEmpty(tbx3f.Text))
            {
                int.TryParse(tbx3f.Text, out amountPaid);
            }

           
            if (!ValidateAmounts(amountDue, amountPaid))
                return;

            
            string status = CalculateStatus(amountDue, amountPaid);

            FeeModel fee = new FeeModel();
            fee.RollNumber = tbx1f.Text;
            fee.AmountDue = amountDue;
            fee.AmountPaid = amountPaid;
            fee.DueDate = dtpf.Value.Date;
            fee.PaymentStatus = status;

            string result = feeBL.SaveFee(fee);

            if (result == "SUCCESS")
            {
                MessageBox.Show($"Fee Record Saved Successfully! Status: {status}");
                ClearForm(); 
                LoadData();   
            }
            else
            {
                MessageBox.Show("Error: " + result);
            }
        }

       
        private void btn2f_Click_1(object sender, EventArgs e)
        {
            if (selectedFeeID == -1)
            {
                MessageBox.Show("Please click on a row to select a fee record first!");
                return;
            }

            
            if (!ValidateForm())
                return;

            int amountDue, amountPaid = 0;
            if (!int.TryParse(tbx2f.Text, out amountDue) || amountDue <= 0)
            {
                MessageBox.Show("Amount Due must be greater than zero!");
                return;
            }

            if (!string.IsNullOrEmpty(tbx3f.Text))
            {
                int.TryParse(tbx3f.Text, out amountPaid);
            }

           
            if (!ValidateAmounts(amountDue, amountPaid))
                return;

            
            string status = CalculateStatus(amountDue, amountPaid);

            FeeModel fee = new FeeModel();
            fee.FeeID = selectedFeeID;
            fee.RollNumber = tbx1f.Text;
            fee.AmountDue = amountDue;
            fee.AmountPaid = amountPaid;
            fee.DueDate = dtpf.Value.Date;
            fee.PaymentStatus = status;

            string result = feeBL.UpdateFee(fee);

            if (result == "SUCCESS")
            {
                MessageBox.Show($"Fee Record Updated Successfully! Status: {status}");
                ClearForm();
                LoadData();
                selectedFeeID = -1;
            }
            else
            {
                MessageBox.Show("Error: " + result);
            }
        }

       
        private void btn3f_Click_1(object sender, EventArgs e)
        {
            if (selectedFeeID == -1)
            {
                MessageBox.Show("Please click on a row to select a fee record first!");
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this fee record?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                string result = feeBL.DeleteFee(selectedFeeID);

                if (result == "SUCCESS")
                {
                    MessageBox.Show("Fee Record Deleted Successfully!");
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