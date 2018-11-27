using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace FINAL_LOAN_PACKAGING
{
    public partial class MAIN_FORM : Form
    {
        public MAIN_FORM()
        {
            InitializeComponent();


            clsDeclaration.sSAPServerName = ConfigurationManager.AppSettings["oServer"];
            clsDeclaration.sSAPDatabaseName = ConfigurationManager.AppSettings["oCompanyDB"];
            clsDeclaration.sSAPUsername = ConfigurationManager.AppSettings["oDbUserName"];
            clsDeclaration.sSAPPassword = ConfigurationManager.AppSettings["oDbPassword"];

            clsDeclaration.sServerName = ConfigurationManager.AppSettings["oServer"];
            clsDeclaration.sDatabaseName = ConfigurationManager.AppSettings["olocalDB"];
            clsDeclaration.sUsername = ConfigurationManager.AppSettings["oDbUserName"];
            clsDeclaration.sPassword = ConfigurationManager.AppSettings["oDbPassword"];

        }
        private void MAIN_FORM_Load(object sender, EventArgs e)
        {
            clsSAPFunctions.SAPConnection();

            clsDeclaration.sSystemConnection = clsSQLClientFunctions.GlobalConnectionString(
           clsDeclaration.sSAPServerName,
           clsDeclaration.sSAPDatabaseName,
           clsDeclaration.sSAPUsername,
           clsDeclaration.sSAPPassword);


            clsDeclaration.sLclSystemConnection = clsSQLClientFunctions.LocalConnectionString(
           clsDeclaration.sServerName,
           clsDeclaration.sDatabaseName,
           clsDeclaration.sUsername,
           clsDeclaration.sPassword);

            if (clsSQLClientFunctions.CheckConnection(clsDeclaration.sLclSystemConnection) == false && clsSQLClientFunctions.CheckConnection(clsDeclaration.sSystemConnection) == false)
            {
                lOANMASTERToolStripMenuItem_Click(sender, e);
            }
        }
       
        private void sETTINGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(MODULE.SETTINGS))
                {
                    form.Activate();
                    return;
                }
            }

            LOAN_MASTER LOANMASTER = new LOAN_MASTER();
            LOANMASTER.MdiParent = this;
            LOANMASTER.Show();
        }

    
        private void lOANMASTERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MODULE.SETTINGS SS = new MODULE.SETTINGS();
            SS.ShowDialog();
        }

        private void lEDGERToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(MODULE.SETTINGS))
                {
                    form.Activate();
                    return;
                }
            }

            VIEWER ledger = new VIEWER();
            ledger.MdiParent = this;
            ledger.Show();

            //MessageBox.Show("NOT WORKING");
            //ledger.Close();
        }

        private void lEDGERToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(MODULE.SETTINGS))
                {
                    form.Activate();
                    return;
                }
            }

            SUB_LEDGER SL = new SUB_LEDGER();
            SL.MdiParent = this;
            SL.Show();

            //MessageBox.Show("NOT WORKING");
            //SL.Close();
        }
    }
}
