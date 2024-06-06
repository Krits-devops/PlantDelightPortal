using PlantDelightPortal.Areas.Admin.Models;
using PlantDelightPortal.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Areas.Admin.Classes
{
    public class clsOrderStatus
    {
        public List<mdlOrderStatus> ListAll()
        {
            List<mdlOrderStatus> lstOrderStatus = new List<mdlOrderStatus>();
            SqlConnection con = new SqlConnection(new clsSQL().ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("OrderStatusSP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "All");

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstOrderStatus.Add(new mdlOrderStatus()
                    {
                        Id = Convert.ToString(dr["Id"]),
                        Status = Convert.ToString(dr["Status"])
                    });
                }
            }
            catch (Exception ex)
            {
                new clsCommon().CreateException("ListAllOrderStatus", ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return lstOrderStatus;
        }

        public bool Validation(mdlOrderStatus objModel, string strAction)
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

        public int Add(mdlOrderStatus objModel)
        {
            int ResultCount = 0;
            if (Validation(objModel, "Add"))
            {
                using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
                {
                    con.Open();
                    SqlCommand com = new SqlCommand("OrderStatusSP", con);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Action", "Create");
                    com.Parameters.AddWithValue("@Id", objModel.Id);
                    com.Parameters.AddWithValue("@Status", objModel.Status);
                    ResultCount = com.ExecuteNonQuery();
                }
            }

            return ResultCount;
        }

        public mdlOrderStatus GetbyId(string Id)
        {
            mdlOrderStatus objModel = new mdlOrderStatus();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OrderStatusSP";
                cmd.Parameters.AddWithValue("@Action", "Select");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objModel.Id = Convert.ToString(dr["Id"]);
                    objModel.Status = Convert.ToString(dr["Status"]);
                }
                con.Close();
            }
            return objModel;
        }

        public int Update(mdlOrderStatus objModel)
        {
            int i;
            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("OrderStatusSP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Update");
                com.Parameters.AddWithValue("@Id", objModel.Id);
                com.Parameters.AddWithValue("@Status", objModel.Status);
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
                SqlCommand com = new SqlCommand("OrderStatusSP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "Delete");
                com.Parameters.AddWithValue("@Id", Id);
                i = com.ExecuteNonQuery();
            }
            return i;
        }

        public int UpdateOrderStatus(string OrderId, string OrderStatusId)
        {
            int i;
            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                con.Open();
                SqlCommand com = new SqlCommand("OrderStatusSP", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Action", "UpdateOrderStatus");
                com.Parameters.AddWithValue("@OrderId", OrderId);
                com.Parameters.AddWithValue("@OrderStatusId", OrderStatusId);
                i = com.ExecuteNonQuery();
            }
            return i;
        }
    }
}