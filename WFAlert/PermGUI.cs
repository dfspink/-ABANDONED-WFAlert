using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFAlert
{
    public partial class PermGUI : Form
    {
        public PermGUI()
        {
            InitializeComponent();
        }

        private void bt_newcred_Click(object sender, EventArgs e)
        {
            WFAlert.TM.SetNeedCode(1);
            Close();
        }

        private void bt_prevcred_Click(object sender, EventArgs e)
        {
            WFAlert.TM.SetNeedCode(0);
            Close();
        }
    }
}
