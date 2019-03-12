using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Truck_Records
{
    public partial class AddUser : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DEV-001\\SQLEXPRESS;Initial Catalog=Truck_Records;Integrated Security=True");
        

        public AddUser()
        {
            InitializeComponent();
        }

        //CreateNew User Account:
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO User_Login Values('" + textBox1.Text + "','" + comboBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')";
                cmd.ExecuteNonQuery();
                conn.Close();

                //Call Clear Function/Method...
                button4_Click(sender, e);

                //Refresh DataGridView...
                AddUser_Load(sender, e);
                MessageBox.Show("User Account Created Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Clear Form:
        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            comboBox1.Text = "";
            textBox2.Clear();
            textBox3.Clear();
        }

        //Get User:
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT Name, Role, Password FROM User_Login where UserName='" + textBox2.Text + "'";
                //string sql = "SELECT * FROM [StockControl].[dbo].[Maintenance_Stock] where ItemCode='" + textBox1.Text + "'";
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        textBox1.Text = reader[0].ToString(); //Name
                        //comboBox1.Text = reader[1].ToString(); //UserName
                        comboBox1.Text = reader[1].ToString(); //Role
                        textBox3.Text = reader[2].ToString(); //Password

                    }
                    else
                    {
                        MessageBox.Show("User does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        //Delete User Account
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM User_Login WHERE Username='" + textBox2.Text + "'";
                command.ExecuteNonQuery();
                conn.Close();

                //Call Clear Function/Method...
                button4_Click(sender, e);

                //Refresh DataGridView...
                AddUser_Load(sender, e);
                MessageBox.Show("User Account Deleted Successfully!");

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'truck_RecordsDataSet2.User_Login' table. You can move, or remove it, as needed.
            this.user_LoginTableAdapter.Fill(this.truck_RecordsDataSet2.User_Login);

            List<String> rolelist = new List<string>();
            rolelist.Add("--Select Role--");
            rolelist.Add("Software Engineer");
            rolelist.Add("Security Officer I");
            rolelist.Add("Security Officer II");
            

            //rolelist.Add("Logistics Manager");
            //rolelist.Add("Import & Export Manager");
            //rolelist.Add("Data Entry Officer");

            this.comboBox1.DataSource = rolelist;
        }
    }
}
