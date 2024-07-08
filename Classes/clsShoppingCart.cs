using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Classes
{
    public class clsShoppingCart
    {
        public List<mdlUserCart> GetUserCart(string Id)
        {
            List<mdlUserCart> lstUserCart = new List<mdlUserCart>();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ShoppingCartSP";
                cmd.Parameters.AddWithValue("@Action", "GetUserCartByUserId");
                cmd.Parameters.AddWithValue("@UserId", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstUserCart.Add(new mdlUserCart
                    {
                        Id = Convert.ToString(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        CategoryId = Convert.ToString(dr["CategoryId"]),
                        CategoryName = Convert.ToString(dr["CategoryName"]),
                        SubCategoryId = Convert.ToString(dr["SubCategoryId"]),
                        SubCategoryName = Convert.ToString(dr["SubCategoryName"]),
                        Price = Convert.ToInt64(dr["Price"]),
                        StkQty = Convert.ToInt64(dr["StkQty"]),
                        ImagePath = Convert.ToString(dr["ImagePath"]),
                        Description = Convert.ToString(dr["Description"]),
                        Rating = Convert.ToDecimal(dr["Rating"]),
                        Qty = Convert.ToInt64(dr["Qty"])
                    });
                }
                con.Close();
            }
            return lstUserCart;
        }

        public int Add(mdlShoppingCart objModel)
        {
            int ResultCount = 0;
            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("ShoppingCartSP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "CreateDelete");
                com.Parameters.AddWithValue("@Id", objModel.Id);
                com.Parameters.AddWithValue("@UserId", objModel.UserId);
                com.Parameters.AddWithValue("@ProductId", objModel.ProductId);
                com.Parameters.AddWithValue("@Qty", objModel.Qty);
                ResultCount = com.ExecuteNonQuery();
                con.Close();
            }
            return ResultCount;
        }

        public int Delete(mdlShoppingCart objModel)
        {
            int ResultCount = 0;
            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("ShoppingCartSP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Delete");
                com.Parameters.AddWithValue("@UserId", objModel.UserId);
                com.Parameters.AddWithValue("@ProductId", objModel.ProductId);
                ResultCount = com.ExecuteNonQuery();
                con.Close();
            }
            return ResultCount;
        }

        public int UpdateUserCartQty(mdlShoppingCart objModel)
        {
            int ResultCount = 0;
            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("ShoppingCartSP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "UpdateUserCartQty");
                com.Parameters.AddWithValue("@UserId", objModel.UserId);
                com.Parameters.AddWithValue("@ProductId", objModel.ProductId);
                com.Parameters.AddWithValue("@Qty", objModel.Qty);
                ResultCount = com.ExecuteNonQuery();
                con.Close();
            }
            return ResultCount;
        }
    }
}