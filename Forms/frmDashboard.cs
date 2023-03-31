using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Assignment.Forms
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            pnlDashboard.Controls.Clear();
            Forms.Users.frmUserList frmUserList = new Forms.Users.frmUserList();
            frmUserList.TopLevel = false;
            pnlDashboard.Controls.Add(frmUserList);
            frmUserList.Dock = DockStyle.Fill;
            frmUserList.Show();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            pnlDashboard.Controls.Clear();
            Forms.Customers.frmCustomerList frmCustomerList = new Forms.Customers.frmCustomerList();
            frmCustomerList.TopLevel = false;
            pnlDashboard.Controls.Add(frmCustomerList);
            frmCustomerList.Dock = DockStyle.Fill;
            frmCustomerList.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to exit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
