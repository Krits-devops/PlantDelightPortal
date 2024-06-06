using PlantDelightPortal.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Areas.Admin.Classes
{
    public class clsProductAttribute
    {
        #region Variable Declaration
        private SqlCommand cmd;
        private DataSet ds;
        #endregion

        #region CRUD Operation
        public void CreateProductAttribute(mdlProductAttribute objModel)
        {
            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ProductAttributeSP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Create");
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
                cmd.Parameters.AddWithValue("@ProductId", objModel.ProductId);
                cmd.Parameters.AddWithValue("@AttributeId", objModel.AttributeId);
                cmd.Parameters.AddWithValue("@AttributeValue", objModel.AttributeValue);
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region Other
        public List<mdlProductAttribute> GetDataByProductId(string ProductId)
        {
            List<mdlProductAttribute> lstProductAttribute = new List<mdlProductAttribute>();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ProductAttributeSP";
                cmd.Parameters.AddWithValue("@Action", "SelectByProductId");
                cmd.Parameters.AddWithValue("@ProductId", ProductId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstProductAttribute.Add(new mdlProductAttribute
                    {
                        SrNo = Convert.ToInt32(dr["SrNo"]),
                        Id = Convert.ToString(dr["Id"]),
                        ProductId = Convert.ToString(dr["ProductId"]),
                        AttributeId = Convert.ToString(dr["AttributeId"]),
                        AttributeName = Convert.ToString(dr["AttributeName"]),
                        AttributeValue = Convert.ToString(dr["AttributeValue"])
                    });
                }
                con.Close();
            }
            return lstProductAttribute;
        }
        #endregion
    }
}