using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Configuration;


namespace FINAL_LOAN_PACKAGING
{
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            string sysDBServer = ConfigurationManager.AppSettings["oServer"];

            string sysDftDBCompany = ConfigurationManager.AppSettings["oCompanyDB"];
            string sysDBUsername = ConfigurationManager.AppSettings["oDbUserName"];
            string sysDBPassword = ConfigurationManager.AppSettings["oDbPassword"];

            string syslclDBName = ConfigurationManager.AppSettings["olocalDB"];
          
            clsDeclaration.sSAPConnection = clsSQLClientFunctions.GlobalConnectionString(sysDBServer, sysDftDBCompany, sysDBUsername, sysDBPassword);
            clsDeclaration.sLclSystemConnection =  clsSQLClientFunctions.LocalConnectionString(sysDBServer, syslclDBName, sysDBUsername, sysDBPassword);

            if (clsSQLClientFunctions.CheckConnection(clsDeclaration.sSAPConnection) == false && clsSQLClientFunctions.CheckConnection(clsDeclaration.sLclSystemConnection) == false)
            {
                MODULE.SETTINGS set = new MODULE.SETTINGS();
                set.ShowDialog();
            }
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.DoEvents();
            Application.Run(new MAIN_FORM()); 
        }
    }
}
