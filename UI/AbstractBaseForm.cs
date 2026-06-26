using System;
using System.Windows.Forms;

namespace hostel_management_system_oop.UI
{
    // ✅ KEEP 'abstract' keyword
    public abstract class AbstractBaseForm : Form
    {
        // ✅ KEEP abstract methods
        public abstract void LoadData();
        public abstract void ClearForm();
        public abstract bool ValidateForm();
        public abstract string FormTitle { get; }

        public AbstractBaseForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.WindowState = FormWindowState.Maximized;
        }
    }
}