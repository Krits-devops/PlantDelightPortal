using PlantDelightPortal.Areas.Admin.Classes;
using PlantDelightPortal.Areas.Admin.Models;
using PlantDelightPortal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Added by aish
namespace PlantDelightPortal.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FillProduct()
        {
            List<mdlProduct> lstModel = new List<mdlProduct>();
            lstModel = new clsProduct().ListAll();

            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateProduct()
        {
            mdlProduct objModel = new mdlProduct();
            objModel.Id = Convert.ToString(Guid.NewGuid());
            objModel.lstCategory = new clsCategory().ListAll();
            objModel.lstSubCategory = new clsSubCategory().ListAll();
            return View(objModel);
        }
        [HttpPost]
        public ActionResult CreateProduct(mdlProduct objModel)
        {
            int ResultCount = 0;

            try
            {
                List<mdlProductAttribute> lstProductAttribute = new List<mdlProductAttribute>();
                lstProductAttribute = JsonConvert.DeserializeObject<List<mdlProductAttribute>>(objModel.JsonProductAttributeList);

                for (int i = 0; i < lstProductAttribute.Count; i++)
                {
                    if (string.IsNullOrEmpty(lstProductAttribute[i].Id))
                        lstProductAttribute[i].Id = Convert.ToString(Guid.NewGuid());
                    new clsProductAttribute().CreateProductAttribute(lstProductAttribute[i]);
                }

                ResultCount = new clsProduct().CreateProduct(objModel);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(ResultCount);
            }
        }

        public ActionResult EditProduct(string Id)
        {
            mdlProduct objModel = new mdlProduct();

            try
            {
                if (!string.IsNullOrEmpty(Id))
                {
                    objModel.Id = Id;

                    objModel = new clsProduct().GetProductDetailByProductIdForEdit(Id);
                    objModel.lstProductImage = new clsProductImage().GetDataByProductId(objModel.Id);
                    objModel.lstProductAttribute = new clsProductAttribute().GetDataByProductId(objModel.Id);
                    //objModel.lstProductRatingReview = new clsProductRating().lstProductRatingReviewByProductId(objModel.Id);
                    objModel.lstCategory = new clsCategory().ListAll();
                    objModel.lstSubCategory = new clsSubCategory().ListAll();
                }
            }
            catch (Exception ex)
            {

            }
            return View(objModel);

        }
        [HttpPost]
        public ActionResult EditProduct(mdlProduct objModel)
        {
            int ResultCount = 0;

            try
            {
                List<mdlProductAttribute> lstProductAttribute = new List<mdlProductAttribute>();
                lstProductAttribute = JsonConvert.DeserializeObject<List<mdlProductAttribute>>(objModel.JsonProductAttributeList);

                for (int i = 0; i < lstProductAttribute.Count; i++)
                {
                    if (string.IsNullOrEmpty(lstProductAttribute[i].Id))
                        lstProductAttribute[i].Id = Convert.ToString(Guid.NewGuid());
                    new clsProductAttribute().CreateProductAttribute(lstProductAttribute[i]);
                }

                ResultCount = new clsProduct().CreateProduct(objModel);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(ResultCount);
            }
        }

        public JsonResult Delete(string Id)
        {
            return Json(new clsProduct().Delete(Id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
            string pathforDB = "\\Areas\\Admin\\Content\\Images\\Product\\";
            string path = Server.MapPath(pathforDB);

            string ProductImageId = Request.Form["ProductImageId"];
            string ImagePath = null;

            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                ImagePath = path + ProductImageId + Path.GetExtension(file.FileName);
                pathforDB = pathforDB + ProductImageId + Path.GetExtension(file.FileName);
                file.SaveAs(ImagePath);
            }

            mdlProductImage objModel = new mdlProductImage();
            objModel.Id = ProductImageId;
            objModel.ProductId = Request.Form["ProductId"];
            objModel.ImagePath = pathforDB;

            int ResultCount = 0;
            ResultCount = new clsProductImage().UploadProductImage(objModel);

            return Json(string.Empty);
        }

        [HttpPost]
        public JsonResult DeleleImage(string Id)
        {
            clsProductImage objCls = new clsProductImage();
            objCls.DeleteProductImageFile(Id);
            objCls.DeleteProductImage(Id);
            return Json(string.Empty);
        }

        [HttpPost]
        public JsonResult NewGuid()
        {
            return Json(Guid.NewGuid());
        }

        public JsonResult GetProductAttributeBySubCategoryId(string Id)
        {
            List<mdlAttribute> lstmdlAttribute = new List<mdlAttribute>();
            lstmdlAttribute = new clsAttribute().GetAttributeBySubCategoryId(Id);

            return Json(new { lstAttribute = lstmdlAttribute }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult FillAllProduct()
        {
            List<mdlProduct> lstModel = new List<mdlProduct>();
            lstModel = new clsProduct().ListAll(Convert.ToString(Session["UserId"]));

            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductBySubCategoryId(string SubCategoryId = "", string SortBy = "", string MinPrice = "", string MaxPrice = "", string FilterRating = "", string DynamicFilter = "")
        {
            string whereCondition = "";

            if (!string.IsNullOrEmpty(DynamicFilter))
            {
                string[] strCon = DynamicFilter.Split('^');

                for (int i = 0; i < strCon.Length; i++)
                {
                    string[] str = strCon[i].Split('~');
                    if (str.Length > 1)
                        whereCondition += " OR (PA.AttributeId = '" + str[0] + "' AND PA.AttributeValue = '" + str[1] + "')";
                }
                whereCondition = whereCondition.Substring(4, whereCondition.Length - 4);
            }

            List<mdlProduct> lstModel = new List<mdlProduct>();
            lstModel = new clsProduct().GetProductBySubCategoryId(SubCategoryId, SortBy, MinPrice, MaxPrice, FilterRating, whereCondition, Convert.ToString(Session["UserId"]));

            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }
    }
}