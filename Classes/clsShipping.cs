using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Classes
{
    public class clsShipping
    {
        public int Create(mdlShipping objModel)
        {
            int ResultCount = 0;

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ShippingSP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Create");
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
                cmd.Parameters.AddWithValue("@UserId", objModel.UserId);
                cmd.Parameters.AddWithValue("@FirstName", objModel.FirstName);
                cmd.Parameters.AddWithValue("@LastName", objModel.LastName);
                cmd.Parameters.AddWithValue("@AddressLine1", objModel.AddressLine1);
                cmd.Parameters.AddWithValue("@AddressLine2", objModel.AddressLine2);
                cmd.Parameters.AddWithValue("@CityId", objModel.CityId);
                cmd.Parameters.AddWithValue("@PostalCode", objModel.PostalCode);
                cmd.Parameters.AddWithValue("@MobileNo", objModel.MobileNo);
                cmd.Parameters.AddWithValue("@Email", objModel.Email);
                ResultCount = cmd.ExecuteNonQuery();
            }

            return ResultCount;
        }

        public List<mdlUserOrderlist> GetUserOrderList(string Id)
        {
            List<mdlUserOrderlist> lstUserOrderlist = new List<mdlUserOrderlist>();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ShippingSP";
                cmd.Parameters.AddWithValue("@Action", "GetUserOrderList");
                cmd.Parameters.AddWithValue("@UserId", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstUserOrderlist.Add(new mdlUserOrderlist
                    {
                        Id = Convert.ToString(dr["Id"]),
                        OrderNo = Convert.ToString(dr["OrderNo"]),
                        OrderDate = Convert.ToString(dr["OrderDate"]),
                        OrderStatus = Convert.ToString(dr["OrderStatus"]),
                        OrderAmount = Convert.ToString(dr["OrderAmount"]),
                        ItemCount = Convert.ToString(dr["ItemCount"]),
                        SName = Convert.ToString(dr["SName"]),
                        AddressLine1 = Convert.ToString(dr["AddressLine1"]),
                        AddressLine2 = Convert.ToString(dr["AddressLine2"]),
                        City = Convert.ToString(dr["City"]),
                        Province = Convert.ToString(dr["Province"]),
                        Country = Convert.ToString(dr["Country"]),
                        PostalCode = Convert.ToString(dr["PostalCode"]),
                        MobileNo = Convert.ToString(dr["MobileNo"])
                    });
                }
                con.Close();
            }
            return lstUserOrderlist;
        }
    }
}