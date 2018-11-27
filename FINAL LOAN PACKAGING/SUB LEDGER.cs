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
    public partial class SUB_LEDGER : Form
    {
        public SUB_LEDGER()
        {
            InitializeComponent();
        }
        public void tocomboboxproduct(ComboBox combo)
        {
            string _sqlqry;
            DataTable _table = new DataTable();
            _sqlqry = @"SELECT A.Dscription,A.BaseCard FROM RDR1 A INNER JOIN INV6 B  ON A.DocEntry = B.DocEntry WHERE A.BaseCard =  '" + txtclientid.Text + "' AND A.TrgetEntry ='" + txtdocnum.Text + "' ";
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
        void ClientInformation(TextBox clientId, TextBox docnum, TextBox name, TextBox address, TextBox controlno, Label loandate)
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
            string _FDD = "SELECT  A.DueDate   from INV6 A WHERE A.DocEntry = '" + txtdocnum.Text + "' AND A.InstlmntID = '1'";
            DataTable _sqltable = new DataTable();
            _sqltable = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _FDD);
            foreach (DataRow row in _sqltable.Rows)
            {
                txtfirstduedate.Text = Convert.ToDateTime(row["DueDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            }

        }
        public void SearchCostumerLoantype(TextBox loantype, TextBox Trans,
            TextBox loanterm, TextBox intrate,
            TextBox penaltyrate, TextBox maturitydate,
            TextBox amortization, TextBox graceperiod,
            TextBox baseon, TextBox Prncipalamnt)
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
                Prncipalamnt.Text = string.Format("{0:N2}", Convert.ToDouble(row["DocTotal"].ToString()));
                fdd();
            }

        }
        void cardcode(TextBox cardcode)
        {
            string _gendata = "";
            _gendata = @"SELECT B.CardCode FROM OCRD B where (B.U_FN +' '+B.U_MN+' '+B.U_SN) = '" + txtsearchname.Text + "'";
            DataTable _sqltable = new DataTable();
            _sqltable = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _gendata);
            foreach (DataRow row in _sqltable.Rows)
            {
                cardcode.Text = row["CardCode"].ToString().ToUpper();
            }
        }
        private void SUB_LEDGER_Load(object sender, EventArgs e)
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
            ClientInformation(txtclientid, txtdocnum, txtname, txtaddress, txtcontrolno, lbloandate);
            cardcode(txtclientid);
            tocomboboxproduct(cmbloantype);
        }
        public void Loadtype()
        {

            string _getInstallment = "";
            _getInstallment = @"
                    DECLARE @DocEntry AS NVARCHAR(30)
                    DECLARE @Dscription AS NVARCHAR(30)
                    DECLARE @Start as DATETIME
                    DECLARE @Due as DATETIME

                    SET @DocEntry = '" + txtdocnum.Text + @"'
                    SET @Dscription = '" + cmbloantype.Text + @"'
                    SET  @Start = '" + txtfirstduedate.Text + @"'
                    SET  @Due = '" + textBox1.Text + @"'

 
                     SELECT A.InstlmntID as [INSTALLMENT], A.DueDate AS [DUEDATE]
                     , A.InsTotal AS [TOTAL],0.00 as [PBSB], A.U_Interest AS [INTEREST], 0.00 AS [PAID INTEREST]
                     , A.U_PenDate AS [PENALTY DATE], A.U_PenRate AS [PENALTY RATE], A.Paid AS [PAID]

				     ,0.00 as [BALANCE]
                     ,0.00 as [PENALTY]
                     ,0.00 as [PAID PENALTY]

                     from INV6 A 
                     INNER JOIN INV1 B ON A.DocEntry = B.DocEntry where B.docentry = @DocEntry  AND B.Dscription = @Dscription AND A.DueDate BETWEEN @Start AND @Due  ";
//                    UNION ALL
//
//                    SELECT C.LineID, FirstDue, 0.00, 0.00, 0.00, 
//                    CASE WHEN C.CreditAcct = '4010006' THEN FirstSum * -1 ELSE 0 END
//                    , NULL, 0.00, 0.00, 0.00, 0.00, 
//                    CASE WHEN C.CreditAcct = '4060001' THEN FirstSum * -1 ELSE 0 END
//                    FROM RCT3 C 
//                    WHERE C.DocNum IN (SELECT B.DocNum
//                    FROM RCT2 C INNER JOIN ORCT B ON B.DocNum = C.DocNum WHERE C.DocEntry = @DocEntry AND B.Canceled = 'N') 
//
//                    ) XX ORDER BY DUEDATE  ";
            DataTable tablename = new DataTable();
            tablename = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _getInstallment);
            clsFunctions.DataGridViewSetup(dataGridView1, tablename);

        }
        private void cmbloantype_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchCostumerLoantype(txtloantype, txttransaction, txtloanterm, txtintrate, txtpenaltyrate, txtmaturity, txtamort, txtgraceperiod, txtbaseon, txtprinamount);
        }
        public void calculate()
        {
            double _Balance;
            double.TryParse(txtprinamount.Text, out _Balance);
        
            double InterestTotal = 0;
            foreach (DataGridViewRow Row in dataGridView1.Rows)
            {

                double c9 = double.Parse(Row.Cells[2].Value.ToString());
                _Balance = _Balance - c9;

                double _PBTP = double.Parse(Row.Cells[3].Value.ToString());
                double _Interest = double.Parse(Row.Cells[4].Value.ToString());
                InterestTotal = InterestTotal + _Interest;
                double _RBalance = double.Parse(Row.Cells[9].Value.ToString());
                double _PRate = double.Parse(Row.Cells[7].Value.ToString());
                double _PaidInterest = double.Parse(Row.Cells[5].Value.ToString());


                double _ActualBalance = (_Balance - double.Parse(Row.Cells[8].Value.ToString()));
                double _PenaltyAmt = (((_RBalance - _PBTP) + (InterestTotal - _PaidInterest)) * (_PRate / 100));

                Row.Cells[11].Value = double.Parse(_Balance.ToString("N2"));
                Row.Cells[9].Value = double.Parse(_PenaltyAmt.ToString("N2"));

            }

            double _Balance2;
            double.TryParse(txtprinamount.Text, out _Balance2);
            foreach (DataGridViewRow Row in dataGridView1.Rows)
            {
                string _Value = Row.Cells[2].Value.ToString();
                _Balance2 = _Balance2 - double.Parse(double.Parse(_Value).ToString("N2"));

                Row.Cells[3].Value = double.Parse(_Balance2.ToString("N2"));
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            Loadtype();
            calculate();
        }
        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            DateTime start = Convert.ToDateTime(txtfirstduedate.Text);
            DateTime end = Convert.ToDateTime(txtmaturity.Text);
        }
    }
}
