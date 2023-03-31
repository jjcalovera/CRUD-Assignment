using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Assignment.Forms.Customers
{
    public partial class frmViewCustomer : Form
    {
        public frmViewCustomer()
        {
            InitializeComponent();
        }

        Components.Value val = new Components.Value();

        private void frmViewCustomer_Load(object sender, EventArgs e)
        {
            txtFirstName.Text = val.CustomerFirstName;
            txtMiddleName.Text = val.CustomerMiddleName;
            txtLastName.Text = val.CustomerLastName;
            txtGender.Text = val.CustomerGender;
            txtBirthday.Text = val.CustomerBirthday.Date.ToString();
            txtContactNumber.Text = val.CustomerContactNumber;
            txtEmail.Text = val.CustomerEmail;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Forms.Customers.frmCustomerList frmCustomerList = new Forms.Customers.frmCustomerList();
            frmCustomerList.TopLevel = false;

            Forms.frmDashboard frmDashboard1 = new Forms.frmDashboard();
            frmDashboard1.Show();

            Forms.frmDashboard frmDashboard2 = (Forms.frmDashboard)Application.OpenForms["frmDashboard"];
            Panel pnlDashboard = (Panel)frmDashboard2.Controls["pnlDashboard"];

            pnlDashboard.Controls.Add(frmCustomerList);
            frmCustomerList.Dock = DockStyle.Fill;
            frmCustomerList.Show();
            this.Close();
        }
    }
}
