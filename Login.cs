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
    public partial class Login : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DEV-001\\SQLEXPRESS;Initial Catalog=Truck_Records;Integrated Security=True");
        public Login()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.label6.Text = dateTime.ToString();
        }

        //[Login]: [Login_Trace]:
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //SqlConnection conn = new SqlConnection("Data Source=DEV-001\\SQLEXPRESS;Initial Catalog=StockControl;Integrated Security=True");
                SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [Truck_Records].[dbo].[User_Login] where UserName='" + textBox1.Text + "' and Password='" + textBox2.Text + "'", conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                //User Authentication...
                if (dt.Rows.Count == 1)
                {

                    string role = textBox3.Text; //TextBox containing User/Role...
                    switch (role)
                    {
                        //Verify User Role & Identity...
                        case "Software Engineer":
                            this.Hide();
                            HomePage home = new HomePage();
                            //home.label3.Text = label7.Text;
                            home.Show();
                            //ValidateUser();
                            break;

                        case "Security Officer I":
                            this.Hide();
                            HomePage homePage = new HomePage();
                            //user2.label3.Text = label7.Text;
                            homePage.Show();
                            break;

                        case "Security Officer II":
                            this.Hide();
                            HomePage Page = new HomePage();
                            //user2.label3.Text = label7.Text;
                            Page.Show();
                            break;

                        default:
                            MessageBox.Show("Verify Role!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Credentials!, Contact System Admin!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Call the Clear Event Handler(Method)...
                    button2_Click(sender, e);
                }

                //[Login_Trace]:
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Login_Trace Values('" + label5.Text + "','" + textBox3.Text + "','" + textBox1.Text + "','" + label6.Text + "')";

                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //[Verify User]:
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //SqlConnection conn = new SqlConnection("Data Source=DEV-001\\SQLEXPRESS;Initial Catalog=StockControl;Integrated Security=True");
                SqlCommand command;
                string sql = "SELECT Role, Name FROM User_Login where Username='" + textBox1.Text + "' and Password='" + textBox2.Text + "'";
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    command = new SqlCommand(sql, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        textBox3.Text = reader[0].ToString(); //Role TextBox... 
                        label5.Text = reader[1].ToString(); //Name Label...
                    }
                    else
                    {
                        MessageBox.Show("User does not exist!","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    conn.Close();

                }
            }
            catch (Exception ex)
            {
                //conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        //Load UserForm:
        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        //Exit App:
        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
