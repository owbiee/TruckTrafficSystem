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
    public partial class EntryForm : Form
    {
        SqlConnection conn = new SqlConnection("Data Source=DEV-001\\SQLEXPRESS;Initial Catalog=Truck_Records;Integrated Security=True");
        SqlCommand command;

        public EntryForm()
        {
            InitializeComponent();
        }

        private void EntryForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'truck_RecordsDataSet5.All_Records' table. You can move, or remove it, as needed.
            this.all_RecordsTableAdapter2.Fill(this.truck_RecordsDataSet5.All_Records);
            // TODO: This line of code loads data into the 'truck_RecordsDataSet4.All_Records' table. You can move, or remove it, as needed.
            this.all_RecordsTableAdapter1.Fill(this.truck_RecordsDataSet4.All_Records);
            // TODO: This line of code loads data into the 'truck_RecordsDataSet.All_Records' table. You can move, or remove it, as needed.
            this.all_RecordsTableAdapter.Fill(this.truck_RecordsDataSet.All_Records);

            List<String> rolelist = new List<string>();
            rolelist.Add("--Select Column--");
            rolelist.Add("Transir Traffic");
            rolelist.Add("Unstuffing Traffic");
            //rolelist.Add("Description of Goods");
            //rolelist.Add("Truck Reg No");
            //rolelist.Add("Consignee");

            this.comboBox1.DataSource = rolelist;

            List<String> listtype = new List<string>();
            listtype.Add("--Select Column--");
            listtype.Add("20FT");
            listtype.Add("40FT");
            //rolelist.Add("Description of Goods");
            //rolelist.Add("Truck Reg No");
            //rolelist.Add("Consignee");

            this.comboBox2.DataSource = listtype;

        }

        //Create New Record...
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text=="" || textBox2.Text=="" || textBox3.Text=="" || textBox4.Text=="")
                {
                    MessageBox.Show("Enter the necessary informations!");
                }
                else
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO All_Records Values('" + dateTimePicker1.Value + "','"+ comboBox1.Text +"','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "', '"+ comboBox2.Text +"', '"+ textBox9.Text +"','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + dateTimePicker2.Value + "','" + textBox7.Text + "')";

                    cmd.ExecuteNonQuery();
                    conn.Close();

                    //Call the UpdateStock Method...
                    //UpdateStock();
                    EntryForm_Load(sender, e);
                    MessageBox.Show("Data Entry Successfull!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Get Value between two dates...
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //string eir = dateTimePicker2.Value.ToString();
                //textBox6.Text = eir;
                //int date= Convert.To(textBox4.Text);
                //date = dateTimePicker2.Value.Subtract(dateTimePicker3.Value);
                TimeSpan span = dateTimePicker2.Value - dateTimePicker1.Value;

                //int amount = Convert.ToInt32(textBox5.Text); //detention amount
                int days = Convert.ToInt32(span.Days + 1);
                //int newday = days + 1;
                //int amount = 87150 * days;
                //string newamount = amount.ToString();
                //textBox5.Text = amount.ToString();
                textBox7.Text = days.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //GET Record from All_Records DB...
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT Date_Of_Entry, Traffic_Type, Agency, Consignee, Description_Of_Goods, Container_Type, Container_No, Truck_RegNo, Driver_Name, Phone_No, Date_Of_Exit, Duration FROM All_Records where Serial_No='" + textBox8.Text + "'";
                //string sql = "SELECT * FROM [StockControl].[dbo].[Maintenance_Stock] where ItemCode='" + textBox1.Text + "'";
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    command = new SqlCommand(sql, conn);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        dateTimePicker1.Value = Convert.ToDateTime(reader[0]); //Date of Entry
                        comboBox1.Text = reader[1].ToString(); //Traffic Type
                        textBox1.Text = reader[2].ToString(); //Agency                      
                        textBox2.Text = reader[3].ToString(); //Consignee
                        textBox3.Text = reader[4].ToString(); //Description_Of_Goods
                        comboBox2.Text = reader[5].ToString(); //Container_Type
                        textBox9.Text = reader[6].ToString(); //Container Number
                        textBox4.Text = reader[7].ToString(); //Truck_RegNo
                        textBox5.Text = reader[8].ToString(); //Driver_Name
                        textBox6.Text = reader[9].ToString(); //Phone_No                       
                        dateTimePicker2.Value = Convert.ToDateTime(reader[10]);
                        textBox7.Text = reader[11].ToString();
                        //textBox11.Text = reader[10].ToString();

                    }
                    else
                    {
                        MessageBox.Show("Record does not exist!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //Update Records:
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                conn.Open();
                command = conn.CreateCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = "update All_Records set Date_Of_Entry='" + dateTimePicker1.Value + "', Traffic_Type='"+ comboBox1.Text +"', Agency='" + textBox1.Text + "', Consignee='" + textBox2.Text + "', Description_Of_Goods='" + textBox3.Text + "', Container_Type='"+ comboBox2.Text +"', Container_No='"+ textBox9.Text +"', Truck_RegNo='" + textBox4.Text + "', Driver_Name='" + textBox5.Text + "', Phone_No='" + textBox6.Text + "', Date_Of_Exit='" + dateTimePicker2.Value + "', Duration='" + textBox7.Text + "' where Serial_No='" + textBox8.Text + "'";
                command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Record Updated Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //Delete Record:
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are You Sure you want to delete this Record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    SqlCommand command = conn.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "DELETE FROM All_Records WHERE Serial_No='" + textBox8.Text + "'";
                    command.ExecuteNonQuery();
                    conn.Close();

                    EntryForm_Load(sender, e); //Reload Datagrid View
                    MessageBox.Show("Record Deleted Successfully!");

                    //Call Clear Function/Method...
                    //button4_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Refresh or Reload DatagridView...
        private void button6_Click(object sender, EventArgs e)
        {
            EntryForm_Load(sender, e); //Reload Datagrid View
        }

        private void EntryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
