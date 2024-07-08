using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Classes
{
    public class clsProductRating
    {
        public mdlProductRatingReview GetUserProductRating(string ProductId = "", string UserId = "")
        {
            mdlProductRatingReview objModel = new mdlProductRatingReview();
            SqlConnection con = new SqlConnection(new clsSQL().ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("ProductRatingSP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "GetUserProductRating");
                if (!string.IsNullOrEmpty(ProductId))
                    cmd.Parameters.AddWithValue("@ProductId", ProductId);
                if (!string.IsNullOrEmpty(UserId))
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objModel.Id = Convert.ToString(dr["Id"]);
                    objModel.ProductId = Convert.ToString(dr["ProductId"]);
                    objModel.Rating = Convert.ToDecimal(dr["Rating"]);
                    objModel.Review = Convert.ToString(dr["Review"]);

                    objModel.ImagePath = Convert.ToString(dr["ImagePath"]);
                    objModel.Name = Convert.ToString(dr["Name"]);
                    objModel.StkQty = Convert.ToDecimal(dr["StkQty"]);
                    objModel.Price = Convert.ToDecimal(dr["Price"]);
                    objModel.Description = Convert.ToString(dr["Description"]);
                    objModel.ProductRating = Convert.ToDecimal(dr["ProductRating"]);
                }
            }
            catch (Exception ex)
            {
                new clsCommon().CreateException("GetUserProductRating", ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return objModel;
        }

        public int AddProductRating(mdlProductRatingReview objModel)
        {
            int ResultCount = 0;
            SqlConnection con = new SqlConnection(new clsSQL().ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("ProductRatingSP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "CreateUpdate");
                if (!string.IsNullOrEmpty(objModel.Id))
                    cmd.Parameters.AddWithValue("@Id", objModel.Id);
                if (!string.IsNullOrEmpty(objModel.ProductId))
                    cmd.Parameters.AddWithValue("@ProductId", objModel.ProductId);
                if (!string.IsNullOrEmpty(Convert.ToString(objModel.Rating)))
                    cmd.Parameters.AddWithValue("@Rating", objModel.Rating);
                if (!string.IsNullOrEmpty(objModel.Review))
                    cmd.Parameters.AddWithValue("@Review", objModel.Review);
                if (!string.IsNullOrEmpty(objModel.UserId))
                    cmd.Parameters.AddWithValue("@UserId", objModel.UserId);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                ResultCount = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                new clsCommon().CreateException("AddProductRating", ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return ResultCount;
        }

        public List<mdlProductRatingReview> lstProductRatingReviewByProductId(string ProductId = "")
        {
            List<mdlProductRatingReview> lstProductRatingReview = new List<mdlProductRatingReview>();
            SqlConnection con = new SqlConnection(new clsSQL().ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("ProductRatingSP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "GetProductRatingReviewByProductId");
                if (!string.IsNullOrEmpty(ProductId))
                    cmd.Parameters.AddWithValue("@ProductId", ProductId);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstProductRatingReview.Add(new mdlProductRatingReview()
                    {
                        UserName = Convert.ToString(dr["UserName"]),
                        Rating = Convert.ToDecimal(dr["Rating"]),
                        Review = Convert.ToString(dr["Review"])
                    });
                }
            }
            catch (Exception ex)
            {
                new clsCommon().CreateException("lstProductRatingReviewByProductId", ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return lstProductRatingReview;
        }
    }
}