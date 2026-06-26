using System;
using System.Windows.Forms;

namespace hostel_management_system_oop.UI
{
    public class BaseForm : Form
    {
       
        public virtual void LoadData()
        {
           
        }

        public virtual void ClearForm()
        {
            
        }

        public virtual bool ValidateForm()
        {
            return true;
        }

        public BaseForm()
        {
            // Common form settings
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;  
            this.MaximizeBox = true;  
            this.MinimizeBox = true;  
            this.WindowState = FormWindowState.Normal; 
        }
    }
}