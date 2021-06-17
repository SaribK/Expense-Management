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
    public partial class frmInsert : Form
    {
        string user;
        public frmInsert()
        {
            InitializeComponent();
        }

        public frmInsert(string curr_user)
        {
            InitializeComponent();
            user = curr_user;
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OFIG42E\\SQLEXPRESS;Initial Catalog=ExpensesDB;Persist Security Info=True;User ID=sarib;Password=sarib2001");

        void BindData()
        {
            // TODO learn how to make data fitted into datagridview
            SqlCommand command = new SqlCommand("select name as 'Name', expenseType as 'Expense Type', amount as 'Amount', date as 'Date' from tbl_expenses where username = '"+user+"'", con);
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void frmInsert_Load(object sender, EventArgs e)
        {
            BindData();
        }

        //insert into database
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
