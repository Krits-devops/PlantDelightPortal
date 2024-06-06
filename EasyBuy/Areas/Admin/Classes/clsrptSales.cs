using PlantDelightPortal.Areas.Admin.Models;
using PlantDelightPortal.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Areas.Admin.Classes
{
    public class clsrptSales
    {
        public List<mdlrptSalesDetail> GetReportData(mdlrptSales objModel)
        {
            List<mdlrptSalesDetail> lstrptSalesDetail = new List<mdlrptSalesDetail>();
            SqlConnection con = new SqlConnection(new clsSQL().ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("rptSalesSP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "Select");
                if (!string.IsNullOrEmpty(objModel.FromDate))
                    cmd.Parameters.AddWithValue("@FromDate", clsCommon.Get_SQLDate_From_DateString(objModel.FromDate));
                if (!string.IsNullOrEmpty(objModel.ToDate))
                    cmd.Parameters.AddWithValue("@ToDate", clsCommon.Get_SQLDate_From_DateString(objModel.ToDate));
                if (!string.IsNullOrEmpty(objModel.StatusId))
                    cmd.Parameters.AddWithValue("@StatusId", objModel.StatusId);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstrptSalesDetail.Add(new mdlrptSalesDetail()
                    {
                        Id = Convert.ToString(dr["Id"]),
                        SrNo = Convert.ToString(dr["SrNo"]),
                        OrderDate = Convert.ToString(dr["OrderDate"]),
                        OrderNo = Convert.ToString(dr["OrderNo"]),
                        ItemName = Convert.ToString(dr["ItemName"]),
                        Amount = Convert.ToString(dr["Amount"]),
                        OrderStatus = Convert.ToString(dr["OrderStatus"])
                    });
                }
            }
            catch (Exception ex)
            {
                new clsCommon().CreateException("GetReportDataSales", ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return lstrptSalesDetail;
        }
    }
}