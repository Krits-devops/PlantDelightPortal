using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Classes
{
    public class clsOrder
    {
        public mdlUserOrderDetail GetUserOrderDetail(string Id = "", string UserId = "")
        {
            mdlUserOrderDetail objModel = new mdlUserOrderDetail();
            SqlConnection con = new SqlConnection(new clsSQL().ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("OrderSP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "GetUserOrderDetail");
                if (!string.IsNullOrEmpty(Id))
                    cmd.Parameters.AddWithValue("@Id", Id);
                if (!string.IsNullOrEmpty(UserId))
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    objModel.OrderNo = Convert.ToString(ds.Tables[0].Rows[0]["OrderNo"]);
                    objModel.ItemCount = Convert.ToDecimal(ds.Tables[0].Rows[0]["ItemCount"]);
                    objModel.SubTotal = Convert.ToDecimal(ds.Tables[0].Rows[0]["SubTotal"]);
                    objModel.TaxAmt = Convert.ToDecimal(ds.Tables[0].Rows[0]["TaxAmt"]);
                    objModel.TotalAmt = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalAmt"]);

                    mdlShipping objmdlShipping = new mdlShipping();

                    if (ds.Tables[0].Columns.Contains("FirstName"))
                        objmdlShipping.FirstName = Convert.ToString(ds.Tables[0].Rows[0]["FirstName"]);
                    if (ds.Tables[0].Columns.Contains("LastName"))
                        objmdlShipping.LastName = Convert.ToString(ds.Tables[0].Rows[0]["LastName"]);
                    if (ds.Tables[0].Columns.Contains("AddressLine1"))
                        objmdlShipping.AddressLine1 = Convert.ToString(ds.Tables[0].Rows[0]["AddressLine1"]);
                    if (ds.Tables[0].Columns.Contains("AddressLine2"))
                        objmdlShipping.AddressLine2 = Convert.ToString(ds.Tables[0].Rows[0]["AddressLine2"]);
                    if (ds.Tables[0].Columns.Contains("CityName"))
                        objmdlShipping.CityName = Convert.ToString(ds.Tables[0].Rows[0]["CityName"]);
                    if (ds.Tables[0].Columns.Contains("ProvinceName"))
                        objmdlShipping.ProvinceName = Convert.ToString(ds.Tables[0].Rows[0]["ProvinceName"]);
                    if (ds.Tables[0].Columns.Contains("CountryName"))
                        objmdlShipping.CountryName = Convert.ToString(ds.Tables[0].Rows[0]["CountryName"]);
                    if (ds.Tables[0].Columns.Contains("PostalCode"))
                        objmdlShipping.PostalCode = Convert.ToString(ds.Tables[0].Rows[0]["PostalCode"]);
                    if (ds.Tables[0].Columns.Contains("MobileNo"))
                        objmdlShipping.MobileNo = Convert.ToString(ds.Tables[0].Rows[0]["MobileNo"]);

                    objModel.objmdlShipping = objmdlShipping;

                    List<mdlProductDetail> lstProductDetail = new List<mdlProductDetail>();

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow item in ds.Tables[1].Rows)
                        {
                            mdlProductDetail objProduct = new mdlProductDetail();

                            objProduct.Id = Convert.ToString(item["Id"]);
                            objProduct.ImagePath = Convert.ToString(item["ImagePath"]);
                            objProduct.Name = Convert.ToString(item["Name"]);
                            objProduct.Price = Convert.ToDecimal(item["Price"]);
                            objProduct.Qty = Convert.ToDecimal(item["Qty"]);
                            objProduct.StkQty = Convert.ToDecimal(item["StkQty"]);
                            objProduct.Description = Convert.ToString(item["Description"]);
                            objProduct.Rating = Convert.ToDecimal(item["Rating"]);
                            objProduct.IsRated = Convert.ToBoolean(item["IsRated"]);

                            lstProductDetail.Add(objProduct);
                        }
                        objModel.lstProductDetail = lstProductDetail;
                    }
                }
            }
            catch (Exception ex)
            {
                new clsCommon().CreateException("GetUserOrderDetail", ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return objModel;
        }

        public mdlUserOrderlist GetOrderDetailByOrderId(string OrderId = "", string UserId = "")
        {
            mdlUserOrderlist objModel = new mdlUserOrderlist();
            SqlConnection con = new SqlConnection(new clsSQL().ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("OrderSP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "GetOrderDetailByOrderId");
                if (!string.IsNullOrEmpty(OrderId))
                    cmd.Parameters.AddWithValue("@Id", OrderId);
                if (!string.IsNullOrEmpty(UserId))
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objModel.Id = Convert.ToString(dr["Id"]);
                    objModel.OrderNo = Convert.ToString(dr["OrderNo"]);
                    objModel.OrderDate = Convert.ToString(dr["OrderDate"]);
                    objModel.OrderStatus = Convert.ToString(dr["OrderStatus"]);
                    objModel.OrderAmount = Convert.ToString(dr["OrderAmount"]);
                    objModel.ItemCount = Convert.ToString(dr["ItemCount"]);
                    objModel.SName = Convert.ToString(dr["SName"]);
                    objModel.AddressLine1 = Convert.ToString(dr["AddressLine1"]);
                    objModel.AddressLine2 = Convert.ToString(dr["AddressLine2"]);
                    objModel.City = Convert.ToString(dr["City"]);
                    objModel.Province = Convert.ToString(dr["Province"]);
                    objModel.Country = Convert.ToString(dr["Country"]);
                    objModel.PostalCode = Convert.ToString(dr["PostalCode"]);
                    objModel.MobileNo = Convert.ToString(dr["MobileNo"]);
                }
            }
            catch (Exception ex)
            {
                new clsCommon().CreateException("GetOrderDetailByOrderId", ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return objModel;
        }
    }
}