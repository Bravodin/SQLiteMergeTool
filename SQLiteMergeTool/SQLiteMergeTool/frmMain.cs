using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLiteMergeTool
{
    public partial class frmMain : Form
    {
        List<String> _leftTables = new List<String>();
        List<String> _rightTables = new List<String>();

        public static ListBoxLog listBoxLog;

        Core.Functions funct = new Core.Functions();
        public frmMain()
        {
            InitializeComponent();
            frmMain.CheckForIllegalCrossThreadCalls = false;

            listBoxLog = new ListBoxLog(listBox1);
            
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
                txtSearchLeft.Text = "";

                _leftTables = funct.LoadDataBaseTables(txtLeft.Text).OrderBy(f => f).ToList();

                foreach (var x in _leftTables)
                    lstLeftTable.Items.Add(x);
            }
        }

        private void btnLoadRightDatabase_Click(object sender, EventArgs e)
        {
            if (txtRight.Text != "")
            {
                txtSearchRight.Text = "";

                _rightTables = funct.LoadDataBaseTables(txtRight.Text).OrderBy(f => f).ToList();

                foreach (var x in _rightTables)
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
            try
            {
                funct.InsertProgress += Funct_InsertProgress;

                foreach (var x in lstLeftTable.CheckedItems)
                {
                    DateTime start = DateTime.Now;

                    listBoxLog.Log(Level.Info, $"COPY OF {x} INITIALIZED.");

                    lblTabela.Text = x.ToString();
                    funct.TransferTable(txtLeft.Text, txtRight.Text, x.ToString(), rdoReplaceData.Checked ? Core.Functions.TransferType.Replace : Core.Functions.TransferType.Union);

                    DateTime end = DateTime.Now;

                    listBoxLog.Log(Level.Info, $"COPY OF {x} END IN {(end - start).TotalSeconds} SECONDS.");
                }

                for (int i = 0; i < lstLeftTable.Items.Count; i++)
                {
                    lstLeftTable.SetItemChecked(i, false);
                }
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
                listBoxLog.Log(Level.Error, $"ERROR: {ex.Message}");
            }

            btnLeftDialog.Enabled = true;
            btnRightDialog.Enabled = true;
        }

        public void TransferRightTable()
        {
            try
            {
                funct.InsertProgress += Funct_InsertProgress;

                foreach (var x in lstRightTable.CheckedItems)
                {
                    DateTime start = DateTime.Now;

                    listBoxLog.Log(Level.Info, $"COPY OF {x} INITIALIZED.");

                    lblTabela.Text = x.ToString();
                    funct.TransferTable(txtRight.Text, txtLeft.Text, x.ToString(), rdoReplaceData.Checked ? Core.Functions.TransferType.Replace : Core.Functions.TransferType.Union);

                    DateTime end = DateTime.Now;

                    listBoxLog.Log(Level.Info, $"COPY OF {x} END IN {(end - start).TotalSeconds} SECONDS.");
                }

                for (int i = 0; i < lstRightTable.Items.Count; i++)
                {
                    lstRightTable.SetItemChecked(i, false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                listBoxLog.Log(Level.Error, $"ERROR: {ex.Message}");
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
            btnLeftDialog.Enabled = false;
            btnRightDialog.Enabled = false;
            new System.Threading.Thread(TransferRightTable).Start();
        }

        private void txtSearchLeft_TextChanged(object sender, EventArgs e)
        {
            if (_leftTables.Count > 0)
            {
                lstLeftTable.Items.Clear();
                foreach (var x in _leftTables.Where(f=>f.StartsWith(txtSearchLeft.Text)))
                    lstLeftTable.Items.Add(x);
            }
        }

        private void txtSearchRight_TextChanged(object sender, EventArgs e)
        {
            if (_rightTables.Count > 0)
            {
                lstRightTable.Items.Clear();
                foreach (var x in _rightTables.Where(f => f.StartsWith(txtSearchRight.Text)))
                    lstRightTable.Items.Add(x);
            }
        }

        private void btnRunCustomScriptsLeft_Click(object sender, EventArgs e)
        {
            try
            {
                btnRunCustomScriptsLeft.Enabled = false;
                listBoxLog.Log(Level.Info, $"Custom scripts on {txtLeft.Text} INITIALIZED");
                
                funct.RunCustomScripts(txtLeft.Text);

                btnLoadLeftDatabase.PerformClick();

                listBoxLog.Log(Level.Info, $"Custom scripts on {txtLeft.Text} FINISH");
            }
            catch (Exception ex)
            {
                listBoxLog.Log(Level.Error, $"ERROR: {ex.Message}");
            }
            finally
            {
                btnRunCustomScriptsLeft.Enabled = true;
            }
            
        }

        private void btnRunCustomScriptsRight_Click(object sender, EventArgs e)
        {
            try
            {
                btnRunCustomScriptsRight.Enabled = false;
                listBoxLog.Log(Level.Info, $"Custom scripts on {txtRight.Text} INITIALIZED");

                funct.RunCustomScripts(txtRight.Text);

                btnLoadRightDatabase.PerformClick();

                listBoxLog.Log(Level.Info, $"Custom scripts on {txtRight.Text} FINISH");

            }
            catch (Exception ex)
            {
                listBoxLog.Log(Level.Error, $"ERROR: {ex.Message}");
            }
            finally
            {
                btnRunCustomScriptsRight.Enabled = true;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }
    }
}
