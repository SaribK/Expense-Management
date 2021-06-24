using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Expense_Management
{
    public partial class frmSearch : Form
    {
        string user;
        public frmSearch()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OFIG42E\\SQLEXPRESS;Initial Catalog=ExpensesDB;Persist Security Info=True;User ID=sarib;Password=sarib2001");

        public frmSearch(string curr_user)
        {
            InitializeComponent();
            user = curr_user;
        }

        void BindData()
        {
            SqlCommand command = new SqlCommand("select id as 'ID', name as 'Name', expenseType as 'Expense Type', amount as 'Amount', date as 'Date' from tbl_expenses where username = '" + user + "'", con);
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void clearDate()
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = " ";
            dateTimePicker1.ShowCheckBox = true;
            dateTimePicker1.Checked = false;
        }

        private void frmSearch_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            //BindData();
            clearDate();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            //search();
        }

        private void dateTimePicker1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!dateTimePicker1.Checked)
            {
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = " ";
                //search();
            }
            else
            {
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "MMMM dd, yyyy";
                //search();
            }
        }

        void search()
        {
            SqlCommand command;
            if (dateTimePicker1.Text.Length != 1)
            {
                // search date as well
                command = new SqlCommand("select id as 'ID', name as 'Name', expenseType as 'Expense Type', amount as 'Amount', date as 'Date' from tbl_expenses where username = '" + user + "' and name LIKE '%" + txtName.Text + "%' and amount LIKE '%" + txtAmount.Text + "%' and expenseType LIKE '%" + comboBox1.Text + "%' and id LIKE '%" + txtID.Text + "%' and date = '" + dateTimePicker1.Value.ToString("yyyy/MM/dd") + "'", con);
            }
            else
            {
                command = new SqlCommand("select id as 'ID', name as 'Name', expenseType as 'Expense Type', amount as 'Amount', date as 'Date' from tbl_expenses where username = '" + user + "' and name LIKE '%" + txtName.Text + "%' and amount LIKE '%" + txtAmount.Text + "%' and expenseType LIKE '%" + comboBox1.Text + "%' and id LIKE '%" + txtID.Text + "%'", con);
            }
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            //search();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            //search();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            //search();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //search();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new dashboard(user).Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            search();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                Microsoft.Office.Interop.Excel.Application excl = new Microsoft.Office.Interop.Excel.Application();
                excl.Application.Workbooks.Add(Type.Missing);
                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    excl.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        excl.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                excl.Columns.AutoFit();
                excl.Visible = true;
            }
            /*using(SaveFileDialog sfd = new SaveFileDialog() { Filter="Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using(XLWorkbook workbook = new XLWorkbook())
                        {
                            workbook.Worksheets.Add(this.dataGridView1.Columns.Cop)
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            */
        }
    }
}
