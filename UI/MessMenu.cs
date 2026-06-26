using System;
using System.Data;
using System.Windows.Forms;
using hostel_management_system_oop.BL;
using hostel_management_system_oop.Model;
using hostel_management_system_oop.UI; 
namespace hostel_management_system_oop
{
    public partial class MessMenu : BaseForm 
    {
        private MessMenuBL menuBL = new MessMenuBL();
        private int selectedMenuID = -1;

        public MessMenu()
        {
            InitializeComponent();
            SetupDataGridView();
            LoadData(); 
            LoadDayNames();
        }

       
        public override void LoadData()
        {
            try
            {
                DataTable dt = menuBL.GetAllMenu();
                dgvMenu.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dgvMenu.Rows.Add(
                        row["MenuID"].ToString(),
                        row["DayName"].ToString(),
                        row["MealType"].ToString(),
                        row["FoodItem"].ToString()
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading menu: " + ex.Message);
            }
        }

        // ← ADD THIS OVERRIDE METHOD (Replaces ClearForm)
        public override void ClearForm()
        {
            cbx1f8.SelectedIndex = -1;
            rb1f8.Checked = false;
            rb2f8.Checked = false;
            rb3f8.Checked = false;
            tbx1f8.Clear();
            tbx1f8.Focus();
            selectedMenuID = -1;
        }

       
        public override bool ValidateForm()
        {
            if (string.IsNullOrEmpty(cbx1f8.Text))
            {
                MessageBox.Show("Please select Day Name!");
                cbx1f8.Focus();
                return false;
            }

            string mealType = "";
            if (rb1f8.Checked) mealType = "Breakfast";
            else if (rb2f8.Checked) mealType = "Lunch";
            else if (rb3f8.Checked) mealType = "Dinner";

            if (string.IsNullOrEmpty(mealType))
            {
                MessageBox.Show("Please select Meal Type!");
                return false;
            }

            if (string.IsNullOrEmpty(tbx1f8.Text))
            {
                MessageBox.Show("Please enter Food Item!");
                tbx1f8.Focus();
                return false;
            }

            return true;
        }

        private void SetupDataGridView()
        {
            dgvMenu.AutoGenerateColumns = false;
            dgvMenu.Columns.Clear();

            // Add columns
            dgvMenu.Columns.Add("MenuID", "ID");
            dgvMenu.Columns.Add("DayName", "Day Name");
            dgvMenu.Columns.Add("MealType", "Meal Type");
            dgvMenu.Columns.Add("FoodItem", "Food Item");

           
            dgvMenu.Columns["MenuID"].Width = 60;
            dgvMenu.Columns["DayName"].Width = 120;
            dgvMenu.Columns["MealType"].Width = 100;
            dgvMenu.Columns["FoodItem"].Width = 250;

           
            dgvMenu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMenu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvMenu.AllowUserToAddRows = false;
            dgvMenu.ReadOnly = true;
            dgvMenu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMenu.RowHeadersVisible = false;

            dgvMenu.CellClick += dgvMenu_CellClick;
        }

        private void LoadDayNames()
        {
            cbx1f8.Items.Clear();
            cbx1f8.Items.Add("Monday");
            cbx1f8.Items.Add("Tuesday");
            cbx1f8.Items.Add("Wednesday");
            cbx1f8.Items.Add("Thursday");
            cbx1f8.Items.Add("Friday");
            cbx1f8.Items.Add("Saturday");
            cbx1f8.Items.Add("Sunday");
            cbx1f8.SelectedIndex = 0;
        }

        private void dgvMenu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMenu.Rows[e.RowIndex];

                selectedMenuID = Convert.ToInt32(row.Cells["MenuID"].Value);

                cbx1f8.Text = row.Cells["DayName"].Value.ToString();

                string mealType = row.Cells["MealType"].Value.ToString();
                rb1f8.Checked = (mealType == "Breakfast");
                rb2f8.Checked = (mealType == "Lunch");
                rb3f8.Checked = (mealType == "Dinner");

                tbx1f8.Text = row.Cells["FoodItem"].Value.ToString();
            }
        }

       
        private void btn2f8_Click(object sender, EventArgs e)
        {
           
            if (!ValidateForm())
                return;

            string mealType = "";
            if (rb1f8.Checked) mealType = "Breakfast";
            else if (rb2f8.Checked) mealType = "Lunch";
            else if (rb3f8.Checked) mealType = "Dinner";

            MessMenuModel menu = new MessMenuModel();
            menu.DayName = cbx1f8.Text;
            menu.MealType = mealType;
            menu.FoodItem = tbx1f8.Text;

            string result = menuBL.SaveMenu(menu);

            if (result == "SUCCESS")
            {
                MessageBox.Show("Menu Saved Successfully!");
                ClearForm(); 
                LoadData();  
            }
            else
            {
                MessageBox.Show("Error: " + result);
            }
        }

        
        private void btn1f8_Click(object sender, EventArgs e)
        {
            if (selectedMenuID == -1)
            {
                MessageBox.Show("Please click on a row to select a menu item first!");
                return;
            }

           
            if (!ValidateForm())
                return;

            string mealType = "";
            if (rb1f8.Checked) mealType = "Breakfast";
            else if (rb2f8.Checked) mealType = "Lunch";
            else if (rb3f8.Checked) mealType = "Dinner";

            MessMenuModel menu = new MessMenuModel();
            menu.MenuID = selectedMenuID;
            menu.DayName = cbx1f8.Text;
            menu.MealType = mealType;
            menu.FoodItem = tbx1f8.Text;

            string result = menuBL.UpdateMenu(menu);

            if (result == "SUCCESS")
            {
                MessageBox.Show("Menu Updated Successfully!");
                ClearForm();
                LoadData();
                selectedMenuID = -1;
            }
            else
            {
                MessageBox.Show("Error: " + result);
            }
        }

        // DELETE Button
        private void btn3f8_Click_1(object sender, EventArgs e)
        {
            if (selectedMenuID == -1)
            {
                MessageBox.Show("Please click on a row to select a menu item first!");
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this menu item?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                string result = menuBL.DeleteMenu(selectedMenuID);

                if (result == "SUCCESS")
                {
                    MessageBox.Show("Menu Deleted Successfully!");
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