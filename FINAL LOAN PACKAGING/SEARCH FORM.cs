using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FINAL_LOAN_PACKAGING
{
    public partial class SEARCH_FORM : Form
    {
      
        public SEARCH_FORM()
        {
            InitializeComponent();

        }

        private void SEARCH_FORM_Load(object sender, EventArgs e)
        {
          
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("NO RECORDS FOUND!", "NO RECORD", MessageBoxButtons.OK);
            }
            else
            {
                try
                {
                    if (radioButton1.Checked == true)
                    {
                        LOAN_MASTER.mystring = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                        LOAN_MASTER.myDocEntry = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        if (radioButton2.Checked == true)
                        {
                            LOAN_MASTER.mystring = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                            LOAN_MASTER.myDocEntry = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        void load()
        {
            string _sqlLoadedQuery = "";
            _sqlLoadedQuery = @"Select a.DocNum as [Document No.], a.DocEntry as [DocEntry] ,UPPER(b.U_FN+' '+b.U_MN+' '+b.U_SN) as [Name],a.CardCode as [Card Code],convert(varchar,a.DocDate,101)as [Date] from ordr a 
             inner join ocrd b on a.CardCode = b.CardCode where  (b.U_FN+' '+b.U_MN+' '+b.U_SN) Like '%" + txtsearchdoc2.Text + @"%'";
            DataTable _tblDataDispaly = new DataTable();
            _tblDataDispaly = clsSQLClientFunctions.DataList(clsDeclaration.sSAPConnection, _sqlLoadedQuery);
            clsFunctions.DataGridViewSetup(dataGridView1, _tblDataDispaly);
        }

        void Pendingload()
        {
            string _sqlLoadedQuery = "";
            _sqlLoadedQuery = @"select a.DocEntry AS [Document No.],a.Posted as [POSTED STATUS], UPPER ( a.FirstName+' '+a.LastName+' '+a.MiddleName ) AS [Name],A.ClientId as [Client ID],convert(varchar ,  a.Loandate, 101) AS [Loan Date] 
            from LOANMASTERDATA a where a.Posted = 'N' and ( a.FirstName+' '+a.LastName+' '+a.MiddleName ) like '%" + txtsearchdoc2.Text + @"%'";
            DataTable _tblDispaly = new DataTable();
            _tblDispaly = clsSQLClientFunctions.DataList(clsDeclaration.sLclSystemConnection, _sqlLoadedQuery);
            clsFunctions.DataGridViewSetup(dataGridView1, _tblDispaly);
        }
        
        private void txtsearchdoc2_TextChanged(object sender, EventArgs e)
        {
            if (txtsearchdoc2.Text == "0" || txtsearchdoc2.Text == null)
            {
                load();
                Pendingload();
                txtsearchdoc2.Focus();
            }
            else
            {
                load();
                Pendingload();
            }
        }

       
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("NO RECORDS FOUND!", "NO RECORD", MessageBoxButtons.OK);
            }
            else
            {
                try
                {
                    if (radioButton1.Checked == true)
                    {
                        LOAN_MASTER.mystring = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                        LOAN_MASTER.myDocEntry = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        if (radioButton2.Checked == true)
                        {
                            LOAN_MASTER.mystring = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                            LOAN_MASTER.myDocEntry = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            load();
           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Pendingload();
        }
    }
}
