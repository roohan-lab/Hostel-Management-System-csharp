using System;
using System.Windows.Forms;

namespace hostel_management_system_oop
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AdminLogin());
        }
    }
}