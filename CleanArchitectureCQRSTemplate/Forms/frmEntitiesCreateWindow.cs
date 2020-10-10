using CleanArchitectureCQRSTemplate.Common;
using CleanArchitectureCQRSTemplate.Data;
using Microsoft.VisualStudio.TemplateWizard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanArchitectureCQRSTemplate.Forms
{
    public partial class frmEntitiesCreateWindow : Form
    {
        private static List<string> _selectedTables;

        public frmEntitiesCreateWindow()
        {
            InitializeComponent();
        }

        public static List<string> SelectedTables
        {
            get => _selectedTables ?? (_selectedTables = new List<string>());
            set => _selectedTables = value;
        }
        private void SetLog(string LogText)
        {
            txtLog.Text += string.Format("{0}\r\n", LogText);
        }

        private void btnGetTable_Click(object sender, EventArgs e)
        {
            SchemaData schemaData = new SchemaData();
            try
            {
                RefreshTreeView(schemaData);
                btnGen.Enabled = true;
                ConnectionServices.CleanErrorMsg();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("讀取資料表時發生錯誤. SysInfo={0}", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGen.Enabled = false;
                ConnectionServices.Error(ex);
                ConnectionServices.ShowConnectWindow();
                RefreshTreeView(schemaData);
            }
        }

        private void RefreshTreeView(SchemaData schemaData)
        {
            var result = schemaData.GetTableData();
            SetLog("讀取 Schema..");
            treeView1.Nodes[0].Text = ConnectionServices.ConnectionInfo.Initial_Catalog;
            SetLog(string.Format("讀取資料庫 {0}..", ConnectionServices.ConnectionInfo.Initial_Catalog));
            treeView1.Nodes[0].Nodes.Clear();
            foreach (var schema in result)
            {
                TreeNode node = new TreeNode(schema.SCHEMA_Field03);
                treeView1.Nodes[0].Nodes.Add(node);
            }
            SetLog("讀取資料表完成.");
            treeView1.ExpandAll();
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Name == "Root")
            {
                foreach (TreeNode n in e.Node.Nodes)
                {
                    n.Checked = e.Node.Checked;
                }
            }
            else
            {
                if (e.Node.Checked)
                {
                    SelectedTables.Add(e.Node.Text);
                }
                else
                    SelectedTables.Remove(e.Node.Text);
            }
        }

        private void frmEntitiesCreateWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel)
                throw new WizardCancelledException("使用者取消作業！");
        }
    }
}
