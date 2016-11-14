namespace SQLiteMergeTool
{
    partial class frmMain
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
            this.grpLeft = new System.Windows.Forms.GroupBox();
            this.btnLoadLeftDatabase = new System.Windows.Forms.Button();
            this.lstLeftTable = new System.Windows.Forms.CheckedListBox();
            this.btnLeftDialog = new System.Windows.Forms.Button();
            this.txtLeft = new System.Windows.Forms.TextBox();
            this.grpRight = new System.Windows.Forms.GroupBox();
            this.btnLoadRightDatabase = new System.Windows.Forms.Button();
            this.lstRightTable = new System.Windows.Forms.CheckedListBox();
            this.btnRightDialog = new System.Windows.Forms.Button();
            this.txtRight = new System.Windows.Forms.TextBox();
            this.btnSendLeftToRight = new System.Windows.Forms.Button();
            this.btnSendRightToLeft = new System.Windows.Forms.Button();
            this.chkSelectAllLeft = new System.Windows.Forms.CheckBox();
            this.chkSelectAllRight = new System.Windows.Forms.CheckBox();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblTabela = new System.Windows.Forms.Label();
            this.grpLeft.SuspendLayout();
            this.grpRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLeft
            // 
            this.grpLeft.Controls.Add(this.chkSelectAllLeft);
            this.grpLeft.Controls.Add(this.btnLoadLeftDatabase);
            this.grpLeft.Controls.Add(this.lstLeftTable);
            this.grpLeft.Controls.Add(this.btnLeftDialog);
            this.grpLeft.Controls.Add(this.txtLeft);
            this.grpLeft.Location = new System.Drawing.Point(12, 12);
            this.grpLeft.Name = "grpLeft";
            this.grpLeft.Size = new System.Drawing.Size(320, 437);
            this.grpLeft.TabIndex = 0;
            this.grpLeft.TabStop = false;
            this.grpLeft.Text = "Database 1";
            // 
            // btnLoadLeftDatabase
            // 
            this.btnLoadLeftDatabase.Location = new System.Drawing.Point(6, 45);
            this.btnLoadLeftDatabase.Name = "btnLoadLeftDatabase";
            this.btnLoadLeftDatabase.Size = new System.Drawing.Size(308, 24);
            this.btnLoadLeftDatabase.TabIndex = 3;
            this.btnLoadLeftDatabase.Text = "Load";
            this.btnLoadLeftDatabase.UseVisualStyleBackColor = true;
            this.btnLoadLeftDatabase.Click += new System.EventHandler(this.btnLoadLeftDatabase_Click);
            // 
            // lstLeftTable
            // 
            this.lstLeftTable.FormattingEnabled = true;
            this.lstLeftTable.Location = new System.Drawing.Point(6, 90);
            this.lstLeftTable.Name = "lstLeftTable";
            this.lstLeftTable.Size = new System.Drawing.Size(308, 334);
            this.lstLeftTable.TabIndex = 2;
            // 
            // btnLeftDialog
            // 
            this.btnLeftDialog.Location = new System.Drawing.Point(281, 17);
            this.btnLeftDialog.Name = "btnLeftDialog";
            this.btnLeftDialog.Size = new System.Drawing.Size(33, 23);
            this.btnLeftDialog.TabIndex = 1;
            this.btnLeftDialog.Text = "...";
            this.btnLeftDialog.UseVisualStyleBackColor = true;
            this.btnLeftDialog.Click += new System.EventHandler(this.btnLeftDialog_Click);
            // 
            // txtLeft
            // 
            this.txtLeft.Location = new System.Drawing.Point(6, 19);
            this.txtLeft.Name = "txtLeft";
            this.txtLeft.Size = new System.Drawing.Size(269, 20);
            this.txtLeft.TabIndex = 0;
            // 
            // grpRight
            // 
            this.grpRight.Controls.Add(this.chkSelectAllRight);
            this.grpRight.Controls.Add(this.btnLoadRightDatabase);
            this.grpRight.Controls.Add(this.lstRightTable);
            this.grpRight.Controls.Add(this.btnRightDialog);
            this.grpRight.Controls.Add(this.txtRight);
            this.grpRight.Location = new System.Drawing.Point(352, 12);
            this.grpRight.Name = "grpRight";
            this.grpRight.Size = new System.Drawing.Size(320, 437);
            this.grpRight.TabIndex = 1;
            this.grpRight.TabStop = false;
            this.grpRight.Text = "Database 2";
            // 
            // btnLoadRightDatabase
            // 
            this.btnLoadRightDatabase.Location = new System.Drawing.Point(6, 45);
            this.btnLoadRightDatabase.Name = "btnLoadRightDatabase";
            this.btnLoadRightDatabase.Size = new System.Drawing.Size(308, 24);
            this.btnLoadRightDatabase.TabIndex = 5;
            this.btnLoadRightDatabase.Text = "Load";
            this.btnLoadRightDatabase.UseVisualStyleBackColor = true;
            this.btnLoadRightDatabase.Click += new System.EventHandler(this.btnLoadRightDatabase_Click);
            // 
            // lstRightTable
            // 
            this.lstRightTable.FormattingEnabled = true;
            this.lstRightTable.Location = new System.Drawing.Point(6, 90);
            this.lstRightTable.Name = "lstRightTable";
            this.lstRightTable.Size = new System.Drawing.Size(308, 334);
            this.lstRightTable.TabIndex = 4;
            // 
            // btnRightDialog
            // 
            this.btnRightDialog.Location = new System.Drawing.Point(281, 17);
            this.btnRightDialog.Name = "btnRightDialog";
            this.btnRightDialog.Size = new System.Drawing.Size(33, 23);
            this.btnRightDialog.TabIndex = 3;
            this.btnRightDialog.Text = "...";
            this.btnRightDialog.UseVisualStyleBackColor = true;
            this.btnRightDialog.Click += new System.EventHandler(this.btnRightDialog_Click);
            // 
            // txtRight
            // 
            this.txtRight.Location = new System.Drawing.Point(6, 19);
            this.txtRight.Name = "txtRight";
            this.txtRight.Size = new System.Drawing.Size(269, 20);
            this.txtRight.TabIndex = 2;
            // 
            // btnSendLeftToRight
            // 
            this.btnSendLeftToRight.Location = new System.Drawing.Point(94, 455);
            this.btnSendLeftToRight.Name = "btnSendLeftToRight";
            this.btnSendLeftToRight.Size = new System.Drawing.Size(103, 27);
            this.btnSendLeftToRight.TabIndex = 2;
            this.btnSendLeftToRight.Text = "Left to Right";
            this.btnSendLeftToRight.UseVisualStyleBackColor = true;
            this.btnSendLeftToRight.Click += new System.EventHandler(this.btnSendLeftToRight_Click);
            // 
            // btnSendRightToLeft
            // 
            this.btnSendRightToLeft.Location = new System.Drawing.Point(476, 455);
            this.btnSendRightToLeft.Name = "btnSendRightToLeft";
            this.btnSendRightToLeft.Size = new System.Drawing.Size(103, 27);
            this.btnSendRightToLeft.TabIndex = 3;
            this.btnSendRightToLeft.Text = "Right to Left";
            this.btnSendRightToLeft.UseVisualStyleBackColor = true;
            this.btnSendRightToLeft.Click += new System.EventHandler(this.btnSendRightToLeft_Click);
            // 
            // chkSelectAllLeft
            // 
            this.chkSelectAllLeft.AutoSize = true;
            this.chkSelectAllLeft.Location = new System.Drawing.Point(9, 71);
            this.chkSelectAllLeft.Name = "chkSelectAllLeft";
            this.chkSelectAllLeft.Size = new System.Drawing.Size(70, 17);
            this.chkSelectAllLeft.TabIndex = 4;
            this.chkSelectAllLeft.Text = "Select All";
            this.chkSelectAllLeft.UseVisualStyleBackColor = true;
            this.chkSelectAllLeft.CheckedChanged += new System.EventHandler(this.chkSelectAllLeft_CheckedChanged);
            // 
            // chkSelectAllRight
            // 
            this.chkSelectAllRight.AutoSize = true;
            this.chkSelectAllRight.Location = new System.Drawing.Point(9, 71);
            this.chkSelectAllRight.Name = "chkSelectAllRight";
            this.chkSelectAllRight.Size = new System.Drawing.Size(70, 17);
            this.chkSelectAllRight.TabIndex = 6;
            this.chkSelectAllRight.Text = "Select All";
            this.chkSelectAllRight.UseVisualStyleBackColor = true;
            this.chkSelectAllRight.CheckedChanged += new System.EventHandler(this.chkSelectAllRight_CheckedChanged);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.ForeColor = System.Drawing.Color.DarkRed;
            this.lblProgress.Location = new System.Drawing.Point(312, 491);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(69, 13);
            this.lblProgress.TabIndex = 6;
            this.lblProgress.Text = "0000/0000";
            // 
            // lblTabela
            // 
            this.lblTabela.AutoSize = true;
            this.lblTabela.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTabela.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTabela.Location = new System.Drawing.Point(312, 478);
            this.lblTabela.Name = "lblTabela";
            this.lblTabela.Size = new System.Drawing.Size(46, 13);
            this.lblTabela.TabIndex = 7;
            this.lblTabela.Text = "Tabela";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 510);
            this.Controls.Add(this.lblTabela);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnSendRightToLeft);
            this.Controls.Add(this.btnSendLeftToRight);
            this.Controls.Add(this.grpRight);
            this.Controls.Add(this.grpLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMain";
            this.Text = "SQLite Merge Tool";
            this.grpLeft.ResumeLayout(false);
            this.grpLeft.PerformLayout();
            this.grpRight.ResumeLayout(false);
            this.grpRight.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLeft;
        private System.Windows.Forms.Button btnLoadLeftDatabase;
        private System.Windows.Forms.CheckedListBox lstLeftTable;
        private System.Windows.Forms.Button btnLeftDialog;
        private System.Windows.Forms.TextBox txtLeft;
        private System.Windows.Forms.GroupBox grpRight;
        private System.Windows.Forms.Button btnLoadRightDatabase;
        private System.Windows.Forms.CheckedListBox lstRightTable;
        private System.Windows.Forms.Button btnRightDialog;
        private System.Windows.Forms.TextBox txtRight;
        private System.Windows.Forms.Button btnSendLeftToRight;
        private System.Windows.Forms.Button btnSendRightToLeft;
        private System.Windows.Forms.CheckBox chkSelectAllLeft;
        private System.Windows.Forms.CheckBox chkSelectAllRight;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblTabela;
    }
}

