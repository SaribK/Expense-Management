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
            BindData();
            clearDate();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("select id as 'ID', name as 'Name', expenseType as 'Expense Type', amount as 'Amount', date as 'Date' from tbl_expenses where username = '" + user + "' and name LIKE '%" + txtName.Text + "%'", con);
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dateTimePicker1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!dateTimePicker1.Checked)
            {
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = " ";
            }
            else
            {
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "MMMM dd, yyyy";
            }
        }
    }
}
