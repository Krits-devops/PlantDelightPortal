using PlantDelightPortal.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Areas.Admin.Classes
{
    public class clsAttribute
    {
        public List<mdlAttribute> ListAll()
        {
            List<mdlAttribute> lstAttribute = new List<mdlAttribute>();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AttributeSP";
                cmd.Parameters.AddWithValue("@Action", "All");
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstAttribute.Add(new mdlAttribute
                    {
                        Id = Convert.ToString(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        SubCategoryId = Convert.ToString(dr["SubCategoryId"]),
                        SubCategoryName = Convert.ToString(dr["SubCategoryName"]),
                        CategoryId = Convert.ToString(dr["CategoryId"]),
                        CategoryName = Convert.ToString(dr["CategoryName"])
                    });
                }
                con.Close();
            }
            return lstAttribute;
        }

        public bool Validation(mdlAttribute objModel, string strAction)
        {
            if (strAction == "Add")
            {
                if (string.IsNullOrEmpty(objModel.Id))
                {
                    objModel.strMessage = "Id is not available.";
                    return false;
                }
            }

            return true;
        }

        public int Add(mdlAttribute objModel)
        {
            int ResultCount = 0;
            if (Validation(objModel, "Add"))
            {
                using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("AttributeSP", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Action", "Create");
                    com.Parameters.AddWithValue("@Id", objModel.Id);
                    com.Parameters.AddWithValue("@Name", objModel.Name);
                    com.Parameters.AddWithValue("@SubCategoryId", objModel.SubCategoryId);
                    ResultCount = com.ExecuteNonQuery();
                }
            }

            return ResultCount;
        }

        public mdlAttribute GetbyId(string Id)
        {
            mdlAttribute objModel = new mdlAttribute();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AttributeSP";
                cmd.Parameters.AddWithValue("@Action", "Select");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objModel.Id = Convert.ToString(dr["Id"]);
                    objModel.Name = Convert.ToString(dr["Name"]);
                    objModel.SubCategoryId = Convert.ToString(dr["SubCategoryId"]);
                    objModel.SubCategoryName = Convert.ToString(dr["SubCategoryName"]);
                    objModel.CategoryId = Convert.ToString(dr["CategoryId"]);
                    objModel.CategoryName = Convert.ToString(dr["CategoryName"]);
                }
                con.Close();
            }
            return objModel;
        }

        public int Update(mdlAttribute objModel)
        {
            int i;
            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("AttributeSP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Update");
                com.Parameters.AddWithValue("@Id", objModel.Id);
                com.Parameters.AddWithValue("@Name", objModel.Name);
                com.Parameters.AddWithValue("@SubCategoryId", objModel.SubCategoryId);
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        public int Delete(string Id)
        {
            int i;
            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("AttributeSP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Delete");
                com.Parameters.AddWithValue("@Id", Id);
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        #region Common Methods
        public List<mdlAttribute> GetAttributeBySubCategoryId(string SubCategoryId)
        {
            List<mdlAttribute> lstAttribute = new List<mdlAttribute>();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AttributeSP";
                cmd.Parameters.AddWithValue("@Action", "GetAttributeBySubCategoryId");
                cmd.Parameters.AddWithValue("@SubCategoryId", SubCategoryId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstAttribute.Add(new mdlAttribute
                    {
                        Id = Convert.ToString(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        SubCategoryName = Convert.ToString(dr["SubCategoryName"])
                    });
                }
                con.Close();
            }
            return lstAttribute;
        }
        #endregion
    }
}