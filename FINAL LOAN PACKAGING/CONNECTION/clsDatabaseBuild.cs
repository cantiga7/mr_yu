using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

using System.Configuration;

class clsDatabaseBuild
{
    //public static void CreateDatabase(string myConn)
    //{
    //    try
    //    {

    //        string _SQLSyntax = "IF NOT EXISTS ( SELECT name FROM sys.databases WHERE name = '" + clsDeclaration.sDatabaseName + "' ) ";
    //        _SQLSyntax = _SQLSyntax + "BEGIN ";
    //        _SQLSyntax = _SQLSyntax + "    CREATE DATABASE [" + clsDeclaration.sDatabaseName + "]; ";
    //        _SQLSyntax = _SQLSyntax + "END ";

    //        clsSQLClientFunctions.GlobalExecuteCommand(myConn, _SQLSyntax);


    //        _SQLSyntax = @"IF NOT EXISTS ( SELECT A.TABLE_NAME FROM INFORMATION_SCHEMA.TABLES A WHERE A.TABLE_NAME = 'OPDN' )
    //                       BEGIN
    //                            CREATE TABLE [dbo].[OPDN](
	   //                             [oID] [int] IDENTITY(1,1) NOT NULL,
	   //                             [DocNum] [int] NULL,
	   //                             [DocDate] [datetime] NULL,
	   //                             [CardCode] [nvarchar](15) NULL,
	   //                             [CardName] [nvarchar](250) NULL,
	   //                             [BPLName] [nvarchar](100) NULL,
	   //                             [PONum] [nvarchar](100) NULL,
	   //                             [ItemCode] [nvarchar](50) NULL,
	   //                             [Dscription] [nvarchar](250) NULL,
	   //                             [Quantity] [numeric](19, 6) NULL,
	   //                             [PriceAfVAT] [numeric](19, 6) NULL,
	   //                             [ChasisNumber] [nvarchar](250) NULL,
	   //                             [SerialNumber] [nvarchar](250) NULL

    //                            ) ON [PRIMARY]
    //                       END";
    //        clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sSystemConnection, _SQLSyntax);

    //    }
    //    catch
    //    {  }
    //}


}
