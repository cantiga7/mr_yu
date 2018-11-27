using System;
using System.Windows.Forms;

namespace FINAL_LOAN_PACKAGING
{
    public partial class LOAN_MASTER : Form
    {
        MYCLASS.CLASS_LOAN mc = new MYCLASS.CLASS_LOAN();
        MYCLASS.CLASS_REPORT cc = new MYCLASS.CLASS_REPORT();

        public static string mystring = "";
        public static string myDocEntry = "";
        public static string Myposted = "";
        TextBox address;
        TextBox txtwholeyearinterest;

        public LOAN_MASTER()
        {
            InitializeComponent();
            address = new TextBox();
            txtwholeyearinterest = new TextBox();
        }
        private void LOAN_MASTER_Load(object sender, EventArgs e)
        {
            panel6.Hide();

            this.Refresh();
            disable();

            lbdate.Text = DateTime.Now.ToLongDateString();
            cmbdiminish.Enabled = false;

            mskinterestduedate.Enabled = false;
            mskduematurdue.Enabled = false;
            mskprinduedate.Enabled = false;

            txtlampsum.Hide();
            cmblampsum.Hide();
            txtintlampsum.Hide();
            cmbintlampsum.Hide();

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                mskinterestduedate.Enabled = true;
            }
            else
            {
                mskinterestduedate.Enabled = false;
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                mskprinduedate.Enabled = true;
            }
            else
            {
                mskprinduedate.Enabled = false;
            }
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                mskduematurdue.Enabled = true;
            }
            else
            {
                mskduematurdue.Enabled = false;
            }
        }
        private void txtprincipal_TextChanged(object sender, EventArgs e)
        {
            if (txtprincipal.Text == "" || txtprincipal.Text == "0")
            {
                lbprinamount.Text = "----";
            }
            else
            {
                //if (System.Text.RegularExpressions.Regex.IsMatch(txtprincipal.Text, "  ^ [0-9]"))
                //{
                //    txtprincipal.Text = "";
                //}
                //else
                //{
                //    if (!string.IsNullOrEmpty(txtprincipal.Text))
                //    {

                //        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US");
                //        int valueBefore = Int32.Parse(txtprincipal.Text, System.Globalization.NumberStyles.AllowThousands);
                //        txtprincipal.Text = String.Format(culture, "{0:N0}", valueBefore);
                //        txtprincipal.Select(txtprincipal.Text.Length, 0);
                //    }
                //}
                lbprinamount.Text = txtprincipal.Text;
                if (lbdocnum.Text == "______")
                {
                    lbprinamount.Text = "----";
                }
                else
                {
                    PRINCIPAL();
                    STRAIGHTLINEINTEREST();
                }
            }
        }
        private void INTERESTDUEDATE()
        {
            mc.interestduedate(cmbbaseno, cmbduedateopt, cmbintpymtmode, mskinterestduedate, mskloandate);
        }
        private void MATURITYDATE()
        {
            mc.Maturitydate(mskloandate, txtterm, cmbduedateopt, cmbterm, cmbprinpymntmode, mskmaturitydate);
        }
        private void PRINCIPAL()
        {
            mc.principalgenerate(txtprincipal, txtterm, lbprinamount, cmbterm, cmbprinpymntmode, cmbbaseno, cmbdiminish, cmbintpymtmode, txtinterest);
        }
        private void PRINCIPALDUEDATE()
        {
            mc.principalduedate(cmbbaseno, cmbdiminish, cmbduedateopt, cmbprinpymntmode, mskprinduedate, mskloandate);
        }
        private void STRAIGHTLINEINTEREST()
        {
            mc.STRAIGHTLINEINTEREST(txtinterest, txtprincipal, txtterm, lbinterest, cmbintpymtmode, cmbterm, txtwholeyearinterest);
        }
        private void button1_Click(object sender, EventArgs e)
        {

            SEARCH_FORM sf = new SEARCH_FORM();
            sf.ShowDialog();

            if (sf.DialogResult == DialogResult.OK)
            {
                this.lbdocnum.Text = LOAN_MASTER.mystring;
                this.lblDocEntry.Text = LOAN_MASTER.myDocEntry;

            }

        }
        private void cmbpymtmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            PRINCIPAL();
            MATURITYDATE();
            STRAIGHTLINEINTEREST();
            PRINCIPALDUEDATE();
            if (cmbprinpymntmode.Text == "EVERY NP")
            {
                cmbprinpymntmode.Width = 80;

                txtlampsum.Show();
                cmblampsum.Show();
                txtlampsum.Focus();
            }
            else
            {
                txtlampsum.Hide();
                cmblampsum.Hide();
                txtlampsum.Clear();

                cmbprinpymntmode.Width = 192;
            }

        }
        private void txtterm_TextChanged(object sender, EventArgs e)
        {
            double terms;
            if (txtterm.Text == "0" || txtterm.Text == null)
            {
                mskmaturitydate.Clear();
                lbprinamount.Text = "----";
                lbprinamount.Text = txtprincipal.Text;
            }
            else
            {

                if (!double.TryParse(txtterm.Text, out terms))
                {
                    txtterm.Clear();
                    txtterm.Focus();
                }
                else
                {
                    PRINCIPAL();
                    MATURITYDATE();
                    STRAIGHTLINEINTEREST();
                    PRINCIPALDUEDATE();
                    errorProvider2.Clear();
                    errorProvider1.Clear();
                }

            }
        }
        private void cmbterm_SelectedIndexChanged(object sender, EventArgs e)
        {
            PRINCIPAL();
            MATURITYDATE();
            STRAIGHTLINEINTEREST();
            INTERESTDUEDATE();
            PRINCIPALDUEDATE();

            errorProvider2.Clear();
            errorProvider1.Clear();
        }
        private void cmbpymtmode2_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
            errorProvider1.Clear();

            STRAIGHTLINEINTEREST();
            INTERESTDUEDATE();
            PRINCIPAL();
            if (cmbintpymtmode.Text == "EVERY NP")
            {
                txtintlampsum.Show();
                cmbintlampsum.Show();

                cmbintpymtmode.Width = 80;
            }
            else
            {
                txtintlampsum.Hide();
                cmbintlampsum.Hide();
                cmbintpymtmode.Width = 192;
            }
        }
        private void cmbbaseno_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
            errorProvider1.Clear();

            if (cmbbaseno.Text == "STRAIGHT LINE")
            {
                PRINCIPAL();
                MATURITYDATE();
                STRAIGHTLINEINTEREST();
                PRINCIPALDUEDATE();
                INTERESTDUEDATE();

                label12.Show();
                lbinterest.Show();
                cmbdiminish.Enabled = false;
                cmbprinpymntmode.Enabled = true;
                label47.Show();
                lbprinamount.Show();
            }
            else if (cmbbaseno.Text == "DIMINISHING BAL")
            {
                PRINCIPAL();
                MATURITYDATE();
                STRAIGHTLINEINTEREST();
                PRINCIPALDUEDATE();
                INTERESTDUEDATE();

                label12.Hide();
                lbinterest.Hide();
                label47.Show();
                lbprinamount.Show();
                cmbdiminish.Enabled = true;
                cmbprinpymntmode.Enabled = false;
                label11.Text = "DIMINISH:";
            }
            else if (cmbbaseno.Text == "ANNUITY")
            {
                PRINCIPAL();
                MATURITYDATE();
                STRAIGHTLINEINTEREST();
                PRINCIPALDUEDATE();
                INTERESTDUEDATE();

                cmbdiminish.Enabled = true;
                cmbprinpymntmode.Enabled = false;
                label12.Hide();
                lbinterest.Hide();
                label11.Text = "ANNUITY:";
                label47.Hide();
                lbprinamount.Hide();
            }
        }
        private void txtinterest_TextChanged(object sender, EventArgs e)
        {
            double interest;
            if (txtinterest.Text == "" || txtinterest.Text == "0")
            {
                txtinterest.Focus();
            }
            else
            {
                if (!double.TryParse(txtinterest.Text, out interest))
                {
                    txtinterest.Clear();
                    txtinterest.Focus();
                }
                else
                {
                    PRINCIPAL();
                    MATURITYDATE();
                    STRAIGHTLINEINTEREST();
                    PRINCIPALDUEDATE();
                    INTERESTDUEDATE();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Are you sure you want to close ?", "CONFIRMATION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult.Yes == r)
            {
                Application.ExitThread();
            }
        }
        void enable()
        {
            cmbbaseno.Enabled = cmbcollaterals.Enabled =
                cmbcolltype.Enabled = cmbcomputed.Enabled = cmbcomputed.Enabled = cmbdiminish.Enabled =
                cmbduedateopt.Enabled = cmbintlampsum.Enabled = cmbintpymtmode.Enabled = cmblampsum.Enabled =
                txtloantype.Enabled = txtprpse.Enabled = cmbstatus.Enabled =
                cmbterm.Enabled = txtterm.Enabled = txtpenalty.Enabled = txtmaker1.Enabled = txtmaker2.Enabled =
                txtmaker3.Enabled = txtmaker4.Enabled = txtinterest.Enabled = txtcreditlimit.Enabled = mskloandate.Enabled =
                txtappvalue.Enabled = txtrefno.Enabled = txtprincipal.Enabled = txtothers.Enabled = txtgraceperiod.Enabled =
                button2.Enabled = button7.Enabled = true;
        }
        void disable()
        {
            cmbbaseno.Enabled = cmbcollaterals.Enabled =
                cmbcolltype.Enabled = cmbcomputed.Enabled = cmbcomputed.Enabled = cmbdiminish.Enabled =
                cmbduedateopt.Enabled = cmbintlampsum.Enabled = cmbintpymtmode.Enabled = cmblampsum.Enabled =
                txtloantype.Enabled = cmbprinpymntmode.Enabled = txtprpse.Enabled = cmbstatus.Enabled =
                cmbterm.Enabled = txtterm.Enabled = txtpenalty.Enabled = txtmaker1.Enabled = txtmaker2.Enabled =
                txtmaker3.Enabled = txtmaker4.Enabled = txtinterest.Enabled = txtcreditlimit.Enabled = txtgraceperiod.Enabled =
                txtappvalue.Enabled = txtrefno.Enabled = txtprincipal.Enabled = txtothers.Enabled = mskloandate.Enabled = button2.Enabled =
                 button7.Enabled = false;
        }
        private void lbdocnum_TextChanged(object sender, EventArgs e)
        {
            //if (lbdocnum.Text == "______")
            //{
            //    disable();
            //}
            //else
            //{
            //    if (lblDocEntry.Text == "N")
            //    {
            //        mc.GETPENDINGDATA(lbdocnum.Text, lbclientid.Text, txtlastname.Text, txtfirstname.Text, txtmiddlename.Text, txtorganame.Text, txtdateopened.Text,
            //           txtloantype.Text, txtprpse.Text, txtprincipal.Text, mskloandate.Text, txtterm.Text, cmbterm.Text, cmbbaseno.Text, cmbdiminish.Text, cmbprinpymntmode.Text, cmbstatus.Text, txtinterest.Text,
            //           cmbintpymtmode.Text, txtpenalty.Text, cmbcomputed.Text, txtgraceperiod.Text, cmbduedateopt.Text, cmbcolltype.Text, lblDocEntry.Text);

            //        enable();
            //    }
            //    else
            //    {
            //        mc.ACCTINFO(lbdocnum.Text, lbclientid, txtfirstname, txtmiddlename, txtlastname,
            //          txtorganame, address, txtprincipal, mskloandate, txtinterest, txtpenalty, txtgraceperiod, mskduematurdue,
            //          mskinterestduedate, mskprinduedate, txtloantype, txtprpse, cmbbaseno, cmbduedateopt, txtmaker1, txtmaker2, txtmaker3, txtmaker4, txtdateopened);

            //        enable();
            //    }
            //}
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lbtime.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }
        private void txtprincipal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void mskmaturitydate_TextChanged(object sender, EventArgs e)
        {
            if (mskmaturitydate.Text == "/ /")
            {
                txtterm.Focus();
            }
            else
            {
                mskduematurdue.Text = mskmaturitydate.Text;
            }
        }
        void clear()
        {
            cmbbaseno.Text = cmbcollaterals.Text =
                 cmbcolltype.Text = cmbcomputed.Text = cmbcomputed.Text = cmbdiminish.Text =
                 cmbduedateopt.Text = cmbintlampsum.Text = cmbintpymtmode.Text = cmblampsum.Text =
                 txtloantype.Text = cmbprinpymntmode.Text = txtprpse.Text = cmbstatus.Text =
                 cmbterm.Text = txtterm.Text = txtpenalty.Text = txtmaker1.Text = txtmaker2.Text =
                 txtmaker3.Text = txtmaker4.Text = txtinterest.Text = txtcreditlimit.Text =
                 txtappvalue.Text = txtrefno.Text = txtprincipal.Text = txtothers.Text = txtfirstname.Text =
                 txtmiddlename.Text = txtlastname.Text = txtdateopened.Text = txtorganame.Text = txtterm.Text =
                 txtpenalty.Text = txtinterest.Text = txtgraceperiod.Text = mskmaturitydate.Text = null;
            lbdocnum.Text = "______";
            lbclientid.Text = "0000";
            button1.Focus();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            cc.showReport(txtorganame.Text, txtprincipal.Text, txtterm.Text, cmbterm.Text, lbclientid.Text,
            cmbbaseno.Text, txtinterest.Text, mskduematurdue.Text, mskloandate.Text, cmbcolltype.Text, address.Text, lbprinamount.Text, lbinterest.Text, mskinterestduedate.Text,
            cmbprinpymntmode.Text, cmbintpymtmode.Text, cmbduedateopt.Text, cmbdiminish.Text, txtwholeyearinterest.Text, dataGridView1);

            SAVEDATAINLOCALDB();
            clear();
            disable();
            panel6.Hide();
        }
        void UPDATEDATAINLOCALDB()
        {
            string _SQLsavedate = "";

            _SQLsavedate = @" 
                            UPDATE  SET Posted = '" + "Y" + "' FROM LOANMASTERDATA A WHERE DocEntry = '" + lbclientid.Text + "'";
            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sLclSystemConnection, _SQLsavedate);
            MessageBox.Show("SAVE SUCCESSFULLY ", "SUCCESS", MessageBoxButtons.OK);
        }
        void SAVEDATAINLOCALDB()
        {
            string _SQLsavedate = "";
            if (cmbbaseno.Text == "STRAIGHT LINE")
            {
                _SQLsavedate = @" SET IDENTITY_INSERT LOANMASTERDATA ON
                            Insert into LOANMASTERDATA (DocEntry, ClientId,LastName,FirstName,MiddleName,Date,LoanType,Purpose,Principal, Loandate, NoTerm,Term , Baseno , PaymentMode, Status , Interest,IntpaymentMode , Penalty, Computed, GracePeriod, DueDateOpt , CollectionType  ) 
                            values ('" + int.Parse(lblDocEntry.Text) + "','" + lbclientid.Text + "','" + txtlastname.Text + "','" + txtfirstname.Text + "', '" + txtmiddlename.Text + "', '" + DateTime.Parse(lbdate.Text) + "', '" + txtloantype.Text + "','" + txtprpse.Text + "','" + txtprincipal.Text + "', '" + DateTime.Parse(mskloandate.Text) + "', '" + int.Parse(txtterm.Text) + "', '" + cmbterm.Text + "','" + cmbbaseno.Text + "','" + cmbprinpymntmode.Text + "','" + cmbstatus.Text + "','" + int.Parse(txtinterest.Text) + "','" + cmbintpymtmode.Text + "','" + int.Parse(txtpenalty.Text) + "','" + cmbcomputed.Text + "','" + int.Parse(txtgraceperiod.Text) + "','" + cmbduedateopt.Text + "','" + cmbcolltype.Text + "') SET IDENTITY_INSERT LOANMASTERDATA OFF";
            }
            else
            {
                _SQLsavedate = @"
                            SET IDENTITY_INSERT LOANMASTERDATA ON
                            Insert into LOANMASTERDATA (DocEntry, ClientId,LastName,FirstName,MiddleName,Date,LoanType,Purpose,Principal, Loandate, NoTerm,Term , Baseno , PaymentMode, Status , Interest,IntpaymentMode , Penalty, Computed, GracePeriod, DueDateOpt ,CollectionType  ) 
                            values ('" + int.Parse(lblDocEntry.Text) + "','" + lbclientid.Text + "','" + txtlastname.Text + "','" + txtfirstname.Text + "', '" + txtmiddlename.Text + "', '" + DateTime.Parse(lbdate.Text) + "', '" + txtloantype.Text + "','" + txtprpse.Text + "','" + double.Parse(txtprincipal.Text) + "', '" + DateTime.Parse(mskloandate.Text) + "', '" + int.Parse(txtterm.Text) + "', '" + cmbterm.Text + "','" + cmbbaseno.Text + "','" + cmbdiminish.Text + "','" + cmbstatus.Text + "','" + int.Parse(txtinterest.Text) + "','" + cmbintpymtmode.Text + "','" + int.Parse(txtpenalty.Text) + "','" + cmbcomputed.Text + "','" + int.Parse(txtgraceperiod.Text) + "','" + cmbduedateopt.Text + "','" + cmbcolltype.Text + "') SET IDENTITY_INSERT LOANMASTERDATA OFF";
            }

            clsSQLClientFunctions.GlobalExecuteCommand(clsDeclaration.sLclSystemConnection, _SQLsavedate);
            MessageBox.Show("SAVE SUCCESSFULLY ", "SUCCESS", MessageBoxButtons.OK);
        }
        private void mskduematurdue_TextChanged(object sender, EventArgs e)
        {
            mskmaturitydate.Text = mskduematurdue.Text;
        }
        void viewreport()
        {
            if (cmbduedateopt.Text == "END OF WEEK(SAT)" || cmbduedateopt.Text == "END OF WEEK(FRIDAY)")
            {
                errorProvider1.SetError(cmbduedateopt, "NO INSTALLMENT CREATED!");
                panel6.Hide();
            }
            else
            {
                cc.ViewInstallment(txtorganame, txtprincipal, txtterm, cmbterm, lbclientid,
                 cmbbaseno, txtinterest, mskduematurdue, mskloandate, cmbcolltype, address, lbprinamount, lbinterest, mskinterestduedate,
                 cmbprinpymntmode, cmbintpymtmode, cmbduedateopt, cmbdiminish, txtwholeyearinterest, dataGridView1);
                errorProvider1.Clear();
                errorProvider2.Clear();
                panel6.Show();
                MaximizeBox = false;
                this.ControlBox = false;
            }
        }
        #region ERRORMESSAGE

        void errormesagenotcompatible()
        {
            if (cmbbaseno.Text == "STRAIGHT LINE")
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cmbintpymtmode, "Payment Mode: Principal and Interest are NOT Compatible");
                errorProvider1.SetError(cmbprinpymntmode, "Payment Mode: Principal and Interest are NOT Compatible");
            }
            else
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cmbintpymtmode, "Payment Mode: Principal and Interest are NOT Compatible");
                errorProvider1.SetError(cmbdiminish, "Payment Mode: Principal and Interest are NOT Compatible");
            }
            panel6.Hide();
        }
        void erroemesaageduedateopt()
        {
            errorProvider1.SetError(cmbduedateopt, "Due Date Option and Interest are NOT Compatible");
            errorProvider1.SetError(cmbintpymtmode, "Due Date Option and Interest are NOT Compatible");
            panel6.Hide();
        }
        void errormessagenotcompatibletoterm()
        {
            if (cmbbaseno.Text == "STRAIGHT LINE")
            {
                errorProvider2.Clear();
                errorProvider2.SetError(cmbprinpymntmode, "Payment Mode: Principal and Payment Terms are NOT Compatible");
                errorProvider2.SetError(cmbterm, "Payment Mode: Principal and Payment Terms are NOT Compatible");
            }
            else
            {
                errorProvider2.Clear();
                errorProvider2.SetError(cmbdiminish, "Payment Mode: Principal and Payment Terms are NOT Compatible");
                errorProvider2.SetError(cmbterm, "Payment Mode: Principal and Payment Terms are NOT Compatible");
            }
            panel6.Hide();
        }
        #endregion
        #region BLOCKINGS
        //void errormessageiffieldsisempty()
        //{
        //    if()
        //}
        void errormesageinterestandpenalty()
        {
            if (cmbintpymtmode.Text == "S-MONTHS")
            {
                Blockings();
            }
            else if (cmbintpymtmode.Text == "MONTHLY")
            {
                if (cmbcomputed.Text == "S-MONTHS")
                {
                    errorProvider1.SetError(cmbcomputed, "PENALTY MODE AND INTEREST ARE NOT COMPATIBLE!");
                    errorProvider1.SetError(cmbintpymtmode, "PENALTY MODE AND INTEREST ARE NOT COMPATIBLE!");
                }
                else
                {
                    Blockings();
                }
            }
            else if (cmbintpymtmode.Text == "QUARTERLY")
            {
                if (cmbcomputed.Text == "S-MONTHS" || cmbcomputed.Text == "MONTHLY")
                {
                    errorProvider1.SetError(cmbcomputed, "PENALTY MODE AND INTEREST ARE NOT COMPATIBLE!");
                    errorProvider1.SetError(cmbintpymtmode, "PENALTY MODE AND INTEREST ARE NOT COMPATIBLE!");
                }
                else
                {
                    Blockings();
                }
            }
            else if (cmbintpymtmode.Text == "SEMESTRAL")
            {
                if (cmbcomputed.Text == "S-MONTHS" || cmbcomputed.Text == "MONTHLY" || cmbcomputed.Text == "QUARTERLY")
                {
                    errorProvider1.SetError(cmbcomputed, "PENALTY MODE AND INTEREST ARE NOT COMPATIBLE!");
                    errorProvider1.SetError(cmbintpymtmode, "PENALTY MODE AND INTEREST ARE NOT COMPATIBLE!");
                }
                else
                {
                    Blockings();
                }
            }
            else if (cmbintpymtmode.Text == "YEARLY")
            {
                if (cmbcomputed.Text == "S-MONTHS" || cmbcomputed.Text == "MONTHLY" || cmbcomputed.Text == "QUARTERLY" || cmbcomputed.Text == "SEMESTRAL")
                {
                    errorProvider1.SetError(cmbcomputed, "PENALTY MODE AND INTEREST ARE NOT COMPATIBLE!");
                    errorProvider1.SetError(cmbintpymtmode, "PENALTY MODE AND INTEREST ARE NOT COMPATIBLE!");
                }
                else
                {
                    Blockings();
                }
            }
        }
        void Blockings()
        {
            double term;
            double.TryParse(txtterm.Text, out term);

            if (cmbduedateopt.Text == "DEFAULT")
            {
                #region SL
                if (cmbbaseno.Text == "STRAIGHT LINE")
                {

                    if (cmbterm.Text == "S-MONTHS")
                    {
                        if (cmbintpymtmode.Text == "S-MONTHS")
                        {
                            if (cmbprinpymntmode.Text == "S-MONTHS")
                            {
                                viewreport();
                            }
                            else if (cmbprinpymntmode.Text == "MONTHLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "QUARTERLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 24)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "MONTHLY")
                        {
                            if (cmbprinpymntmode.Text == "MONTHLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "QUARTERLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 24)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "QUARTERLY")
                        {
                            if (cmbprinpymntmode.Text == "QUARTERLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 24)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "SEMESTRAL")
                        {
                            if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 24)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "YEARLY")
                        {
                            if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 24)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "EVERY NP")
                        {
                        }
                        else if (cmbintpymtmode.Text == "LUMPSUM")
                        {
                            viewreport();
                        }
                        else
                        {

                        }
                    }
                    else if (cmbterm.Text == "MONTHS")
                    {
                        if (cmbintpymtmode.Text == "S-MONTHS")
                        {
                            if (cmbprinpymntmode.Text == "S-MONTHS")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "MONTHLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "MONTHLY")
                        {
                            if (cmbprinpymntmode.Text == "MONTHLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "QUARTERLY")
                        {
                            if (cmbprinpymntmode.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "SEMESTRAL")
                        {
                            if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "YEARLY")
                        {
                            if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "EVERY NP")
                        {
                        }
                        else if (cmbintpymtmode.Text == "LUMPSUM")
                        {
                            viewreport();
                        }
                        else
                        {
                            //weekly wa pa
                        }
                    }
                    else if (cmbterm.Text == "QUARTERS")
                    {
                        if (cmbintpymtmode.Text == "S-MONTHS")
                        {
                            if (cmbprinpymntmode.Text == "S-MONTHS")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "MONTHLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "QUARTERLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "MONTHLY")
                        {
                            if (cmbprinpymntmode.Text == "MONTHLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "QUARTERLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "QUARTERLY")
                        {
                            if (cmbprinpymntmode.Text == "QUARTERLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "SEMESTRAL")
                        {
                            if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "YEARLY")
                        {
                            if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "EVERY NP")
                        {
                        }
                        else if (cmbintpymtmode.Text == "LUMPSUM")
                        {
                            viewreport();
                        }
                        else
                        {
                            //weekly wa pa
                        }
                    }
                    else if (cmbterm.Text == "SEMESTERS")
                    {
                        if (cmbintpymtmode.Text == "S-MONTHS")
                        {
                            if (cmbprinpymntmode.Text == "S-MONTHS")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "MONTHLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "QUARTERLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "MONTHLY")
                        {
                            if (cmbprinpymntmode.Text == "MONTHLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "QUARTERLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "QUARTERLY")
                        {
                            if (cmbprinpymntmode.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "SEMESTRAL")
                        {
                            if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                viewreport();
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "YEARLY")
                        {
                            if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "EVERY NP")
                        {
                        }
                        else if (cmbintpymtmode.Text == "LUMPSUM")
                        {
                            viewreport();
                        }
                        else
                        {
                            //weekly wa pa
                        }
                    }
                    else if (cmbterm.Text == "YEARS")
                    {
                        if (cmbintpymtmode.Text == "S-MONTHS")
                        {
                            if (cmbprinpymntmode.Text == "S-MONTHS")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "MONTHLY")
                            {


                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "QUARTERLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "MONTHLY")
                        {
                            if (cmbprinpymntmode.Text == "MONTHLY")
                            {


                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "QUARTERLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                viewreport();
                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "QUARTERLY")
                        {
                            if (cmbprinpymntmode.Text == "QUARTERLY")
                            {

                                viewreport();
                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "SEMESTRAL")
                        {
                            if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "YEARLY")
                        {
                            if (cmbprinpymntmode.Text == "YEARLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "EVERY NP")
                            {
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbintpymtmode.Text == "EVERY NP")
                        {
                        }
                        else if (cmbintpymtmode.Text == "LUMPSUM")
                        {
                            viewreport();
                        }
                        else
                        {
                            //weekly wa pa
                        }
                    }
                }
                #endregion
                #region DB
                else if (cmbbaseno.Text == "DIMINISHING BAL")
                {
                    if (cmbintpymtmode.Text == "S-MONTHS")
                    {
                        if (cmbterm.Text == "S-MONTHS")
                        {
                            if (cmbdiminish.Text == "S-MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                if (term < 7)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                        }
                        else if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbdiminish.Text == "S-MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                if (term < 7)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbdiminish.Text == "S-MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            if (cmbdiminish.Text == "S-MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbdiminish.Text == "S-MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                        }
                    }
                    else if (cmbintpymtmode.Text == "MONTHLY")
                    {
                        if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                if (term < 7)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                    }
                    else if (cmbintpymtmode.Text == "QUARTERLY")
                    {
                        if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbdiminish.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {

                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                    }
                    else if (cmbintpymtmode.Text == "SEMESTRAL")
                    {
                        if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {

                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTRES")
                        {
                            if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                    }
                    else if (cmbintpymtmode.Text == "YEARLY")
                    {
                        if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            if (cmbdiminish.Text == "ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbdiminish.Text == "ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                    }
                }
                #endregion
                #region ANNT
                else if (cmbbaseno.Text == "ANNUITY")
                {
                    if (cmbintpymtmode.Text == "S-MONTHS")
                    {
                        if (cmbterm.Text == "S-MONTHS")
                        {
                            if (cmbdiminish.Text == "S-MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                if (term < 7)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                        }
                        if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbdiminish.Text == "S-MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                if (term < 7)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbdiminish.Text == "S-MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            if (cmbdiminish.Text == "S-MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbdiminish.Text == "S-MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                        }
                    }
                    else if (cmbintpymtmode.Text == "MONTHLY")
                    {
                        if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                if (term < 7)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                    }
                    else if (cmbintpymtmode.Text == "QUARTERLY")
                    {
                        if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbdiminish.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                };
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                    }
                    else if (cmbintpymtmode.Text == "SEMESTRAL")
                    {
                        if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                    }
                    else if (cmbintpymtmode.Text == "YEARLY")
                    {
                        if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbdiminish.Text == "ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                    }
                }
                #endregion
            }
            else if (cmbduedateopt.Text == "15th/END OF EACH MNTH" || cmbduedateopt.Text == "5th/20th OF EACH MNTH" ||
                cmbduedateopt.Text == "10th/25th OF EACH MNTH" || cmbduedateopt.Text == "15TH/30TH OF EACH MONTH")
            {
                #region S-MONHTS
                if (cmbbaseno.Text == "STRAIGHT LINE")
                {
                    if (cmbintpymtmode.Text == "S-MONTHS")
                    {
                        if (cmbterm.Text == "S-MONTHS")
                        {
                            viewreport();
                        }
                        else if (cmbterm.Text == "MONTHS")
                        {
                            viewreport();
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            viewreport();
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            viewreport();
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            viewreport();
                        }
                    }
                    else
                    {
                        erroemesaageduedateopt();
                    }
                }
                else
                {
                    if (cmbintpymtmode.Text == "S-MONTHS")
                    {
                        if (cmbterm.Text == "S-MONTHS")
                        {
                            viewreport();
                        }
                        else if (cmbterm.Text == "MONTHS")
                        {
                            viewreport();
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            viewreport();
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            viewreport();
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            viewreport();
                        }
                    }
                    else
                    {
                        erroemesaageduedateopt();
                    }
                }
                #endregion
            }
            else
            {
                #region S-MONHTS
                if (cmbbaseno.Text == "STRAIGHT LINE")
                {
                    if (cmbintpymtmode.Text == "S-MONTHS")
                    {
                        if (cmbterm.Text == "S-MONTHS")
                        {
                            erroemesaageduedateopt();
                        }
                        else if (cmbterm.Text == "MONTHS")
                        {
                            erroemesaageduedateopt();
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            erroemesaageduedateopt();
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            erroemesaageduedateopt();
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            erroemesaageduedateopt();
                        }
                    }
                    else if (cmbintpymtmode.Text == "MONTHLY")
                    {
                        if (cmbterm.Text == "S-MONTHS")
                        {
                            if (cmbprinpymntmode.Text == "MONTHLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "QUARTERLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 24)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbprinpymntmode.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbprinpymntmode.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbprinpymntmode.Text == "MONTHLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "QUARTERLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            if (cmbprinpymntmode.Text == "MONTHLY")
                            {
                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "QUARTERLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbprinpymntmode.Text == "MONTHLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                    }
                    else if (cmbintpymtmode.Text == "QUARTERLY")
                    {
                        if (cmbterm.Text == "S-MONTHS")
                        {
                            if (cmbprinpymntmode.Text == "QUARTERLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 24)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbprinpymntmode.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbprinpymntmode.Text == "QUARTERLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            if (cmbprinpymntmode.Text == "QUARTERLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbprinpymntmode.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                    }
                    else if (cmbintpymtmode.Text == "SEMESTRAL")
                    {
                        if (cmbterm.Text == "S-MONTHS")
                        {
                            if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 24)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbprinpymntmode.Text == "SEMESTRAL")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "YEARLY")
                            {

                                viewreport();

                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                    }
                    else if (cmbintpymtmode.Text == "SEMESTRAL")
                    {
                        if (cmbterm.Text == "S-MONTHS")
                        {
                            if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 24)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            if (cmbprinpymntmode.Text == "YEARLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbprinpymntmode.Text == "YEARLY")
                            {

                                errormessagenotcompatibletoterm();

                            }
                            else if (cmbprinpymntmode.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                    }
                }
                else
                {
                    if (cmbintpymtmode.Text == "S-MONTHS")
                    {
                        if (cmbterm.Text == "S-MONTHS")
                        {
                            erroemesaageduedateopt();
                        }
                        else if (cmbterm.Text == "MONTHS")
                        {
                            erroemesaageduedateopt();
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            erroemesaageduedateopt();
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            erroemesaageduedateopt();
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            erroemesaageduedateopt();
                        }
                    }
                    else if (cmbintpymtmode.Text == "MONTHLY")
                    {
                        if (cmbterm.Text == "S-MONTHS")
                        {
                            if (cmbdiminish.Text == "MONTHLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                if (term < 14)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 24)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                if (term < 7)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbdiminish.Text == "MONTHLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB3")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "MONTHLY EMB6")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "BIMONTHLY")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "QUARTERLY")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                    }
                    else if (cmbintpymtmode.Text == "QUARTERS")
                    {
                        if (cmbterm.Text == "S-MONTHS")
                        {
                            if (cmbdiminish.Text == "QUARTERLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 24)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbdiminish.Text == "QUARTERLY")
                            {
                                if (term < 3)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbdiminish.Text == "QUARTERLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            if (cmbdiminish.Text == "QUARTERLY")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbdiminish.Text == "QUARTERLY")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                    }
                    else if (cmbintpymtmode.Text == "SEMESTRAL")
                    {
                        if (cmbterm.Text == "S-MONTHS")
                        {
                            if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 24)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 6)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbdiminish.Text == "SEMI ANNUALLY")
                            {

                                viewreport();

                            }
                            else if (cmbdiminish.Text == "ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                    }
                    else if (cmbintpymtmode.Text == "YEARLY")
                    {
                        if (cmbterm.Text == "S-MONTHS")
                        {
                            if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 24)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "MONTHS")
                        {
                            if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 12)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "QUARTERS")
                        {
                            if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 4)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "SEMESTERS")
                        {
                            if (cmbdiminish.Text == "ANNUALLY")
                            {
                                if (term < 2)
                                {
                                    errormessagenotcompatibletoterm();
                                }
                                else
                                {
                                    viewreport();
                                }
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                        else if (cmbterm.Text == "YEARS")
                        {
                            if (cmbdiminish.Text == "ANNUALLY")
                            {
                                viewreport();
                            }
                            else if (cmbdiminish.Text == "LUMPSUM")
                            {
                                viewreport();
                            }
                            else
                            {
                                errormesagenotcompatible();
                            }
                        }
                    }
                }
                #endregion
            }
        }
        #endregion
        void savetosap()
        {
            DialogResult r = MessageBox.Show("UPLOADED TO SAP AND PRINT REPORT?", "UPLOAD AND PRINT!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.Yes)
            {
                String _Error = "";

                bool created = clsSAPFunctions.ARRecievableInvoice(dataGridView1, lbdocnum.Text, lblDocEntry.Text, lbclientid.Text, mskduematurdue.Text, mskloandate.Text, txtloantype.Text, txtprincipal.Text,
                   txtmaker1.Text, txtmaker2.Text, txtmaker3.Text, txtmaker4.Text, txtinterest.Text, cmbbaseno.Text, txtpenalty.Text, txtgraceperiod.Text,
                   mskinterestduedate.Text, mskprinduedate.Text, txtloantype.Text, cmbduedateopt.Text, txtterm.Text, cmbterm.Text, txtloantype.Text, out _Error);

                UPDATEDATAINLOCALDB();
                clear();
                disable();
                panel6.Hide();
            }
            else
            {
                return;

            }
        }
        private void cmbdiminish_SelectedIndexChanged(object sender, EventArgs e)
        {
            PRINCIPAL();
            MATURITYDATE();
            STRAIGHTLINEINTEREST();
            PRINCIPALDUEDATE();
            errorProvider2.Clear();
            errorProvider1.Clear();
        }
        private void cmbduedateopt_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
            errorProvider1.Clear();
            MATURITYDATE();
            INTERESTDUEDATE();
            PRINCIPALDUEDATE();
        }
        private void cmbbaseno_TextChanged(object sender, EventArgs e)
        {
            if (cmbbaseno.Text == "STRAIGHT LINE")
            {

                label12.Show();
                lbinterest.Show();
                cmbdiminish.Enabled = false;
                cmbprinpymntmode.Enabled = true;
                label47.Show();
                lbprinamount.Show();
            }
            else if (cmbbaseno.Text == "DIMINISHING BAL")
            {

                label12.Hide();
                lbinterest.Hide();
                label47.Show();
                lbprinamount.Show();
                cmbdiminish.Enabled = true;
                cmbprinpymntmode.Enabled = false;
                label11.Text = "DIMINISH:";
            }
            else if (cmbbaseno.Text == "ANNUITY")
            {
                cmbdiminish.Enabled = true;
                cmbprinpymntmode.Enabled = false;
                label12.Hide();
                lbinterest.Hide();
                label11.Text = "ANNUITY:";
                label47.Hide();
                lbprinamount.Hide();
            }
        }

        private void cmbcomputed_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorProvider2.Clear();
            errorProvider1.Clear();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtterm.Text))
            {
                errorProvider1.SetError(txtterm, "SET NO. OF TERM");
            }
            else if (string.IsNullOrEmpty(cmbterm.Text))
            {
                errorProvider1.SetError(cmbterm, "SET NO. OF TERM");
            }
            else if (string.IsNullOrEmpty(cmbstatus.Text))
            {
                errorProvider1.SetError(cmbstatus, "SET NO. OF TERM");
            }
            else if (string.IsNullOrEmpty(cmbintpymtmode.Text))
            {
                errorProvider1.SetError(cmbintpymtmode, "SET NO. OF TERM");
            }
            else if (string.IsNullOrEmpty(txtinterest.Text))
            {
                errorProvider1.SetError(txtinterest, "SET NO. OF INTEREST");
            }
            else if (string.IsNullOrEmpty(cmbbaseno.Text))
            {
                errorProvider1.SetError(cmbbaseno, "SET NO. OF BASE ON");
            }
            else if (string.IsNullOrEmpty(txtpenalty.Text))
            {
                errorProvider1.SetError(txtpenalty, "SET NO. OF PENALTY");
            }
            else if (string.IsNullOrEmpty(cmbcomputed.Text))
            {
                errorProvider1.SetError(cmbcomputed, "SET NO. OF COMPUTED");
            }
            else if (string.IsNullOrEmpty(txtgraceperiod.Text))
            {
                errorProvider1.SetError(txtgraceperiod, "SET NO. OF GRACE PERIOD");
            }
            else if (string.IsNullOrEmpty(cmbcolltype.Text))
            {
                errorProvider1.SetError(cmbcolltype, "SET NO. OF COLLECTION TYPE");
            }
            else if (string.IsNullOrEmpty(cmbduedateopt.Text))
            {
                errorProvider1.SetError(cmbduedateopt, "SET NO. OF DUEDATE OPTION");
            }
            else
            {
                errorProvider1.Clear();
                errormesageinterestandpenalty();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string c3 = row.Cells[3].Value.ToString();
                    string c4 = row.Cells[4].Value.ToString();
                    string c5 = Convert.ToString(Convert.ToDouble(c3) + Convert.ToDouble(c4));

                    row.Cells[5].Value = c5;
                }
            }
        }
        private void mskloandate_TextChanged(object sender, EventArgs e)
        {
            if (mskloandate.Text == "  /  /")
            {
                button1.Focus();
            }
            else
            {
                MATURITYDATE();
                INTERESTDUEDATE();
                PRINCIPALDUEDATE();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.ControlBox = true;
            panel6.Hide();
            enable();
        }

        private void txtloantype_TextChanged(object sender, EventArgs e)
        {
            mc.LoanType(txtloantype.Text, txtterm, txtinterest, txtpenalty, cmbbaseno, cmbintpymtmode, cmbdiminish, cmbduedateopt, cmbterm, cmbcomputed, txtgraceperiod);
        }

        private void lblDocEntry_TextChanged(object sender, EventArgs e)
        {
            //if (lbdocnum.Text == "______")
            //{
            //    disable();
            //}
            //else
            //{
                if (lblDocEntry.Text == "N")
                {
                    mc.GETPENDINGDATA(lbdocnum.Text, lbclientid, txtlastname, txtfirstname, txtmiddlename, txtorganame, txtdateopened,
                       txtloantype, txtprpse, txtprincipal, mskloandate, txtterm, cmbterm, cmbbaseno, cmbdiminish, cmbprinpymntmode, cmbstatus, txtinterest,
                       cmbintpymtmode, txtpenalty, cmbcomputed, txtgraceperiod, cmbduedateopt, cmbcolltype, lblDocEntry);

                    enable();
                }
                else
                {
                    mc.ACCTINFO(lbdocnum.Text, lbclientid, txtfirstname, txtmiddlename, txtlastname,
                      txtorganame, address, txtprincipal, mskloandate, txtinterest, txtpenalty, txtgraceperiod, mskduematurdue,
                      mskinterestduedate, mskprinduedate, txtloantype, txtprpse, cmbbaseno, cmbduedateopt, txtmaker1, txtmaker2, txtmaker3, txtmaker4, txtdateopened);

                    enable();
                }
            //}
        }

    }
}
