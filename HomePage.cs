using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Truck_Records
{
    public partial class HomePage : Form
    {
        //private int childFormNumber = 0;

        public HomePage()
        {
            InitializeComponent();
        }
                          
        private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        //Opens EntryForm for new Record...
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EntryForm entry = new EntryForm();
            entry.MdiParent = this;
            entry.Show();
        }

        //Exit Application...
        private void HomePage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //Open ReportView:
        private void viewReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportView reportView = new ReportView();
            reportView.MdiParent = this;
            reportView.Show();
        }

        //Add New User:
        private void addUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminLogin admin = new AdminLogin();
            admin.MdiParent = this;
            admin.Show();
        }

        //[Log Out]:
        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }
    }
}
