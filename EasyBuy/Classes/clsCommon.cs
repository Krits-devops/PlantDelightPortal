using PlantDelightPortal.Areas.Admin.Classes;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Classes
{
    public class clsCommon
    {
        #region General Functions
        public int CheckDataExists(string TableName = "", string ColumnName = "", string ColumnValue = "", string WhereCondition = "")
        {
            int ResultCount = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("CheckDataExistsSP", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (!string.IsNullOrEmpty(TableName))
                        cmd.Parameters.AddWithValue("@TableName", TableName);
                    if (!string.IsNullOrEmpty(ColumnName))
                        cmd.Parameters.AddWithValue("@ColumnName", ColumnName);
                    if (!string.IsNullOrEmpty(ColumnValue))
                        cmd.Parameters.AddWithValue("@ColumnValue", ColumnValue);
                    if (!string.IsNullOrEmpty(WhereCondition))
                        cmd.Parameters.AddWithValue("@WhereCondition", WhereCondition);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds);

                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                        ResultCount = ds.Tables[0].Rows.Count;
                }
            }
            catch (Exception ex)
            {
                return ResultCount;
            }
            return ResultCount;
        }
        #endregion

        #region Exception
        public void CreateException(string Method = "", string Msg = "")
        {
            SqlConnection con = new SqlConnection(new clsSQL().ConnectionString);
            if (con.State == ConnectionState.Closed)
                con.Open();

            try
            {
                SqlCommand com = new SqlCommand("ExceptionSP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Create");
                com.Parameters.AddWithValue("@Method", Method);
                com.Parameters.AddWithValue("@Msg", Msg);
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        #endregion

        public static string Get_SQLDate_From_DateString(string strDate)
        {
            if (string.IsNullOrEmpty(strDate))
                return null;

            string[] dates = strDate.Split('/');
            return Convert.ToInt32(dates[2]) + "-" + Convert.ToInt32(dates[1]) + "-" + Convert.ToInt32(dates[0]);
        }
    }
}