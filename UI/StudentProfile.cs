using System;
using System.Data;
using System.Windows.Forms;
using hostel_management_system_oop.BL;
using hostel_management_system_oop.Model;
using hostel_management_system_oop.UI;

namespace hostel_management_system_oop
{
    public partial class StudentProfile : AbstractBaseForm
    {
        private StudentBL studentBL = new StudentBL();
        private int selectedStudentID = -1;

        public StudentProfile()
        {
            // ✅ FIX: Check if NOT in design mode
            if (!DesignMode)
            {
                InitializeComponent();
                this.Text = FormTitle;
                LoadData();
                dgvst.SelectionChanged += dgvst_SelectionChanged;
            }
            else
            {
                InitializeComponent();
            }
        }

        public override string FormTitle
        {
            get { return "Student Management"; }
        }

        public override void LoadData()
        {
            try
            {
                DataTable dt = studentBL.GetAllStudents();
                dgvst.DataSource = dt;

                // Set column headers
                if (dgvst.Columns.Contains("StudentID"))
                    dgvst.Columns["StudentID"].HeaderText = "ID";
                if (dgvst.Columns.Contains("RollNumber"))
                    dgvst.Columns["RollNumber"].HeaderText = "Roll No";
                if (dgvst.Columns.Contains("FullName"))
                    dgvst.Columns["FullName"].HeaderText = "Full Name";
                if (dgvst.Columns.Contains("CNIC"))
                    dgvst.Columns["CNIC"].HeaderText = "CNIC";
                if (dgvst.Columns.Contains("Gender"))
                    dgvst.Columns["Gender"].HeaderText = "Gender";
                if (dgvst.Columns.Contains("PhoneNumber"))
                    dgvst.Columns["PhoneNumber"].HeaderText = "Phone";
                if (dgvst.Columns.Contains("Email"))
                    dgvst.Columns["Email"].HeaderText = "Email";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message);
            }
        }

        public override void ClearForm()
        {
            tbx2f3.Clear();
            tbx3f3.Clear();
            tbx4f3.Clear();
            tbx5f3.Clear();
            tbx6f3.Clear();
            tbx7f3.Clear();
            tbx2f3.Focus();
            selectedStudentID = -1;
        }

        public override bool ValidateForm()
        {
            if (string.IsNullOrEmpty(tbx2f3.Text))
            {
                MessageBox.Show("Please enter Roll Number!");
                tbx2f3.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(tbx3f3.Text))
            {
                MessageBox.Show("Please enter Full Name!");
                tbx3f3.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(tbx4f3.Text))
            {
                MessageBox.Show("Please enter CNIC!");
                tbx4f3.Focus();
                return false;
            }

            return true;
        }

        private void dgvst_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvst.SelectedRows.Count > 0)
            {
                selectedStudentID = Convert.ToInt32(dgvst.SelectedRows[0].Cells["StudentID"].Value);

                tbx2f3.Text = dgvst.SelectedRows[0].Cells["RollNumber"].Value.ToString();
                tbx3f3.Text = dgvst.SelectedRows[0].Cells["FullName"].Value.ToString();
                tbx4f3.Text = dgvst.SelectedRows[0].Cells["CNIC"].Value.ToString();
                tbx5f3.Text = dgvst.SelectedRows[0].Cells["Gender"].Value.ToString();
                tbx6f3.Text = dgvst.SelectedRows[0].Cells["PhoneNumber"].Value.ToString();
                tbx7f3.Text = dgvst.SelectedRows[0].Cells["Email"].Value.ToString();
            }
        }

        // SAVE Button
        private void btnsave_Click_1(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            Student student = new Student();
            student.RollNumber = tbx2f3.Text;
            student.FullName = tbx3f3.Text;
            student.CNIC = tbx4f3.Text;
            student.Gender = tbx5f3.Text;
            student.PhoneNumber = tbx6f3.Text;
            student.Email = tbx7f3.Text;

            string result = studentBL.SaveStudent(student);

            if (result == "SUCCESS")
            {
                MessageBox.Show("Student Saved Successfully!");
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
            if (selectedStudentID == -1)
            {
                MessageBox.Show("Please select a student from the list first!");
                return;
            }

            if (!ValidateForm())
                return;

            Student student = new Student();
            student.StudentID = selectedStudentID;
            student.RollNumber = tbx2f3.Text;
            student.FullName = tbx3f3.Text;
            student.CNIC = tbx4f3.Text;
            student.Gender = tbx5f3.Text;
            student.PhoneNumber = tbx6f3.Text;
            student.Email = tbx7f3.Text;

            string result = studentBL.UpdateStudent(student);

            if (result == "SUCCESS")
            {
                MessageBox.Show("Student Updated Successfully!");
                ClearForm();
                LoadData();
                selectedStudentID = -1;
            }
            else
            {
                MessageBox.Show("Error: " + result);
            }
        }

        // DELETE Button
        private void btndel_Click(object sender, EventArgs e)
        {
            if (selectedStudentID == -1)
            {
                MessageBox.Show("Please select a student from the list first!");
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this student?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                string result = studentBL.DeleteStudent(selectedStudentID);

                if (result == "SUCCESS")
                {
                    MessageBox.Show("Student Deleted Successfully!");
                    ClearForm();
                    LoadData();
                    selectedStudentID = -1;
                }
                else
                {
                    MessageBox.Show("Error: " + result);
                }
            }
        }
    }
}