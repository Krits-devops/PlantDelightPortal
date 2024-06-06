using PlantDelightPortal.Areas.Admin.Models;
using PlantDelightPortal.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Areas.Admin.Classes
{
    public class clsrptUser
    {
        public List<mdlrptUserDetail> GetReportData(mdlrptUser objModel)
        {
            List<mdlrptUserDetail> lstrptUserDetail = new List<mdlrptUserDetail>();
            SqlConnection con = new SqlConnection(new clsSQL().ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("rptUserSP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "Select");
                if (!string.IsNullOrEmpty(objModel.FromDate))
                    cmd.Parameters.AddWithValue("@FromDate", clsCommon.Get_SQLDate_From_DateString(objModel.FromDate));
                if (!string.IsNullOrEmpty(objModel.ToDate))
                    cmd.Parameters.AddWithValue("@ToDate", clsCommon.Get_SQLDate_From_DateString(objModel.ToDate));

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lstrptUserDetail.Add(new mdlrptUserDetail()
                    {
                        SrNo = Convert.ToString(dr["SrNo"]),
                        ProfileImage = Convert.ToString(dr["ProfileImage"]),
                        RegDate = Convert.ToString(dr["RegDate"]),
                        Name = Convert.ToString(dr["Name"]),
                        MobileNo = Convert.ToString(dr["MobileNo"]),
                        Email = Convert.ToString(dr["Email"]),
                        GoogleLogin = Convert.ToString(dr["GoogleLogin"])
                    });
                }
            }
            catch (Exception ex)
            {
                new clsCommon().CreateException("GetReportDataUser", ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return lstrptUserDetail;
        }
    }
}