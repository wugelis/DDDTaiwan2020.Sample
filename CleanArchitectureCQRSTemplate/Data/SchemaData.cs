using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureCQRSTemplate.Data
{
    public class SchemaData
    {
        public class SchemaField
        {
            public string SCHEMA_Field01 { get; set; }
            public string SCHEMA_Field02 { get; set; }
            public string SCHEMA_Field03 { get; set; }
            public string SCHEMA_Field04 { get; set; }
        }
        /// <summary>
        /// 取得 DB 的 Schema 定義 (Schema=Database)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SchemaField> GetInitialCatalogData()
        {
            try
            {
                DAL dal = new DAL();
                var result = from schema in dal.GetSchemaDataTable("Databases").AsEnumerable()
                             select new SchemaField
                             {
                                 SCHEMA_Field01 = schema["dbid"].ToString(),
                                 SCHEMA_Field02 = (string)schema["database_name"]
                             };
                return result.OrderBy(c => c.SCHEMA_Field02);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 取得 DB 的 Schema 定義 (Schema=Tables)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SchemaField> GetTableData()
        {
            try
            {
                DAL dal = new DAL();
                var result = from schema in dal.GetSchemaDataTable("Tables").AsEnumerable()
                             where (string)schema["TABLE_TYPE"] == "BASE TABLE" //先只找出 Table 類型的 Schema
                             select new SchemaField
                             {
                                 SCHEMA_Field01 = (string)schema["TABLE_CATALOG"],
                                 SCHEMA_Field02 = (string)schema["TABLE_SCHEMA"],
                                 SCHEMA_Field03 = (string)schema["TABLE_NAME"],
                                 SCHEMA_Field04 = (string)schema["TABLE_TYPE"]
                             };
                return result.OrderBy(c => c.SCHEMA_Field03);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
