using CleanArchitectureCQRSTemplate.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureCQRSTemplate.Data
{
    /// <summary>
    /// 保存 Typed DataSet 的 XML 資料
    /// </summary>
    /// <typeparam name="TSQL"></typeparam>
    public class SQLStoreClass<TSQL> : IRepository<TSQL>
        where TSQL : class
    {
        TSQL SqlTabData = default(TSQL);
        string SqlXMLFileName
        {
            get
            {
                string XmlFileName = "";
                if (SqlTabData is SQLStoreDataSet.SQLTableDataTable)
                    XmlFileName = "SQLStoreClass.xml";
                else if (SqlTabData is SQLStoreDataSet.SqlConnectionTableDataTable)
                    XmlFileName = "SQLConnectionClass.xml";

                string appSettingPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WistronITsORM");
                if (!Directory.Exists(appSettingPath))
                    Directory.CreateDirectory(appSettingPath);
                return Path.Combine(appSettingPath, XmlFileName);
            }
        }

        public SQLStoreClass() { }

        #region CreateInstance
        protected virtual void CreateInstance()
        {
            SqlTabData = Activator.CreateInstance<TSQL>();//new SQLStoreDataSet.SQLTableDataTable();
        }
        #endregion

        #region Fill Data 重新Fill資料，不管目前資料版本，重新到磁碟讀取資料。
        /// <summary>
        /// Fill Data 重新Fill資料，不管目前資料版本，重新到磁碟讀取資料。
        /// </summary>
        void Fill()
        {
            CreateInstance();

            if (!File.Exists(SqlXMLFileName))
            {
                lock (this)
                {
                    (SqlTabData as DataTable).WriteXml(SqlXMLFileName);
                }
            }
            else
            {
                (SqlTabData as DataTable).ReadXml(SqlXMLFileName);
            }
        }
        #endregion

        #region 實際的寫入作業
        /// <summary>
        /// 實際的寫入作業
        /// </summary>
        void Write()
        {
            if (SqlTabData == null)
                return;

            lock (this)
            {
                (SqlTabData as DataTable).WriteXml(SqlXMLFileName);
            }
        }
        #endregion

        #region 取得目前資料的筆數
        /// <summary>
        /// 取得目前資料的筆數。
        /// </summary>
        public int getSQLCount
        {
            get
            {
                if (SqlTabData == null)
                    CreateInstance();

                Fill();
                return (SqlTabData as DataTable).Rows.Count;
            }
        }
        #endregion

        #region 判斷是否已經有這個 MenuTitle 的資料。
        /// <summary>
        /// 判斷是否已經有這個 MenuTitle 的資料。
        /// </summary>
        /// <param name="MenuTitle"></param>
        /// <returns></returns>
        public bool CheckDuplicate(string ContentText)
        {
            Fill();
            if (SqlTabData is SQLStoreDataSet.SQLTableDataTable)
                return (SqlTabData as DataTable).Select(string.Format("SQLCommandName='{0}'", ContentText)).Length > 0;
            else if (SqlTabData is SQLStoreDataSet.SqlConnectionTableDataTable)
                return (SqlTabData as DataTable).Select(string.Format("DataSourceName='{0}'", ContentText)).Length > 0;
            else
                return false;
        }
        #endregion

        #region 取得所有資料
        /// <summary>
        /// 取得所有資料.
        /// </summary>
        /// <returns></returns>
        public TSQL GetAllData()
        {
            Fill();
            return this.SqlTabData;
        }
        #endregion

        #region 取得 Anonymous Object for SQL.
        /// <summary>
        /// 取得 Anonymous Object for SQL.
        /// </summary>
        /// <typeparam name="T">泛型物件，但傳入 Anonymous 物件</typeparam>
        /// <param name="MenuTitle"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public T GetOneDataByMenuTitle<T>(string MenuTitle, T type)
        {
            Fill();
            DataRow[] dr = (SqlTabData as DataTable).Select(string.Format("SQLCommandName='{0}'", MenuTitle));
            if (dr.Length > 0)
            {
                object SqlObj = Utils.Cast(
                    new { SQL = dr[0]["SQL"].ToString(), SQLCommandName = dr[0]["SQLCommandName"].ToString() },
                    new { SQL = "", SQLCommandName = "" });
                return (T)SqlObj;
            }
            return default(T);
        }
        #endregion
        /// <summary>
        /// 取得一筆資料 (DataRow)
        /// </summary>
        /// <param name="Title">要查詢的條件 (如果是：SqlDataTable 就是SQLCommandName，如果是SqlConnectionTable 就使用DataSourceName)</param>
        /// <returns></returns>
        public DataRow GetOne(string Title)
        {
            DataRow result = null;
            if (SqlTabData is SQLStoreDataSet.SqlConnectionTableDataTable) //如果是：SqlConnectionTableDataTable 就是DataSourceName
            {
                SQLStoreDataSet.SqlConnectionTableDataTable c = SqlTabData as SQLStoreDataSet.SqlConnectionTableDataTable;
                DataRow[] dr = c.Select(string.Format("DataSourceName='{0}'", Title));
                if (dr.Length > 0)
                    result = dr[0];
            }
            else if (SqlTabData is SQLStoreDataSet.SQLTableDataTable) //如果是SQLTableDataTable 就使用SQLCommandName
            {
                SQLStoreDataSet.SQLTableDataTable dt = SqlTabData as SQLStoreDataSet.SQLTableDataTable;
                DataRow[] dr = dt.Select(string.Format("SQLCommandName='{0}'", Title));
                if (dr.Length > 0)
                    result = dr[0];
            }
            return result;
        }

        #region Add 方法.將SQL Statement 加入到資料集中
        /// <summary>
        /// 將SQL Statement 加入到資料集中
        /// </summary>
        /// <param name="MenuTitle"></param>
        /// <param name="SQLStatement"></param>
        public void Add(string Title, string ContextText, string UserID, string Password)
        {
            Fill();

            if (SqlTabData is SQLStoreDataSet.SQLTableDataTable)
            {
                if (CheckDuplicate(Title))
                    throw new Exception(string.Format("已經有名稱為 '{0}' 的SQL Statement, 請使用其它名稱!", ContextText));

                object ID = (SqlTabData as SQLStoreDataSet.SQLTableDataTable).Max(a => a["ID"]);
                if (ID == null)
                    ID = 0;

                SQLStoreDataSet.SQLTableRow SqlDR = (SqlTabData as SQLStoreDataSet.SQLTableDataTable).NewSQLTableRow();
                SqlDR["ID"] = int.Parse(ID.ToString()) + 1;
                SqlDR["SQL"] = ContextText;
                SqlDR["SQLCommandName"] = Title;
                SqlDR["CreateDate"] = DateTime.Now;
                (SqlTabData as SQLStoreDataSet.SQLTableDataTable).Rows.Add(SqlDR);
            }
            else if (SqlTabData is SQLStoreDataSet.SqlConnectionTableDataTable)
            {
                if (CheckDuplicate(Title))
                {
                    Edit(Title, ContextText, UserID, Password);
                    return;
                }
                SQLStoreDataSet.SqlConnectionTableRow CnnDR = (SqlTabData as SQLStoreDataSet.SqlConnectionTableDataTable).NewSqlConnectionTableRow();
                CnnDR["ConnName"] = Title;
                CnnDR["DataSourceName"] = Title;
                CnnDR["InitialCatalogName"] = ContextText;
                CnnDR["UserID"] = UserID;
                CnnDR["Password"] = Password;
                CnnDR["IsDefault"] = false;
                (SqlTabData as SQLStoreDataSet.SqlConnectionTableDataTable).Rows.Add(CnnDR);
            }

            try
            {
                Write();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 更新特定的一筆資料
        /// <summary>
        /// 更新特定的一筆資料
        /// </summary>
        /// <param name="MenuTitle"></param>
        /// <param name="SQLStatement"></param>
        public void Edit(string Title, string ContentText, string UserID, string Password)
        {
            Fill();
            DataRow[] drStore = null;
            if (SqlTabData is SQLStoreDataSet.SQLTableDataTable)
            {
                drStore = (SqlTabData as SQLStoreDataSet.SQLTableDataTable).Select(string.Format("SQLCommandName='{0}'", Title));
                drStore[0]["SQL"] = ContentText;
            }
            else if (SqlTabData is SQLStoreDataSet.SqlConnectionTableDataTable)
            {
                SQLStoreDataSet.SqlConnectionTableDataTable tabCnn = SqlTabData as SQLStoreDataSet.SqlConnectionTableDataTable;
                SetIsDefaultByDataSourceName(tabCnn, Title);
                drStore = tabCnn.Select(string.Format("ConnName='{0}'", Title));
                drStore[0]["InitialCatalogName"] = ContentText;
                drStore[0]["UserID"] = UserID;
                drStore[0]["Password"] = Password;
            }
            if (drStore == null)
                return;

            Write();
        }
        #endregion
        public void SetIsDefaultByDataSourceName(string DataSourceName)
        {
            if (SqlTabData is SQLStoreDataSet.SqlConnectionTableDataTable)
            {
                Fill();
                SetIsDefaultByDataSourceName(this.SqlTabData as SQLStoreDataSet.SqlConnectionTableDataTable, DataSourceName);
                Write();
            }
        }
        /// <summary>
        /// 將 DataSourceName 設定為預設的連線.
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="DataSourceName"></param>
        private void SetIsDefaultByDataSourceName(
            SQLStoreDataSet.SqlConnectionTableDataTable tab,
            string DataSourceName)
        {
            foreach (DataRow row in tab.Rows)
                row["IsDefault"] = false;
            DataRow[] drCnn = tab.Select(string.Format("DataSourceName='{0}'", DataSourceName));
            foreach (DataRow rowSelect in drCnn)
                rowSelect["IsDefault"] = true;
        }
        #region 刪除 SQL Statement (目前未實做)
        /// <summary>
        /// 刪除 SQL Statement (目前未實做)
        /// </summary>
        /// <param name="MenuTitle"></param>
        public void Del(string ContentText)
        {
        }
        #endregion
    }
}
