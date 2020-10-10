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
    public partial class frmMyORMappingWindow : Form
    {
        public frmMyORMappingWindow()
        {
            InitializeComponent();
        }

        private void frmMyORMappingWindow_Load(object sender, EventArgs e)
        {
            //ConnectionServices.ConnectionInfo = new ConnectionWindow.SqlConnectionInfo();
            if (selectedTables != null)
                selectedTables.Clear();
        }
        private static List<string> selectedTables = null;
        /// <summary>
        /// 紀錄畫面所選擇的Tables名稱
        /// </summary>
        public static List<string> SelectedTables
        {
            get
            {
                if (selectedTables == null)
                    selectedTables = new List<string>();
                return selectedTables;
            }
            set { selectedTables = value; }
        }

        private void SetLog(string LogText)
        {
            txtLog.Text += string.Format("{0}\r\n", LogText);
        }

        private void btnGetTables_Click(object sender, EventArgs e)
        {
            SchemaData schemaData = new SchemaData();
            try
            {
                RefreshTreeView(schemaData);
                btnNext.Enabled = true;
                ConnectionServices.CleanErrorMsg();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("讀取資料表時發生錯誤. SysInfo={0}", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnNext.Enabled = false;
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
            if (result.Count() > 0)
                btnNext.Enabled = !btnNext.Enabled;

            SetLog("讀取資料表完成.");
            treeView1.ExpandAll();
        }

        #region 下一步 Button
        private void btnNext_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }
        #endregion

        #region 點選 TreeView 的 Node 的 CheckBox 之後觸發的事件.
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

            int NodeCheckedCount = 0;
            if (e.Action == TreeViewAction.ByMouse)
            {
                CheckSelectNodeCount(sender as TreeView, ref NodeCheckedCount);
            }
            btnNext.Enabled = NodeCheckedCount > 0;
        }
        #endregion

        private void CheckSelectNodeCount(TreeView treeView, ref int NodeCheckedCount)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                CheckTreeNodeCount(node, ref NodeCheckedCount);
            }
        }

        #region 當有任一個 CheckBox 被選取時(Root不算在內)，下一步的 BUTTON 才可以 ON 起來 (遞迴方式處裡)
        /// <summary>
        /// 當有任一個 CheckBox 被選取時(Root不算在內)，下一步的 BUTTON 才可以 ON 起來 (遞迴方式處裡)
        /// </summary>
        /// <param name="node"></param>
        /// <param name="NodeCheckedCount"></param>
        private void CheckTreeNodeCount(TreeNode node, ref int NodeCheckedCount)
        {
            if (node.Checked && node.Name != "Root")
                NodeCheckedCount++;

            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode subNode in node.Nodes)
                {
                    CheckTreeNodeCount(subNode, ref NodeCheckedCount);
                }
            }
        }
        #endregion

        private void frmMyORMappingWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel)
                throw new WizardCancelledException("使用者取消作業！");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
            //throw new WizardCancelledException("使用者取消作業！");
        }
    }
}
