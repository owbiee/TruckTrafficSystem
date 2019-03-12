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
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        //Admin Login:
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin-terminal-admin@123.com")
            {
                AddUser user = new AddUser();
                this.Hide();
                user.Show();
            }
            else
            {
                MessageBox.Show("Unauthorized Admin Key!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
