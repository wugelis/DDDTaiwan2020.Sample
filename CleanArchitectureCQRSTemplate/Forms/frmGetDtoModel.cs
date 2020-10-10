using CleanArchitectureCQRSTemplate.Common;
using CleanArchitectureCQRSTemplate.Data;
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
    public partial class frmGetDtoModel : Form
    {
        
        public frmGetDtoModel()
        {
            InitializeComponent();
        }

        private void btnGetTables_Click(object sender, EventArgs e)
        {
            SchemaData schemaData = new SchemaData();
            try
            {
                RefreshTreeView(schemaData);
                btnOK.Enabled = true;
                ConnectionServices.CleanErrorMsg();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("讀取資料表時發生錯誤. SysInfo={0}", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnOK.Enabled = false;
                ConnectionServices.Error(ex);
                ConnectionServices.ShowConnectWindow();
                RefreshTreeView(schemaData);
            }
        }

        private void RefreshTreeView(SchemaData schemaData)
        {
            var result = schemaData.GetTableData();
            //SetLog("讀取 Schema..");
            treeView1.Nodes[0].Text = ConnectionServices.ConnectionInfo.Initial_Catalog;
            //SetLog(string.Format("讀取資料庫 {0}..", ConnectionServices.ConnectionInfo.Initial_Catalog));
            treeView1.Nodes[0].Nodes.Clear();
            foreach (var schema in result)
            {
                TreeNode node = new TreeNode(schema.SCHEMA_Field03);
                treeView1.Nodes[0].Nodes.Add(node);
            }
            if (result.Count() > 0)
                btnOK.Enabled = !btnOK.Enabled;

            //SetLog("讀取資料表完成.");
            treeView1.ExpandAll();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(treeView1.SelectedNode.Index == 0)
            {
                MessageBox.Show("您必須最少選一個資料表以當作建立該 DTO 欄位參考。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// 撰寫一個新的 ShowDialog() 方法以傳回多個參數
        /// </summary>
        /// <returns></returns>
        public new Tuple<DialogResult, string> ShowDialog()
        {
            DialogResult result = base.ShowDialog();
            return Tuple.Create(result, treeView1.SelectedNode.Text);
        }
    }
}
