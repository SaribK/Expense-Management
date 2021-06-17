using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    }
}
