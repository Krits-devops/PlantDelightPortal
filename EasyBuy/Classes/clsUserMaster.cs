using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PlantDelightPortal.Classes
{
    public class clsUserMaster
    {
        #region Variable Declaration

        private SqlCommand cmd;
        private DataSet ds;

        #endregion

        #region CRUD Opetion

        public int CreateUser(mdlUserMaster objModel)
        {
            int ResultCount = 0;
            if (Validation(objModel, "Create"))
            {
                using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UserMasterSP", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "Create");
                    cmd.Parameters.AddWithValue("@Id", objModel.Id);
                    cmd.Parameters.AddWithValue("@FirstName", objModel.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", objModel.LastName);
                    cmd.Parameters.AddWithValue("@Email", objModel.Email);
                    cmd.Parameters.AddWithValue("@MobileNo", objModel.MobileNo);
                    cmd.Parameters.AddWithValue("@Password", objModel.Password);
                    cmd.Parameters.AddWithValue("@DateOfBirth", objModel.DateOfBirth);
                    cmd.Parameters.AddWithValue("@GenderId", objModel.GenderId);
                    cmd.Parameters.AddWithValue("@SecurityQueId", objModel.SecurityQueId);
                    cmd.Parameters.AddWithValue("@SecurityAns", objModel.SecurityAns);
                    ResultCount = cmd.ExecuteNonQuery();
                }
            }

            return ResultCount;
        }

        public int UpdateUser(mdlUserMaster objModel)
        {
            int ResultCount = 0;
            if (Validation(objModel, "Update"))
            {
                using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UserMasterSP", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "Update");
                    cmd.Parameters.AddWithValue("@Id", objModel.Id);
                    cmd.Parameters.AddWithValue("@FirstName", objModel.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", objModel.LastName);
                    cmd.Parameters.AddWithValue("@MobileNo", objModel.MobileNo);
                    cmd.Parameters.AddWithValue("@GenderId", objModel.GenderId);
                    cmd.Parameters.AddWithValue("@SecurityQueId", objModel.SecurityQueId);
                    cmd.Parameters.AddWithValue("@SecurityAns", objModel.SecurityAns);
                    ResultCount = cmd.ExecuteNonQuery();
                }
            }

            return ResultCount;
        }

        public bool Validation(mdlUserMaster objModel, string strAction)
        {
            if (strAction == "Create" || strAction == "Update")
            {
                if (string.IsNullOrEmpty(objModel.Id))
                {
                    objModel.strMessage = "Id is not available.";
                    return false;
                }
            }

            return true;
        }

        public void ChangePassword(mdlUserMaster objModel)
        {
            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UserMasterSP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ChangePassword");
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
                cmd.Parameters.AddWithValue("@Password", objModel.Password);
                cmd.ExecuteNonQuery();
            }
        }

        public void CreateGoogleUser(mdlUserMaster objModel)
        {
            SqlConnection con = new SqlConnection(new clsSQL().ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand("UserMasterSP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "CreateGoogleUser");
                if (!string.IsNullOrEmpty(objModel.Id))
                    cmd.Parameters.AddWithValue("@Id", objModel.Id);
                if (!string.IsNullOrEmpty(objModel.Id))
                    cmd.Parameters.AddWithValue("@FirstName", objModel.FirstName);
                if (!string.IsNullOrEmpty(objModel.Id))
                    cmd.Parameters.AddWithValue("@LastName", objModel.LastName);
                if (!string.IsNullOrEmpty(objModel.Id))
                    cmd.Parameters.AddWithValue("@Email", objModel.Email);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                new clsCommon().CreateException("CreateGoogleUser", ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public string getUserIdByEmail(string Email)
        {
            SqlConnection con = new SqlConnection(new clsSQL().ConnectionString);
            string UserId = "";

            try
            {
                SqlCommand cmd = new SqlCommand("UserMasterSP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "getUserIdByEmail");
                if (!string.IsNullOrEmpty(Email))
                    cmd.Parameters.AddWithValue("@Email", Email);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                UserId = Convert.ToString(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                new clsCommon().CreateException("getUserIdByEmail", ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return UserId;
        }

        public mdlUserMaster getUserDetailByEmail(string Email)
        {
            SqlConnection con = new SqlConnection(new clsSQL().ConnectionString);
            mdlUserMaster objmdlUserMaster = new mdlUserMaster();

            try
            {
                SqlCommand cmd = new SqlCommand("UserMasterSP", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "getUserDetailByEmail");
                if (!string.IsNullOrEmpty(Email))
                    cmd.Parameters.AddWithValue("@Email", Email);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objmdlUserMaster.Id = Convert.ToString(dr["Id"]);
                    objmdlUserMaster.FirstName = Convert.ToString(dr["FirstName"]);
                    objmdlUserMaster.LastName = Convert.ToString(dr["LastName"]);
                    objmdlUserMaster.Email = Convert.ToString(dr["Email"]);
                    objmdlUserMaster.MobileNo = Convert.ToString(dr["MobileNo"]);
                    objmdlUserMaster.Password = Convert.ToString(dr["Password"]);
                    if (!string.IsNullOrEmpty(Convert.ToString(dr["DateOfBirth"])))
                        objmdlUserMaster.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]).ToShortDateString();
                    objmdlUserMaster.GenderId = Convert.ToString(dr["GenderId"]);
                    objmdlUserMaster.SecurityQueId = Convert.ToString(dr["SecurityQueId"]);
                    objmdlUserMaster.SecurityQue = Convert.ToString(dr["SecurityQue"]);
                    objmdlUserMaster.SecurityAns = Convert.ToString(dr["SecurityAns"]);
                    objmdlUserMaster.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                    objmdlUserMaster.ProfileImage = Convert.ToString(dr["ProfileImage"]);
                    objmdlUserMaster.GoogleLogin = Convert.ToBoolean(dr["GoogleLogin"]);
                }
            }
            catch (Exception ex)
            {
                new clsCommon().CreateException("getUserIdByEmail", ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return objmdlUserMaster;
        }
        #endregion

        #region Public Methods

        public mdlUserMaster CheckLogin(mdlUserMaster objModel)
        {
            mdlUserMaster objmdlUserMaster = new mdlUserMaster();

            try
            {
                using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "UserMasterSP";
                    cmd.Parameters.AddWithValue("@Action", "CHECKLOGIN");
                    cmd.Parameters.AddWithValue("@Email", objModel.Email);
                    cmd.Parameters.AddWithValue("@Password", objModel.Password);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        objmdlUserMaster.Id = Convert.ToString(dr["Id"]);
                        objmdlUserMaster.FirstName = Convert.ToString(dr["FirstName"]);
                        objmdlUserMaster.IsAdmin = Convert.ToBoolean(dr["IsAdmin"]);
                        objmdlUserMaster.ProfileImage = Convert.ToString(dr["ProfileImage"]);
                    }
                    con.Close();
                }
                return objmdlUserMaster;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public mdlUserMaster GetUserData(mdlUserMaster objModel)
        {
            mdlUserMaster objmdlUserMaster = new mdlUserMaster();

            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UserMasterSP";
                cmd.Parameters.AddWithValue("@Action", "Select");
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    objmdlUserMaster.Id = Convert.ToString(dr["Id"]);
                    objmdlUserMaster.FirstName = Convert.ToString(dr["FirstName"]);
                    objmdlUserMaster.LastName = Convert.ToString(dr["LastName"]);
                    objmdlUserMaster.Email = Convert.ToString(dr["Email"]);
                    objmdlUserMaster.MobileNo = Convert.ToString(dr["MobileNo"]);
                    objmdlUserMaster.Password = Convert.ToString(dr["Password"]);
                    if (!string.IsNullOrEmpty(Convert.ToString(dr["DateOfBirth"])))
                        objmdlUserMaster.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]).ToShortDateString();
                    objmdlUserMaster.GenderId = Convert.ToString(dr["GenderId"]);
                    objmdlUserMaster.SecurityQueId = Convert.ToString(dr["SecurityQueId"]);
                    objmdlUserMaster.SecurityAns = Convert.ToString(dr["SecurityAns"]);
                    objmdlUserMaster.CreatedDate = Convert.ToString(dr["CreatedDate"]);
                    objmdlUserMaster.ProfileImage = Convert.ToString(dr["ProfileImage"]);
                    objmdlUserMaster.GoogleLogin = Convert.ToBoolean(dr["GoogleLogin"]);
                }
                con.Close();
            }
            return objmdlUserMaster;
        }

        public List<mdlUserMaster> ConvertDSToUserList(DataSet ds)
        {
            if (ds == null || ds.Tables == null || ds.Tables.Count == 0)
                return new List<mdlUserMaster>();

            List<mdlUserMaster> lstUser = new List<mdlUserMaster>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                mdlUserMaster objModel = new mdlUserMaster();

                if (dr.Table.Columns.Contains("Id"))
                    objModel.Id = Convert.ToString(dr["Id"]);
                if (dr.Table.Columns.Contains("UserId"))
                    objModel.Email = Convert.ToString(dr["UserId"]);

                lstUser.Add(objModel);
            }
            return lstUser;
        }

        public void ChangeUserProfileImage(mdlUserMaster objModel)
        {
            using (SqlConnection con = new SqlConnection(new clsSQL().ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UserMasterSP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ChangeUserProfileImage");
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
                cmd.Parameters.AddWithValue("@ProfileImage", objModel.ProfileImage);
                cmd.ExecuteNonQuery();
            }
        }

        #endregion
    }
}