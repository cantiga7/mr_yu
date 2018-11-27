using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace FINAL_LOAN_PACKAGING
{
    public partial class VIEWER : Form
    {
        public VIEWER()
        {
            InitializeComponent();
        }
        public static DataTable _tblledger = new DataTable();
        public void tocomboboxproduct(ComboBox combo)
        {
            string _sqlqry;
            DataTable _table = new DataTable();
            _sqlqry = @"SELECT A.Dscription,A.BaseCard FROM RDR1 A INNER JOIN INV6 B  ON A.DocEntry = B.DocEntry WHERE A.BaseCard =  '"+txtclientid.Text+"' AND A.TrgetEntry ='" + txtdocnum.Text + "' ";
            _table = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlqry);
            SqlDataAdapter da = new SqlDataAdapter(_sqlqry, clsDeclaration.sSAPConnection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            combo.DataSource = ds.Tables[0];
            combo.DisplayMember = "Dscription";
            combo.ValueMember = "BaseCard";
		 
	
        }
        public static AutoCompleteStringCollection LoadAutoComplete(DataTable _table, int num)
        {
            DataTable dt = _table;
            AutoCompleteStringCollection stringcol = new AutoCompleteStringCollection();
            foreach (DataRow row in dt.Rows)
            {
                stringcol.Add(Convert.ToString(row[num]));
            }
            return stringcol;
        }
        void ClientInformation(TextBox clientId, TextBox docnum,  TextBox name, TextBox address, TextBox controlno, Label loandate)
        {
            string _gendata = "";
            _gendata = @" SELECT
                                A.DocNum
								,B.U_FN ,B.U_MN,B.U_SN
                                ,A.DOCDATE
                                ,A.ADDRESS
                                ,A.CardCode
                                FROM OINV A
	                            INNER JOIN OCRD B ON A.CardCode = B.CardCode where  (B.U_FN +' '+B.U_MN+' '+B.U_SN) = '" + txtsearchname.Text + "'  ";
            DataTable _sqltable = new DataTable();
            _sqltable = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _gendata);

            foreach (DataRow row in _sqltable.Rows)
            {
                docnum.Text = row["DocNum"].ToString().ToUpper();
                name.Text = (row["U_FN"].ToString().ToUpper() + ' ' + row["U_MN"].ToString().ToUpper() + ' ' + row["U_SN"].ToString().ToUpper());
                address.Text = row["Address"].ToString().ToUpper();
                clientId.Text = row["CardCode"].ToString().ToUpper();
                loandate.Text = Convert.ToDateTime(row["DocDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
                              
        }
        void fdd()
        {
            string _FDD = "SELECT  A.DueDate   from INV6 A WHERE A.DocEntry = '"+txtdocnum.Text+"' AND A.InstlmntID = '1'";
            DataTable _sqltable = new DataTable();
            _sqltable = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _FDD);
            foreach (DataRow row in _sqltable.Rows)
            {
                txtfirstduedate.Text = Convert.ToDateTime(row["DueDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            }

        }
        public void SearchCostumerLoantype(TextBox loantype, TextBox Trans,
            TextBox loanterm, TextBox intrate,
            TextBox penaltyrate,TextBox maturitydate, 
            TextBox amortization, TextBox graceperiod,
            TextBox baseon , TextBox Prncipalamnt)
        {

            string _gendata = "";
            _gendata = @" 		select A.DocEntry 
	                                    , a.U_Prpse 
	                                    , a.U_INTREST
	                                    , a.U_PrinIstlment
	                                    , a.U_PrincPTerm 
	                                    ,a.U_PNLTY
	                                    ,a.U_GPRIOD 
                                        ,b.Dscription
                                        ,A.DocDueDate
                                        ,A.U_BASE
	                                    ,A.DocTotal
	                                    from oinv A 
	                                    left join inv1 b on a.DocEntry = b.DocEntry where a.DocStatus = 'O' and   b.Dscription ='" + cmbloantype.Text + "'";
            DataTable _sqltable = new DataTable();
            _sqltable = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _gendata);

            foreach (DataRow row in _sqltable.Rows)
            {
                loantype.Text = row["Dscription"].ToString();
                Trans.Text = row["U_Prpse"].ToString();
                loanterm.Text = (row["U_PrinIstlment"].ToString() + ' ' + row["U_PrincPTerm"].ToString());
                intrate.Text = row["U_INTREST"].ToString();
                penaltyrate.Text = row["U_PNLTY"].ToString();
                maturitydate.Text = Convert.ToDateTime(row["DocDueDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                //amortization.Text = row["DocTotal"].ToString();
                graceperiod.Text = row["U_GPRIOD"].ToString();
                baseon.Text = row["U_BASE"].ToString();
                Prncipalamnt.Text =string.Format("{0:N2}",Convert.ToDouble( row["DocTotal"].ToString()));
                fdd();
            }

        }
        void cardcode(TextBox cardcode)
        {
            string _gendata = "";
            _gendata = @"SELECT B.CardCode FROM OCRD B where (B.U_FN +' '+B.U_MN+' '+B.U_SN) = '"+txtsearchname.Text+"'";
            DataTable _sqltable = new DataTable();
            _sqltable = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _gendata);
            foreach (DataRow row in _sqltable.Rows)
            {
                cardcode.Text = row["CardCode"].ToString().ToUpper();
            }
        }
        private void LEDGER_Load(object sender, EventArgs e)
        {
            string _sqlname = @"SELECT (B.U_FN +' '+B.U_MN+' '+B.U_SN) FROM OCRD B ";
            DataTable tablename = new DataTable();
            tablename = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlname);
            
            txtsearchname.AutoCompleteCustomSource = LoadAutoComplete(tablename, 0);
            txtsearchname.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtsearchname.AutoCompleteSource = AutoCompleteSource.CustomSource;
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ClientInformation(txtclientid, txtdocnum, txtname,txtaddress, txtcontrolno, lbloandate);
            cardcode(txtclientid);
            tocomboboxproduct(cmbloantype);


            displayledger(dataGridView1);
          
        }
        private void cmbloantype_TextChanged(object sender, EventArgs e)
        {
            SearchCostumerLoantype( txtloantype, txttransaction, txtloanterm, txtintrate, txtpenaltyrate, txtmaturity, txtamort, txtgraceperiod,txtbaseon, txtprinamount);
        }

        void displayledger(DataGridView dvg)
        {
          

            string _getdata = "";
            _getdata = @" 
                    DECLARE @DocEntry AS NVARCHAR(30)
                    DECLARE @Dscription AS NVARCHAR(30)
                  
                    SET @DocEntry = '" + txtdocnum.Text + @"'
                    SET @Dscription = '" + cmbloantype.Text + @"'
 
                     SELECT A.InstlmntID AS [INSTLMNT #], A.DueDate AS [SCHEDULE] ,0.00 AS [DAYS], A.InsTotal AS [PRINCIPAL AMOUNT] , A.U_Interest AS [INTEREST],0.00 AS [AMORTIZATION] , 0.00 AS [PRINCIPAL BALANCE]
                     from INV6 A 
                     INNER JOIN INV1 B ON A.DocEntry = B.DocEntry where B.docentry = @DocEntry  AND B.Dscription = @Dscription  
                    
                      ";
          

            DataTable tablename = new DataTable();
            tablename = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _getdata);

            clsFunctions.DataGridViewSetup(dvg, tablename);

        }
    
        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            double _Balance2;
            double.TryParse(txtprinamount.Text, out _Balance2);
            //DateTime fi = Convert.ToDateTime(txtfirstduedate.Text);
            //DateTime days;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string c3 = row.Cells[3].Value.ToString();
                string c4 = row.Cells[4].Value.ToString();
                string c2 = row.Cells[1].Value.ToString();
                string c5 = Convert.ToString(Convert.ToDouble(c3) + Convert.ToDouble(c4));
                string _Value = row.Cells[3].Value.ToString();
                _Balance2 = _Balance2 - double.Parse(double.Parse(_Value).ToString("N2"));

                row.Cells[6].Value = double.Parse(_Balance2.ToString("N2"));
                row.Cells[5].Value = c5;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MYCLASS.CLASS_REPORT cc = new MYCLASS.CLASS_REPORT();
            cc.showReport(txtname.Text, txtprinamount.Text, txtloanterm.Text, "",txtdocnum.Text, txtbaseon.Text, txtintrate.Text, txtmaturity.Text, lbloandate.Text, "", txtaddress.Text, txtprinamount.Text, txtintrate.Text, txtfirstduedate.Text, "", "", "", "", "", dataGridView1);
        }
    }
}
