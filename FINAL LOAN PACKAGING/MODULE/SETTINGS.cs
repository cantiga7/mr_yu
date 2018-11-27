using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
namespace FINAL_LOAN_PACKAGING.MODULE
{
    public partial class SETTINGS : Form
    {
        public SETTINGS()
        {
            InitializeComponent();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clsFunctions.SetSetting("oLicenseServer", txtlicenseserver.Text);

            clsFunctions.SetSetting("oServer", txtServeripaddress.Text);
            clsFunctions.SetSetting("oCompanyDB", txtdbname.Text);
            clsFunctions.SetSetting("oDbUserName", txtdbusername.Text);
            clsFunctions.SetSetting("oDbPassword", txtdbpassword.Text);

            clsFunctions.SetSetting("olocalDB", txtlocal.Text);

            clsFunctions.SetSetting("oUserName",txtdefaultuser.Text);
            clsFunctions.SetSetting("oPassword", txtdefaultpass.Text);

            MessageBox.Show("New Settings Applied!", "Connection Settings", MessageBoxButtons.OK, MessageBoxIcon.None);
            Application.Restart();
        }

        private void SETTINGS_Load(object sender, EventArgs e)
        {
            txtlicenseserver.Text = ConfigurationManager.AppSettings["oLicenseServer"];

            txtServeripaddress.Text = ConfigurationManager.AppSettings["oServer"];
            txtdbname.Text = ConfigurationManager.AppSettings["oCompanyDB"];
            txtdbusername.Text = ConfigurationManager.AppSettings["oDbUserName"];
            txtdbpassword.Text = ConfigurationManager.AppSettings["oDbPassword"];

            txtlocal.Text = ConfigurationManager.AppSettings["olocalDB"];

            txtdefaultuser.Text = ConfigurationManager.AppSettings["oUserName"];
            txtdefaultpass.Text = ConfigurationManager.AppSettings["oPassword"];
        }
    }
}
