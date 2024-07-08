using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Classes
{
    public class clsDynamicFilter
    {
        public List<mdlDynamicFilter> getDynamicFilter(string SubCategoryId)
        {
            SqlConnection con = new SqlConnection(new clsSQL().ConnectionString);
            List<mdlDynamicFilter> lstmdlDynamicFilter = new List<mdlDynamicFilter>();

            try
            {
                SqlCommand cmd = new SqlCommand("DynamicFilterSP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "getDynamicFilter");
                if (!string.IsNullOrEmpty(SubCategoryId))
                    cmd.Parameters.AddWithValue("@SubCategoryId", SubCategoryId);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstmdlDynamicFilter.Add(new mdlDynamicFilter
                    {
                        AttributeId = Convert.ToString(dr["AttributeId"]),
                        AttributeName = Convert.ToString(dr["AttributeName"])
                    });
                }
            }
            catch (Exception ex)
            {
                new clsCommon().CreateException("getDynamicFilter", ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return lstmdlDynamicFilter;
        }

        public List<mdlDynamicFilterValue> getDynamicFilterValue(string SubCategoryId)
        {
            SqlConnection con = new SqlConnection(new clsSQL().ConnectionString);
            List<mdlDynamicFilterValue> lstmdlDynamicFilterValue = new List<mdlDynamicFilterValue>();

            try
            {
                SqlCommand cmd = new SqlCommand("DynamicFilterSP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "getDynamicFilterValue");
                if (!string.IsNullOrEmpty(SubCategoryId))
                    cmd.Parameters.AddWithValue("@SubCategoryId", SubCategoryId);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstmdlDynamicFilterValue.Add(new mdlDynamicFilterValue
                    {
                        AttributeId = Convert.ToString(dr["AttributeId"]),
                        AttributeValue = Convert.ToString(dr["AttributeValue"])
                    });
                }
            }
            catch (Exception ex)
            {
                new clsCommon().CreateException("getDynamicFilterValue", ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return lstmdlDynamicFilterValue;
        }
    }
}