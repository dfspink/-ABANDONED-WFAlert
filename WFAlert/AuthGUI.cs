using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace WFAlert
{
    public partial class AuthGUI : Form
    {
        private string code = "";

        public AuthGUI()
        {
            InitializeComponent();
        }

        private void bt_code_Click(object sender, EventArgs e)
        {
            code = rtb_code.Text;
            WFAlert.TM.SetCode(code);
            Close();
        }
    }
}