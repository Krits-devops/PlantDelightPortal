using PlantDelightPortal.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Areas.Admin.Classes
{
    public class clsSecurityQuestions
    {
        public List<mdlSecurityQuestions> ListAll()
        {
            List<mdlSecurityQuestions> lstSecurityQuestions = new List<mdlSecurityQuestions>();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SecurityQuestionsSP";
                cmd.Parameters.AddWithValue("@Action", "All");
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstSecurityQuestions.Add(new mdlSecurityQuestions
                    {
                        Id = Convert.ToString(dr["Id"]),
                        Question = Convert.ToString(dr["Question"])
                    });
                }
                con.Close();
            }
            return lstSecurityQuestions;
        }

        public bool Validation(mdlSecurityQuestions objModel, string strAction)
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

        public int Add(mdlSecurityQuestions objModel)
        {
            int ResultCount = 0;
            if (Validation(objModel, "Add"))
            {
                using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("SecurityQuestionsSP", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Action", "Create");
                    com.Parameters.AddWithValue("@Id", objModel.Id);
                    com.Parameters.AddWithValue("@Question", objModel.Question);
                    ResultCount = com.ExecuteNonQuery();
                }
            }

            return ResultCount;
        }

        public mdlSecurityQuestions GetbyId(string Id)
        {
            mdlSecurityQuestions objModel = new mdlSecurityQuestions();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SecurityQuestionsSP";
                cmd.Parameters.AddWithValue("@Action", "Select");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objModel.Id = Convert.ToString(dr["Id"]);
                    objModel.Question = Convert.ToString(dr["Question"]);
                }
                con.Close();
            }
            return objModel;
        }

        public int Update(mdlSecurityQuestions objModel)
        {
            int i;
            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("SecurityQuestionsSP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Update");
                com.Parameters.AddWithValue("@Id", objModel.Id);
                com.Parameters.AddWithValue("@Question", objModel.Question);
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
                SqlCommand com = new SqlCommand("SecurityQuestionsSP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Delete");
                com.Parameters.AddWithValue("@Id", Id);
                i = com.ExecuteNonQuery();
            }
            return i;
        }
    }
}