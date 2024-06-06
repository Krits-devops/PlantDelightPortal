using PlantDelightPortal.Areas.Admin.Models;
using PlantDelightPortal.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Areas.Admin.Classes
{
    public class clsrptOrder
    {
        public List<mdlrptOrderDetail> GetReportData(mdlrptOrder objModel)
        {
            List<mdlrptOrderDetail> lstrptOrderDetail = new List<mdlrptOrderDetail>();
            SqlConnection con = new SqlConnection(new clsSQL().ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("rptOrderSP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "Select");
                if (!string.IsNullOrEmpty(objModel.FromDate))
                    cmd.Parameters.AddWithValue("@FromDate", clsCommon.Get_SQLDate_From_DateString(objModel.FromDate));
                if (!string.IsNullOrEmpty(objModel.ToDate))
                    cmd.Parameters.AddWithValue("@ToDate", clsCommon.Get_SQLDate_From_DateString(objModel.ToDate));
                if (!string.IsNullOrEmpty(objModel.OrderNo))
                    cmd.Parameters.AddWithValue("@OrderNo", objModel.OrderNo);
                if (!string.IsNullOrEmpty(objModel.StatusId))
                    cmd.Parameters.AddWithValue("@StatusId", objModel.StatusId);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstrptOrderDetail.Add(new mdlrptOrderDetail()
                    {
                        Id = Convert.ToString(dr["Id"]),
                        SrNo = Convert.ToString(dr["SrNo"]),
                        OrderDate = Convert.ToString(dr["OrderDate"]),
                        OrderNo = Convert.ToString(dr["OrderNo"]),
                        SName = Convert.ToString(dr["SName"]),
                        OrderAmount = Convert.ToString(dr["OrderAmount"]),
                        OrderStatusId = Convert.ToString(dr["OrderStatusId"]),
                        OrderStatus = Convert.ToString(dr["OrderStatus"])
                    });
                }
            }
            catch (Exception ex)
            {
                new clsCommon().CreateException("GetReportDataOrder", ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return lstrptOrderDetail;
        }
    }
}