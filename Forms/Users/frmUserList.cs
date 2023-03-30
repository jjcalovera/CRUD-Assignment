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
    public partial class frmUserList : Form
    {
        public frmUserList()
        {
            InitializeComponent();
        }

        Functions.User user = new Functions.User();

        private void frmUserList_Load(object sender, EventArgs e)
        {
            user.LoadUsers(gridUsers);
        }
    }
}
