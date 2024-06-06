using PlantDelightPortal.Areas.Admin.Models;
using PlantDelightPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Areas.Admin.Classes
{
    public class clsProduct
    {
        #region Variable Declaration

        private SqlCommand cmd;
        private DataSet ds;

        #endregion

        #region CRUD Operation

        public int CreateProduct(mdlProduct objModel)
        {
            int ResultCount = 0;
            if (Validation(objModel, "Create"))
            {
                using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("ProductSP", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "Create");
                    cmd.Parameters.AddWithValue("@Id", objModel.Id);
                    cmd.Parameters.AddWithValue("@Name", objModel.Name);
                    cmd.Parameters.AddWithValue("@SubCategoryId", objModel.SubCategoryId);
                    cmd.Parameters.AddWithValue("@Price", objModel.Price);
                    cmd.Parameters.AddWithValue("@StkQty", objModel.StkQty);
                    cmd.Parameters.AddWithValue("@Description", objModel.Description);
                    ResultCount = cmd.ExecuteNonQuery();
                }
            }

            return ResultCount;
        }

        public bool Validation(mdlProduct objModel, string strAction)
        {
            if (strAction == "Create")
            {
                if (string.IsNullOrEmpty(objModel.Id))
                {
                    objModel.strMessage = "Id is not available.";
                    return false;
                }
            }

            return true;
        }

        public List<mdlProduct> ListAll(string UserId = "")
        {
            List<mdlProduct> lstProduct = new List<mdlProduct>();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ProductSP";
                cmd.Parameters.AddWithValue("@Action", "All");
                if (!string.IsNullOrEmpty(UserId))
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstProduct.Add(new mdlProduct
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
                        Rating = Convert.ToDecimal(dr["Rating"]),
                        InCart = Convert.ToBoolean(dr["InCart"]),
                        InWishList = Convert.ToBoolean(dr["InWishList"])
                    });
                }
                con.Close();
            }
            return lstProduct;
        }

        public int Delete(string Id)
        {
            int i;
            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("ProductSP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Delete");
                com.Parameters.AddWithValue("@Id", Id);
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        #endregion

        public List<mdlProduct> GetProductBySubCategoryId(string SubCategoryId = "", string SortBy = "", string MinPrice = "", string MaxPrice = "", string FilterRating = "", string whereCondition = "", string UserId = "")
        {
            List<mdlProduct> lstProduct = new List<mdlProduct>();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ProductSP";
                cmd.Parameters.AddWithValue("@Action", "GetProductBySubCategoryId");

                if (!string.IsNullOrEmpty(SubCategoryId))
                    cmd.Parameters.AddWithValue("@SubCategoryId", SubCategoryId);
                if (!string.IsNullOrEmpty(SortBy))
                    cmd.Parameters.AddWithValue("@SortBy", SortBy);
                if (!string.IsNullOrEmpty(MinPrice))
                    cmd.Parameters.AddWithValue("@MinPrice", MinPrice);
                if (!string.IsNullOrEmpty(MaxPrice))
                    cmd.Parameters.AddWithValue("@MaxPrice", MaxPrice);
                if (!string.IsNullOrEmpty(FilterRating))
                    cmd.Parameters.AddWithValue("@FilterRating", FilterRating);
                if (!string.IsNullOrEmpty(whereCondition))
                    cmd.Parameters.AddWithValue("@whereCondition", whereCondition);
                if (!string.IsNullOrEmpty(UserId))
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstProduct.Add(new mdlProduct
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
                        Rating = Convert.ToDecimal(dr["Rating"]),
                        InCart = Convert.ToBoolean(dr["InCart"]),
                        InWishList = Convert.ToBoolean(dr["InWishList"])
                    });
                }
                con.Close();
            }
            return lstProduct;
        }

        public mdlProductDetail GetProductDetailByProductId(string Id, string UserId = "")
        {
            mdlProductDetail objmdlProductDetail = new mdlProductDetail();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ProductSP";
                cmd.Parameters.AddWithValue("@Action", "GetProductDetailByProductId");
                if (!string.IsNullOrEmpty(UserId))
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objmdlProductDetail = new mdlProductDetail()
                    {
                        Id = Convert.ToString(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        Price = Convert.ToInt64(dr["Price"]),
                        StkQty = Convert.ToInt64(dr["StkQty"]),
                        Description = Convert.ToString(dr["Description"]),
                        SubCategoryId = Convert.ToString(dr["SubCategoryId"]),
                        Rating = Convert.ToDecimal(dr["Rating"]),
                        InCart = Convert.ToBoolean(dr["InCart"]),
                        InWishList = Convert.ToBoolean(dr["InWishList"])
                    };
                }
                con.Close();
            }
            return objmdlProductDetail;
        }

        public mdlProduct GetProductDetailByProductIdForEdit(string Id, string UserId = "")
        {
            mdlProduct objmdlProduct = new mdlProduct();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ProductSP";
                cmd.Parameters.AddWithValue("@Action", "GetProductDetailByProductId");
                if (!string.IsNullOrEmpty(UserId))
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objmdlProduct = new mdlProduct()
                    {
                        Id = Convert.ToString(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        CategoryId = Convert.ToString(dr["CategoryId"]),
                        CategoryName = Convert.ToString(dr["CategoryName"]),
                        SubCategoryId = Convert.ToString(dr["SubCategoryId"]),
                        SubCategoryName = Convert.ToString(dr["SubCategoryName"]),
                        Price = Convert.ToInt64(dr["Price"]),
                        StkQty = Convert.ToInt64(dr["StkQty"]),
                        Description = Convert.ToString(dr["Description"])
                    };
                }
                con.Close();
            }
            return objmdlProduct;
        }
    }
}