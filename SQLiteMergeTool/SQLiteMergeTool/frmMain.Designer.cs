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
            this.txtSearchLeft = new System.Windows.Forms.TextBox();
            this.chkSelectAllLeft = new System.Windows.Forms.CheckBox();
            this.btnLoadLeftDatabase = new System.Windows.Forms.Button();
            this.lstLeftTable = new System.Windows.Forms.CheckedListBox();
            this.btnLeftDialog = new System.Windows.Forms.Button();
            this.txtLeft = new System.Windows.Forms.TextBox();
            this.grpRight = new System.Windows.Forms.GroupBox();
            this.txtSearchRight = new System.Windows.Forms.TextBox();
            this.chkSelectAllRight = new System.Windows.Forms.CheckBox();
            this.btnLoadRightDatabase = new System.Windows.Forms.Button();
            this.lstRightTable = new System.Windows.Forms.CheckedListBox();
            this.btnRightDialog = new System.Windows.Forms.Button();
            this.txtRight = new System.Windows.Forms.TextBox();
            this.btnSendLeftToRight = new System.Windows.Forms.Button();
            this.btnSendRightToLeft = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblTabela = new System.Windows.Forms.Label();
            this.btnRunCustomScriptsRight = new System.Windows.Forms.Button();
            this.btnRunCustomScriptsLeft = new System.Windows.Forms.Button();
            this.grpTransferType = new System.Windows.Forms.GroupBox();
            this.rdoReplaceData = new System.Windows.Forms.RadioButton();
            this.rdoUnionData = new System.Windows.Forms.RadioButton();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.grpLeft.SuspendLayout();
            this.grpRight.SuspendLayout();
            this.grpTransferType.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLeft
            // 
            this.grpLeft.Controls.Add(this.btnRunCustomScriptsLeft);
            this.grpLeft.Controls.Add(this.txtSearchLeft);
            this.grpLeft.Controls.Add(this.chkSelectAllLeft);
            this.grpLeft.Controls.Add(this.btnLoadLeftDatabase);
            this.grpLeft.Controls.Add(this.lstLeftTable);
            this.grpLeft.Controls.Add(this.btnLeftDialog);
            this.grpLeft.Controls.Add(this.txtLeft);
            this.grpLeft.Location = new System.Drawing.Point(12, 12);
            this.grpLeft.Name = "grpLeft";
            this.grpLeft.Size = new System.Drawing.Size(320, 390);
            this.grpLeft.TabIndex = 0;
            this.grpLeft.TabStop = false;
            this.grpLeft.Text = "Database 1";
            // 
            // txtSearchLeft
            // 
            this.txtSearchLeft.Location = new System.Drawing.Point(6, 94);
            this.txtSearchLeft.Name = "txtSearchLeft";
            this.txtSearchLeft.Size = new System.Drawing.Size(308, 20);
            this.txtSearchLeft.TabIndex = 5;
            this.txtSearchLeft.TextChanged += new System.EventHandler(this.txtSearchLeft_TextChanged);
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
            this.lstLeftTable.Location = new System.Drawing.Point(6, 120);
            this.lstLeftTable.Name = "lstLeftTable";
            this.lstLeftTable.Size = new System.Drawing.Size(308, 229);
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
            this.grpRight.Controls.Add(this.btnRunCustomScriptsRight);
            this.grpRight.Controls.Add(this.txtSearchRight);
            this.grpRight.Controls.Add(this.chkSelectAllRight);
            this.grpRight.Controls.Add(this.btnLoadRightDatabase);
            this.grpRight.Controls.Add(this.lstRightTable);
            this.grpRight.Controls.Add(this.btnRightDialog);
            this.grpRight.Controls.Add(this.txtRight);
            this.grpRight.Location = new System.Drawing.Point(352, 12);
            this.grpRight.Name = "grpRight";
            this.grpRight.Size = new System.Drawing.Size(320, 390);
            this.grpRight.TabIndex = 1;
            this.grpRight.TabStop = false;
            this.grpRight.Text = "Database 2";
            // 
            // txtSearchRight
            // 
            this.txtSearchRight.Location = new System.Drawing.Point(6, 94);
            this.txtSearchRight.Name = "txtSearchRight";
            this.txtSearchRight.Size = new System.Drawing.Size(308, 20);
            this.txtSearchRight.TabIndex = 7;
            this.txtSearchRight.TextChanged += new System.EventHandler(this.txtSearchRight_TextChanged);
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
            this.lstRightTable.Location = new System.Drawing.Point(6, 120);
            this.lstRightTable.Name = "lstRightTable";
            this.lstRightTable.Size = new System.Drawing.Size(308, 229);
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
            this.btnSendLeftToRight.Location = new System.Drawing.Point(103, 471);
            this.btnSendLeftToRight.Name = "btnSendLeftToRight";
            this.btnSendLeftToRight.Size = new System.Drawing.Size(103, 27);
            this.btnSendLeftToRight.TabIndex = 2;
            this.btnSendLeftToRight.Text = "Left to Right";
            this.btnSendLeftToRight.UseVisualStyleBackColor = true;
            this.btnSendLeftToRight.Click += new System.EventHandler(this.btnSendLeftToRight_Click);
            // 
            // btnSendRightToLeft
            // 
            this.btnSendRightToLeft.Location = new System.Drawing.Point(485, 471);
            this.btnSendRightToLeft.Name = "btnSendRightToLeft";
            this.btnSendRightToLeft.Size = new System.Drawing.Size(103, 27);
            this.btnSendRightToLeft.TabIndex = 3;
            this.btnSendRightToLeft.Text = "Right to Left";
            this.btnSendRightToLeft.UseVisualStyleBackColor = true;
            this.btnSendRightToLeft.Click += new System.EventHandler(this.btnSendRightToLeft_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.ForeColor = System.Drawing.Color.DarkRed;
            this.lblProgress.Location = new System.Drawing.Point(320, 471);
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
            this.lblTabela.Location = new System.Drawing.Point(320, 458);
            this.lblTabela.Name = "lblTabela";
            this.lblTabela.Size = new System.Drawing.Size(46, 13);
            this.lblTabela.TabIndex = 7;
            this.lblTabela.Text = "Tabela";
            // 
            // btnRunCustomScriptsRight
            // 
            this.btnRunCustomScriptsRight.Location = new System.Drawing.Point(80, 355);
            this.btnRunCustomScriptsRight.Name = "btnRunCustomScriptsRight";
            this.btnRunCustomScriptsRight.Size = new System.Drawing.Size(195, 29);
            this.btnRunCustomScriptsRight.TabIndex = 8;
            this.btnRunCustomScriptsRight.Text = "Run Custom Scripts";
            this.btnRunCustomScriptsRight.UseVisualStyleBackColor = true;
            this.btnRunCustomScriptsRight.Click += new System.EventHandler(this.btnRunCustomScriptsRight_Click);
            // 
            // btnRunCustomScriptsLeft
            // 
            this.btnRunCustomScriptsLeft.Location = new System.Drawing.Point(48, 355);
            this.btnRunCustomScriptsLeft.Name = "btnRunCustomScriptsLeft";
            this.btnRunCustomScriptsLeft.Size = new System.Drawing.Size(195, 29);
            this.btnRunCustomScriptsLeft.TabIndex = 9;
            this.btnRunCustomScriptsLeft.Text = "Run Custom Scripts";
            this.btnRunCustomScriptsLeft.UseVisualStyleBackColor = true;
            this.btnRunCustomScriptsLeft.Click += new System.EventHandler(this.btnRunCustomScriptsLeft_Click);
            // 
            // grpTransferType
            // 
            this.grpTransferType.Controls.Add(this.rdoUnionData);
            this.grpTransferType.Controls.Add(this.rdoReplaceData);
            this.grpTransferType.Location = new System.Drawing.Point(12, 408);
            this.grpTransferType.Name = "grpTransferType";
            this.grpTransferType.Size = new System.Drawing.Size(660, 47);
            this.grpTransferType.TabIndex = 10;
            this.grpTransferType.TabStop = false;
            this.grpTransferType.Text = "Transfer Type";
            // 
            // rdoReplaceData
            // 
            this.rdoReplaceData.AutoSize = true;
            this.rdoReplaceData.Location = new System.Drawing.Point(223, 20);
            this.rdoReplaceData.Name = "rdoReplaceData";
            this.rdoReplaceData.Size = new System.Drawing.Size(91, 17);
            this.rdoReplaceData.TabIndex = 0;
            this.rdoReplaceData.Text = "Replace Data";
            this.rdoReplaceData.UseVisualStyleBackColor = true;
            // 
            // rdoUnionData
            // 
            this.rdoUnionData.AutoSize = true;
            this.rdoUnionData.Checked = true;
            this.rdoUnionData.Location = new System.Drawing.Point(368, 20);
            this.rdoUnionData.Name = "rdoUnionData";
            this.rdoUnionData.Size = new System.Drawing.Size(79, 17);
            this.rdoUnionData.TabIndex = 1;
            this.rdoUnionData.TabStop = true;
            this.rdoUnionData.Text = "Union Data";
            this.rdoUnionData.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(12, 504);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(660, 121);
            this.listBox1.TabIndex = 11;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 631);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.grpTransferType);
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
            this.grpTransferType.ResumeLayout(false);
            this.grpTransferType.PerformLayout();
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
        private System.Windows.Forms.TextBox txtSearchLeft;
        private System.Windows.Forms.TextBox txtSearchRight;
        private System.Windows.Forms.Button btnRunCustomScriptsLeft;
        private System.Windows.Forms.Button btnRunCustomScriptsRight;
        private System.Windows.Forms.GroupBox grpTransferType;
        private System.Windows.Forms.RadioButton rdoUnionData;
        private System.Windows.Forms.RadioButton rdoReplaceData;
        private System.Windows.Forms.ListBox listBox1;
    }
}

