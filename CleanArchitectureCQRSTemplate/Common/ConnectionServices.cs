using CleanArchitectureCQRSTemplate.Forms;
using Microsoft.Internal.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureCQRSTemplate.Common
{
    /// <summary>
    /// 提供全域、共用的 SqlConnection 連線資訊
    /// 說明：使用 Static 的用意在於確保在行程中 Process，只會有一個 Connection 被產生，所有的 Form 也都共用這個連線資訊
    /// </summary>
    public class ConnectionServices
    {
        private static ConnectionWindow.SqlConnectionInfo _connectionInfo;
        private static string _errorMsg = string.Empty;

        /// <summary>
        /// 對 Assembly 本身公開連線資訊 
        /// </summary>
        internal static ConnectionWindow.SqlConnectionInfo ConnectionInfo { 
            get => _connectionInfo = (_connectionInfo ?? new ConnectionWindow.SqlConnectionInfo());
            set => _connectionInfo = value;
        }

        internal static System.Windows.Forms.DialogResult ShowConnectWindow()
        {
            ConnectionWindow cw = new ConnectionWindow();
            return cw.ShowDialog();
        }

        internal static bool IsConnect
        {
            get => _connectionInfo.IsConnect;
        }

        internal static void Error(Exception ex)
        {
            _errorMsg = ex.Message;
        }

        internal static void CleanErrorMsg()
        {
            _errorMsg = string.Empty;
        }

        internal static bool IsConnectError
        {
            get => !string.IsNullOrEmpty(_errorMsg);
        }
        //internal static bool IsConnectError
        //{
        //    get
        //    {
        //        Exception connEx = new Exception();
        //        bool result = _connectionInfo.ToOpenConnection();
        //        if (result) 
        //        { 
        //            Error(connEx); 
        //        }
        //        return result;
        //    }
        //}
    }
}
