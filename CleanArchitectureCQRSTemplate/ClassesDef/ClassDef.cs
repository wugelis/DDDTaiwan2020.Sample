using CleanArchitectureCQRSTemplate.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureCQRSTemplate.ClassesDef
{
    /// <summary>
    /// 產生類別敘述定義
    /// </summary>
    public class ClassDef
    {
        /// <summary>
        /// 取得類別基本描述 (暫時放置程式碼中，建議放置在 Resources 或 Txt 中)
        /// </summary>
        public static string GetClassTemplate
        {
            get
            {
                return @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;$(USING)$
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace $(NAMESPACE_DEF)$.$(QUERY_COMMAND_NAME)$.$(FOLDER_NAME)$
{
	$(CLASS_DEF)$
}";
            }
        }
        /// <summary>
        /// 透過 DataTable 產生類別敘述定義
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string GetClassDef(DataTable dt, CLASS_TYPE classType)
        {
            return GetClassDef(dt, null, classType);
        }
        /// <summary>
        /// 透過 DataTable 產生類別敘述定義
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="className">自訂類別名稱</param>
        /// <returns></returns>
        public string GetClassDef(DataTable dt, string className, CLASS_TYPE classType)
        {
            return GetClassDef(dt, className, new string[] { }, classType);
        }
        /// <summary>
        /// 透過 DataTable 產生類別敘述定義
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="className">自訂類別名稱</param>
        /// <param name="keyValues">Primary Key (string Array)</param>
        /// <returns></returns>
        public string GetClassDef(
            DataTable dt,
            string className,
            string[] keyValues,
            CLASS_TYPE classType)
        {
            string baseClassWord = classType == CLASS_TYPE.DTO ? "" : " : AuditableEntity";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("public class {0}", className == null ? $"{dt.TableName.ToUpperFirstWord()}{baseClassWord}" : $"{className}{baseClassWord}"));
            sb.AppendLine("\t{");

            int columnOrder = 0;
            columnOrder = GetClassProperties(dt, keyValues, sb, columnOrder);
            sb.AppendLine("\t}");
            return sb.ToString();
        }
        /// <summary>
        /// 透過 DataColumn/DataTable 建立 C# 自動屬性 (Auto Properties)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="keyValues"></param>
        /// <param name="sb"></param>
        /// <param name="columnOrder"></param>
        /// <returns></returns>
        public static int GetClassProperties(DataTable dt, string[] keyValues, StringBuilder sb, int columnOrder)
        {
            foreach (DataColumn col in dt.Columns)
            {
                var result = keyValues.Where(c => c == col.ColumnName);
                string attribute = result.Count() > 0 ? result.First() : string.Empty;
                if (col.ColumnName == attribute)
                {
                    sb.AppendLine("\t\t[Key]");
                    sb.AppendLine(string.Format("\t\t[Column(Order = {0})]", columnOrder));
                    columnOrder++;
                }

                switch (col.DataType.ToString())
                {
                    case "System.String":
                        //sb.AppendLine("\t\t[StringLength(50)]");
                        sb.AppendLine(string.Format("\t\tpublic {0} {1} {{get; set;}}", "string", col.ColumnName));
                        break;
                    case "System.Int16":
                        sb.AppendLine(string.Format("\tpublic {0} {1} {{get; set;}}", "short", col.ColumnName));
                        break;
                    case "System.Int32":
                        sb.AppendLine(string.Format("\t\tpublic {0} {1} {{get; set;}}", "int", col.ColumnName));
                        break;
                    case "System.Int64":
                        sb.AppendLine(string.Format("\t\tpublic {0} {1} {{get; set;}}", "long", col.ColumnName));
                        break;
                    case "System.DateTime":
                        sb.AppendLine(string.Format("\t\tpublic {0} {1} {{get; set;}}", "DateTime", col.ColumnName));
                        break;
                    case "System.Byte[]":
                        sb.AppendLine(string.Format("\t\tpublic {0} {1} {{get; set;}}", "byte[]", col.ColumnName));
                        break;
                    case "System.Decimal":
                        sb.AppendLine(string.Format("\t\tpublic {0} {1} {{get; set;}}", "decimal", col.ColumnName));
                        break;
                    case "System.Guid":
                        sb.AppendLine(string.Format("\t\tpublic {0} {1} {{get; set;}}", "Guid", col.ColumnName));
                        break;
                    case "System.Boolean":
                        sb.AppendLine(string.Format("\tpublic {0} {1} {{get; set;}}", "bool", col.ColumnName));
                        break;
                    default:
                        sb.AppendLine(string.Format("\t\tpublic {0} {1} {{get; set;}}", "object", col.ColumnName));
                        break;
                }
                //sb.AppendLine("\n");
            }

            return columnOrder;
        }
    }
}
