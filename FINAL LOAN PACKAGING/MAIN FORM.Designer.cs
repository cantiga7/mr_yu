namespace FINAL_LOAN_PACKAGING
{
    partial class MAIN_FORM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fILEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lOANMASTERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sETTINGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lEDGERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataTable1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lEDGERToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fILEToolStripMenuItem,
            this.sETTINGToolStripMenuItem,
            this.lEDGERToolStripMenuItem1,
            this.lEDGERToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1316, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fILEToolStripMenuItem
            // 
            this.fILEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lOANMASTERToolStripMenuItem});
            this.fILEToolStripMenuItem.Name = "fILEToolStripMenuItem";
            this.fILEToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.fILEToolStripMenuItem.Text = "FILE";
            // 
            // lOANMASTERToolStripMenuItem
            // 
            this.lOANMASTERToolStripMenuItem.Name = "lOANMASTERToolStripMenuItem";
            this.lOANMASTERToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.lOANMASTERToolStripMenuItem.Text = "SETTINGS";
            this.lOANMASTERToolStripMenuItem.Click += new System.EventHandler(this.lOANMASTERToolStripMenuItem_Click);
            // 
            // sETTINGToolStripMenuItem
            // 
            this.sETTINGToolStripMenuItem.Name = "sETTINGToolStripMenuItem";
            this.sETTINGToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.sETTINGToolStripMenuItem.Text = "LOAN MASTER";
            this.sETTINGToolStripMenuItem.Click += new System.EventHandler(this.sETTINGToolStripMenuItem_Click);
            // 
            // lEDGERToolStripMenuItem
            // 
            this.lEDGERToolStripMenuItem.Name = "lEDGERToolStripMenuItem";
            this.lEDGERToolStripMenuItem.Size = new System.Drawing.Size(139, 20);
            this.lEDGERToolStripMenuItem.Text = "INSTALLMENT VIEWER";
            this.lEDGERToolStripMenuItem.Click += new System.EventHandler(this.lEDGERToolStripMenuItem_Click_1);
            // 
            // dataTable1BindingSource
            // 
            this.dataTable1BindingSource.DataMember = "DataTable1";
            // 
            // lEDGERToolStripMenuItem1
            // 
            this.lEDGERToolStripMenuItem1.Name = "lEDGERToolStripMenuItem1";
            this.lEDGERToolStripMenuItem1.Size = new System.Drawing.Size(60, 20);
            this.lEDGERToolStripMenuItem1.Text = "LEDGER";
            this.lEDGERToolStripMenuItem1.Click += new System.EventHandler(this.lEDGERToolStripMenuItem1_Click);
            // 
            // MAIN_FORM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1316, 579);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MAIN_FORM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MAIN_FORM_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1BindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       
        private System.Windows.Forms.BindingSource dataTable1BindingSource;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fILEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lOANMASTERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sETTINGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lEDGERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lEDGERToolStripMenuItem1;





    }
}