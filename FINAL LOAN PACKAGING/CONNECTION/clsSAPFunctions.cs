using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.Data;

using System.Configuration;

using System.Data.SqlClient;
class clsSAPFunctions
{
    public static SAPbobsCOM.Company oCompany;

    public static bool SAPConnection()
    {
        int lRetCode;

        oCompany = new SAPbobsCOM.Company();

        oCompany.LicenseServer = ConfigurationManager.AppSettings["oLicenseServer"];
        oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2012;
        oCompany.Server = ConfigurationManager.AppSettings["oServer"];
        oCompany.CompanyDB = ConfigurationManager.AppSettings["oCompanyDB"];
        oCompany.DbUserName = ConfigurationManager.AppSettings["oDbUserName"];
        oCompany.DbPassword = ConfigurationManager.AppSettings["oDbPassword"];
        oCompany.UserName = ConfigurationManager.AppSettings["oUserName"];
        oCompany.Password = ConfigurationManager.AppSettings["oPassword"];
        oCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;


        lRetCode = oCompany.Connect();

        return DIErrorHandler(lRetCode, "Connecting To SAP", "SAP Connection");
    }


    //public static bool MarketingDoc(string Docnum, string DocEntry, string CardCode,
    //    string Duedate, string loandate,
    //    string Itemname, string UnitPrice,
    //    string comaker1, string comaker2,
    //    string comaker3, string comaker4,
    //    string interest, string baseon,
    //    string penalty, string gperiod,
    //    string intduedate, string prinduedate,
    //    string purpose, string duedateopt,
    //     out string _Error)
    //{

    //    #region Declaration
    //    int lRetCode;
    //    string sErrMsg;
    //    int lErrCode;


    //    SAPbobsCOM.Documents Invoice;
    //    Invoice = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices); // ??? Waht Document will you use



    //    #endregion

    //    #region Body

    //    #region Header

    //    Invoice.CardCode = CardCode;
    //    //Invoice.DocDueDate = DateTime.Parse(Duedate);
    //    Invoice.DocDate = DateTime.Parse(loandate);


    //    Invoice.UserFields.Fields.Item("U_COMAKER1").Value = comaker1;
    //    Invoice.UserFields.Fields.Item("U_CONAKER2").Value = comaker2;
    //    Invoice.UserFields.Fields.Item("U_COMAKER3").Value = comaker3;
    //    Invoice.UserFields.Fields.Item("U_COMAKER4").Value = comaker4;
    //    Invoice.UserFields.Fields.Item("U_INTREST").Value = interest;
    //    Invoice.UserFields.Fields.Item("U_BASE").Value = baseon;
    //    Invoice.UserFields.Fields.Item("U_INTDDATE").Value = intduedate;
    //    Invoice.UserFields.Fields.Item("U_PRINDUEDATE").Value = prinduedate;
    //    Invoice.UserFields.Fields.Item("U_Prpse").Value = purpose;
    //    Invoice.UserFields.Fields.Item("U_DUEOPT").Value = duedateopt;
    //    Invoice.UserFields.Fields.Item("U_PNLTY").Value = penalty;
    //    Invoice.UserFields.Fields.Item("U_GPRIOD").Value = gperiod;
    //    //Invoice.UserFields.Fields.Item("U_PRINC").Value =  UnitPrice;

    //    #endregion

    //    #region Items

    //    string _getcode;
    //    DataTable _table;

    //    _getcode = @"SELECT A.DocEntry, A.LineNum, A.ObjType FROM RDR1 A WHERE A.DocEntry = '" + DocEntry + "'";
    //    _table = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _getcode);

    //    int i = 0;
    //    foreach (DataRow row in _table.Rows)
    //    {
    //        if (i != 0)
    //            Invoice.Lines.Add();

    //        //itemcode = row["ITEMCODE"].ToString();
    //        Invoice.Lines.BaseEntry = (int)row["DocEntry"];
    //        Invoice.Lines.BaseType = (int)row["ObjType"];
    //        Invoice.Lines.BaseLine = (int)row["LineNum"];


    //        Invoice.Lines.Price = double.Parse(UnitPrice);
    //        //Invoice.Lines.ItemCode = itemcode;
    //        i++;
    //    }
 


    //    #endregion
  
    //    #region Installment

    //    Invoice.Installments.DueDate = DateTime.Now;
    //    Invoice.Installments.Total = 0.00;
    //    Invoice.Installments.Add();


    //    #endregion

    //    #endregion

    //    #region Execution

    //    Application.DoEvents();
    //    lRetCode = Invoice.Add();
    //    if (lRetCode != 0)
    //    {
    //        oCompany.GetLastError(out lErrCode, out sErrMsg);
    //        _Error = lErrCode + " " + sErrMsg;
    //        return false;
    //    }
    //    else
    //    {

    //        _Error = " Items Successfully Added";
    //        MessageBox.Show(_Error);
    //        return true;
    //    }

    //    #endregion

    //}
    public static bool ARRecievableInvoice(DataGridView _DataGrid,string Docnum, string DocEntry, string CardCode, 
        string Duedate, string loandate,
        string Itemname, string UnitPrice,
        string comaker1 , string comaker2,
        string comaker3, string comaker4,
        string interest , string baseon , 
        string penalty , string gperiod , 
        string intduedate , string prinduedate,
        string purpose ,string duedateopt,
        string installment , string printerm,
        string loantype,
         out string _Error)
    {
        int lRetCode;
        string sErrMsg;
        int lErrCode;

        SAPbobsCOM.Documents Invoice;
        Invoice = (SAPbobsCOM.Documents)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInvoices);


        Invoice.CardCode = CardCode;
        Invoice.DocDueDate = DateTime.Parse(Duedate);
        Invoice.DocDate = DateTime.Parse(loandate);

        Invoice.UserFields.Fields.Item("U_COMAKER1").Value = comaker1;
        Invoice.UserFields.Fields.Item("U_CONAKER2").Value = comaker2;
        Invoice.UserFields.Fields.Item("U_COMAKER3").Value = comaker3;
        Invoice.UserFields.Fields.Item("U_COMAKER4").Value = comaker4;
        Invoice.UserFields.Fields.Item("U_INTREST").Value = interest;
        Invoice.UserFields.Fields.Item("U_BASE").Value = baseon;
        Invoice.UserFields.Fields.Item("U_INTDDATE").Value = intduedate;
        Invoice.UserFields.Fields.Item("U_PRINDUEDATE").Value = prinduedate;
        Invoice.UserFields.Fields.Item("U_Prpse").Value = purpose;
        Invoice.UserFields.Fields.Item("U_DUEOPT").Value = duedateopt;
        Invoice.UserFields.Fields.Item("U_PNLTY").Value = penalty;
        Invoice.UserFields.Fields.Item("U_GPRIOD").Value = gperiod;
        Invoice.UserFields.Fields.Item("U_PrinIstlment").Value = installment;
        Invoice.UserFields.Fields.Item("U_PrincPTerm").Value = printerm;

      
 
        string _getcode;
        DataTable _table;

        _getcode = @"SELECT A.DocEntry, A.LineNum, A.ObjType FROM RDR1 A WHERE A.DocEntry = '" + DocEntry + "'";
        _table = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _getcode);

        int i = 0;
        foreach (DataRow row in _table.Rows)
        {
            if (i != 0)
                Invoice.Lines.Add();
              
            Invoice.Lines.BaseEntry = (int)row["DocEntry"];
            Invoice.Lines.BaseType = (int)SAPbobsCOM.BoObjectTypes.oOrders;
            Invoice.Lines.BaseLine = (int)row["LineNum"];

            Invoice.Lines.Price = double.Parse(UnitPrice);
            i++;
        }

        string _getAcct = "";
        DataTable _Acttname;
        _getAcct = @"SELECT A.AcctCode FROM OACT A WHERE A.AcctName  = '" + loantype + "'";
        _Acttname = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _getAcct);
        int ii = 0;
        foreach (DataRow row in _Acttname.Rows)
        {
            if (ii != 0)
                Invoice.Lines.Add();
            Invoice.ControlAccount = row["AcctCode"].ToString();
        }

        i = 0;
        foreach (DataGridViewRow row in _DataGrid.Rows)
        {
            if (i != 0)
                Invoice.Installments.Add();

            string _DueDate = row.Cells[1].Value.ToString();
            string _Total = row.Cells[3].Value.ToString();
            string _Interest = row.Cells[4].Value.ToString();

            Invoice.Installments.DueDate = DateTime.Parse(_DueDate);
            Invoice.Installments.Total = double.Parse(double.Parse(_Total).ToString("N2"));
            Invoice.Installments.UserFields.Fields.Item("U_Interest").Value = double.Parse(double.Parse(_Interest).ToString("N2"));
            Invoice.Installments.UserFields.Fields.Item("U_PenDate").Value = DateTime.Parse(_DueDate).AddDays(int.Parse(gperiod) + 1);
            Invoice.Installments.UserFields.Fields.Item("U_PenRate").Value = penalty;
            i++;
        }


        Application.DoEvents();
        lRetCode = Invoice.Add();
        if (lRetCode != 0)
        {
            oCompany.GetLastError(out lErrCode, out sErrMsg);
            _Error = lErrCode + " " + sErrMsg;
            MessageBox.Show(_Error);
            return false;
        }
        else
        {
          
            _Error = " Items Successfully Added";
            MessageBox.Show(_Error);
            return true;
        }
    }
  
    public static bool DIErrorHandler(int lRetCode, string Action, string MsgTitle)
    {
        string sErrMsg;
        int lErrCode;

        if (lRetCode != 0)
        {
            oCompany.GetLastError(out lErrCode, out sErrMsg);
            MessageBox.Show(lErrCode + " " + sErrMsg);
            
            MessageBox.Show(Action + " Failed. Error Code: " + lErrCode + " Error Msg: " + sErrMsg, MsgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        else
        {
            //MessageBox.Show("Successfully Added");
            return true;
        }


    }

    

    public static bool JournalEntryPettyCash(string _PVCNum,
        string _PetyNo,
        string _Custodian,
        string _BranchCode, DataTable _DataTable, out string _Error)
    {
        int lRetCode;
        string sErrMsg;
        int lErrCode;


        DataTable _dataSelect;
        string _sqlSelect;

        _sqlSelect = @"SELECT A.BPLId, A.DflWhs FROM OBPL A WHERE A.BPLName = '" + _BranchCode + "'";
        _dataSelect = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);
        int _BPLId = int.Parse(clsSQLClientFunctions.GetData(_dataSelect, "BPLId", "0"));
        string _DflWhs = clsSQLClientFunctions.GetData(_dataSelect, "DflWhs", "0");

        double parsedValue;
        SAPbobsCOM.JournalEntries _JournalEntries;
        _JournalEntries = (SAPbobsCOM.JournalEntries)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oJournalEntries);



        _sqlSelect = @"SELECT A.Series FROM NNM1 A WHERE A.ObjectCode = '30' AND A.SeriesName = '" + _DflWhs + @"'";
        _dataSelect = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlSelect);
        string _Series = clsSQLClientFunctions.GetData(_dataSelect, "Series", "0");


        string _PRJCODE = "";
        string _Department = ""; 

        foreach (DataRow row in _DataTable.Rows)
        {
            string _RowCount = row[0].ToString();
            if (double.TryParse(_RowCount, out parsedValue) == true)
            {
                if (double.Parse(_RowCount) > 0)
                {
                    //string _CardName = row["F3"].ToString();
                    //string _Date = row["F2"].ToString();
                    //string _NumAtCard = row["F7"].ToString();
                    //string _ORNo = row["F8"].ToString();
                    //string _SINo = row["F9"].ToString();
                    _PRJCODE = row["F6"].ToString();
                    _Department = row[4].ToString();

                    _JournalEntries.ReferenceDate = DateTime.Parse(DateTime.Today.ToShortDateString());
                    _JournalEntries.TaxDate = DateTime.Parse(DateTime.Today.ToShortDateString());
                    _JournalEntries.DueDate = DateTime.Parse(DateTime.Today.ToShortDateString());
                    _JournalEntries.Memo = "Petty Cash Number " + _PetyNo + " ; Custodian : " + _Custodian;
                    _JournalEntries.ProjectCode = _Department;
                    _JournalEntries.UserFields.Fields.Item("U_PRJCODE").Value = _PRJCODE;
                    _JournalEntries.Series = int.Parse(_Series);

                    break;
                }
            }
        }


        //_JournalEntries.Lines.ShortName = "VPET" + _BranchCode;
        //_JournalEntries.Lines.Debit = 100;
        //_JournalEntries.Lines.Credit = 0;
        ////_JournalEntries.Lines.LineMemo = _Particulars.Trim();
        //_JournalEntries.Lines.ProjectCode = "ADM";
        //_JournalEntries.Lines.UserFields.Fields.Item("U_PRJCODE").Value = "-- NO PROJECT --";
        //_JournalEntries.Lines.BPLID = _BPLId;
        //_JournalEntries.Lines.Add();

        //_JournalEntries.Lines.ShortName = "VPET" + _BranchCode;
        //_JournalEntries.Lines.Debit = 0;
        //_JournalEntries.Lines.Credit = 100;
        ////_JournalEntries.Lines.BPLID = _BPLId;
        //_JournalEntries.Lines.ProjectCode = "ADM";
        //_JournalEntries.Lines.UserFields.Fields.Item("U_PRJCODE").Value = "-- NO PROJECT --";
        //_JournalEntries.Lines.BPLID = _BPLId;

        //_JournalEntries.Lines.Add();



        string _TIN;
        string _Address;
        string _Vendor;


        string _PCVNo;
        string _Dept;
        string _Particulars;
        string _CashAmount;
        string _ORNum;
        string _SINum;
        string _Project;
        string _Requestor;

        string _HeaderName;
        string _DueToCode;
        string _Amount;
        string _ItemCode;

        double _TotalAmount = 0;

        int _count = 0;

        int columns = 0;
        columns = _DataTable.Columns.Count;



        foreach (DataRow row in _DataTable.Rows)
        {


            string _RowCount = row[0].ToString();
            if (double.TryParse(_RowCount, out parsedValue) == true)
            {
                if (double.Parse(_RowCount) > 0)
                {
                    _PCVNo = row["F7"].ToString();
                    _Requestor = row["F3"].ToString();
                    _Dept = row[4].ToString();
                    _Particulars = row[3].ToString();
                    _CashAmount = row[12].ToString();
                    _ORNum = row[7].ToString();
                    _SINum = row[8].ToString();
                    _Project = row[5].ToString();


                    _Vendor = row[9].ToString();
                    _TIN = row[10].ToString();
                    _Address = row[11].ToString();




                    int x = 14;
                    do
                    {
                        _HeaderName = _DataTable.Rows[5][x].ToString();
                        _DueToCode =  _DataTable.Rows[8][x].ToString();
  
                        _Amount = row[x].ToString() == "" ? "0" : row[x].ToString();

                        if (double.TryParse(_Amount, out parsedValue) == true)
                        {
                            if (double.Parse(_Amount) != 0)
                            {
                                _ItemCode = _HeaderName;

                                string _sqlAcct;
                                DataTable _tblAcct = new DataTable();

                                _sqlAcct = @"SELECT A.ExpensesAc FROM OITW A WHERE A.ITEMCODE = '" + _ItemCode + @"' AND A.WhsCode = '" + _DflWhs + @"'";
                                _tblAcct = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlAcct);
                                string _AcctCode =  clsSQLClientFunctions.GetData(_tblAcct, "ExpensesAc", "0");

                                string _CardCode = "";
                                if (_DueToCode != "")
                                {
                                    //_sqlAcct = @"SELECT A.CardCode FROM OCRD A WHERE A.CardFName = '" + _DueToCode + @"'";
                                    _sqlAcct = @"SELECT A.CardCode FROM OCRD A WHERE A.CardFName = '" + _DueToCode + @"'";
                                    _tblAcct = clsSQLClientFunctions.DataList(clsDeclaration.sSystemConnection, _sqlAcct);
                                    _CardCode = clsSQLClientFunctions.GetData(_tblAcct, "CardCode", "0");

                                    _Particulars = "Due To / Due From " + _DueToCode;
                                }


                                if(_CardCode != "")
                                {
                                    _JournalEntries.Lines.ShortName = _CardCode;
                                }
                                else
                                {
                                    _JournalEntries.Lines.AccountCode = _AcctCode;
                                }

                                //MessageBox.Show(_AcctCode);
                                _JournalEntries.Lines.Debit = double.Parse(double.Parse(_Amount).ToString("N2"));
                                _JournalEntries.Lines.Credit = 0;
                                _JournalEntries.Lines.LineMemo = _Particulars.Trim();
                                _JournalEntries.Lines.ProjectCode = _Dept;
                                _JournalEntries.Lines.UserFields.Fields.Item("U_PRJCODE").Value = _Project.Trim();
                                _JournalEntries.Lines.UserFields.Fields.Item("U_EMPLOY").Value = _Requestor.Trim();
                                _JournalEntries.Lines.Reference1 = _PCVNo;
                                _JournalEntries.Lines.BPLID = _BPLId;
                                _JournalEntries.Lines.Add();

                                _TotalAmount = _TotalAmount + double.Parse(double.Parse(_Amount).ToString("N2"));
                                _count++;
                            }
                        }

                        x++;
                    } while (x <= (columns - 1));
                }
            }



        }



        _JournalEntries.Lines.ShortName = "VPET" + _DflWhs;
        _JournalEntries.Lines.Debit = 0;
        //MessageBox.Show(_TotalAmount.ToString("N2"));
        _JournalEntries.Lines.Credit = double.Parse(_TotalAmount.ToString("N2"));
        _JournalEntries.Lines.BPLID = _BPLId;
        _JournalEntries.Lines.ProjectCode = _Department;
        _JournalEntries.Lines.UserFields.Fields.Item("U_PRJCODE").Value = _PRJCODE;

        _JournalEntries.Lines.Add();



        Application.DoEvents();
        lRetCode = _JournalEntries.Add();

        if (lRetCode != 0)
        {
            oCompany.GetLastError(out lErrCode, out sErrMsg);
            _Error = lErrCode + " " + sErrMsg;
            return false;
        }
        else
        {
            _Error = "Put Away Items Successfully Added";
            return true;
        }
    }

}