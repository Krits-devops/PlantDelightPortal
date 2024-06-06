using PlantDelightPortal.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Areas.Admin.Classes
{
    public class clsAttributeChoice
    {
        public List<mdlAttributeChoice> ListAll()
        {
            List<mdlAttributeChoice> lstAttributeChoice = new List<mdlAttributeChoice>();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AttributeChoiceSP";
                cmd.Parameters.AddWithValue("@Action", "All");
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstAttributeChoice.Add(new mdlAttributeChoice
                    {
                        Id = Convert.ToString(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        AttributeName = Convert.ToString(dr["AttributeName"])
                    });
                }
                con.Close();
            }
            return lstAttributeChoice;
        }

        public bool Validation(mdlAttributeChoice objModel, string strAction)
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

        public int Add(mdlAttributeChoice objModel)
        {
            int ResultCount = 0;
            if (Validation(objModel, "Add"))
            {
                using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("AttributeChoiceSP", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Action", "Create");
                    com.Parameters.AddWithValue("@Id", objModel.Id);
                    com.Parameters.AddWithValue("@Name", objModel.Name);
                    com.Parameters.AddWithValue("@AttributeId", objModel.AttributeId);
                    ResultCount = com.ExecuteNonQuery();
                }
            }

            return ResultCount;
        }

        public mdlAttributeChoice GetbyId(string Id)
        {
            mdlAttributeChoice objModel = new mdlAttributeChoice();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AttributeChoiceSP";
                cmd.Parameters.AddWithValue("@Action", "Select");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objModel.Id = Convert.ToString(dr["Id"]);
                    objModel.Name = Convert.ToString(dr["Name"]);
                    objModel.AttributeId = Convert.ToString(dr["AttributeId"]);
                }
                con.Close();
            }
            return objModel;
        }

        public int Update(mdlAttributeChoice objModel)
        {
            int i;
            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("AttributeChoiceSP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Update");
                com.Parameters.AddWithValue("@Id", objModel.Id);
                com.Parameters.AddWithValue("@Name", objModel.Name);
                com.Parameters.AddWithValue("@AttributeId", objModel.AttributeId);
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
                SqlCommand com = new SqlCommand("AttributeChoiceSP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Delete");
                com.Parameters.AddWithValue("@Id", Id);
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        #region Common Methods
        public List<mdlAttributeChoice> GetAttributeChoiceByAttributeId(string AttributeId)
        {
            List<mdlAttributeChoice> lstAttributeChoice = new List<mdlAttributeChoice>();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AttributeChoiceSP";
                cmd.Parameters.AddWithValue("@Action", "GetAttributeChoiceByAttributeId");
                cmd.Parameters.AddWithValue("@AttributeId", AttributeId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstAttributeChoice.Add(new mdlAttributeChoice
                    {
                        Id = Convert.ToString(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        AttributeName = Convert.ToString(dr["AttributeName"])
                    });
                }
                con.Close();
            }
            return lstAttributeChoice;
        }
        #endregion
    }
}