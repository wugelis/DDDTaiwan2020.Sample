using CleanArchitectureCQRSTemplate.Common;
using CleanArchitectureCQRSTemplate.Data;
//using Microsoft.Internal.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanArchitectureCQRSTemplate.Forms
{
    public partial class ConnectionWindow : Form
    {
        /// <summary>
        /// 連線視窗 Connection Window
        /// 說明：使用 IsConnect 維護目前是否已連線，未連練則自動跳出 Connection Window 畫面.
        /// </summary>
        public class SqlConnectionInfo
        {
            public int WindowCount { get; set; }
            public bool IsConnect { get; set; }
            /// <summary>
            /// 取得連線狀態
            /// </summary>
            public ConnectionState ConnectionStatus
            {
                get
                {
                    if (IsConnect)
                        return ConnectionState.Open;
                    else
                        return ConnectionState.Closed;
                }
            }
            public string DataSourceName { get; set; }
            public string UserId { get; set; }
            public string Password { get; set; }
            public string Initial_Catalog { get; set; }
            public bool? IntegratedSecurity { get; set; }
            public bool? UseLocalDB { get; set; }
            public int AuthType { get; set; }
            /// <summary>
            /// 取得連線字串.
            /// </summary>
            public string ConnectionString
            {
                get
                {
                    string result = string.Empty;
                    System.Windows.Forms.DialogResult connectResult = DialogResult.None;

                    if (ConnectionStatus != ConnectionState.Open)
                    {
                        if (ConnectionServices.ConnectionInfo.WindowCount < 1)
                        {
                            /*
                            ConnectionWindow cw = new ConnectionWindow();
                            IsConnect = cw.ShowDialog() == DialogResult.OK; //將對話框的(Yes/No)作為IsConnect狀態值.
                            */
                            //Exception connEx = new Exception("");
                            connectResult = ConnectionServices.ShowConnectWindow();
                        }
                    }

                    result = GetConnectionString();

                    IsConnect = ToOpenConnection(result);

                    return result;
                }
            }

            private static string GetConnectionString()
            {
                string result = string.Empty;

                if (ConnectionServices.ConnectionInfo.UseLocalDB.HasValue && ConnectionServices.ConnectionInfo.UseLocalDB.Value)
                {
                    result = string.Format(
                                "Data Source={0};AttachDbFilename={1};Integrated Security=true",
                                ConnectionServices.ConnectionInfo.DataSourceName,
                                ConnectionServices.ConnectionInfo.Initial_Catalog);
                }
                else
                {
                    switch (ConnectionServices.ConnectionInfo.AuthType)
                    {
                        case 0: //SQL Server 驗證
                            result = string.Format(
                                "Data Source={0};Initial Catalog={1};User Id={2};Password={3}",
                                ConnectionServices.ConnectionInfo.DataSourceName,
                                ConnectionServices.ConnectionInfo.Initial_Catalog,
                                ConnectionServices.ConnectionInfo.UserId,
                                ConnectionServices.ConnectionInfo.Password);
                            break;
                        case 1: //Windows 驗證
                            result = string.Format(
                                "Data Source={0};Initial Catalog={1};User Id={2};Password={3};Integrated Security=true",
                                ConnectionServices.ConnectionInfo.DataSourceName,
                                ConnectionServices.ConnectionInfo.Initial_Catalog,
                                ConnectionServices.ConnectionInfo.UserId,
                                ConnectionServices.ConnectionInfo.Password);
                            break;
                    }
                }

                return result;
            }

            /// <summary>
            /// 嘗試開啟連線字串 （成功：True; 失敗：False）
            /// </summary>
            /// <param name="connectionString"></param>
            /// <returns></returns>
            public bool ToOpenConnection(string connectionString)
            {
                SqlConnection conn = new SqlConnection(connectionString);
                bool result = true;
                try
                {
                    conn.Open();
                }
                catch(Exception ex)
                {
                    //connEx = ex;
                    result = false;
                }
                return result;
            }
            /// <summary>
            /// 嘗試開啟連線字串 [請先指定自身連線字串的 相關 屬性] （成功：True; 失敗：False）
            /// </summary>
            /// <returns></returns>
            public bool ToOpenConnection()
            {
                string connectionString = GetConnectionString();

                return ToOpenConnection(connectionString);
            }
        }

        public ConnectionWindow()
        {
            InitializeComponent();
            GetData();
            ConnectionServices.ConnectionInfo.WindowCount++;
            cbAuthType.SelectedIndex = 0;
        }

        ~ConnectionWindow()
        {
            ConnectionServices.ConnectionInfo.WindowCount--;
        }
        //取得一個 SqlConnectionTableDataTable 的 泛型 SqlStoreTable 物件.
        SQLStoreClass<SQLStoreDataSet.SqlConnectionTableDataTable> ConnStore
                        = new SQLStoreClass<SQLStoreDataSet.SqlConnectionTableDataTable>();

        private void GetData()
        {
            SQLStoreDataSet.SqlConnectionTableDataTable tabConn = ConnStore.GetAllData();
            cbServer.DisplayMember = "DataSourceName";
            cbServer.ValueMember = "DataSourceName";
            cbServer.DataSource = tabConn;
            //帶入上一次的 Default 值.
            var result = tabConn.Rows.OfType<DataRow>().Where(r => (bool)r["IsDefault"]).FirstOrDefault();
            if (result != null)
            {
                cbServer.Text = result["DataSourceName"].ToString();
                txtPassword.Text = result["Password"].ToString();
                txtUserID.Text = result["UserId"].ToString();
                cbInitialCatalog.Text = result["InitialCatalogName"].ToString();
                chkSetDefault.Checked = (bool)result["IsDefault"];
                chkUseLocalDB.Checked = result["UseLocalDB"] != DBNull.Value ? (bool)result["UseLocalDB"] : false;
            }
        }

        private void GetInitialCatalogData()
        {
            try
            {
                SetConnectionInfo(true);
                SchemaData schemaData = new SchemaData();
                var result = schemaData.GetInitialCatalogData();
                cbInitialCatalog.DisplayMember = "SCHEMA_Field02";
                cbInitialCatalog.ValueMember = "SCHEMA_Field01";
                cbInitialCatalog.DataSource = result.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("取得資料庫名稱時發生錯誤. SysInfo={0}", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetConnectionInfo(bool isConnect)
        {
            try
            {
                ConnectionServices.ConnectionInfo = new SqlConnectionInfo()
                {
                    DataSourceName = cbServer.Text,
                    Initial_Catalog = cbInitialCatalog.Text,
                    UserId = txtUserID.Text,
                    Password = txtPassword.Text,
                    IsConnect = isConnect,
                    IntegratedSecurity = cbAuthType.SelectedIndex == 1,
                    UseLocalDB = chkUseLocalDB.Checked,
                    AuthType = cbAuthType.SelectedIndex
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ConnStore.Add(cbServer.Text,
                    cbInitialCatalog.Text,
                    txtUserID.Text,
                    txtPassword.Text);
                if (chkSetDefault.Checked)
                    ConnStore.SetIsDefaultByDataSourceName(cbServer.Text);
                SetConnectionInfo(true);
                this.DialogResult = DialogResult.OK;
                //ConnectionServices.CleanErrorMsg();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("取得資料庫時發生錯誤. SysInfo={0}", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //ConnectionServices.Error(ex);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            Close();
        }

        private void cbInitialCatalog_DropDown(object sender, EventArgs e)
        {
            GetInitialCatalogData();
        }

        private void cbAuthType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            var selectAuth = cb.SelectedIndex;
            txtUserID.Enabled = selectAuth == 0;
            txtPassword.Enabled = selectAuth == 0;
        }

        protected string OriginalServer = string.Empty;
        private void chkUseLocalDB_CheckedChanged(object sender, EventArgs e)
        {
            OriginalServer = !chkUseLocalDB.Checked ? cbServer.Text : "";

            switch (chkUseLocalDB.Checked)
            {
                case true:
                    cbAuthType.SelectedIndex = 1;
                    cbServer.Text = @"(LocalDB)\v11.0";
                    cbServer.Enabled = true;
                    break;
                case false:
                    cbAuthType.SelectedIndex = 1;
                    cbServer.Text = OriginalServer;
                    cbServer.Enabled = false;
                    break;
            }
        }
    }
}
