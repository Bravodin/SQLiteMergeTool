using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLiteMergeTool
{
    public partial class frmMain : Form
    {
        Core.Functions funct = new Core.Functions();
        public frmMain()
        {
            InitializeComponent();
            frmMain.CheckForIllegalCrossThreadCalls = false;
        }

        private void OpenDialog(TextBox target)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "SQLite files (*.sqlite)|*.sqlite";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                target.Text = dialog.FileName;
            }
        }

        private void btnLeftDialog_Click(object sender, EventArgs e)
        {
            OpenDialog(txtLeft);
        }

        private void btnRightDialog_Click(object sender, EventArgs e)
        {
            OpenDialog(txtRight);
        }

        private void btnLoadLeftDatabase_Click(object sender, EventArgs e)
        {
            if (txtLeft.Text != "")
            {
                foreach (var x in funct.LoadDataBaseTables(txtLeft.Text).OrderBy(f => f))
                    lstLeftTable.Items.Add(x);
            }
        }

        private void btnLoadRightDatabase_Click(object sender, EventArgs e)
        {
            if (txtRight.Text != "")
            {
                foreach (var x in funct.LoadDataBaseTables(txtRight.Text).OrderBy(f => f))
                    lstRightTable.Items.Add(x);
            }
        }

        private void chkSelectAllLeft_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lstLeftTable.Items.Count; i++)
            {
                lstLeftTable.SetItemChecked(i, chkSelectAllLeft.Checked);
            }
        }

        private void chkSelectAllRight_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lstRightTable.Items.Count; i++)
            {
                lstRightTable.SetItemChecked(i, chkSelectAllLeft.Checked);
            }
        }

        public void TransferLeftTable()
        {
            funct.InsertProgress += Funct_InsertProgress;

            foreach (var x in lstLeftTable.SelectedItems)
            {
                lblTabela.Text = x.ToString();
                funct.TransferTable(txtLeft.Text, txtRight.Text, x.ToString());
            }

            for (int i = 0; i < lstLeftTable.Items.Count; i++)
            {
                lstLeftTable.SetItemChecked(i, false);
            }

            btnLeftDialog.Enabled = true;
            btnRightDialog.Enabled = true;
        }

        public void TransferRightTable()
        {
            funct.InsertProgress += Funct_InsertProgress;

            foreach (var x in lstRightTable.SelectedItems)
            {
                lblTabela.Text = x.ToString();
                funct.TransferTable(txtRight.Text, txtLeft.Text, x.ToString());
            }

            for (int i = 0; i < lstRightTable.Items.Count; i++)
            {
                lstRightTable.SetItemChecked(i, false);
            }

            btnLeftDialog.Enabled = true;
            btnRightDialog.Enabled = true;
        }

        private void btnSendLeftToRight_Click(object sender, EventArgs e)
        {
            btnLeftDialog.Enabled = false;
            btnRightDialog.Enabled = false;
            new System.Threading.Thread(TransferLeftTable).Start();
        }

        private void Funct_InsertProgress(object sender, Core.InsertProgressEventArgs e)
        {
            lblProgress.Text = e.CurrentValue + " / " + e.TotalValue;
        }

        private void btnSendRightToLeft_Click(object sender, EventArgs e)
        {
            new System.Threading.Thread(TransferRightTable).Start();
        }
    }
}
