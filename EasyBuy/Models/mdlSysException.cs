using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlantDelightPortal.Models
{
    public class mdlSysException
    {
        #region  Common variables
        #endregion

        #region  Private variables
        private int _ID;
        private string _TransactionID;
        private string _UserId;
        private string _ExCode;
        private string _ExMessage;
        private string _ExStackTrace;
        private string _InnerExCode;
        private string _InnerExMessage;
        private string _InnerExStackTrace;
        private string _ExDate;
        private bool _Archive;
        private string _CompanyCode;
        private string _IPAddress;
        private string _MacAddress;
        private string _Version;
        private string _PCName;

        #endregion

        #region  Properties
        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        public string TransactionID
        {
            get
            {
                return _TransactionID;
            }
            set
            {
                _TransactionID = value;
            }
        }

        public string UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                _UserId = value;
            }
        }

        public string ExCode
        {
            get
            {
                return _ExCode;
            }
            set
            {
                _ExCode = value;
            }
        }

        public string ExMessage
        {
            get
            {
                return _ExMessage;
            }
            set
            {
                _ExMessage = value;
            }
        }

        public string ExStackTrace
        {
            get
            {
                return _ExStackTrace;
            }
            set
            {
                _ExStackTrace = value;
            }
        }

        public string InnerExCode
        {
            get
            {
                return _InnerExCode;
            }
            set
            {
                _InnerExCode = value;
            }
        }

        public string InnerExMessage
        {
            get
            {
                return _InnerExMessage;
            }
            set
            {
                _InnerExMessage = value;
            }
        }

        public string InnerExStackTrace
        {
            get
            {
                return _InnerExStackTrace;
            }
            set
            {
                _InnerExStackTrace = value;
            }
        }

        public string ExDate
        {
            get
            {
                return _ExDate;
            }
            set
            {
                _ExDate = value;
            }
        }

        public bool Archive
        {
            get
            {
                return _Archive;
            }
            set
            {
                _Archive = value;
            }
        }

        public string CompanyCode
        {
            get
            {
                return _CompanyCode;
            }
            set
            {
                _CompanyCode = value;
            }
        }

        public string IPAddress
        {
            get
            {
                return _IPAddress;
            }
            set
            {
                _IPAddress = value;
            }
        }

        public string MacAddress
        {
            get
            {
                return _MacAddress;
            }
            set
            {
                _MacAddress = value;
            }
        }

        public string Version
        {
            get
            {
                return _Version;
            }
            set
            {
                _Version = value;
            }
        }

        public string PCName
        {
            get
            {
                return _PCName;
            }
            set
            {
                _PCName = value;
            }
        }

        #endregion

        public string strAction { get; set; }
        public string Id { get; set; }
        public string Ids { get; set; }
        //public sysLog objsysLog { get; set; }
        public string strMessage { get; set; }
        public string strMessageType { get; set; }
        public string CompanyName { get; set; }
    }
}