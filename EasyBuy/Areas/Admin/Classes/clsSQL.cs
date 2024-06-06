using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Areas.Admin.Classes
{
    public class clsSQL
    {
        #region Class Variables
        public string ConnectionString;
        private SqlTransaction _sqlTrans = null;
        private SqlConnection _sqlCon = null;
        private SqlCommand _sqlCommand = null;
        #endregion

        public clsSQL()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["PlantDelightPortalDB"].ConnectionString;
        }

        public void SQLConWithBeginTrans(SqlCommand _sqlCommand)
        {
            _sqlCon = new SqlConnection(ConnectionString);
            _sqlCon.Open();

            _sqlTrans = _sqlCon.BeginTransaction(IsolationLevel.Serializable);
            _sqlCommand.Connection = _sqlCon;
            _sqlCommand.Transaction = _sqlTrans;
        }

        public void CommitTransaction()
        {
            try
            {
                if (_sqlTrans != null)
                    _sqlTrans.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RollBackTransaction()
        {
            try
            {
                if (_sqlTrans != null)
                    _sqlTrans.Rollback();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ClearObjects()
        {
            if (_sqlCommand != null)
            {
                if (_sqlCommand.Parameters.Count != 0)
                    _sqlCommand.Parameters.Clear();
                _sqlCommand.Cancel();
                _sqlCommand.Dispose();
                _sqlCommand = null;
            }
            if (_sqlTrans != null)
            {
                _sqlTrans.Dispose();
                _sqlTrans = null;
            }
            if (_sqlCon != null)
            {
                if (_sqlCon.State == ConnectionState.Open)
                    _sqlCon.Close();
                _sqlCon.Dispose();
                _sqlCon = null;
            }
        }
    }
}