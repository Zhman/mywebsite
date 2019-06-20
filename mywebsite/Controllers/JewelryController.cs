using JewelryStore.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using JewelryStore.UI.Models;

namespace JewelryStore.UI.Controllers 
{
    public class JewelryController : Controller
    {
        ApplicationDbContext dbContext = ApplicationDbContext.Create();

        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Content(int? jewelryId)
        {
            var jewelry = dbContext.Jewelries.SingleOrDefault(j => j.JewelryId == jewelryId);

            return View(jewelry);
        }

        // GET: Jewelry
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? categoryId)
        {            

            var jewelryModel = dbContext.Jewelries.Join(dbContext.Categories, jew => jew.CategoryId, cat => cat.CategoryId, (jew, cat) => new JewelryListModel { Jewelry =  jew, CategoryName = cat.Name }).ToList();

            if (!categoryId.HasValue)
                return View(jewelryModel);
            else
            {
                List<Jewelry> jewelries = dbContext.Jewelries.Where(j => j.CategoryId == categoryId).ToList();
                return View(jewelries);
            }
        }        

        // GET: Jewelry/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Jewelry/Create
        public ActionResult Create()
        {
            ViewData["Categories"] = dbContext.Categories.ToList();
            return View();
        }

        // POST: Jewelry/Create
        [HttpPost]
        public ActionResult Create(Jewelry jewelry)
        {           
            dbContext.Jewelries.Add(jewelry);

            dbContext.SaveChanges();

            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Jewelry/Edit/5
        public ActionResult Edit(int id)
        {
            Jewelry jewelry = dbContext.Jewelries.First(j => j.JewelryId == id);
            ViewData["Categories"] = dbContext.Categories.ToList();
            return View(jewelry);
        }

        // POST: Jewelry/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, HttpPostedFileBase image)
        {
            //NumberFormatInfo fi = new NumberFormatInfo();
            //fi.PercentDecimalSeparator = ".";

            Jewelry jewelryOld = dbContext.Jewelries.First(j => j.JewelryId == id);

            jewelryOld.Name = collection["Name"];
            jewelryOld.Price = Convert.ToDecimal(collection["Price"]/*, fi*/);
            jewelryOld.Description = collection["Description"];
            jewelryOld.CategoryId = Convert.ToInt32(collection["CategoryId"]);
            jewelryOld.IsAvailable = Convert.ToBoolean(collection["IsAvailable"].Split(',')[0]);
            jewelryOld.Novelty = Convert.ToBoolean(collection["Novelty"].Split(',')[0]);
            jewelryOld.Discounted= Convert.ToBoolean(collection["Discounted"].Split(',')[0]);
            jewelryOld.Offer = Convert.ToDecimal(collection["Offer"]/*, fi*/);
            jewelryOld.OfferPrice = jewelryOld.Price - jewelryOld.Offer;

            if (image != null)
            {
                jewelryOld.ImageMimeType = image.ContentType;
                jewelryOld.Image = new byte[image.ContentLength];
                image.InputStream.Read(jewelryOld.Image, 0, image.ContentLength);
            }


            dbContext.SaveChanges();

            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public FileContentResult GetImage(int jewelryId)
        {
            Jewelry jew = dbContext.Jewelries.FirstOrDefault(j => j.JewelryId == jewelryId);
            if (jew != null)
            {
                return File(jew.Image, jew.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        // GET: Jewelry/Delete/5
        public ActionResult Delete(int id)
        {
            Jewelry jewelry = dbContext.Jewelries.First(j => j.JewelryId == id);
            return View(jewelry);
        }

        // POST: Jewelry/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Jewelry jewelry = dbContext.Jewelries.First(j => j.JewelryId == id);
            dbContext.Jewelries.Remove(jewelry);

            dbContext.SaveChanges();

            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
