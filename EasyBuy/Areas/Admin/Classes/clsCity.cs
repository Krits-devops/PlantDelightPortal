using PlantDelightPortal.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Areas.Admin.Classes
{
    public class clsCity
    {
        public List<mdlCity> ListAll()
        {
            List<mdlCity> lstCity = new List<mdlCity>();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CitySP";
                cmd.Parameters.AddWithValue("@Action", "All");
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstCity.Add(new mdlCity
                    {
                        Id = Convert.ToString(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        ProvinceId = Convert.ToString(dr["ProvinceId"]),
                        ProvinceName = Convert.ToString(dr["ProvinceName"]),
                        CountryId = Convert.ToString(dr["CountryId"]),
                        CountryName = Convert.ToString(dr["CountryName"])
                    });
                }
                con.Close();
            }
            return lstCity;
        }

        public bool Validation(mdlCity objModel, string strAction)
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

        public int Add(mdlCity objModel)
        {
            int ResultCount = 0;
            if (Validation(objModel, "Add"))
            {
                using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("CitySP", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Action", "Create");
                    com.Parameters.AddWithValue("@Id", objModel.Id);
                    com.Parameters.AddWithValue("@Name", objModel.Name);
                    com.Parameters.AddWithValue("@ProvinceId", objModel.ProvinceId);
                    ResultCount = com.ExecuteNonQuery();
                }
            }

            return ResultCount;
        }

        public mdlCity GetbyId(string Id)
        {
            mdlCity objModel = new mdlCity();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CitySP";
                cmd.Parameters.AddWithValue("@Action", "Select");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objModel.Id = Convert.ToString(dr["Id"]);
                    objModel.Name = Convert.ToString(dr["Name"]);
                    objModel.ProvinceId = Convert.ToString(dr["ProvinceId"]);
                    objModel.ProvinceName = Convert.ToString(dr["ProvinceName"]);
                    objModel.CountryId = Convert.ToString(dr["CountryId"]);
                    objModel.CountryName = Convert.ToString(dr["CountryName"]);
                }
                con.Close();
            }
            return objModel;
        }

        public int Update(mdlCity objModel)
        {
            int i;
            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("CitySP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Update");
                com.Parameters.AddWithValue("@Id", objModel.Id);
                com.Parameters.AddWithValue("@Name", objModel.Name);
                com.Parameters.AddWithValue("@ProvinceId", objModel.ProvinceId);
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
                SqlCommand com = new SqlCommand("CitySP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Delete");
                com.Parameters.AddWithValue("@Id", Id);
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        #region Common Methods
        public List<mdlCity> GetCityByProvinceId(string ProvinceId)
        {
            List<mdlCity> lstCity = new List<mdlCity>();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CitySP";
                cmd.Parameters.AddWithValue("@Action", "GetCityByProvinceId");
                cmd.Parameters.AddWithValue("@ProvinceId", ProvinceId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstCity.Add(new mdlCity
                    {
                        Id = Convert.ToString(dr["Id"]),
                        Name = Convert.ToString(dr["Name"])
                    });
                }
                con.Close();
            }
            return lstCity;
        }
        #endregion
    }
}