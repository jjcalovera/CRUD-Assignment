using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Assignment.Forms.Users
{
    public partial class frmViewUser : Form
    {
        public frmViewUser()
        {
            InitializeComponent();
        }

        Components.Value val = new Components.Value();

        private void frmViewUser_Load(object sender, EventArgs e)
        {
            txtFirstName.Text = val.UserFirstName;
            txtMiddleName.Text = val.UserMiddleName;
            txtLastName.Text = val.UserLastName;
            txtGender.Text = val.UserGender;
            txtBirthday.Text = val.UserBirthday.ToString("d");
            txtContactNumber.Text = val.UserContactNumber;
            txtEmail.Text = val.UserEmail;
            txtUsername.Text = val.UserUsername;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Forms.Users.frmUserList frmUserList = new Forms.Users.frmUserList();
            frmUserList.TopLevel = false;

            Forms.frmDashboard frmDashboard1 = new Forms.frmDashboard();
            frmDashboard1.Show();

            Forms.frmDashboard frmDashboard2 = (Forms.frmDashboard)Application.OpenForms["frmDashboard"];
            Panel pnlDashboard = (Panel)frmDashboard2.Controls["pnlDashboard"];

            pnlDashboard.Controls.Add(frmUserList);
            frmUserList.Dock = DockStyle.Fill;
            frmUserList.Show();
            this.Close();
        }
    }
}
