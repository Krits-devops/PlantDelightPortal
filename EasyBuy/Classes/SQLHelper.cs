using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EasyBuy.Classes
{
    public class SQLHelper
    {
        #region Class Variables
        private SqlTransaction _sqltrn = null;
        private SqlConnection _sqlcon = null;
        private SqlCommand _sqlcom = null;
        private Boolean _result = false;
        private String typevalue = String.Empty;
        private Dictionary<String, SqlDbType> _output = null;
        private Dictionary<String, String> _paranames = null;
        public static string ConnectionString;
        #endregion

        public SQLHelper()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["EasyBuyDB"].ConnectionString;
        }

        public SQLHelper(string ConnectionStringLocal)
        {
            ConnectionString = ConnectionStringLocal;
        }

        public void CreateObjects(Boolean istransaction)
        {
            _sqlcon = new SqlConnection(SQLHelper.ConnectionString);
            _sqlcon.Open();
            if (istransaction)
                _sqltrn = _sqlcon.BeginTransaction(IsolationLevel.Serializable);
            _sqlcom = new SqlCommand();
            _sqlcom.Connection = _sqlcon;
            if (istransaction)
                _sqlcom.Transaction = _sqltrn;
        }

        public void CommitTransaction()
        {
            try
            {
                if (_sqltrn != null)
                    _sqltrn.Commit();
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
                if (_sqltrn != null)
                    _sqltrn.Rollback();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ClearObjects()
        {
            if (_sqlcom != null)
            {
                if (_sqlcom.Parameters.Count != 0)
                    _sqlcom.Parameters.Clear();
                _sqlcom.Cancel();
                _sqlcom.Dispose();
                _sqlcom = null;
            }
            if (_sqltrn != null)
            {
                _sqltrn.Dispose();
                _sqltrn = null;
            }
            if (_sqlcon != null)
            {
                if (_sqlcon.State == ConnectionState.Open)
                    _sqlcon.Close();
                _sqlcon.Dispose();
                _sqlcon = null;
            }

            if (_output != null)
                _output = null;
            if (_paranames != null)
                _paranames = null;
        }

        public DataSet SelectProcDataDS(string Proc, SqlCommand cmd)
        {
            DataSet _data = null;
            string strAction = "";
            try
            {
                CreateObjects(false);
                if (cmd.Parameters.Contains("@Action"))
                    strAction = Convert.ToString(cmd.Parameters["@Action"].Value);

                _data = null;
                _sqlcom.CommandText = Proc;
                _sqlcom.CommandType = CommandType.StoredProcedure;
                _sqlcom.CommandTimeout = 120;
                _sqlcom.Parameters.Clear();
                foreach (SqlParameter parameter in cmd.Parameters)
                {
                    SqlParameter spNew = new SqlParameter();
                    spNew.Value = parameter.Value;
                    spNew.ParameterName = parameter.ParameterName;

                    _sqlcom.Parameters.Add(spNew);
                }
                SqlDataAdapter _adapter = new SqlDataAdapter(_sqlcom);
                DataSet dataset = new DataSet("SQLHelper");
                _adapter.Fill(dataset);
                _data = dataset;
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                ClearObjects();
                clsSysException objclsSysException = new clsSysException();
                objclsSysException.SetException(ex, Proc + "." + strAction);
                return new DataSet();
            }
            return _data;
        }

        public Boolean IUDProcData(string Proc, SqlCommand cmd, Boolean istran = true)
        {
            string strAction = "";
            try
            {
                if (istran == true)
                    CreateObjects(istran);

                if (cmd.Parameters.Contains("@Action"))
                    strAction = Convert.ToString(cmd.Parameters["@Action"].Value);

                _result = false;
                _sqlcom.CommandText = Proc;
                _sqlcom.CommandType = CommandType.StoredProcedure;
                _sqlcom.Parameters.Clear();
                foreach (SqlParameter parameter in cmd.Parameters)
                {
                    SqlParameter spNew = new SqlParameter();
                    spNew.Value = parameter.Value;
                    spNew.ParameterName = parameter.ParameterName;
                    _sqlcom.Parameters.Add(spNew);
                }
                _sqlcom.ExecuteNonQuery();
                _result = true;
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                ClearObjects();
                if (Proc.ToString().ToUpper() != "SysExceptionLog".ToString().ToUpper())
                {
                    clsSysException objclsSysException = new clsSysException();
                    objclsSysException.SetException(ex, Proc + "." + strAction);
                }
                return false;
            }
            return _result;
        }

        public bool ExecuteScalar(string strProcName, SqlCommand cmd)
        {
            string strAction = "";
            try
            {
                CreateObjects(false);

                if (cmd.Parameters.Contains("@Action"))
                    strAction = Convert.ToString(cmd.Parameters["@Action"].Value);

                _sqlcom.CommandText = strProcName;
                _sqlcom.CommandType = CommandType.StoredProcedure;
                _sqlcom.Parameters.Clear();
                foreach (SqlParameter parameter in cmd.Parameters)
                {
                    SqlParameter spNew = new SqlParameter();
                    spNew.Value = parameter.Value;
                    spNew.ParameterName = parameter.ParameterName;
                    _sqlcom.Parameters.Add(spNew);
                }
                object tmpObj = null;
                tmpObj = _sqlcom.ExecuteScalar();

                if (!string.IsNullOrEmpty(Convert.ToString(tmpObj)))
                {
                    return Convert.ToBoolean(tmpObj);
                }
                else
                {
                    return false;
                }
            }
            catch (InvalidCastException ex)
            {
                RollBackTransaction();
                ClearObjects();
                clsSysException objclsSysException = new clsSysException();
                objclsSysException.SetException(ex, strProcName + "." + strAction);
                return false;
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                ClearObjects();
                clsSysException objclsSysException = new clsSysException();
                objclsSysException.SetException(ex, strProcName + "." + strAction);
                return false;
            }
        }

        public int ExecuteScalarReturnInteger(string strProcName, SqlCommand cmd)
        {
            string strAction = "";
            try
            {
                CreateObjects(false);

                if (cmd.Parameters.Contains("@Action"))
                    strAction = Convert.ToString(cmd.Parameters["@Action"].Value);

                _sqlcom.CommandText = strProcName;
                _sqlcom.CommandType = CommandType.StoredProcedure;
                _sqlcom.Parameters.Clear();
                foreach (SqlParameter parameter in cmd.Parameters)
                {
                    SqlParameter spNew = new SqlParameter();
                    spNew.Value = parameter.Value;
                    spNew.ParameterName = parameter.ParameterName;
                    _sqlcom.Parameters.Add(spNew);
                }
                object tmpObj = null;
                tmpObj = _sqlcom.ExecuteScalar();

                if (!string.IsNullOrEmpty(Convert.ToString(tmpObj)))
                {
                    return Convert.ToInt32(tmpObj);
                }
                else
                {
                    return 0;
                }
            }
            catch (InvalidCastException ex)
            {
                RollBackTransaction();
                ClearObjects();
                clsSysException objclsSysException = new clsSysException();
                objclsSysException.SetException(ex, strProcName + "." + strAction);
                return 0;
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                ClearObjects();
                clsSysException objclsSysException = new clsSysException();
                objclsSysException.SetException(ex, strProcName + "." + strAction);
                return 0;
            }
        }
    }
}