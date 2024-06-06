using PlantDelightPortal.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Areas.Admin.Classes
{
    public class clsProvince
    {
        public List<mdlProvince> ListAll()
        {
            List<mdlProvince> lstProvince = new List<mdlProvince>();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ProvinceSP";
                cmd.Parameters.AddWithValue("@Action", "All");
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstProvince.Add(new mdlProvince
                    {
                        Id = Convert.ToString(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        CountryName = Convert.ToString(dr["CountryName"])
                    });
                }
                con.Close();
            }
            return lstProvince;
        }

        public bool Validation(mdlProvince objModel, string strAction)
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

        public int Add(mdlProvince objModel)
        {
            int ResultCount = 0;
            if (Validation(objModel, "Add"))
            {
                using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("ProvinceSP", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Action", "Create");
                    com.Parameters.AddWithValue("@Id", objModel.Id);
                    com.Parameters.AddWithValue("@Name", objModel.Name);
                    com.Parameters.AddWithValue("@CountryId", objModel.CountryId);
                    ResultCount = com.ExecuteNonQuery();
                }
            }

            return ResultCount;
        }

        public mdlProvince GetbyId(string Id)
        {
            mdlProvince objModel = new mdlProvince();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ProvinceSP";
                cmd.Parameters.AddWithValue("@Action", "Select");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objModel.Id = Convert.ToString(dr["Id"]);
                    objModel.Name = Convert.ToString(dr["Name"]);
                    objModel.CountryId = Convert.ToString(dr["CountryId"]);
                }
                con.Close();
            }
            return objModel;
        }

        public int Update(mdlProvince objModel)
        {
            int i;
            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("ProvinceSP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Update");
                com.Parameters.AddWithValue("@Id", objModel.Id);
                com.Parameters.AddWithValue("@Name", objModel.Name);
                com.Parameters.AddWithValue("@CountryId", objModel.CountryId);
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
                SqlCommand com = new SqlCommand("ProvinceSP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Delete");
                com.Parameters.AddWithValue("@Id", Id);
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        #region Common Methods
        public List<mdlProvince> GetProvinceByCountryId(string CountryId)
        {
            List<mdlProvince> lstProvince = new List<mdlProvince>();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ProvinceSP";
                cmd.Parameters.AddWithValue("@Action", "GetProvinceByCountryId");
                cmd.Parameters.AddWithValue("@CountryId", CountryId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstProvince.Add(new mdlProvince
                    {
                        Id = Convert.ToString(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        CountryName = Convert.ToString(dr["CountryName"])
                    });
                }
                con.Close();
            }
            return lstProvince;
        }
        #endregion
    }
}