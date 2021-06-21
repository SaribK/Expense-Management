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
    public partial class dashboard : Form
    {
        string user;
        public dashboard()
        {
            InitializeComponent();
        }
        public dashboard(string curr_user)
        {
            InitializeComponent();
            user = curr_user;
        }

        private void dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new frmInsert(user).Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new frmSearch().Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-OFIG42E\\SQLEXPRESS;Initial Catalog=ExpensesDB;Persist Security Info=True;User ID=sarib;Password=sarib2001");

        private void dashboard_Load(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            int monthNum = time.Month;
            int yearNum = time.Year;
            string month = time.ToString("MMMM");
            label2.Text = "Statistics for the month of " + month;

            //total expenses
            SqlCommand command = new SqlCommand("SELECT SUM(amount) from tbl_expenses where username = '"+user+"' and MONTH(date) = "+monthNum+" and YEAR(date) = "+yearNum, con);
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.SelectCommand.CommandType = CommandType.Text;
            sd.Fill(dt);
            string value = dt.Rows[0].ItemArray[0].ToString();
            if (value == "")
            {
                lblTotalExp.Text = "Total Expenses: $0";
            }
            else
            {
                lblTotalExp.Text = "Total Expenses: $" + value;
            }

            //fixed expenses
            SqlCommand command2 = new SqlCommand("SELECT SUM(amount) from tbl_expenses where username = '" + user + "' and expenseType = 'Fixed Expense' and MONTH(date) = " + monthNum + " and YEAR(date) = " + yearNum, con);
            SqlDataAdapter sd2 = new SqlDataAdapter(command2);
            DataTable dt2 = new DataTable();
            sd2.SelectCommand.CommandType = CommandType.Text;
            sd2.Fill(dt2);
            string value2 = dt2.Rows[0].ItemArray[0].ToString();
            if (value2 == "")
            {
                lblFixedExp.Text = "Fixed Expenses: $0";
            }
            else
            {
                lblFixedExp.Text = "Fixed Expenses: $" + value2;
            }

            //irregular expenses
            SqlCommand command3 = new SqlCommand("SELECT SUM(amount) from tbl_expenses where username = '" + user + "' and expenseType = 'Irregular Expense' and MONTH(date) = " + monthNum + " and YEAR(date) = " + yearNum, con);
            SqlDataAdapter sd3 = new SqlDataAdapter(command3);
            DataTable dt3 = new DataTable();
            sd3.SelectCommand.CommandType = CommandType.Text;
            sd3.Fill(dt3);
            string value3 = dt3.Rows[0].ItemArray[0].ToString();
            if (value3 == "")
            {
                lblIrregularExp.Text = "Irregular Expenses: $0";
            }
            else
            {
                lblIrregularExp.Text = "Irregular Expenses: $" + value3;
            }

            //variable costs
            SqlCommand command4 = new SqlCommand("SELECT SUM(amount) from tbl_expenses where username = '" + user + "' and expenseType = 'Variable Cost' and MONTH(date) = " + monthNum + " and YEAR(date) = " + yearNum, con);
            SqlDataAdapter sd4 = new SqlDataAdapter(command4);
            DataTable dt4 = new DataTable();
            sd4.SelectCommand.CommandType = CommandType.Text;
            sd4.Fill(dt4);
            string value4 = dt4.Rows[0].ItemArray[0].ToString();
            if (value4 == "")
            {
                lblVariableCosts.Text = "Variable Costs: $0";
            }
            else
            {
                lblVariableCosts.Text = "Variable Costs: $" + value4;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Hide();
            MessageBox.Show("Successfully Logged Out");
        }
    }
}
