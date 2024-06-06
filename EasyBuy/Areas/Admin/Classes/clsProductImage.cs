using PlantDelightPortal.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace PlantDelightPortal.Areas.Admin.Classes
{
    public class clsProductImage
    {
        #region Variable Declaration
        private SqlCommand cmd;
        private DataSet ds;
        #endregion

        #region CRUD Operation
        public int UploadProductImage(mdlProductImage objModel)
        {
            int ResultCount = 0;
            if (Validation(objModel, "Create"))
            {
                using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("ProductImageSP", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "CreateUpdate");
                    cmd.Parameters.AddWithValue("@Id", objModel.Id);
                    cmd.Parameters.AddWithValue("@ProductId", objModel.ProductId);
                    cmd.Parameters.AddWithValue("@ImagePath", objModel.ImagePath);
                    ResultCount = cmd.ExecuteNonQuery();
                }
            }
            return ResultCount;
        }

        public bool Validation(mdlProductImage objModel, string strAction)
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

        public void DeleteProductImage(string Id)
        {
            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("ProductImageSP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Delete");
                com.Parameters.AddWithValue("@Id", Id);
                com.ExecuteNonQuery();
            }
        }

        public void DeleteProductImageFile(string Id)
        {
            string ImagePath = null;

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("ProductImageSP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SelectById");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ImagePath = Convert.ToString(dr["ImagePath"]);
                }
                con.Close();

                if (File.Exists(ImagePath))
                    File.Delete(ImagePath);
            }
        }
        #endregion

        #region Other
        public List<mdlProductImage> GetDataByProductId(string ProductId)
        {
            List<mdlProductImage> lstProductImage = new List<mdlProductImage>();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ProductImageSP";
                cmd.Parameters.AddWithValue("@Action", "SelectByProductId");
                cmd.Parameters.AddWithValue("@ProductId", ProductId);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstProductImage.Add(new mdlProductImage
                    {
                        Id = Convert.ToString(dr["Id"]),
                        ProductId = Convert.ToString(dr["ProductId"]),
                        ImagePath = Convert.ToString(dr["ImagePath"])
                    });
                }
                con.Close();
            }
            return lstProductImage;
        }
        #endregion
    }
}