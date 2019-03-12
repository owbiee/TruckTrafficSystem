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
using DGVPrinterHelper;


namespace Truck_Records
{
    public partial class ReportView : Form
    {
        //Sql Server Connection String:
        SqlConnection conn = new SqlConnection("Data Source=DEV-001\\SQLEXPRESS;Initial Catalog=Truck_Records;Integrated Security=True");

        public ReportView()
        {
            InitializeComponent();
        }

        private void ReportView_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'truck_RecordsDataSet6.All_Records' table. You can move, or remove it, as needed.
            this.all_RecordsTableAdapter2.Fill(this.truck_RecordsDataSet6.All_Records);
            // TODO: This line of code loads data into the 'truck_RecordsDataSet3.All_Records' table. You can move, or remove it, as needed.
            this.all_RecordsTableAdapter1.Fill(this.truck_RecordsDataSet3.All_Records);
            // TODO: This line of code loads data into the 'truck_RecordsDataSet1.All_Records' table. You can move, or remove it, as needed.
            this.all_RecordsTableAdapter.Fill(this.truck_RecordsDataSet1.All_Records);

            //Populate ComboBox:
            List<String> rolelist = new List<string>();
            rolelist.Add("--Select Column--");
            rolelist.Add("Agency");
            rolelist.Add("Consignee");
            rolelist.Add("Description of Goods");
            rolelist.Add("Truck Reg No");
            rolelist.Add("Traffic Type");
            rolelist.Add("Container Type");
            //rolelist.Add("Consignee");

            this.comboBox1.DataSource = rolelist;
        }

        //Filter Data By Combobox Value:
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //Value: 1
                if (comboBox1.Text == "Agency")
                {
                    
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT Date_Of_Entry,Traffic_Type,Agency,Consignee,Description_Of_Goods,Container_Type,Container_No,Truck_RegNo,Driver_Name,Phone_No,Date_Of_Exit,Duration FROM All_Records Where Agency like '" + textBox2.Text + "%'", conn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                //Value: 2
                else if (comboBox1.Text == "Consignee")
                {
                    
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT Date_Of_Entry, Traffic_Type, Agency, Consignee, Description_Of_Goods,Container_Type,Container_No, Truck_RegNo, Driver_Name, Phone_No, Date_Of_Exit, Duration FROM All_Records Where Consignee like '" + textBox2.Text + "%'", conn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                //Value: 3
                else if (comboBox1.Text == "Description of Goods")
                {
                    
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT Date_Of_Entry, Traffic_Type, Agency, Consignee, Description_Of_Goods, Container_Type,Container_No,Truck_RegNo, Driver_Name, Phone_No, Date_Of_Exit, Duration FROM All_Records Where Description_Of_Goods like '" + textBox2.Text + "%'", conn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                //Value: 4
                else if (comboBox1.Text == "Truck Reg No")
                {
                    
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT Date_Of_Entry, Traffic_Type, Agency, Consignee, Description_Of_Goods,Container_Type,Container_No, Truck_RegNo, Driver_Name, Phone_No, Date_Of_Exit, Duration FROM All_Records Where Truck_RegNo like '" + textBox2.Text + "%'", conn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                //Value: 5
                else if (comboBox1.Text == "Traffic Type")
                {

                    SqlDataAdapter sda = new SqlDataAdapter("SELECT Date_Of_Entry, Traffic_Type, Agency, Consignee, Description_Of_Goods,Container_Type, Container_No, Truck_RegNo, Driver_Name, Phone_No, Date_Of_Exit, Duration FROM All_Records Where Traffic_Type like '" + textBox2.Text + "%'", conn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                //Value: 6
                else if (comboBox1.Text == "Container Type")
                {

                    SqlDataAdapter sda = new SqlDataAdapter("SELECT Date_Of_Entry, Traffic_Type, Agency, Consignee, Description_Of_Goods,Container_Type,Container_No, Truck_RegNo, Driver_Name, Phone_No, Date_Of_Exit, Duration FROM All_Records Where Container_Type like '" + textBox2.Text + "%'", conn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Filter Data by Date:
        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM All_Records WHERE Date_Of_Entry between '"+dateTimePicker1.Value+"' and '"+dateTimePicker2.Value+"'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        //Generate Report to PDF using DGV Printer Class:
        //Printer Configurations:
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DGVPrinter printer = new DGVPrinter();
                printer.Title = "Hanover Terminal Ltd."; //Customize Header Here
                printer.SubTitle = textBox1.Text; //Customize Sub-Header Here
                printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                printer.PageNumbers = true;
                printer.PageNumberInHeader = false;
                printer.PorportionalColumns = true;
                printer.HeaderCellAlignment = StringAlignment.Near;
                printer.Footer = "www.hanoverterminal.com"; // Customize Footer Here
                printer.FooterSpacing = 15;
                printer.PrintPreviewDataGridView(dataGridView1);
                printer.PrintDataGridView(dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
