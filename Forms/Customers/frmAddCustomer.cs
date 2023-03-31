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
    public partial class frmAddCustomer : Form
    {
        public frmAddCustomer()
        {
            InitializeComponent();
        }

        Functions.Customer customer = new Functions.Customer();
        Functions.Gender gender = new Functions.Gender();

        private void frmAddCustomer_Load(object sender, EventArgs e)
        {
            gender.LoadGendersInCmbGender(cmbGender);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("First name is required!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstName.Focus();
            }
            else if(String.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Last name is required!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLastName.Focus();
            }
            else if(String.IsNullOrWhiteSpace(cmbGender.Text))
            {
                MessageBox.Show("Gender is required!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbGender.Focus();
            }
            else if(String.IsNullOrWhiteSpace(txtContactNumber.Text) && String.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please provide contact information either contact number or email!", "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtContactNumber.Focus();
            }
            else
            {
                int age = DateTime.Today.Year - dateBirthday.Value.Year;
                if (dateBirthday.Value.Date > DateTime.Today.AddYears(-age)) age--;

                if(customer.AddCustomer(txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, cmbGender.Text, age,
                    dateBirthday.Value.Date, txtContactNumber.Text, txtEmail.Text))
                {
                    MessageBox.Show("Customer successfully added!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtFirstName.ResetText();
                    txtMiddleName.ResetText();
                    txtLastName.ResetText();
                    txtContactNumber.ResetText();
                    txtEmail.ResetText();

                    cmbGender.Text = null;
                    dateBirthday.Value = DateTime.Today;

                    txtFirstName.Focus();
                }
                else
                {
                    MessageBox.Show("Failed to add customer!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
