using EasyBuy.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;

namespace EasyBuy.Classes
{
    public class clsSysException
    {
        #region  Common variables
        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();
        private SQLHelper ObjSQLHelperMaster = new SQLHelper();
        private SqlCommand cmd = new SqlCommand();
        #endregion

        public void SetException(Exception ex, string TransId = "")
        {
            mdlSysException objmdlSysException = new mdlSysException();
            objmdlSysException.ExCode = Convert.ToString(ex.GetHashCode());
            objmdlSysException.ExMessage = ex.Message;
            objmdlSysException.ExStackTrace = Convert.ToString(ex.StackTrace) + "|" + System.Web.HttpContext.Current.Request.UrlReferrer;

            string hostname = Dns.GetHostName();
            string IP = Dns.GetHostEntry(hostname).AddressList[0].ToString();

            objmdlSysException.IPAddress = IP;
            objmdlSysException.MacAddress = GetMACAddress();
            objmdlSysException.PCName = hostname;
            if (ex.InnerException != null)
            {
                objmdlSysException.InnerExCode = Convert.ToString(ex.InnerException.GetHashCode());
                objmdlSysException.InnerExMessage = Convert.ToString(ex.InnerException.Message);
                objmdlSysException.InnerExStackTrace = Convert.ToString(ex.InnerException.StackTrace);
            }
            if (!string.IsNullOrEmpty(TransId))
            {
                if (TransId.Length > 2000)
                {
                    TransId = TransId.Substring(-1, 1999);
                }
            }
            objmdlSysException.TransactionID = TransId;
            Create(objmdlSysException);
        }

        private void SetParameters(Enums.Action Action, mdlSysException objmdlSysException)
        {
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@Action", Action.ToString());
            cmd.Parameters.AddWithValue("@ID", objmdlSysException.ID);
            cmd.Parameters.AddWithValue("@Ids", objmdlSysException.Ids);
            cmd.Parameters.AddWithValue("@TransactionID", objmdlSysException.TransactionID);
            cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"]);
            cmd.Parameters.AddWithValue("@ExCode", objmdlSysException.ExCode);
            cmd.Parameters.AddWithValue("@ExMessage", objmdlSysException.ExMessage);
            cmd.Parameters.AddWithValue("@ExStackTrace", objmdlSysException.ExStackTrace);
            cmd.Parameters.AddWithValue("@InnerExCode", objmdlSysException.InnerExCode);
            cmd.Parameters.AddWithValue("@InnerExMessage", objmdlSysException.InnerExMessage);
            cmd.Parameters.AddWithValue("@InnerExStackTrace", objmdlSysException.InnerExStackTrace);
            cmd.Parameters.AddWithValue("@Archive", objmdlSysException.Archive);
            cmd.Parameters.AddWithValue("@CompanyCode", objmdlSysException.CompanyCode);
            cmd.Parameters.AddWithValue("@IPAddress", objmdlSysException.IPAddress);
            cmd.Parameters.AddWithValue("@MacAddress", objmdlSysException.MacAddress);
            cmd.Parameters.AddWithValue("@Version", objmdlSysException.Version);
            cmd.Parameters.AddWithValue("@PCName", objmdlSysException.PCName);
        }

        public void Create(mdlSysException objmdlSysException)
        {
            ObjSQLHelperMaster = new SQLHelper();
            cmd = new SqlCommand();
            SetParameters(Enums.Action.CREATE, objmdlSysException);
            if (!(ObjSQLHelperMaster.IUDProcData("SysExceptionLog", cmd)))
            {
                ObjSQLHelperMaster.RollBackTransaction();
                clsCommon.LogActivity("SysLog Fail" + objmdlSysException.TransactionID + ":" + objmdlSysException.ExMessage);
            }
            else
            {
                ObjSQLHelperMaster.CommitTransaction();
            }
            ObjSQLHelperMaster.ClearObjects();
        }

        public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            string sMACAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMACAddress == string.Empty)
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMACAddress = adapter.GetPhysicalAddress().ToString();

                }
            }
            return sMACAddress;
        }
    }
}