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
    public partial class frmAddUser : Form
    {
        public frmAddUser()
        {
            InitializeComponent();
        }

        Functions.Check check = new Functions.Check();
        Functions.Gender gender = new Functions.Gender();
        Functions.User user = new Functions.User();

        private void ShowToastMessage(String header, String message, Color bgColor)
        {
            Forms.toastmessage.frmToastMessage frmToastMessage = new Forms.toastmessage.frmToastMessage(header, message, bgColor);
            frmToastMessage.Show();
        }

        private void RemovePhoto()
        {
            imgLocation = string.Empty;
            picProfileUser.ImageLocation = imgLocation;
        }

        private void frmAddUser_Load(object sender, EventArgs e)
        {
            gender.LoadGendersInCmbGender(cmbGender);
        }

        string imgLocation = string.Empty;
        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "C:\\Users\\joven joshua alovera\\Pictures";
            dialog.Filter = "PNG Files|*.png|JPG Files|*.jpg|All Files|*.*";
            
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName;
                picProfileUser.ImageLocation = imgLocation;
            }

            txtFirstName.Focus();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemovePhoto();
            txtFirstName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                ShowToastMessage("Required!", "First name field is required!", Color.Red);
                txtFirstName.Focus();
            }
            else if(String.IsNullOrWhiteSpace(txtLastName.Text))
            {
                ShowToastMessage("Required!", "Last name field is required!", Color.Red);
                txtLastName.Focus();
            }
            else if(String.IsNullOrWhiteSpace(cmbGender.Text))
            {
                ShowToastMessage("Required!", "Gender field is required!", Color.Red);
                cmbGender.Focus();
            }
            else if(String.IsNullOrWhiteSpace(txtContactNumber.Text) && String.IsNullOrWhiteSpace(txtEmail.Text))
            {
                ShowToastMessage("Required!", "Please provide contact information\neither contact number or email!", Color.Red);
                txtContactNumber.Focus();
            }
            else if(String.IsNullOrWhiteSpace(txtUsername.Text))
            {
                ShowToastMessage("Required!", "Username field is required!", Color.Red);
                txtUsername.Focus();
            }
            else if(String.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ShowToastMessage("Required!", "Password field is required!", Color.Red);
                txtPassword.Focus();
            }
            else if(String.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                ShowToastMessage("Required!", "Confirm password field is required!", Color.Red);
                txtConfirmPassword.Focus();
            }
            else if(check.CheckUsernameIfExists(txtUsername.Text))
            {
                ShowToastMessage("Already Taken!", "Username is already taken!", Color.Red);

                txtUsername.ResetText();
                txtUsername.Focus();

            }
            else if(txtPassword.Text != txtConfirmPassword.Text)
            {
                ShowToastMessage("Error!", "Password do not match!", Color.Red);

                txtPassword.ResetText();
                txtConfirmPassword.ResetText();

                txtPassword.Focus();
            }
            else
            {
                byte[] profilePicture = null;
                if (!String.IsNullOrWhiteSpace(imgLocation))
                {
                    FileStream fs = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    profilePicture = br.ReadBytes((int)fs.Length);
                }

                int age = DateTime.Today.Year - dateBirthday.Value.Year;
                if (dateBirthday.Value.Date > DateTime.Today.AddYears(-age)) age--;

                if (user.AddUser(profilePicture, txtFirstName.Text, txtMiddleName.Text, txtLastName.Text, cmbGender.Text, age, dateBirthday.Value.Date,
                    txtContactNumber.Text, txtEmail.Text, txtUsername.Text, txtPassword.Text))
                {
                    ShowToastMessage("Success!", "User successfully created!", Color.LimeGreen);
                    RemovePhoto();

                    txtFirstName.ResetText();
                    txtMiddleName.ResetText();
                    txtLastName.ResetText();
                    txtContactNumber.ResetText();
                    txtEmail.ResetText();
                    txtUsername.ResetText();
                    txtPassword.ResetText();
                    txtConfirmPassword.ResetText();

                    cmbGender.Text = null;
                    dateBirthday.Value = DateTime.Today;

                    txtFirstName.Focus();
                }
                else
                {
                    ShowToastMessage("Failed!", "Failed to create user!", Color.Red);
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
