using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureCQRSTemplate.Data
{
    public class SQLStore : DAL
    {
        /// <summary>
        /// 使用 TableName 取得一個空的 Schema DataTable.
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public DataTable GetNoDataDataTableByName(string TableName)
        {
            return Query(
                string.Format(@"select TOP 0 A.* from {0} A", TableName)).Tables[0];
        }
        /// <summary>
        /// 使用 TableName 取得 PrimaryKey 欄位，並以 string[] 回傳.
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public string[] GetTableKeyByName(string TableName)
        {
            string Sql = @"
						SELECT Col.Column_Name As ColumnName from 
							INFORMATION_SCHEMA.TABLE_CONSTRAINTS Tab, 
							INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE Col 
						WHERE 
							Col.Constraint_Name = Tab.Constraint_Name
							AND Col.Table_Name = Tab.Table_Name
							AND Constraint_Type = 'PRIMARY KEY'
							AND Col.Table_Name = @TableName";

            DataTable dt = Query(
                Sql,
                new SqlParameter[] {
                    new SqlParameter("@TableName", TableName)
                }).Tables[0];

            var result = dt.AsEnumerable().Select(c => c["ColumnName"].ToString());
            if (result.Count() > 0)
            {
                return result.ToArray();
            }
            else
            {
                var result2 = dt.AsEnumerable().Select(c => c["ColumnName"].ToString());
                return result2.ToArray();
            }

        }
    }
}
