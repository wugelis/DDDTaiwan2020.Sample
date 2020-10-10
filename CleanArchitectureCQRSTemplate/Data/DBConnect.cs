using CleanArchitectureCQRSTemplate.Common;

namespace CleanArchitectureCQRSTemplate
{
    public class DBConnect
    {
        public static string Connect()
        {
            return ConnectionServices.ConnectionInfo.ConnectionString; //ConfigurationManager.AppSettings["CDCDDbConfig"];
        }
    }
}