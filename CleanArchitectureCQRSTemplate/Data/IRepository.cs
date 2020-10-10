using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureCQRSTemplate.Data
{
    /// <summary>
    /// Design Pattern for Repository.
    /// </summary>
    public interface IRepository<T>
    {
        /// <summary>
        /// 新增 SQL Statement 到儲存體物件.
        /// </summary>
        /// <param name="MenuTitle"></param>
        /// <param name="SQLStatement"></param>
        void Add(string Title, string ContextText, string UserID, string Password);
        /// <summary>
        /// 新增儲存體物件的 SQL Statement.
        /// </summary>
        /// <param name="MenuTitle"></param>
        /// <param name="SQLStatement"></param>
        void Edit(string Title, string ContextText, string UserID, string Password);
        /// <summary>
        /// 刪除儲存體物件的 SQL Statement.
        /// </summary>
        void Del(string MenuTitle);
        /// <summary>
        /// 取得所有的 SQL Statement.
        /// </summary>
        /// <returns></returns>
        T GetAllData();
    }
}
