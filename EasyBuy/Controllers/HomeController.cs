using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Classes;
using PlantDelightPortal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PlantDelightPortal.Controllers
{
    public class HomeController : Controller
    {
        #region Layout
        public ActionResult Index()
        {
            //StringBuilder
            mdlLayout objmdlLayout = new mdlLayout();
            objmdlLayout.lstGender = new clsGender().ListAll();
            objmdlLayout.lstSecurityQue = new clsSecurityQuestions().ListAll();

            if (!string.IsNullOrEmpty(Convert.ToString(Session["strMessage"])) && !string.IsNullOrEmpty(Convert.ToString(Session["strMessageType"])))
            {
                objmdlLayout.strMessage = Convert.ToString(Session["strMessage"]);
                objmdlLayout.strMessageType = Convert.ToString(Session["strMessageType"]);
            }

            return View(objmdlLayout);
        }

        [HttpPost]
        public ActionResult SignUp(mdlUserMaster objModel)
        {
            int ResultCount = 0;

            try
            {
                if (string.IsNullOrEmpty(objModel.Id))
                    objModel.Id = Convert.ToString(Guid.NewGuid());

                ResultCount = new clsUserMaster().CreateUser(objModel);

                return Json(ResultCount);
            }
            catch (Exception ex)
            {
                return Json(ResultCount);
            }
        }

        [HttpPost]
        public ActionResult Login(mdlUserMaster objmdlUser)
        {
            try
            {
                //objmdlUser.Password = Classes.Crypto.Encrypt(objmdlUser.Password.ToString(), clsCommon.constEncKey);
                mdlUserMaster objmdlUserMaster = new clsUserMaster().CheckLogin(objmdlUser);

                if (string.IsNullOrEmpty(objmdlUserMaster.Id))
                {
                    objmdlUser.strMessage = "Invalid userid or password.";
                    return Json(new { success = false, Msg = objmdlUser.strMessage });
                }
                else
                {
                    Session["UserId"] = objmdlUserMaster.Id;
                    Session["UserFirstName"] = objmdlUserMaster.FirstName;
                    Session["IsAdmin"] = objmdlUserMaster.IsAdmin;
                    Session["ProfileImage"] = objmdlUserMaster.ProfileImage;
                }
                return Json(new { success = true, IsAdmin = objmdlUserMaster.IsAdmin });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Msg = ex.Message });
            }
        }
        #endregion

        #region Login With Google
        public void LoginWithGmail()
        {
            string ClientId = ConfigurationManager.AppSettings["Google.ClientID"];
            string SecretKey = ConfigurationManager.AppSettings["Google.SecretKey"];
            string RedirectUrl = ConfigurationManager.AppSettings["Google.RedirectUrl"];

            Response.Redirect($"https://accounts.google.com/o/oauth2/v2/auth?client_id={ClientId}&response_type=code&scope=openid%20email%20profile&redirect_uri={RedirectUrl}&state=abcdef");
        }
        public ActionResult CreateGoogleUser(string code = "", string state = "", string session_state = "")
        {
            string ClientId = ConfigurationManager.AppSettings["Google.ClientID"];
            string SecretKey = ConfigurationManager.AppSettings["Google.SecretKey"];
            string RedirectUrl = ConfigurationManager.AppSettings["Google.RedirectUrl"];
            string url = "https://accounts.google.com/o/oauth2/token";

            string poststring = "grant_type=authorization_code&code=" + code + "&client_id=" + ClientId + "&client_secret=" + SecretKey + "&redirect_uri=" + RedirectUrl + "";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            UTF8Encoding utfenc = new UTF8Encoding();
            byte[] bytes = utfenc.GetBytes(poststring);
            Stream outputstream = null;
            try
            {
                request.ContentLength = bytes.Length;
                outputstream = request.GetRequestStream();
                outputstream.Write(bytes, 0, bytes.Length);
            }
            catch { }
            var response = (HttpWebResponse)request.GetResponse();
            var streamReader = new StreamReader(response.GetResponseStream());
            string responseFromServer = streamReader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            GoogleToken obj = js.Deserialize<GoogleToken>(responseFromServer);
            GoogleUserDetail objGoogleUserDetail = GetuserProfile(obj.access_token);

            clsUserMaster objclsUserMaster = new clsUserMaster();
            string UserId = objclsUserMaster.getUserIdByEmail(objGoogleUserDetail.email);

            mdlUserMaster objmdlUserMaster = new mdlUserMaster();

            if (string.IsNullOrEmpty(UserId)) // User not Exists
            {
                objmdlUserMaster.Id = Convert.ToString(Guid.NewGuid());
                objmdlUserMaster.Email = objGoogleUserDetail.email;
                objmdlUserMaster.FirstName = objGoogleUserDetail.given_name;
                objmdlUserMaster.LastName = objGoogleUserDetail.family_name;
                objclsUserMaster.CreateGoogleUser(objmdlUserMaster);
            }
            else
            {
                objmdlUserMaster.Id = UserId;
                objmdlUserMaster = objclsUserMaster.GetUserData(objmdlUserMaster);
            }

            Session["UserId"] = objmdlUserMaster.Id;
            Session["UserFirstName"] = objmdlUserMaster.FirstName;
            Session["IsAdmin"] = objmdlUserMaster.IsAdmin;
            Session["ProfileImage"] = objmdlUserMaster.ProfileImage;

            return RedirectToAction("UserHome", "Home");
        }
        public GoogleUserDetail GetuserProfile(string accesstoken = "")
        {
            string url = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token=" + accesstoken + "";
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            JavaScriptSerializer js = new JavaScriptSerializer();
            GoogleUserDetail userinfo = js.Deserialize<GoogleUserDetail>(responseFromServer);
            return userinfo;
        }
        #endregion

        #region User
        public ActionResult UserHome()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
                return RedirectToAction("Index", "Home");

            return View();
        }

        public ActionResult UserLogout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        #endregion

       

     
    }
}