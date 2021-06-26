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
            SqlCommand command = new SqlCommand("select id as 'ID', name as 'Name', expenseType as 'Expense Type', amount as 'Amount', date as 'Date' from tbl_expenses where username = '"+user+"'", con);
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void frmInsert_Load(object sender, EventArgs e)
        {
            BindData();
            clearDate();
        }

        void clearDate()
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = " ";
            dateTimePicker1.ShowCheckBox = true;
            dateTimePicker1.Checked = false;
        }

        //insert into database
        private void button1_Click(object sender, EventArgs e)
        {
            //check that no text boxes are left empty
            if (txtName.TextLength == 0 || txtAmount.TextLength == 0 || comboBox1.Text.Length == 0 || dateTimePicker1.Text.Length == 1)
            {
                MessageBox.Show("Fill out all required parts of the form");
                txtName.Focus();
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
                    txtName.Text = "";
                    txtAmount.Text = "";
                    comboBox1.SelectedItem = null;
                    clearDate();
                    txtName.Focus();
                }
            }
        }

        private void frmInsert_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //update each column depending on if it has input or not (i.e if name is filled, update name)
            if (!int.TryParse(txtID.Text, out _))
            {
                MessageBox.Show("Enter a number for ID");
                txtID.Text = "";
                txtID.Focus();
            }
            else
            {
                if (txtAmount.Text.Length != 0 && !int.TryParse(txtAmount.Text, out _))
                {
                    MessageBox.Show("Amount section must be a number");
                }
                else
                {
                    bool changed = false;
                    SqlCommand command2 = new SqlCommand("SELECT count(name) from tbl_expenses where id = '" + int.Parse(txtID.Text) + "' and username = '" + user + "'", con);
                    SqlDataAdapter sd = new SqlDataAdapter(command2);
                    DataTable dt = new DataTable();
                    sd.SelectCommand.CommandType = CommandType.Text;
                    sd.Fill(dt);
                    int count = int.Parse(dt.Rows[0].ItemArray[0].ToString());
                    if (count > 0)
                    {
                        if (txtName.Text.Length != 0)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("update tbl_expenses set " + "name = '" + txtName.Text + "' where id = '" + int.Parse(txtID.Text) + "' and username = '" + user + "'", con);
                            command.ExecuteNonQuery();
                            con.Close();
                            changed = true;
                        }
                        if (txtAmount.Text.Length != 0)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("update tbl_expenses set " + "amount = '" + int.Parse(txtAmount.Text) + "' where id = '" + int.Parse(txtID.Text) + "' and username = '" + user + "'", con);
                            command.ExecuteNonQuery();
                            con.Close();
                            changed = true;
                        }
                        if (comboBox1.Text.Length != 0)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("update tbl_expenses set " + "expenseType = '" + comboBox1.Text + "' where id = '" + int.Parse(txtID.Text) + "' and username = '" + user + "'", con);
                            command.ExecuteNonQuery();
                            con.Close();
                            changed = true;
                        }
                        if (dateTimePicker1.Text.Length != 1)
                        {
                            con.Open();
                            SqlCommand command = new SqlCommand("update tbl_expenses set " + "date = '" + DateTime.Parse(dateTimePicker1.Text) + "' where id = '" + int.Parse(txtID.Text) + "' and username = '" + user + "'", con);
                            command.ExecuteNonQuery();
                            con.Close();
                            changed = true;
                        }
                        if (changed)
                            MessageBox.Show("Successfully Updated.");
                        else
                            MessageBox.Show("Nothing was changed");
                        BindData();
                        txtName.Text = "";
                        txtAmount.Text = "";
                        comboBox1.SelectedItem = null;
                        clearDate();
                        txtID.Text = "";
                        txtName.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Enter a valid ID");
                        txtID.Text = "";
                        txtID.Focus();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //TODO delete everything that matches a certain category (i.e delete everything that has a name 'Bank' or delete all Fixed expenses in 2021)
            if (txtID.Text != "" && int.TryParse(txtID.Text, out _))
            {
                SqlCommand command2 = new SqlCommand("SELECT count(name) from tbl_expenses where id = '" + int.Parse(txtID.Text) + "' and username = '"+user+"'", con);
                SqlDataAdapter sd = new SqlDataAdapter(command2);
                DataTable dt = new DataTable();
                sd.SelectCommand.CommandType = CommandType.Text;
                sd.Fill(dt);
                int count = int.Parse(dt.Rows[0].ItemArray[0].ToString());
                if (count > 0)
                {
                    if (MessageBox.Show("Are you sure?", "Delete Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        con.Open();
                        SqlCommand command = new SqlCommand("Delete tbl_expenses where id = '" + int.Parse(txtID.Text) + "' and username = '"+user+"'", con);
                        command.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Successfully Deleted.");
                        BindData();
                        txtName.Text = "";
                        txtAmount.Text = "";
                        comboBox1.SelectedItem = null;
                        clearDate();
                        txtID.Text = "";
                        txtName.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("This ID does not exist");
                    txtID.Text = "";
                    txtID.Focus();
                }
            }
            else
            {
                MessageBox.Show("Enter a number for the ID");
                txtID.Text = "";
                txtID.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new dashboard(user).Show();
            this.Hide();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //clicking cell displays all info in each textbox
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                txtName.Text = row.Cells["Name"].Value.ToString();
                txtAmount.Text = row.Cells["Amount"].Value.ToString();
                txtID.Text = row.Cells["Id"].Value.ToString();
                comboBox1.Text = row.Cells["Expense Type"].Value.ToString();
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "MMMM dd, yyyy";
                dateTimePicker1.Text = row.Cells["Date"].Value.ToString();
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
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
    }
}
