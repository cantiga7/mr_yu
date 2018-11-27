using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace FINAL_LOAN_PACKAGING.MYCLASS
{
    public class CLASS_LOAN
    {
        public static SAPbobsCOM.Company oCompany;
        #region PENDINGdATA
        public void GETPENDINGDATA(string Docentry, Label ClientId, TextBox lastname, TextBox firstnme, TextBox middlename, TextBox orgname,
            TextBox date, TextBox Loantype, TextBox prpse, TextBox principl, MaskedTextBox loandate, TextBox noterm, ComboBox term, ComboBox baseno,
              ComboBox dimisnish, ComboBox paymntMode, ComboBox status, TextBox Interest, ComboBox intpymntmode, TextBox penalty, ComboBox computed, TextBox graceperiod
              , ComboBox duedateopt, ComboBox collectiontype, Label Posted)
        {
            string _sqlDataDisplay = "";
              DataTable _sqltable = new DataTable();

            _sqlDataDisplay = @"Select 
                                A.DocEntry
                                , A.ClientId 
                                , A.LastName
                                , A.FirstName 
                                , A.MiddleName 
                                , A.Date
                                , A.LoanType
                                , A.Purpose 
                                , A.Principal 
                                , A.Loandate
                                , A.NoTerm
                                , A.Term
                                , A.Baseno
                                , A.PaymentMode 
                                ,A.Status
                                , A.Interest 
                                ,A.IntpaymentMode,
                                A.Penalty 
                                , A.Computed 
                                ,A.GracePeriod
                                , A.DueDateOpt 
                                ,A.CollectionType 
                                , A.Posted
                                 from LOANMASTERDATA A WHERE A.DocEntry = '" + Docentry + "'";

            _sqltable = clsSQLClientFunctions.DataList(clsDeclaration.sLclSystemConnection , _sqlDataDisplay );

            foreach (DataRow row in _sqltable.Rows)
            {
                ClientId.Text = row["ClientId"].ToString();
                lastname.Text = row["LastName"].ToString();
                firstnme.Text = row["FirstName"].ToString();
                middlename.Text = row["MiddleName"].ToString();
                orgname.Text = (row["FirstName"].ToString() + ' ' + row["MiddleName"].ToString() + ' ' + row["LastName"].ToString());
                date.Text = Convert.ToDateTime(row["Date"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                Loantype.Text = row["LoanType"].ToString();
                prpse.Text = row["Purpose"].ToString();
                principl.Text = string.Format("{0:N2}", Convert.ToDouble(row["Principal"].ToString()));
                loandate.Text = DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);//Convert.ToDateTime(row["Loandate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                noterm.Text = row["NoTerm"].ToString();
                term.Text = row["Term"].ToString();
                baseno.Text = row["Baseno"].ToString();
                if (row["Baseno"].ToString() == "STRAIGHT LINE")
                {
                    paymntMode.Text = row["PaymentMode"].ToString();
                }
                else
                {
                    dimisnish.Text = row["PaymentMode"].ToString();
                }
                status.Text = row["Status"].ToString();
                Interest.Text = row["Interest"].ToString();
                intpymntmode.Text = row["IntpaymentMode"].ToString();
                penalty.Text = row["Penalty"].ToString();
                computed.Text = row["Computed"].ToString();
                graceperiod.Text = row["GracePeriod"].ToString();
                duedateopt.Text = row["DueDateOpt"].ToString();
                collectiontype.Text = row["CollectionType"].ToString();
                Posted.Text = row["Posted"].ToString();
            }
        }
        #endregion


        #region RETRIEVE DATA
        public void ACCTINFO(string no, Label clientid, TextBox fn, TextBox mn, TextBox ln, TextBox orgname, TextBox address,
            TextBox princpl, MaskedTextBox pdate, TextBox interst, TextBox pnlty, TextBox gperiod,
             MaskedTextBox maturdate, MaskedTextBox intdate, MaskedTextBox pricipaldate, TextBox Loantype, TextBox purpose, ComboBox baseon, ComboBox duedateopt,
             TextBox maker1, TextBox maker2, TextBox maker3, TextBox maker4, TextBox dateopened)
        {

            string _gendata = "";
            DataTable _sqltable = new DataTable();


            _gendata = @"SELECT
                                A.DocNum
                                , A.CARDCODE
                                ,B.U_FN
                                ,B.U_SN
                                ,B.U_MN
                                ,A.DOCDATE
                                ,C.Price
                                ,A.ADDRESS
                                ,A.U_INTREST
                                ,A.U_PNLTY
                                ,A.U_GPRIOD
                                ,(SELECT P.NAME FROM [@LOANTYPE] P WHERE P.CODE = A.U_LOANTYPE ) AS PURPOSE
                                ,(SELECT L.NAME FROM [@PRODUCT] L WHERE L.CODE =A.U_PRODUCT) AS LOANTYPE
                                ,A.U_BASE
                                ,A.U_DUEOPT
                                ,A.U_COMAKER1
                                ,A.U_CONAKER2
                                ,A.U_COMAKER3
                                ,A.U_COMAKER4

                                FROM ORDR A
	                            INNER JOIN OCRD B ON A.CardCode = B.CardCode
                                INNER JOIN RDR1 C ON A.DocEntry = C.DocEntry   WHERE A.DocNum ='" + no.ToString() + "'";
         
            _sqltable = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _gendata);

            foreach (DataRow row in _sqltable.Rows)
            {
                clientid.Text = row["CardCode"].ToString();
                fn.Text = row["U_FN"].ToString().ToUpper();
                ln.Text = row["U_SN"].ToString().ToUpper();
                mn.Text = row["U_MN"].ToString().ToUpper();
                orgname.Text = (row["U_FN"].ToString().ToUpper() + ' ' + row["U_MN"].ToString().ToUpper() + ' ' + row["U_SN"].ToString().ToUpper());
                princpl.Text = string.Format("{0:N2}",Convert.ToDouble( row["Price"].ToString()));
                pdate.Text =  DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                address.Text = row["Address"].ToString();
                dateopened.Text = DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                interst.Text = row["U_INTREST"].ToString();
                pnlty.Text = row["U_PNLTY"].ToString();
                gperiod.Text = row["U_GPRIOD"].ToString();

                // DUESTATUS//
                maturdate.Text = Convert.ToDateTime(row["DocDate"]).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                intdate.Text = DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);//Convert.ToDateTime(mc.dr["U_INTDDATE"]).ToString("MM/dd/yyyy", CultureInfo.InstalledUICulture);
                pricipaldate.Text = DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture); // Convert.ToDateTime(mc.dr["U_PRINDUEDATE"]).ToString("MM/dd/yyyy", CultureInfo.InstalledUICulture);

                //CMBOBOX//
                Loantype.Text= row["LOANTYPE"].ToString();
                purpose.Text = row["PURPOSE"].ToString();
                baseon.Text = row["U_BASE"].ToString().ToUpper();
                duedateopt.Text = row["U_DUEOPT"].ToString();
                
                ///CO-MAKER
                maker1.Text = row["U_COMAKER1"].ToString().ToUpper();
                maker2.Text = row["U_CONAKER2"].ToString().ToUpper();
                maker3.Text = row["U_COMAKER3"].ToString().ToUpper();
                maker4.Text = row["U_COMAKER4"].ToString().ToUpper();
            }
        }
        #endregion
        #region PRINCIPAL
        public void principalgenerate(TextBox principalamount, TextBox termno, Label prinamounttotal, ComboBox comboterm,
            ComboBox combopymtmode, ComboBox baseno, ComboBox diminishing, ComboBox cmbintmode, TextBox interest)
        {
            double prinamount, term, principal, amort, ttlint, intrst;
            double.TryParse(interest.Text, out intrst);
            double.TryParse(principalamount.Text, out prinamount);
            double.TryParse(termno.Text, out term);
            double.TryParse(prinamounttotal.Text, out principal);

            if (principalamount.Text == "0" || principalamount.Text == "")
            {
                prinamounttotal.Text = "----";
            }
            else
            {
                #region STRAIGHT LINE
                if (baseno.Text == "STRAIGHT LINE")
                {
                    if (comboterm.Text == "DAYS")
                    {
                        // NO DAYS
                        if (combopymtmode.Text == "DAILY")
                        {
                            principal = (prinamount / (term));
                        }
                        else if (combopymtmode.Text == "WEEKLY")
                        {
                            if (term < 7)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {
                                principal = (prinamount / (term / 7));
                            }
                        }
                        //no
                        else if (combopymtmode.Text == "S-MONTHS")
                        {
                            if (term < 15)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {
                                principal = (prinamount / (term / 6));
                            }
                        }
                        else if (combopymtmode.Text == "MONTHLY")
                        {
                            if (term < 1)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {
                                principal = (prinamount / (term / 4));
                            }
                        }
                        else if (combopymtmode.Text == "QUARTERLY")
                        {
                            if (term < 12)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {
                                principal = (prinamount / ((term / 4) / 3));
                            }
                        }

                        else if (combopymtmode.Text == "SEMESTRAL")
                        {
                            if (term < 26)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {
                                principal = ((prinamount / term) * 26);
                            }
                        }

                        else if (combopymtmode.Text == "YEARLY")
                        {
                            if (term < 52)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {
                                principal = ((prinamount / term) * 52);
                            }
                        }
                        else if (combopymtmode.Text == "LUMPSUM")
                        {
                            principal = double.Parse(principalamount.Text);
                        }

                    }
                    else if (comboterm.Text == "WEEKS")
                    {
                        // NO DAYS
                        if (combopymtmode.Text == "DAILY")
                        {
                            principal = (prinamount / (term * 7));
                        }
                        else if (combopymtmode.Text == "WEEKLY")
                        {
                            principal = (prinamount / ((term * 4) / 4));

                        }
                        else if (combopymtmode.Text == "S-MONTHS")
                        {
                            if (term > 2)
                            {
                                principal = (prinamount / ((term / 4) * 2));
                            }
                            else
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                        }
                        else if (combopymtmode.Text == "MONTHLY")
                        {
                            if (term > 4)
                            {
                                principal = (prinamount / ((term / 4) * 1));
                            }
                            else
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                        }
                        else if (combopymtmode.Text == "QUARTERLY")
                        {
                            if (term < 12)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {
                                principal = (prinamount / ((term / 4) / 3));
                            }
                        }

                        else if (combopymtmode.Text == "SEMESTRAL")
                        {
                            if (term < 26)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {
                                principal = ((prinamount / term) * 26);
                            }
                        }

                        else if (combopymtmode.Text == "YEARLY")
                        {
                            if (term < 52)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {
                                principal = ((prinamount / term) * 52);
                            }
                        }
                        else if (combopymtmode.Text == "LUMPSUM")
                        {
                            principal = double.Parse(principalamount.Text);
                        }
                    }
                    else if (comboterm.Text == "S-MONTHS")
                    {
                        // NO DAYS

                        if (combopymtmode.Text == "DAILY")
                        {
                            principal = (prinamount / ((term) * 15));
                        }
                        else if (combopymtmode.Text == "WEEKLY")
                        {
                            principal = (prinamount / ((term / 2) * 4));
                        }
                        else if (combopymtmode.Text == "S-MONTHS")
                        {
                            principal = (prinamount / ((term / 2) * 2));
                        }
                        //error
                        else if (combopymtmode.Text == "MONTHLY")
                        {
                            principal = (prinamount / ((term / 2) * 1));
                        }
                        else if (combopymtmode.Text == "QUARTERLY")
                        {
                            if (term < 6)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {
                                principal = (prinamount / ((term / 2) / 3));
                            }
                        }
                        else if (combopymtmode.Text == "SEMESTRAL")
                        {
                            if (term < 12)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {
                                principal = (prinamount / ((term / 2) / 6));
                            }
                        }
                        else if (combopymtmode.Text == "YEARLY")
                        {
                            if (term < 24)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {
                                principal = (prinamount / ((term / 2) / 12));
                            }
                        }
                        //no
                        else if (combopymtmode.Text == "DAILY")
                        {
                            principal = (prinamount / ((term * 2) * 30));
                        }
                        else if (combopymtmode.Text == "LUMPSUM")
                        {
                            principal = double.Parse(principalamount.Text);
                        }

                    }
                    else if (comboterm.Text == "MONTHS")
                    {
                        // NO DAYS

                        if (combopymtmode.Text == "DAILY")
                        {
                            principal = (prinamount / ((term) * 30));
                        }
                        else if (combopymtmode.Text == "WEEKLY")
                        {
                            principal = (prinamount / ((term * 1) * 4));
                        }
                        else if (combopymtmode.Text == "S-MONTHS")
                        {
                            principal = (prinamount / ((term * 1) * 2));
                        }
                        else if (combopymtmode.Text == "MONTHLY")
                        {
                            principal = (prinamount / ((term * 1) * 1));
                        }
                        else if (combopymtmode.Text == "QUARTERLY")
                        {
                            if (term < 3)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {
                                principal = (prinamount / ((term * 1) / 3));
                            }
                        }
                        else if (combopymtmode.Text == "SEMESTRAL")
                        {
                            if (term < 6)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {

                                principal = (prinamount / ((term * 1) / 6));
                            }
                        }
                        else if (combopymtmode.Text == "YEARLY")
                        {

                            if (term < 12)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {
                                principal = (prinamount / ((term * 1) / 12));
                            }
                        }
                        else if (combopymtmode.Text == "LUMPSUM")
                        {
                            principal = double.Parse(principalamount.Text);
                        }
                    }
                    else if (comboterm.Text == "QUARTERS")
                    {
                        // NO DAYS

                        if (combopymtmode.Text == "DAILY")
                        {
                            principal = (prinamount / ((term) * 90));
                        }
                        else if (combopymtmode.Text == "WEEKLY")
                        {
                            principal = (prinamount / ((term * 3) * 4));
                        }
                        else if (combopymtmode.Text == "S-MONTHS")
                        {
                            principal = (prinamount / ((term * 3) * 2));
                        }
                        else if (combopymtmode.Text == "MONTHLY")
                        {
                            principal = (prinamount / (term * 3));
                        }
                        else if (combopymtmode.Text == "QUARTERLY")
                        {
                            principal = (prinamount / (term * 1));
                        }
                        else if (combopymtmode.Text == "SEMESTRAL")
                        {
                            if (term < 3)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {
                                principal = (prinamount / ((term / 4) * 2));
                            }
                        }
                        else if (combopymtmode.Text == "YEARLY")
                        {
                            if (term < 4)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {
                                principal = (prinamount / ((term / 4) * 1));
                            }
                        }
                        else if (combopymtmode.Text == "LUMPSUM")
                        {
                            principal = double.Parse(principalamount.Text);
                        }
                    }
                    else if (comboterm.Text == "SEMESTRS")
                    {
                        // NO DAYS

                        if (combopymtmode.Text == "DAILY")
                        {
                            principal = (prinamount / ((term) * 180));
                        }
                        else if (combopymtmode.Text == "WEEKLY")
                        {
                            principal = (prinamount / (term * 26));
                        }
                        else if (combopymtmode.Text == "S-MONTHS")
                        {
                            principal = (prinamount / ((term * 6) * 2));
                        }
                        else if (combopymtmode.Text == "MONTHLY")
                        {
                            principal = (prinamount / ((term * 6) * 1));
                        }
                        else if (combopymtmode.Text == "QUARTERLY")
                        {
                            principal = (prinamount / ((term * 6) / 3));
                        }
                        else if (combopymtmode.Text == "SEMESTRAL")
                        {
                            principal = (prinamount / ((term * 6) / 6));
                        }
                        else if (combopymtmode.Text == "YEARLY")
                        {
                            if (term < 2)
                            {
                                principal = double.Parse(principalamount.Text);
                            }
                            else
                            {
                                principal = (prinamount / ((term * 6) / 12));
                            }
                        }
                        else if (combopymtmode.Text == "LUMPSUM")
                        {
                            principal = double.Parse(principalamount.Text);
                        }
                    }
                    else if (comboterm.Text == "YEARS")
                    {
                        // NO DAYS
                        if (combopymtmode.Text == "DAILY")
                        {
                            principal = (prinamount / ((term) * 360));
                        }
                        else if (combopymtmode.Text == "WEEKLY")
                        {
                            principal = (prinamount / (term * 52));
                        }
                        else if (combopymtmode.Text == "S-MONTHS")
                        {
                            principal = (prinamount / ((term * 12) * 2));
                        }
                        else if (combopymtmode.Text == "MONTHLY")
                        {
                            principal = (prinamount / ((term * 12) * 1));
                        }
                        else if (combopymtmode.Text == "QUARTERLY")
                        {
                            principal = (prinamount / ((term * 12) / 3));
                        }
                        else if (combopymtmode.Text == "SEMESTRAL")
                        {
                            principal = (prinamount / ((term * 12) / 6));
                        }
                        else if (combopymtmode.Text == "YEARLY")
                        {
                            principal = (prinamount / ((term * 12) / 12));
                        }
                        else if (combopymtmode.Text == "LUMPSUM")
                        {
                            principal = double.Parse(principalamount.Text);
                        }
                    }
                }
                #endregion
                #region DIMINISHING BAL
                else if (baseno.Text == "DIMINISHING BAL")
                {
                    if (comboterm.Text == "S-MONTHS")
                    {
                        if (diminishing.Text == "S-MONTHLY")
                        {
                            principal = (prinamount / (term / 2));
                        }
                        else if (diminishing.Text == "MONTHLY")
                        {
                            principal = (prinamount / ((term * 1) / 2));
                        }
                        else if (diminishing.Text == "MONTHLY EMB3")
                        {
                            if (term < 3)
                            {
                                principal = 0;
                            }
                            else
                            {
                                principal = (prinamount / (term - 6));
                            }
                        }
                        else if (diminishing.Text == "MONTHLY EMB6")
                        {
                            if (term < 7)
                            {
                                principal = 0;
                            }
                            else
                            {
                                principal = (prinamount / (term - 6));
                            }
                        }
                        else if (diminishing.Text == "BIMONTHLY")
                        {
                            if (term % 2 == 0)
                            {
                                principal = (prinamount / (term / 2));
                            }
                            else
                            {
                                principal = (prinamount / ((term - 1) / 2));
                            }
                        }
                        else if (diminishing.Text == "QUARTERLY")
                        {
                            double total;
                            if (term < 3)
                            {
                                principal = 0.00;
                            }
                            else
                            {
                                if (term % 3 == 0)
                                {
                                    total = Math.Truncate(term / 3);
                                    principal = (prinamount / total);
                                }
                                else
                                {
                                    total = Math.Truncate((term / 3) + 1);
                                    principal = (prinamount / total);
                                }
                            }

                        }
                        else if (diminishing.Text == "SEMI ANNUALLY")
                        {
                            double total;
                            if (term < 6)
                            {
                                principal = 0.00;
                            }
                            else
                            {
                                if (term % 6 == 0)
                                {
                                    total = Math.Truncate(term / 6);
                                    principal = (prinamount / total);
                                }
                                else
                                {
                                    total = Math.Truncate((term / 6) + 1);
                                    principal = (prinamount / total);
                                }
                            }
                        }
                        else if (diminishing.Text == "ANNUALLY")
                        {
                            double total;
                            if (term < 12)
                            {
                                principal = 0.00;
                            }
                            else
                            {
                                if (term % 12 == 0)
                                {
                                    total = Math.Truncate(term / 12);
                                    principal = (prinamount / total);
                                }
                                else
                                {
                                    total = Math.Truncate((term / 12) + 1);
                                    principal = (prinamount / total);
                                }
                            }
                        }
                        else
                        {
                            principal = prinamount;
                        }
                    }
                    else if (comboterm.Text == "MONTHS")
                    {
                        if (diminishing.Text == "S-MONTHLY")
                        {
                            principal = (prinamount / term )/2;
                        }
                        else if (diminishing.Text == "MONTHLY")
                        {
                            principal = (prinamount / ((term * 1) * 1));
                        }
                        else if (diminishing.Text == "MONTHLY EMB3")
                        {
                            if (term < 3)
                            {
                                principal = 0;
                            }
                            else
                            {
                                principal = (prinamount / (term - 3));
                            }
                        }
                        else if (diminishing.Text == "MONTHLY EMB6")
                        {
                            if (term < 7)
                            {
                                principal = 0;
                            }
                            else
                            {
                                principal = (prinamount / (term - 6));
                            }
                        }
                        else if (diminishing.Text == "BIMONTHLY")
                        {
                            if (term % 2 == 0)
                            {
                                principal = (prinamount / (term / 2));
                            }
                            else
                            {
                                principal = (prinamount / ((term - 1) / 2));
                            }
                        }
                        else if (diminishing.Text == "QUARTERLY")
                        {
                            double total;
                            if (term < 3)
                            {
                                principal = 0.00;
                            }
                            else
                            {
                                if (term % 3 == 0)
                                {
                                    total = Math.Truncate(term / 3);
                                    principal = (prinamount / total);
                                }
                                else
                                {
                                    total = Math.Truncate((term / 3) + 1);
                                    principal = (prinamount / total);
                                }
                            }

                        }
                        else if (diminishing.Text == "SEMI ANNUALLY")
                        {
                            double total;
                            if (term < 6)
                            {
                                principal = 0.00;
                            }
                            else
                            {
                                if (term % 6 == 0)
                                {
                                    total = Math.Truncate(term / 6);
                                    principal = (prinamount / total);
                                }
                                else
                                {
                                    total = Math.Truncate((term / 6) + 1);
                                    principal = (prinamount / total);
                                }
                            }
                        }
                        else if (diminishing.Text == "ANNUALLY")
                        {
                            double total;
                            if (term < 12)
                            {
                                principal = 0.00;
                            }
                            else
                            {
                                if (term % 12 == 0)
                                {
                                    total = Math.Truncate(term / 12);
                                    principal = (prinamount / total);
                                }
                                else
                                {
                                    total = Math.Truncate((term / 12) + 1);
                                    principal = (prinamount / total);
                                }
                            }
                        }
                        else
                        {
                            principal = prinamount;
                        }
                    }
                    else if (comboterm.Text == "QUARTERS")
                    {
                        if (diminishing.Text == "S-MONTHLY")
                        {
                            principal = (prinamount / (term / 6));
                        }
                        else if (diminishing.Text == "MONTHLY")
                        {
                            principal = (prinamount / term / 3);
                        }
                        else if (diminishing.Text == "MONTHLY EMB3")
                        {
                            principal = (prinamount / ((term * 3) - 3));
                        }
                        else if (diminishing.Text == "MONTHLY EMB6")
                        {
                            if (term < 3)
                            {
                                principal = 0;
                            }
                            else
                            {
                                principal = (prinamount / ((term * 3) - 6));
                            }
                        }
                        else if (diminishing.Text == "BIMONTHLY")
                        {
                            if (term % 2 == 0)
                            {
                                principal = prinamount / ((term * 3) / 2);
                            }
                            else
                            {
                                principal = prinamount / (((term * 3) - 1) / 2);
                            }
                        }
                        else if (diminishing.Text == "QUARTERLY")
                        {
                            principal = (prinamount / term);
                        }
                        else if (diminishing.Text == "SEMI ANNUALLY")
                        {
                          
                            if (term < 2)
                            {
                                principal = 0;
                            }
                            else
                            {

                                principal = (prinamount / (term / 2));
                            }
                        }
                        else if (diminishing.Text == "ANNUALLY")
                        {
                            
                            if (term < 4)
                            {
                                principal = 0;
                            }
                            else
                            {
                                principal = (prinamount / (term / 4));
                            }
                        }
                        else
                        {
                            principal = prinamount;
                        }
                    }
                    else if (comboterm.Text == "SEMESTERS")
                    {
                        if (diminishing.Text == "S-MONTHLY")
                        {
                            principal = (prinamount / (term / 12));
                        }
                        else if (diminishing.Text == "MONTHLY")
                        {
                            principal = (prinamount / term / 6);
                        }
                        else if (diminishing.Text == "MONTHLY EMB3")
                        {
                            principal = (prinamount / ((term * 6) - 3));
                        }
                        else if (diminishing.Text == "MONTHLY EMB6")
                        {
                            if (term < 2)
                            {
                                principal = 0;
                            }
                            else
                            {
                                principal = (prinamount / ((term * 6) - 6));
                            }
                        }
                        else if (diminishing.Text == "BIMONTHLY")
                        {
                            if (term % 2 == 0)
                            {
                                principal = prinamount / ((term * 6) / 2);
                            }
                            else
                            {
                                principal = prinamount / (((term * 6) - 1) / 2);
                            }
                        }
                        else if (diminishing.Text == "QUARTERLY")
                        {
                            principal = (prinamount / (term)) / 2;
                        }
                        else if (diminishing.Text == "SEMI ANNUALLY")
                        {
                            principal = (prinamount / term);
                        }
                        else if (diminishing.Text == "ANNUALLY")
                        {
                            double total;
                            if (term < 2)
                            {
                                principal = 0;
                            }
                            else
                            {
                                if (term % 2 == 0)
                                {
                                    total = Math.Truncate(term / 2);
                                    principal = (prinamount / total);
                                }
                                else
                                {
                                    total = Math.Truncate((term / 2) + 1);
                                    principal = (prinamount / total);
                                }

                            }
                        }
                        else
                        {
                            principal = prinamount;
                        }

                    }
                    else if (comboterm.Text == "YEARS")
                    {
                        if (diminishing.Text == "S-MONTHLY")
                        {
                            principal = (prinamount / (term / 24));
                        }
                        else if (diminishing.Text == "MONTHLY")
                        {
                            principal = (prinamount / term / 12);
                        }
                        else if (diminishing.Text == "MONTHLY EMB3")
                        {
                            principal = (prinamount / ((term * 12) - 3));
                        }
                        else if (diminishing.Text == "MONTHLY EMB6")
                        {
                            principal = (prinamount / ((term * 12) - 6));
                        }
                        else if (diminishing.Text == "BIMONTHLY")
                        {
                            if (term % 2 == 0)
                            {
                                principal = prinamount / ((term * 12) / 2);
                            }
                            else
                            {
                                principal = prinamount / (((term * 12) - 1) / 2);
                            }
                        }
                        else if (diminishing.Text == "QUARTERLY")
                        {
                            principal = (prinamount / (term)) / 4;
                        }
                        else if (diminishing.Text == "SEMI ANNUALLY")
                        {
                            principal = (prinamount / term) / 2;
                        }
                        else if (diminishing.Text == "ANNUALLY")
                        {
                            principal = (prinamount / term);
                        }
                        else
                        {
                            principal = prinamount;
                        }
                    }
                }
                #endregion
                #region ANNUITY
                else if (baseno.Text == "ANNUITY")
                {
                    if (comboterm.Text == "S-MONTHS")
                    {
                        if (cmbintmode.Text == "S-MONTHS")
                        {
                            if (diminishing.Text == "S-MONTHLY")
                            {
                                ttlint = (intrst / 100) / 24;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, - term  ))/ ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "MONTHLY")
                            {
                                if (term % 2 == 0)
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 2)))) / ttlint);
                                    principal = amort;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 2)+1)))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "MONTHLY EMB3")
                            {
                                if (term < 8)
                                {
                                    principal = 0;
                                }
                                else
                                {
                                    if (term % 2 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term - 3) / 2))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(((term - 3) / 2)+1))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "MONTHLY EMB6")
                            {
                                if (term < 14)
                                {
                                    principal = 0;
                                }
                                else
                                {
                                    if (term % 2 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 6))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term - 6)+1))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "BIMONTHLY")
                            {
                                if (term % 2 == 0)
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term / 2))) / ttlint);
                                    principal = amort;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term - 1) / 2)+1)) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "QUARTERLY")
                            {
                                if (term < 6)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 3 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 3) ))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 3) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }

                            }
                            else if (diminishing.Text == "SEMI ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 6 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 6) ))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 6) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 24)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 12 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 12)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 12) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "MONTHLY")
                        {
                            if (diminishing.Text == "MONTHLY")
                            {
                                ttlint = (intrst / 100) / 12;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -term)) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "MONTHLY EMB3")
                            {
                                if (term < 4)
                                {
                                    principal = 0;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 12;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 3))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "MONTHLY EMB6")
                            {
                                if (term < 7)
                                {
                                    principal = 0;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 12;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 6))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "BIMONTHLY")
                            {
                                if (term % 2 == 0)
                                {
                                    ttlint = (intrst / 100) / 12;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term / 2))) / ttlint);
                                    principal = amort;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 12;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 1) / 2)) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 3 == 0)
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 3)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 3) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }

                            }
                            else if (diminishing.Text == "SEMI ANNUALLY")
                            {

                                if (term < 6)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 6 == 0)
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 6)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 6) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 12 == 0)
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 12)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 12) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "QUARTERLY")
                        {
                            if (diminishing.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 4;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 3)))) / ttlint);
                                    principal = amort;
                                }

                            }
                            else if (diminishing.Text == "SEMI ANNUALLY")
                            {
                                if (term < 6)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 4;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 6)))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 4;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 12)))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "SEMESTRAL")///////////////////////
                        {
                            if (diminishing.Text == "SEMI ANNUALLY")
                            {

                                if (term < 6)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 2;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 6)))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 2;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 12)))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "YEARLY")
                        {
                            if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 1;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 12)))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                    }
                    else if (comboterm.Text == "MONTHS")
                    {
                        if (cmbintmode.Text == "S-MONTHS")
                        {
                            if (diminishing.Text == "S-MONTHLY")
                            {
                                ttlint = (intrst / 100) / 24;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -term)) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "MONTHLY")
                            {
                                if (term % 2 == 0)
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term)))) / ttlint);
                                    principal = amort;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 1) + 1)))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "MONTHLY EMB3")
                            {
                                if (term < 4)
                                {
                                    principal = 0;
                                }
                                else
                                {
                                    if (term % 2 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 3) )) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term - 3) + 1))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "MONTHLY EMB6")
                            {
                                if (term < 7)
                                {
                                    principal = 0;
                                }
                                else
                                {
                                    if (term % 2 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 6))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term - 6) + 1))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "BIMONTHLY")
                            {
                                if (term % 2 == 0)
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -term )) / ttlint);
                                    principal = amort;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 1)+ 1)) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 3 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 3)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 3) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }

                            }
                            else if (diminishing.Text == "SEMI ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 6 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 6)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 6) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 24)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 12 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 12)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 12) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "MONTHLY")
                        {
                            if (diminishing.Text == "MONTHLY")
                            {
                                ttlint = (intrst / 100) / 12;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -term)) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "MONTHLY EMB3")
                            {
                                if (term < 4)
                                {
                                    principal = 0;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 12;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 3))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "MONTHLY EMB6")
                            {
                                if (term < 7)
                                {
                                    principal = 0;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 12;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 6))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "BIMONTHLY")
                            {
                                if (term % 2 == 0)
                                {
                                    ttlint = (intrst / 100) / 12;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term / 2))) / ttlint);
                                    principal = amort;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 12;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 1) / 2)) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 3 == 0)
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 3)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 3) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }

                            }
                            else if (diminishing.Text == "SEMI ANNUALLY")
                            {

                                if (term < 6)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 6 == 0)
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 6)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 6) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 12 == 0)
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 12)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 12) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "QUARTERLY")
                        {

                            if (diminishing.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 4;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 3)))) / ttlint);
                                    principal = amort;
                                }

                            }
                            else if (diminishing.Text == "SEMI ANNUALLY")
                            {
                                if (term < 6)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 4;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 6)))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 4;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 12)))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "SEMESTRAL")
                        {
                            if (diminishing.Text == "SEMI ANNUALLY")
                            {

                                if (term < 6)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 2;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 6)))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 2;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 12)))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "YEARLY")
                        {
                            if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 1;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 12)))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                    }
                    else if (comboterm.Text == "QUARTERS")
                    {
                        if (cmbintmode.Text == "S-MONTHS")
                        {
                            if (diminishing.Text == "S-MONTHLY")
                            {
                                ttlint = (intrst / 100) / 24;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -term)) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "MONTHLY")
                            {
                                if (term % 2 == 0)
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term)))) / ttlint);
                                    principal = amort;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 1) + 1)))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "MONTHLY EMB3")
                            {
                                if (term < 4)
                                {
                                    principal = 0;
                                }
                                else
                                {
                                    if (term % 2 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 3))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term - 3) + 1))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "MONTHLY EMB6")
                            {
                                if (term < 7)
                                {
                                    principal = 0;
                                }
                                else
                                {
                                    if (term % 2 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 6))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term - 6) + 1))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "BIMONTHLY")
                            {
                                if (term % 2 == 0)
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -term)) / ttlint);
                                    principal = amort;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 1) + 1)) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 3 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 3)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 3) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }

                            }
                            else if (diminishing.Text == "SEMI ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 6 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 6)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 6) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 24)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 12 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 12)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 12) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "MONTHLY")
                        {
                            if (diminishing.Text == "MONTHLY")
                            {
                                ttlint = (intrst / 100) / 12;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term * 3))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "MONTHLY EMB3")
                            {
                                if (term < 2)
                                {
                                    principal = 0;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 12;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term * 3) - 3))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "MONTHLY EMB6")
                            {
                                if (term < 4)
                                {
                                    principal = 0;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 12;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term * 3) - 6))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "BIMONTHLY")
                            {
                                if (term % 2 == 0)
                                {
                                    ttlint = (intrst / 100) / 12;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term * 3) / 2))) / ttlint);
                                    principal = amort;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 12;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(((term * 3) - 1) / 2))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "QUARTERLY")
                            {
                                ttlint = (intrst / 100) / 12;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 3) / 3)))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "SEMI ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 2 == 0)
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 3) / 6)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(((term * 3) / 6) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 4 == 0)
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 3) / 12)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(((term * 3) / 12) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "QUARTERLY")
                        {
                            if (diminishing.Text == "QUARTERLY")
                            {
                                ttlint = (intrst / 100) / 4;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 3) / 3)))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "SEMI ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 2 == 0)
                                    {
                                        ttlint = (intrst / 100) / 4;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 3) / 6)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 4;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(((term * 3) / 6) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 4 == 0)
                                    {
                                        ttlint = (intrst / 100) / 4;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 3) / 12)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 4;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((Math.Truncate((term * 3) / 12) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "SEMESTRAL")
                        {
                           if (diminishing.Text == "SEMI ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 2 == 0)
                                    {
                                        ttlint = (intrst / 100) / 2;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 3) / 6)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 2;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(((term * 3) / 6) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 4 == 0)
                                    {
                                        ttlint = (intrst / 100) / 2;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 3) / 12)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 2;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(((term * 3) / 12) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "YEARLY")
                        {
                           if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 4 == 0)
                                    {
                                        ttlint = (intrst / 100) / 1;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 3) / 12)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 1;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(((term * 3) / 12) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                    }
                    else if (comboterm.Text == "SEMESTERS")
                    {
                        if (cmbintmode.Text == "S-MONTHS")
                        {
                            if (diminishing.Text == "S-MONTHLY")
                            {
                                ttlint = (intrst / 100) / 24;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -term)) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "MONTHLY")
                            {
                                if (term % 2 == 0)
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term)))) / ttlint);
                                    principal = amort;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 1) + 1)))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "MONTHLY EMB3")
                            {
                                if (term < 4)
                                {
                                    principal = 0;
                                }
                                else
                                {
                                    if (term % 2 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 3))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term - 3) + 1))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "MONTHLY EMB6")
                            {
                                if (term < 7)
                                {
                                    principal = 0;
                                }
                                else
                                {
                                    if (term % 2 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 6))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term - 6) + 1))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "BIMONTHLY")
                            {
                                if (term % 2 == 0)
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -term)) / ttlint);
                                    principal = amort;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 1) + 1)) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 3 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 3)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 3) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }

                            }
                            else if (diminishing.Text == "SEMI ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 6 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 6)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 6) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 24)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 12 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 12)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 12) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "MONTHLY")
                        {
                            if (diminishing.Text == "MONTHLY")
                            {
                                ttlint = (intrst / 100) / 12;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term * 6))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "MONTHLY EMB3")
                            {

                                ttlint = (intrst / 100) / 12;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term * 6) - 3))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "MONTHLY EMB6")
                            {
                                if (term < 2)
                                {
                                    principal = 0;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 12;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term * 6) - 6))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "BIMONTHLY")
                            {
                                if (term % 2 == 0)
                                {
                                    ttlint = (intrst / 100) / 12;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term * 6) / 2))) / ttlint);
                                    principal = amort;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 12;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(((term * 6) - 1) / 2))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "QUARTERLY")
                            {
                                ttlint = (intrst / 100) / 12;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 6) / 3)))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "SEMI ANNUALLY")
                            {
                                ttlint = (intrst / 100) / 12;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 6) / 6)))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 2 == 0)
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 6) / 12)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 12;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(((term * 6) / 12) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "QUARTERLY")
                        {
                            if (diminishing.Text == "QUARTERLY")
                            {
                                ttlint = (intrst / 100) / 4;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 6) / 3)))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "SEMI ANNUALLY")
                            {
                                ttlint = (intrst / 100) / 4;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 6) / 6)))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 2 == 0)
                                    {
                                        ttlint = (intrst / 100) / 4;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 6) / 12)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 4;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(((term * 6) / 12) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "SEMESTRAL")
                        {
                            if (diminishing.Text == "SEMI ANNUALLY")
                            {
                                ttlint = (intrst / 100) / 2;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 12) / 6)))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 2 == 0)
                                    {
                                        ttlint = (intrst / 100) / 2;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 12) / 12)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 2;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(((term * 12) / 12) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "YEARLY")
                        {
                            if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 2 == 0)
                                    {
                                        ttlint = (intrst / 100) / 1;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 12) / 12)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 1;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(((term * 12) / 12)) + 1))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                    }
                    else if (comboterm.Text == "YEARS")
                    {
                        if (cmbintmode.Text == "S-MONTHS")
                        {
                            if (diminishing.Text == "S-MONTHLY")
                            {
                                ttlint = (intrst / 100) / 24;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -term)) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "MONTHLY")
                            {
                                if (term % 2 == 0)
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term)))) / ttlint);
                                    principal = amort;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 1) + 1)))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "MONTHLY EMB3")
                            {
                                if (term < 4)
                                {
                                    principal = 0;
                                }
                                else
                                {
                                    if (term % 2 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 3))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term - 3) + 1))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "MONTHLY EMB6")
                            {
                                if (term < 7)
                                {
                                    principal = 0;
                                }
                                else
                                {
                                    if (term % 2 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 6))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term - 6) + 1))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "BIMONTHLY")
                            {
                                if (term % 2 == 0)
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -term)) / ttlint);
                                    principal = amort;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 24;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term - 1) + 1)) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 3 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 3)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 3) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }

                            }
                            else if (diminishing.Text == "SEMI ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 6 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 6)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 6) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                if (term < 24)
                                {
                                    principal = 0.00;
                                }
                                else
                                {
                                    if (term % 12 == 0)
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate(term / 12)))) / ttlint);
                                        principal = amort;
                                    }
                                    else
                                    {
                                        ttlint = (intrst / 100) / 24;
                                        amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term / 12) + 1)))) / ttlint);
                                        principal = amort;
                                    }
                                }
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "MONTHLY")
                        {
                            if (diminishing.Text == "MONTHLY")
                            {
                                ttlint = (intrst / 100) / 12;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(term * 12))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "MONTHLY EMB3")
                            {
                                ttlint = (intrst / 100) / 12;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term * 12) - 3))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "MONTHLY EMB6")
                            {
                                ttlint = (intrst / 100) / 12;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term * 12) - 6))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "BIMONTHLY")
                            {
                                if (term % 2 == 0)
                                {
                                    ttlint = (intrst / 100) / 12;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -((term * 12) / 2))) / ttlint);
                                    principal = amort;
                                }
                                else
                                {
                                    ttlint = (intrst / 100) / 12;
                                    amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(((term * 12) - 1) / 2))) / ttlint);
                                    principal = amort;
                                }
                            }
                            else if (diminishing.Text == "QUARTERLY")
                            {
                                ttlint = (intrst / 100) / 12;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 12) / 3)))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "SEMI ANNUALLY")
                            {
                                ttlint = (intrst / 100) / 12;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 12) / 6)))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                ttlint = (intrst / 100) / 12;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 12) / 12)))) / ttlint);
                                principal = amort;
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "QUARTERLY")
                        {
                            if (diminishing.Text == "QUARTERLY")
                            {
                                ttlint = (intrst / 100) / 4;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 12) / 3)))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "SEMI ANNUALLY")
                            {
                                ttlint = (intrst / 100) / 4;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 12) / 6)))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                ttlint = (intrst / 100) / 4;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 12) / 12)))) / ttlint);
                                principal = amort;
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "SEMESTRAL")
                        {
                            if (diminishing.Text == "SEMI ANNUALLY")
                            {
                                ttlint = (intrst / 100) / 2;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 12) / 6)))) / ttlint);
                                principal = amort;
                            }
                            else if (diminishing.Text == "ANNUALLY")
                            {
                                ttlint = (intrst / 100) / 2;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 12) / 12)))) / ttlint);
                                principal = amort;
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                        else if (cmbintmode.Text == "YEARLY")
                        {
                             if (diminishing.Text == "ANNUALLY")
                            {
                                ttlint = (intrst / 100) / 1;
                                amort = prinamount / ((1 - Math.Pow(1 + ttlint, -(Math.Truncate((term * 12) / 12)))) / ttlint);
                                principal = amort;
                            }
                            else
                            {
                                principal = prinamount;
                            }
                        }
                    }
                }
                #endregion

                //annuity wala pa

                prinamounttotal.Text = string.Format("{0:N2}", Convert.ToDouble(principal));
            }
        }
        #endregion
        #region PRINCICAL DUEDATE
        public void principalduedate(ComboBox combobaseon, ComboBox anntdimpymntmode, ComboBox duedateoption,
            ComboBox CMBPYNMTMODE, MaskedTextBox principalduedate, MaskedTextBox loandate)
        {
            DateTime prindate;
            DateTime pdd = Convert.ToDateTime(loandate.Text);
            DateTime thismonth = new DateTime(pdd.Year, pdd.Month, 25);


            DateTime FEOEM = new DateTime(pdd.Year, pdd.Month, 10);//

            DateTime TEOEM = new DateTime(pdd.Year, pdd.Month, 15);//

            DateTime TTEOEM = new DateTime(pdd.Year, pdd.Month, 06);//
            DateTime TTTEOEM = new DateTime(pdd.Year, pdd.Month, 20);

            DateTime FTEOEM = new DateTime(pdd.Year, pdd.Month, 11);//
            DateTime FTTEOEM = new DateTime(pdd.Year, pdd.Month, 25);

            if (combobaseon.Text == "STRAIGHT LINE")
            {
                if (duedateoption.Text == "DEFAULT")
                {
                    if (CMBPYNMTMODE.Text == "DAILY")
                    {
                        prindate = pdd.AddDays(1);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "WEEKLY")
                    {
                        prindate = pdd.AddDays(7);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "S-MONTHS")
                    {
                        prindate = pdd.AddDays(15);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "MONTHLY")
                    {
                        prindate = pdd.AddDays(30);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "QUARTERLY")
                    {
                        prindate = pdd.AddDays(90);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "SEMESTRAL")
                    {
                        prindate = pdd.AddDays(180);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "YEARLY")
                    {
                        prindate = pdd.AddDays(360);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                }
                else if (duedateoption.Text == "END OF MNTH")
                {
                    if (CMBPYNMTMODE.Text == "MONTHLY")
                    {
                        var anydate = pdd.AddMonths(1);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        prindate = lastDayOfMonth;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "QUARTERLY")
                    {
                        var anydate = pdd.AddMonths(3);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        prindate = lastDayOfMonth;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "SEMESTRAL")
                    {
                        var anydate = pdd.AddMonths(6);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        prindate = lastDayOfMonth;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "YEARLY")
                    {
                        var anydate = pdd.AddMonths(12);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        prindate = lastDayOfMonth;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                }
                else if (duedateoption.Text == "15th/END OF EACH MNTH")
                {
                    if (CMBPYNMTMODE.Text == "S-MONTHS")
                    {
                        prindate = pdd.AddDays(15);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "MONTHLY")
                    {
                        if (pdd > FEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now.AddMonths(- 1);
                            var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                            prindate = lastDayOfMonth;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBPYNMTMODE.Text == "QUARTERLY")
                    {
                        if (pdd > FEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now.AddMonths((-1)* 3);
                            var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                            prindate = lastDayOfMonth;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBPYNMTMODE.Text == "SEMESTRAL")
                    {
                        if (pdd > FEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now.AddMonths((-1)*6);
                            var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                            prindate = lastDayOfMonth;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBPYNMTMODE.Text == "YEARLY")
                    {
                        if (pdd > FEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now.AddMonths((-1)* 12);
                            var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                            prindate = lastDayOfMonth;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "SAME DAY OF EACH MNTH")
                {
                    if (CMBPYNMTMODE.Text == "MONTHLY")
                    {
                        prindate = pdd.AddMonths(1);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "QUARTERLY")
                    {
                        prindate = pdd.AddMonths(3);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "SEMESTRAL")
                    {
                        prindate = pdd.AddMonths(6);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "YEARLY")
                    {
                        prindate = pdd.AddMonths(12);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                }
                else if (duedateoption.Text == "5th/20th OF EACH MNTH")
                {
                    if (CMBPYNMTMODE.Text == "S-MONTHS")
                    {
                        prindate = pdd.AddDays(15);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "MONTHLY")
                    {
                        if (pdd > TEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 20);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 05);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBPYNMTMODE.Text == "QUARTERLY")
                    {
                        if (pdd > TEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 20);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 05);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBPYNMTMODE.Text == "SEMESTRAL")
                    {
                        if (pdd > TEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 20);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 05);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBPYNMTMODE.Text == "YEARLY")
                    {
                        if (pdd > TEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 20);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 05);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "10th/25th OF EACH MNTH")
                {
                    if (CMBPYNMTMODE.Text == "S-MONTHS")
                    {
                        prindate = pdd.AddDays(15);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "MONTHLY")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= TTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 10);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBPYNMTMODE.Text == "QUARTERLY")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= TTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 10);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBPYNMTMODE.Text == "SEMESTRAL")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= TTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 10);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBPYNMTMODE.Text == "YEARLY")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= TTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 10);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "END OF WEEK(FRIDAY)")
                {
                }
                else if (duedateoption.Text == "15TH/30TH OF EACH MONTH")
                {
                    if (CMBPYNMTMODE.Text == "S-MONTHS")
                    {
                        prindate = pdd.AddDays(15);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "MONTHLY")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= FTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 30);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBPYNMTMODE.Text == "QUARTERLY")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= FTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 30);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBPYNMTMODE.Text == "SEMESTRAL")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= FTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 30);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBPYNMTMODE.Text == "YEARLY")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= FTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 30);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "25TH THIS MONTH")
                {
                    if (pdd < thismonth)
                    {
                        if (CMBPYNMTMODE.Text == "MONTHLY")
                        {
                            DateTime now = DateTime.Now;
                            prindate = new DateTime(now.Year, now.Month, 25);
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (CMBPYNMTMODE.Text == "QUARTERLY")
                        {
                            DateTime now = DateTime.Now.AddMonths(2);
                            prindate = new DateTime(now.Year, now.Month, 25);
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (CMBPYNMTMODE.Text == "SEMESTRAL")
                        {
                            DateTime now = DateTime.Now.AddMonths(5);
                            prindate = new DateTime(now.Year, now.Month, 25);
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (CMBPYNMTMODE.Text == "YEARLY")
                        {
                            DateTime now = DateTime.Now.AddMonths(11);
                            prindate = new DateTime(now.Year, now.Month, 25);
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else
                    {
                        if (CMBPYNMTMODE.Text == "MONTHLY")
                        {
                            DateTime date = pdd.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (CMBPYNMTMODE.Text == "QUARTERLY")
                        {
                            DateTime date = pdd.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (CMBPYNMTMODE.Text == "SEMESTRAL")
                        {
                            DateTime date = pdd.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (CMBPYNMTMODE.Text == "YEARLY")
                        {
                            DateTime date = pdd.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "25TH NEXT MONTH")
                {
                    if (CMBPYNMTMODE.Text == "MONTHLY")
                    {
                        DateTime date = pdd.AddMonths(1);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        prindate = sameday;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "QUARTERLY")
                    {
                        DateTime date = pdd.AddMonths(3);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        prindate = sameday;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "SEMESTRAL")
                    {
                        DateTime date = pdd.AddMonths(6);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        prindate = sameday;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBPYNMTMODE.Text == "YEARLY")
                    {
                        DateTime date = pdd.AddMonths(12);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        prindate = sameday;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                }
            }
            else
            {
                if (duedateoption.Text == "DEFAULT")
                {
                    if (anntdimpymntmode.Text == "QUARTERLY")
                    {
                        prindate = pdd.AddDays(90);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (anntdimpymntmode.Text == "SEMI ANNUALLY")
                    {
                        prindate = pdd.AddDays(180);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (anntdimpymntmode.Text == "ANNUALLY")
                    {
                        prindate = pdd.AddDays(360);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (anntdimpymntmode.Text == "S-MONTHLY")
                    {
                        prindate = pdd.AddDays(15);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (anntdimpymntmode.Text == "MONTHLY EMB3")
                    {
                        prindate = pdd.AddDays(90);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (anntdimpymntmode.Text == "MONTHLY EMB6")
                    {
                        prindate = pdd.AddDays(180);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        prindate = pdd.AddDays(30);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                }
                else if (duedateoption.Text == "END OF MNTH")
                {
                    if (anntdimpymntmode.Text == "QUARTERLY")
                    {
                        var anydate = pdd.AddMonths(3);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        prindate = lastDayOfMonth;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (anntdimpymntmode.Text == "SEMI ANNUALLY")
                    {
                        var anydate = pdd.AddMonths(6);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        prindate = lastDayOfMonth;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (anntdimpymntmode.Text == "ANNUALLY")
                    {
                        var anydate = pdd.AddMonths(12);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        prindate = lastDayOfMonth;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (anntdimpymntmode.Text == "MONTHLY EMB3")
                    {
                        prindate = pdd.AddMonths(3);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (anntdimpymntmode.Text == "MONTHLY EMB6")
                    {
                        prindate = pdd.AddMonths(7);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        var anydate = pdd.AddMonths(1);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        prindate = lastDayOfMonth;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                }
                else if (duedateoption.Text == "15th/END OF EACH MNTH")////
                {
                    if (anntdimpymntmode.Text == "QUARTERLY")
                    {
                        if (pdd > FEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now.AddMonths(-1);
                            var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                            prindate = lastDayOfMonth;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (anntdimpymntmode.Text == "SEMI ANNUALLY")
                    {
                        if (pdd > FEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now.AddMonths(-1);
                            var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                            prindate = lastDayOfMonth;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (anntdimpymntmode.Text == "ANNUALLY")
                    {
                        if (pdd > FEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now.AddMonths(-1);
                            var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                            prindate = lastDayOfMonth;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else
                    {
                        if (pdd > FEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now.AddMonths(-1);
                            var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                            prindate = lastDayOfMonth;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "SAME DAY OF EACH MNTH")
                {
                    if (anntdimpymntmode.Text == "QUARTERLY")
                    {
                        prindate = pdd.AddMonths(3);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (anntdimpymntmode.Text == "SEMI ANNUALLY")
                    {
                        prindate = pdd.AddMonths(6);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (anntdimpymntmode.Text == "ANNUALLY")
                    {
                        prindate = pdd.AddMonths(12);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (anntdimpymntmode.Text == "MONTHLY EMB3")
                    {
                        prindate = pdd.AddMonths(3);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (anntdimpymntmode.Text == "MONTHLY EMB6")
                    {
                        prindate = pdd.AddMonths(7);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        prindate = pdd.AddMonths(1);
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                }
                else if (duedateoption.Text == "5th/20th OF EACH MNTH")
                {
                    if (anntdimpymntmode.Text == "QUARTERLY")
                    {
                        if (pdd > TEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 20);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 05);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (anntdimpymntmode.Text == "SEMI ANNUALLY")
                    {
                        if (pdd > TEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 20);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 05);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (anntdimpymntmode.Text == "ANNUALLY")
                    {
                        if (pdd > TEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 20);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 05);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else
                    {
                        if (pdd > TEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 20);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 05);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "10th/25th OF EACH MNTH")
                {
                    if (anntdimpymntmode.Text == "QUARTERLY")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= TTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 10);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (anntdimpymntmode.Text == "SEMI ANNUALLY")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= TTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 10);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (anntdimpymntmode.Text == "ANNUALLY")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= TTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 10);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else
                    {
                        if ((TTEOEM <= pdd) && (pdd <= TTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 10);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "END OF WEEK(FRIDAY)")
                {
                }
                else if (duedateoption.Text == "15TH/30TH OF EACH MONTH")
                {
                    if (anntdimpymntmode.Text == "QUARTERLY")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= FTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 30);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (anntdimpymntmode.Text == "SEMI ANNUALLY")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= FTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 30);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (anntdimpymntmode.Text == "ANNUALLY")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= FTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 30);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else
                    {
                        if ((TTEOEM <= pdd) && (pdd <= FTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 30);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "25TH THIS MONTH") // 
                {
                    if (pdd < thismonth)
                    {
                        if (anntdimpymntmode.Text == "QUARTERLY")
                        {
                            DateTime now = DateTime.Now.AddMonths(2);
                            DateTime sameday = new DateTime(now.Year, now.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (anntdimpymntmode.Text == "SEMI ANNUALLY")
                        {
                            DateTime now = DateTime.Now.AddMonths(5);
                            DateTime sameday = new DateTime(now.Year, now.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (anntdimpymntmode.Text == "ANNUALLY")
                        {
                            DateTime now = DateTime.Now.AddMonths(11);
                            DateTime sameday = new DateTime(now.Year, now.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (anntdimpymntmode.Text == "MONTHLY EMB3")
                        {
                            DateTime now = DateTime.Now.AddMonths(3);
                            DateTime sameday = new DateTime(now.Year, now.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (anntdimpymntmode.Text == "MONTHLY EMB6")
                        {
                            DateTime now = DateTime.Now.AddMonths(7);
                            DateTime sameday = new DateTime(now.Year, now.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime now = DateTime.Now;
                            DateTime sameday = new DateTime(now.Year, now.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else
                    {
                        if (anntdimpymntmode.Text == "QUARTERLY")
                        {
                            DateTime date = pdd.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (anntdimpymntmode.Text == "SEMI ANNUALLY")
                        {
                            DateTime date = pdd.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (anntdimpymntmode.Text == "ANNUALLY")
                        {
                            DateTime date = pdd.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (anntdimpymntmode.Text == "MONTHLY EMB3")
                        {
                            DateTime now = DateTime.Now.AddMonths(3);
                            DateTime sameday = new DateTime(now.Year, now.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (anntdimpymntmode.Text == "MONTHLY EMB6")
                        {
                            DateTime now = DateTime.Now.AddMonths(7);
                            DateTime sameday = new DateTime(now.Year, now.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = pdd.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "25TH NEXT MONTH")
                {
                    if (anntdimpymntmode.Text == "QUARTERLY")
                    {
                        DateTime date = pdd.AddMonths(3);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        prindate = sameday;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (anntdimpymntmode.Text == "SEMI ANNUALLY")
                    {
                        DateTime date = pdd.AddMonths(6);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        prindate = sameday;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (anntdimpymntmode.Text == "ANNUALLY")
                    {
                        DateTime date = pdd.AddMonths(12);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        prindate = sameday;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (anntdimpymntmode.Text == "MONTHLY EMB3")
                    {
                        DateTime now = DateTime.Now.AddMonths(3);
                        DateTime sameday = new DateTime(now.Year, now.Month, 25);
                        prindate = sameday;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (anntdimpymntmode.Text == "MONTHLY EMB6")
                    {
                        DateTime now = DateTime.Now.AddMonths(7);
                        DateTime sameday = new DateTime(now.Year, now.Month, 25);
                        prindate = sameday;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        DateTime date = pdd.AddMonths(1);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        prindate = sameday;
                        principalduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                }
            }
        }
        #endregion
        #region INTEREST DUEDATE
        public void interestduedate(ComboBox combobaseon, ComboBox duedateoption,
            ComboBox interstpymtMODE, MaskedTextBox interestduedate, MaskedTextBox loandate)
        {
            DateTime prindate;
            DateTime pdd = Convert.ToDateTime(loandate.Text);
            DateTime thismonth = new DateTime(pdd.Year, pdd.Month, 25);

            DateTime FEOEM = new DateTime(pdd.Year, pdd.Month, 10);//

            DateTime TEOEM = new DateTime(pdd.Year, pdd.Month, 15);//

            DateTime TTEOEM = new DateTime(pdd.Year, pdd.Month, 06);//
            DateTime TTTEOEM = new DateTime(pdd.Year, pdd.Month, 20);

            DateTime FTEOEM = new DateTime(pdd.Year, pdd.Month, 11);//
            DateTime FTTEOEM = new DateTime(pdd.Year, pdd.Month, 25);
            if (combobaseon.Text == "STRAIGHT LINE")
            {
                if (duedateoption.Text == "DEFAULT")
                {
                    if (interstpymtMODE.Text == "DAILY")
                    {
                        prindate = pdd.AddDays(1);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "WEEKLY")
                    {
                        prindate = pdd.AddDays(7);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "S-MONTHS")
                    {
                        prindate = pdd.AddDays(15);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "MONTHLY")
                    {
                        prindate = pdd.AddDays(30);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "QUARTERLY")
                    {
                        prindate = pdd.AddDays(90);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "SEMESTRAL")
                    {
                        prindate = pdd.AddDays(180);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "YEARLY")
                    {
                        prindate = pdd.AddDays(360);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                }
                else if (duedateoption.Text == "END OF MNTH")
                {
                    if (interstpymtMODE.Text == "MONTHLY")
                    {
                        var anydate = pdd.AddMonths(1);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        prindate = lastDayOfMonth;
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "QUARTERLY")
                    {
                        var anydate = pdd.AddMonths(3);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        prindate = lastDayOfMonth;
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "SEMESTRAL")
                    {
                        var anydate = pdd.AddMonths(6);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        prindate = lastDayOfMonth;
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "YEARLY")
                    {
                        var anydate = pdd.AddMonths(12);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        prindate = lastDayOfMonth;
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                }
                else if (duedateoption.Text == "15th/END OF EACH MNTH")
                {
                    if (interstpymtMODE.Text == "S-MONTHS")
                    {
                        if (pdd > FEOEM)
                        {
                            DateTime date = DateTime.Now;
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now;
                            var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                            prindate = lastDayOfMonth;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "SAME DAY OF EACH MNTH")
                {
                    if (interstpymtMODE.Text == "MONTHLY")
                    {
                        prindate = pdd.AddMonths(1);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "QUARTERLY")
                    {
                        prindate = pdd.AddMonths(3);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "SEMESTRAL")
                    {
                        prindate = pdd.AddMonths(6);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "YEARLY")
                    {
                        prindate = pdd.AddMonths(12);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                }
                else if (duedateoption.Text == "5th/20th OF EACH MNTH")
                {
                    if (interstpymtMODE.Text == "S-MONTHS")
                    {
                        if (pdd > TEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 20);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 05);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "10th/25th OF EACH MNTH")
                {
                   if (interstpymtMODE.Text == "S-MONTHS")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= TTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 10);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "END OF WEEK(FRIDAY)")
                {
                }
                else if (duedateoption.Text == "15TH/30TH OF EACH MONTH")
                {
                    if (interstpymtMODE.Text == "S-MONTHS")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= FTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 30);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "25TH THIS MONTH")
                {
                    if (pdd < thismonth)
                    {
                        if (interstpymtMODE.Text == "MONTHLY")
                        {
                            DateTime now = DateTime.Now;
                            prindate = new DateTime(now.Year, now.Month, 25);
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (interstpymtMODE.Text == "QUARTERLY")
                        {
                            DateTime now = DateTime.Now.AddMonths(2);
                            prindate = new DateTime(now.Year, now.Month, 25);
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (interstpymtMODE.Text == "SEMESTRAL")
                        {
                            DateTime now = DateTime.Now.AddMonths(5);
                            prindate = new DateTime(now.Year, now.Month, 25);
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (interstpymtMODE.Text == "YEARLY")
                        {
                            DateTime now = DateTime.Now.AddMonths(11);
                            prindate = new DateTime(now.Year, now.Month, 25);
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else
                    {
                        if (interstpymtMODE.Text == "MONTHLY")
                        {
                            DateTime date = pdd.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (interstpymtMODE.Text == "QUARTERLY")
                        {
                            DateTime date = pdd.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (interstpymtMODE.Text == "SEMESTRAL")
                        {
                            DateTime date = pdd.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (interstpymtMODE.Text == "YEARLY")
                        {
                            DateTime date = pdd.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "25TH NEXT MONTH")
                {
                    if (interstpymtMODE.Text == "MONTHLY")
                    {
                        DateTime date = pdd.AddMonths(1);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        prindate = sameday;
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "QUARTERLY")
                    {
                        DateTime date = pdd.AddMonths(3);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        prindate = sameday;
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "SEMESTRAL")
                    {
                        DateTime date = pdd.AddMonths(6);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        prindate = sameday;
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "YEARLY")
                    {
                        DateTime date = pdd.AddMonths(12);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        prindate = sameday;
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                }

            }
            else
            {
                if (duedateoption.Text == "DEFAULT")
                {
                    if (interstpymtMODE.Text == "QUARTERLY")
                    {
                        prindate = pdd.AddDays(90);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "SEMESTRAL")
                    {
                        prindate = pdd.AddDays(180);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "YEARLY")
                    {
                        prindate = pdd.AddDays(360);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "S-MONTHS")
                    {
                        prindate = pdd.AddDays(15);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "MONTHLY")
                    {
                        prindate = pdd.AddDays(30);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                }
                else if (duedateoption.Text == "END OF MNTH")
                {
                    if (interstpymtMODE.Text == "QUARTERLY")
                    {
                        var anydate = pdd.AddMonths(3);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        prindate = lastDayOfMonth;
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "SEMESTRAL")
                    {
                        var anydate = pdd.AddMonths(6);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        prindate = lastDayOfMonth;
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "YEARLY")
                    {
                        var anydate = pdd.AddMonths(12);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        prindate = lastDayOfMonth;
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "MONTHLY")
                    {
                        var anydate = pdd.AddMonths(1);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        prindate = lastDayOfMonth;
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                }
                else if (duedateoption.Text == "15th/END OF EACH MNTH")
                {
                    if (interstpymtMODE.Text == "S-MONTHS")
                    {
                        if (pdd > FEOEM)
                        {
                            var anydate = DateTime.Now;
                            var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                            prindate = lastDayOfMonth;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now;
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "SAME DAY OF EACH MNTH")
                {
                    if (interstpymtMODE.Text == "QUARTERLY")
                    {
                        prindate = pdd.AddMonths(3);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "SEMESTRAL")
                    {
                        prindate = pdd.AddMonths(6);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "YEARLY")
                    {
                        prindate = pdd.AddMonths(12);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "MONTHLY")
                    {
                        prindate = pdd.AddMonths(1);
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                }
                else if (duedateoption.Text == "5th/20th OF EACH MNTH")
                {
                    if (interstpymtMODE.Text == "S-MONTHS")
                    {
                        if (pdd > TEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 05);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 20);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "10th/25th OF EACH MNTH")
                {
                    if (interstpymtMODE.Text == "S-MONTHS")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= TTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 10);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "END OF WEEK(FRIDAY)")
                {
                }
                else if (duedateoption.Text == "15TH/30TH OF EACH MONTH")
                {
                    if (interstpymtMODE.Text == "S-MONTHS")
                    {
                        if ((TTEOEM <= pdd) && (pdd <= FTTEOEM))
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 30);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "25TH THIS MONTH")
                {
                    if (pdd < thismonth)
                    {
                        if (interstpymtMODE.Text == "QUARTERLY")
                        {
                            DateTime now = DateTime.Now.AddMonths(2);
                            DateTime sameday = new DateTime(now.Year, now.Month, 25);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (interstpymtMODE.Text == "SEMESTRAL")
                        {
                            DateTime now = DateTime.Now.AddMonths(5);
                            DateTime sameday = new DateTime(now.Year, now.Month, 25);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (interstpymtMODE.Text == "YEARLY")
                        {
                            DateTime now = DateTime.Now.AddMonths(11);
                            DateTime sameday = new DateTime(now.Year, now.Month, 25);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (interstpymtMODE.Text == "MONTHLY")
                        {
                            DateTime now = DateTime.Now;
                            DateTime sameday = new DateTime(now.Year, now.Month, 25);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                    else 
                    {
                        if (interstpymtMODE.Text == "QUARTERLY")
                        {
                            DateTime date = pdd.AddMonths(3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (interstpymtMODE.Text == "SEMESTRAL")
                        {
                            DateTime date = pdd.AddMonths(6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (interstpymtMODE.Text == "YEARLY")
                        {
                            DateTime date = pdd.AddMonths(12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                        else if (interstpymtMODE.Text == "MONTHLY")
                        {
                            DateTime date = pdd.AddMonths(1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            prindate = sameday;
                            interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (duedateoption.Text == "25TH NEXT MONTH")
                {
                    if (interstpymtMODE.Text == "QUARTERLY")
                    {
                        DateTime date = pdd.AddMonths(3);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        prindate = sameday;
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "SEMESTRAL")
                    {
                        DateTime date = pdd.AddMonths(6);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        prindate = sameday;
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "YEARLY")
                    {
                        DateTime date = pdd.AddMonths(12);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        prindate = sameday;
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                    else if (interstpymtMODE.Text == "MONTHLY")
                    {
                        DateTime date = pdd.AddMonths(1);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        prindate = sameday;
                        interestduedate.Text = prindate.ToString("MM/dd/yyyy");
                    }
                }
            }
        }
        #endregion
        #region MATURITY DATE
        public void Maturitydate(MaskedTextBox MSKLOANDATE, TextBox TERM, ComboBox DUEDATEOPT, ComboBox CMBTERM, ComboBox CMBPYNMTMODE,
            MaskedTextBox MATURITYDATE1)
        {
            //////////KUWANG PA 
            int terms;
            DateTime newdate;
            DateTime ld = Convert.ToDateTime(MSKLOANDATE.Text);
            DateTime thismonth = new DateTime(ld.Year, ld.Month, 25);


            DateTime FEOEM = new DateTime(ld.Year, ld.Month, 10);//

            DateTime TEOEM = new DateTime(ld.Year, ld.Month, 15);//

            DateTime TTEOEM = new DateTime(ld.Year, ld.Month, 06);//
            DateTime TTTEOEM = new DateTime(ld.Year, ld.Month, 20);

            DateTime FTEOEM = new DateTime(ld.Year, ld.Month, 11);//
            DateTime FTTEOEM = new DateTime(ld.Year, ld.Month, 25);
            int.TryParse(TERM.Text, out terms);

            if (TERM.Text == "" && TERM.Text == "0")
            {
                MATURITYDATE1.Text = "";
                TERM.Focus();
            }
            else
            {

                if (DUEDATEOPT.Text == "DEFAULT")
                {
                    if (CMBTERM.Text == "DAYS")
                    {
                        newdate = ld.AddDays(terms);

                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");

                    }
                    else if (CMBTERM.Text == "WEEKS")
                    {
                        if (CMBPYNMTMODE.Text == "S-MONTHS")
                        {
                            newdate = ld.AddDays((terms * 7.5));
                        }
                        else
                        {
                            newdate = ld.AddDays(terms * 7);
                        }
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBTERM.Text == "MONTHS")
                    {
                        if (CMBPYNMTMODE.Text == "WEEKLY")
                        {
                            newdate = ld.AddDays((terms * 4) * 7);
                        }
                        else if (CMBPYNMTMODE.Text == "S-MONTHS")
                        {
                            newdate = ld.AddDays((terms * 2) * 15);
                        }
                        else
                        {
                            newdate = ld.AddDays(30 * terms);
                        }
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBTERM.Text == "S-MONTHS")
                    {
                        if (CMBPYNMTMODE.Text == "WEEKLY")
                        {
                            newdate = ld.AddDays((terms * 2) * 7);
                        }
                        else
                        {
                            newdate = ld.AddDays(15 * terms);
                        }
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");

                    }
                    else if (CMBTERM.Text == "QUARTERS")
                    {
                        if (CMBPYNMTMODE.Text == "WEEKLY")
                        {
                            newdate = ld.AddDays(((terms * 4) * 7) * 3);
                        }
                        else
                        {
                            newdate = ld.AddDays(90 * terms);

                        }
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");

                    }
                    else if (CMBTERM.Text == "SEMESTERS")
                    {
                        if (CMBPYNMTMODE.Text == "WEEKLY")
                        {
                            newdate = ld.AddDays(((terms * 26) * 7));
                        }
                        else
                        {
                            newdate = ld.AddDays(180 * terms);
                        }
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBTERM.Text == "YEARS")
                    {
                        if (CMBPYNMTMODE.Text == "WEEKLY")
                        {
                            newdate = ld.AddDays((terms * 52) * 7);
                        }
                        else
                        {
                            newdate = ld.AddDays(360 * terms);
                        }
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                }
                else if (DUEDATEOPT.Text == "15th/END OF EACH MNTH")
                {
                    if (CMBTERM.Text == "S-MONTHS")
                    {
                        if (ld > FEOEM)
                        {
                            DateTime date = DateTime.Now.AddDays(15);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15); //// wala pa 
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now.AddMonths(terms - 1);
                            var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                            newdate = lastDayOfMonth;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBTERM.Text == "MONTHS")
                    {
                        if (ld > FEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(terms);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now.AddMonths(terms - 1);
                            var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                            newdate = lastDayOfMonth;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBTERM.Text == "QUARTERS")
                    {
                        if (ld > FEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(terms * 3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now.AddMonths((terms - 1) * 3);
                            var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                            newdate = lastDayOfMonth;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBTERM.Text == "SEMESTERS")
                    {
                        if (ld > FEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(terms * 6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now.AddMonths((terms - 1) * 6);
                            var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                            newdate = lastDayOfMonth;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBTERM.Text == "YEARS")
                    {
                        if (ld > FEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(terms * 12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 15);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now.AddMonths((terms - 1) * 12);
                            var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                            newdate = lastDayOfMonth;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (DUEDATEOPT.Text == "END OF MNTH")
                {
                    if (CMBTERM.Text == "MONTHS")
                    {
                        var anydate = DateTime.Now.AddMonths(1 * terms);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        newdate = lastDayOfMonth;
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBTERM.Text == "QUARTERS")
                    {
                        var anydate = DateTime.Now.AddMonths(3 * terms);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        newdate = lastDayOfMonth;
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBTERM.Text == "SEMESTERS")
                    {
                        var anydate = DateTime.Now.AddMonths(6 * terms);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        newdate = lastDayOfMonth;
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBTERM.Text == "YEARS")
                    {
                        var anydate = DateTime.Now.AddMonths(12 * terms);
                        var lastDayOfMonth = anydate.AddDays(1 - anydate.Day).AddMonths(1).AddDays(-1).Date;
                        newdate = lastDayOfMonth;
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                }
                else if (DUEDATEOPT.Text == "SAME DAY OF EACH MNTH")
                {
                    if (CMBTERM.Text == "MONTHS")
                    {
                        newdate = ld.AddMonths(1 * terms);
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBTERM.Text == "QUARTERS")
                    {
                        newdate = ld.AddMonths(3 * terms);
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBTERM.Text == "SEMESTERS")
                    {
                        newdate = ld.AddMonths(6 * terms);
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBTERM.Text == "YEARS")
                    {
                        newdate = ld.AddMonths(12 * terms);
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                }
                else if (DUEDATEOPT.Text == "5th/20th OF EACH MNTH")
                {
                    if (CMBTERM.Text == "S-MONTHS")
                    {
                        newdate = ld.AddDays((terms * 2) * 15); // wala pa 
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBTERM.Text == "MONTHS")
                    {
                        if (ld > TEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(terms);
                            DateTime sameday = new DateTime(date.Year, date.Month, 20);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now.AddMonths(terms * 1);
                            DateTime sameday = new DateTime(anydate.Year, anydate.Month, 05);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBTERM.Text == "QUARTERS")
                    {
                        if (ld > TEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(terms * 3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 20);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now.AddMonths((terms + 1) * 3);
                            DateTime sameday = new DateTime(anydate.Year, anydate.Month, 05);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBTERM.Text == "SEMESTERS")
                    {
                        if (ld > TEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(terms * 6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 20);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now.AddMonths((terms + 1) * 6);
                            DateTime sameday = new DateTime(anydate.Year, anydate.Month, 05);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBTERM.Text == "YEARS")
                    {
                        if (ld > TEOEM)
                        {
                            DateTime date = DateTime.Now.AddMonths(terms * 12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 20);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            var anydate = DateTime.Now.AddMonths((terms + 1) * 12);
                            DateTime sameday = new DateTime(anydate.Year, anydate.Month, 05);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (DUEDATEOPT.Text == "10th/25th OF EACH MNTH")
                {
                    if (CMBTERM.Text == "S-MONTHS")
                    {
                        newdate = ld.AddDays((terms * 2) * 15); // wala pa 
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBTERM.Text == "MONTHS")
                    {
                        if ((TTEOEM <= ld) && (ld <= TTTEOEM))
                        {
                            var anydate = DateTime.Now.AddMonths(terms * 1);
                            DateTime sameday = new DateTime(anydate.Year, anydate.Month, 10);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(terms);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBTERM.Text == "QUARTERS")
                    {
                        if ((TTEOEM <= ld) && (ld <= TTTEOEM))
                        {
                            var anydate = DateTime.Now.AddMonths(terms * 3);
                            DateTime sameday = new DateTime(anydate.Year, anydate.Month, 10);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(terms * 3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBTERM.Text == "SEMESTERS")
                    {
                        if ((TTEOEM <= ld) && (ld <= TTTEOEM))
                        {
                            var anydate = DateTime.Now.AddMonths(terms * 6);
                            DateTime sameday = new DateTime(anydate.Year, anydate.Month, 10);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(terms * 6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBTERM.Text == "YEARS")
                    {
                        if ((TTEOEM <= ld) && (ld <= TTTEOEM))
                        {
                            var anydate = DateTime.Now.AddMonths(terms * 12);
                            DateTime sameday = new DateTime(anydate.Year, anydate.Month, 10);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(terms * 12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (DUEDATEOPT.Text == "15TH/30TH OF EACH MONTH")
                {
                    if (CMBTERM.Text == "S-MONTHS")
                    {
                        newdate = ld.AddDays((terms * 2) * 15); // wala pa 
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBTERM.Text == "MONTHS")
                    {
                        if ((TTEOEM <= ld) && (ld <= FTTEOEM))
                        {
                            var anydate = DateTime.Now.AddMonths(terms * 1);
                            DateTime sameday = new DateTime(anydate.Year, anydate.Month, 15);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(terms);
                            DateTime sameday = new DateTime(date.Year, date.Month, 30);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBTERM.Text == "QUARTERS")
                    {
                        if ((TTEOEM <= ld) && (ld <= FTTEOEM))
                        {
                            var anydate = DateTime.Now.AddMonths(terms * 3);
                            DateTime sameday = new DateTime(anydate.Year, anydate.Month, 15);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(terms * 3);
                            DateTime sameday = new DateTime(date.Year, date.Month, 30);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBTERM.Text == "SEMESTERS")
                    {
                        if ((FTEOEM <= ld) && (ld <= FTTEOEM))
                        {
                            var anydate = DateTime.Now.AddMonths(terms * 6);
                            DateTime sameday = new DateTime(anydate.Year, anydate.Month, 15);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(terms * 6);
                            DateTime sameday = new DateTime(date.Year, date.Month, 30);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                    else if (CMBTERM.Text == "YEARS")
                    {
                        if ((FTEOEM <= ld) && (ld <= FTTEOEM))
                        {
                            var anydate = DateTime.Now.AddMonths(terms * 12);
                            DateTime sameday = new DateTime(anydate.Year, anydate.Month, 15);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.Now.AddMonths(terms * 12);
                            DateTime sameday = new DateTime(date.Year, date.Month, 30);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (DUEDATEOPT.Text == "25TH THIS MONTH")
                {
                    if (ld < thismonth)
                    {
                        if (CMBTERM.Text == "MONTHS")
                        {
                            DateTime date = DateTime.Now.AddMonths(terms - 1);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else if (CMBTERM.Text == "QUARTERS")
                        {
                            DateTime date = DateTime.Now.AddMonths(2 * terms);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else if (CMBTERM.Text == "SEMESTERS")
                        {
                            DateTime date = DateTime.Now.AddMonths(5 * terms);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else if (CMBTERM.Text == "YEARS")
                        {
                            DateTime date = DateTime.Now.AddMonths(11 * terms);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                    else
                    {
                        if (CMBTERM.Text == "MONTHS")
                        {
                            DateTime date = ld.AddMonths(1 * terms);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else if (CMBTERM.Text == "QUARTERS")
                        {
                            DateTime date = ld.AddMonths(3 * terms);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else if (CMBTERM.Text == "SEMESTERS")
                        {
                            DateTime date = ld.AddMonths(6 * terms);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                        else if (CMBTERM.Text == "YEARS")
                        {
                            DateTime date = ld.AddMonths(12 * terms);
                            DateTime sameday = new DateTime(date.Year, date.Month, 25);
                            newdate = sameday;
                            MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                        }
                    }
                }
                else if (DUEDATEOPT.Text == "25TH NEXT MONTH")
                {
                    if (CMBTERM.Text == "MONTHS")
                    {
                        DateTime date = ld.AddMonths(1 * terms);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        newdate = sameday;
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBTERM.Text == "QUARTERS")
                    {
                        DateTime date = ld.AddMonths(3 * terms);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        newdate = sameday;
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBTERM.Text == "SEMESTERS")
                    {
                        DateTime date = ld.AddMonths(6 * terms);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        newdate = sameday;
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                    else if (CMBTERM.Text == "YEARS")
                    {
                        DateTime date = ld.AddMonths(12 * terms);
                        DateTime sameday = new DateTime(date.Year, date.Month, 25);
                        newdate = sameday;
                        MATURITYDATE1.Text = newdate.ToString("MM/dd/yyyy");
                    }
                }
            }
        }
        #endregion
        #region SL INTEREST
        public void STRAIGHTLINEINTEREST(TextBox txtinterest, TextBox txtprincipal, TextBox txtterm, Label lbinterest, ComboBox cmbpymtmodeint,
             ComboBox cmbterm, TextBox whleyearinterest)
        {
            double interest, loanamount, totalint, term, wholeint;
            double.TryParse(txtinterest.Text, out interest);
            double.TryParse(txtprincipal.Text, out loanamount);
            double.TryParse(txtterm.Text, out term);
            double.TryParse(lbinterest.Text, out totalint);
            double.TryParse(whleyearinterest.Text, out wholeint);

            if (cmbpymtmodeint.Text == "DAILY")
            {
                wholeint = (((((interest / 100) * loanamount) * term) * 30) / 360);
                totalint = (wholeint / (term * 30));
            }
            else if (cmbpymtmodeint.Text == "S-MONTHS")
            {
                if (cmbterm.Text == "WEEKS")
                {
                    if (term < 2)
                    {
                        totalint = (((((interest / 100) / 24) * loanamount) * term) / 2);
                    }
                    else
                    {
                        totalint = (((interest / 100) * loanamount) / 24);
                    }
                }
                else if (cmbterm.Text == "DAYS")
                {
                    if (term < 15)
                    {
                        totalint = ((((interest / 100) * loanamount) * term) / 360);
                    }
                    else
                    {
                        totalint = (((interest / 100) * loanamount) / 24);
                    }
                }
                else
                {
                    totalint = (((interest / 100) * loanamount) / 24);

                }
                wholeint = ((((interest / 100) * loanamount) * term) / 12);
            }
            else if (cmbpymtmodeint.Text == "MONTHLY")
            {
                if (cmbterm.Text == "WEEKS")
                {
                    totalint = (((interest / 100) * loanamount) / 52);
                }
                else if (cmbterm.Text == "S-MONTHS")
                {
                    if (term < 2)
                    {
                        totalint = (((((interest / 100) / 12) * loanamount) * term) / 4);
                    }
                    else
                    {
                        totalint = (((interest / 100) * loanamount) / 12);
                    }
                    wholeint = ((((interest / 100) * loanamount) * term) / 12);
                }
                else if (cmbterm.Text == "DAYS")
                {
                    if (term < 31)
                    {
                        totalint = ((((interest / 100) * loanamount) * term) / 360);
                    }
                    else
                    {
                        totalint = ((((interest / 100) * loanamount) * 30) / 360);
                    }
                }
                else
                {
                    totalint = (((interest / 100) * loanamount) / 12);
                }
                wholeint = ((((interest / 100) * loanamount) * term) / 12);
            }
            else if (cmbpymtmodeint.Text == "QUARTERLY")
            {
                if (cmbterm.Text == "WEEKS")
                {

                    totalint = (((interest / 100) * loanamount) / 52);

                }
                else if (cmbterm.Text == "S-MONTHS")
                {
                    if (term < 6)
                    {
                        totalint = (((((interest / 100) / 12) * loanamount) * term) / 2);
                    }
                    else
                    {
                        totalint = (((interest / 100) * loanamount) / 4);
                    }
                }
                else if (cmbterm.Text == "DAYS")
                {
                    if (term < 91)
                    {
                        totalint = ((((interest / 100) * loanamount) * term) / 360);
                    }
                    else
                    {
                        totalint = ((((interest / 100) * loanamount) * 91) / 360);
                    }
                }
                else if (cmbterm.Text == "MONTHS")
                {
                    if (term < 3)
                    {
                        totalint = (((((interest / 100) * loanamount) / 4) * term) / 3);
                    }
                    else
                    {
                        totalint = (((interest / 100) * loanamount) / 4);
                    }
                }
                else
                {
                    totalint = (((interest / 100) * loanamount) / 4);
                }
                wholeint = ((((interest / 100) * loanamount) * term) / 12);
            }
            else if (cmbpymtmodeint.Text == "SEMESTRAL")
            {
                if (cmbterm.Text == "WEEKS")
                {

                    totalint = (((interest / 100) * loanamount) / 52);

                }
                else if (cmbterm.Text == "S-MONTHS")
                {
                    if (term < 12)
                    {
                        totalint = (((((interest / 100) * loanamount) / 2) * term) / 12);
                    }
                    else
                    {
                        totalint = (((interest / 100) * loanamount) / 2);
                    }
                }
                else if (cmbterm.Text == "DAYS")
                {
                    if (term < 190)
                    {
                        totalint = ((((interest / 100) * loanamount) * term) / 360);
                    }
                    else
                    {
                        totalint = ((((interest / 100) * loanamount) * 191) / 360);
                    }
                }
                else if (cmbterm.Text == "MONTHS")
                {
                    if (term < 6)
                    {
                        totalint = ((((interest / 100) * loanamount) / 12) * term);
                    }
                    else
                    {
                        totalint = (((interest / 100) * loanamount) / 2);
                    }
                }
                else if (cmbterm.Text == "QUARTERS")
                {
                    if (term < 2)
                    {
                        totalint = (((((interest / 100) * loanamount) / 2) * term) / 2);
                    }
                    else
                    {
                        totalint = (((interest / 100) * loanamount) / 2);
                    }
                }
                else
                {
                    totalint = (((interest / 100) * loanamount) / 2);
                }
                wholeint = ((((interest / 100) * loanamount) * term) / 12);
            }
            else if (cmbpymtmodeint.Text == "YEARLY")
            {
                if (cmbterm.Text == "WEEKS")
                {
                    if (term < 52)
                    {
                        totalint = ((((interest / 100) * loanamount) / 52) * term);
                    }
                    else
                    {
                        totalint = ((interest / 100) * loanamount);
                    }
                }
                else if (cmbterm.Text == "S-MONTHS")
                {
                    if (term < 24)
                    {
                        totalint = ((((interest / 100) * loanamount) / 24) * term);
                    }
                    else
                    {
                        totalint = ((interest / 100) * loanamount);
                    }
                }
                else if (cmbterm.Text == "DAYS")
                {
                    if (term < 360)
                    {
                        totalint = ((((interest / 100) * loanamount) * term) / 360);
                    }
                    else
                    {
                        totalint = ((((interest / 100) * loanamount) * 361) / 360);
                    }
                }
                else if (cmbterm.Text == "MONTHS")
                {
                    if (term < 12)
                    {
                        totalint = ((((interest / 100) * loanamount) / 12) * term);
                    }
                    else
                    {
                        totalint = ((interest / 100) * loanamount);
                    }
                }
                else if (cmbterm.Text == "QUARTERS")
                {
                    if (term < 4)
                    {
                        totalint = ((((interest / 100) * loanamount) / 4) * term);
                    }
                    else
                    {
                        totalint = ((interest / 100) * loanamount);
                    }
                }
                else if (cmbterm.Text == "SEMESTERS")
                {
                    if (term < 2)
                    {
                        totalint = ((((interest / 100) * loanamount) / 2) * term);
                    }
                    else
                    {
                        totalint = ((interest / 100) * loanamount);
                    }
                }
                else
                {
                    totalint = ((interest / 100) * loanamount);
                }
                wholeint = ((((interest / 100) * loanamount) * term) / 12);
            }
            else if (cmbpymtmodeint.Text == "WEEKLY")
            {
                if (cmbterm.Text == "WEEKS")
                {

                    totalint = (((interest / 100) * loanamount) / 52);

                }
                else if (cmbterm.Text == "DAYS")
                {
                    if (term < 15)
                    {
                        totalint = (((((interest / 100) * loanamount) * term) / 24) / 2);
                    }
                    else
                    {
                        totalint = (((interest / 100) * loanamount) / 24);
                    }
                }
                else
                {
                    totalint = (((interest / 100) * loanamount) / 52);
                }
                wholeint = (((((interest / 100) * loanamount) * term) * 4) / 52);
            }

            lbinterest.Text = string.Format("{0:N2}", Math.Round(Convert.ToDouble(totalint), 2));
            whleyearinterest.Text = Math.Round(Convert.ToDouble(wholeint), 2).ToString();
        }
        #endregion
        #region LOAD
       

        public void LoanType(string ltype, TextBox term, TextBox interest, TextBox penalty, ComboBox baseon,
            ComboBox prinpymntmode, ComboBox intpymntmode, ComboBox ddoptn, ComboBox pymntterm , ComboBox pterm , TextBox gperiod)
        {
            string _gendata = "";
            _gendata = @"SELECT  *  FROM [@LOANSET]  WHERE CODE ='" + ltype.ToString() +"'";
            DataTable _sqltable = new DataTable();
            _sqltable = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _gendata);

            foreach (DataRow row in _sqltable.Rows)
            {
                term.Text = row["Name"].ToString();
                interest.Text = row["U_INTEREST"].ToString();
                penalty.Text = row["U_PENALTY"].ToString();
                baseon.Text = row["U_BASEON"].ToString();
                intpymntmode.Text = row["U_ITNPMODE"].ToString();
              //  prinpymntmode.Text = row["U_PRINMODE"].ToString();
                ddoptn.Text = row["U_DDOPT"].ToString();
                pymntterm.Text = row["U_PTERM"].ToString();
                pterm.Text = row["U_INTEREST"].ToString();
                gperiod.Text = row["U_GPERIOD"].ToString();
            }
        }
        #endregion
    }
}
