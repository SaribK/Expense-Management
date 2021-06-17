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
            SqlCommand command = new SqlCommand("select id as 'ID', name as 'Name', expenseType as 'Expense Type', amount as 'Amount', date as 'Date' from tbl_expenses where username = '"+user+"'", con);
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
            //check that no text boxes are left empty
            if (txtName.TextLength == 0 || txtAmount.TextLength == 0 || comboBox1.Text.Length == 0)
            {
                MessageBox.Show("Fill out all required parts of the form");
            }
            else
            {
                //check that int required text boxes are only given numbers
                if (!int.TryParse(txtAmount.Text, out _))
                {
                    MessageBox.Show("Amount section must be a number");
                }
                else
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("insert into tbl_expenses values ('" + txtName.Text + "','" + int.Parse(txtAmount.Text) + "','" + comboBox1.Text + "','" + DateTime.Parse(dateTimePicker1.Text) + "','" + user + "')", con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Successfully Inserted.");
                    con.Close();
                    BindData();
                }
            }
        }

        private void frmInsert_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //TODO if the given ID is invalid (e.g user does not own that id or its not a number), clear textbox, focus that textbox, display message
            //TODO update each column depending on if it has input or not (i.e if name is filled, update name). Remove if statement that checks if everything is empty if doing this

            if (txtName.TextLength == 0 || txtAmount.TextLength == 0 || comboBox1.Text.Length == 0 || txtID.Text.Length == 0)
            {
                MessageBox.Show("Fill out all parts of the form");
            }
            else
            {
                con.Open();
                SqlCommand command = new SqlCommand("update tbl_expenses set " + "name = '" + txtName.Text + "', amount ='" + int.Parse(txtAmount.Text) + "', expenseType = '" + comboBox1.Text + "', date = '" + DateTime.Parse(dateTimePicker1.Text) + "' where id = '" + int.Parse(txtID.Text) + "' and username = '"+ user +"'", con);
                command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Updated.");
                BindData();
            }
        }
    }
}
