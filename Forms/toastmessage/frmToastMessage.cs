using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_Assignment.Forms.toastmessage
{
    public partial class frmToastMessage : Form
    {
        public frmToastMessage(String header, String message, Color bgColor)
        {
            InitializeComponent();

            this.BackColor = bgColor;
            lblHeader.Text = header;
            lblMessage.Text = message;
        }

        private void frmToastMessage_Load(object sender, EventArgs e)
        {
            Top = 20;
            Left = Screen.PrimaryScreen.Bounds.Width - Width - 20;

            timerToastMessageClose.Start();
        }

        private void timerToastMessageClose_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
