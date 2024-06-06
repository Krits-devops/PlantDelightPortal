using PlantDelightPortal.Areas.Admin.Models;
using PlantDelightPortal.Classes;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Areas.Admin.Classes
{
    public class clsDashboardAdmin
    {
        public mdlDashboardAdmin GetDashboardData()
        {
            mdlDashboardAdmin objModel = new mdlDashboardAdmin();
            SqlConnection con = new SqlConnection(new clsSQL().ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("DashboardAdminSP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "GetDashboardData");

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objModel.OrderToday = Convert.ToDecimal(dr["OrderToday"]);
                    objModel.OrderThisMonth = Convert.ToDecimal(dr["OrderThisMonth"]);
                    objModel.OrderThisYear = Convert.ToDecimal(dr["OrderThisYear"]);

                    objModel.SalesToday = Convert.ToDecimal(dr["SalesToday"]);
                    objModel.SalesThisMonth = Convert.ToDecimal(dr["SalesThisMonth"]);
                    objModel.SalesThisYear = Convert.ToDecimal(dr["SalesThisYear"]);

                    objModel.UserToday = Convert.ToDecimal(dr["UserToday"]);
                    objModel.UserThisMonth = Convert.ToDecimal(dr["UserThisMonth"]);
                    objModel.UserThisYear = Convert.ToDecimal(dr["UserThisYear"]);
                }
            }
            catch (Exception ex)
            {
                new clsCommon().CreateException("GetDashboardData", ex.Message);
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