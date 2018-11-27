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
    public partial class SYSTEM_LOGIN : Form
    {
        public SYSTEM_LOGIN()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //clsDeclaration.sDatabaseName = cmbCompany.Text;
            //clsDeclaration.sSAPUsername = txtSAPUsername.Text;
            //clsDeclaration.sSAPPassword = txtSAPPassword.Text;

            //string sysDftDBCompany = cmbCompany.Text;
            //string sysSAPUsername = txtSAPUsername.Text;
            //string sysSAPPassword = txtSAPPassword.Text;

            //if (clsSAPFunctions.SAPDIConnection(sysDftDBCompany, sysSAPUsername, sysSAPPassword) == false)
            //{
            //    SETTINGS set = new SETTINGS();
            //    set.ShowDialog();
            //    return;
            //}
            //else
            //{
            //    //clsSAPFunctions.oCompany.Disconnect();
            //}

            Close();
        }

        private void SYSTEM_LOGIN_Load(object sender, EventArgs e)
        {
            //string sysDBServer = ConfigurationManager.AppSettings["sysDBServer"];
            //string sysDftDBCompany = ConfigurationManager.AppSettings["sysDftDBCompany"];
            //string sysDBUsername = ConfigurationManager.AppSettings["sysDBUsername"];
            //string sysDBPassword = ConfigurationManager.AppSettings["sysDBPassword"];

            //clsDeclaration.sSAPConnection = clsSQLClientFunctions.GlobalConnectionString(sysDBServer, sysDftDBCompany, sysDBUsername, sysDBPassword);

            //if (clsSQLClientFunctions.CheckConnection(clsDeclaration.sSAPConnection) == false)
            //{
            //    SETTINGS set = new SETTINGS();
            //    set.ShowDialog();
            //}
            //cmbCompany.Text = sysDftDBCompany;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
    }
}
