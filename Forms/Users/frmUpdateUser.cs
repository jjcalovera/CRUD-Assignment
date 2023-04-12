using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CRUD_Assignment.Forms.Users
{
    public partial class frmUpdateUser : Form
    {
        public frmUpdateUser()
        {
            InitializeComponent();
        }

        Components.Value val = new Components.Value();
        Functions.Check check = new Functions.Check();
        Functions.Gender gender = new Functions.Gender();
        Functions.User user = new Functions.User();

        private void frmUpdateUser_Load(object sender, EventArgs e)
        {
            gender.LoadGendersInCmbGender(cmbGender);

            if (val.UserProfilePicture != null)
            {
                MemoryStream ms = new MemoryStream(val.UserProfilePicture);
                picProfileUser.Image = Image.FromStream(ms);
            }

            txtFirstName.Text = val.UserFirstName;
            txtMiddleName.Text = val.UserMiddleName;
            txtLastName.Text = val.UserLastName;
            cmbGender.Text = val.UserGender;
            dateBirthday.Value = val.UserBirthday.Date;
            txtContactNumber.Text = val.UserContactNumber;
            txtEmail.Text = val.UserEmail;
            txtUsername.Text = val.UserUsername;
        }

        string imgLocation = string.Empty;
        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "C:\\Users\\joven joshua alovera\\Pictures";
            dialog.Filter = "PNG Files|*.png|JPG Files|*.jpg|All Files|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName;
                picProfileUser.ImageLocation = imgLocation;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            imgLocation = string.Empty;
            picProfileUser.ImageLocation = imgLocation;

            val.UserProfilePicture = null;
            picProfileUser.Image = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("First name is required!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstName.Focus();
            }
            else if (String.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Last name is required!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLastName.Focus();
            }
            else if (String.IsNullOrWhiteSpace(cmbGender.Text))
            {
                MessageBox.Show("Gender is required!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbGender.Focus();
            }
            else if (String.IsNullOrWhiteSpace(txtContactNumber.Text) && String.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please provide contact information either contact number or email!", "", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtContactNumber.Focus();

            }
            else if (String.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username is required!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus();
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(imgLocation))
                {
                    FileStream fs = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    val.UserProfilePicture = br.ReadBytes((int)fs.Length);
                }

                int age = DateTime.Today.Year - dateBirthday.Value.Year;
                if (dateBirthday.Value.Date > DateTime.Today.AddYears(-age)) age--;

                if (user.UpdateUser(val.UserId, val.UserProfilePicture, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, cmbGender.Text, age, dateBirthday.Value.Date,
                    txtContactNumber.Text, txtEmail.Text, txtUsername.Text))
                {
                    MessageBox.Show("User successfully updated!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to update user!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
