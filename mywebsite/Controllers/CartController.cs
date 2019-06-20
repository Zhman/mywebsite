using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using JewelryStore.UI.Models;
using JewelryStore.Models;
using JewelryStore.UI.Binders;

namespace JewelryStore.UI.Controllers
{
    public class CartController : Controller
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

        //[Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            Guid? userId = Request.IsAuthenticated ? (Guid?)(new Guid(ControllerContext.HttpContext.User.Identity.GetUserId())) : null;

            var jewelryModel = dbContext.CartItems.Where(c => ((Request.IsAuthenticated && c.UserId == userId) || (!Request.IsAuthenticated && c.SessionId == Session.SessionID)) && c.OrderId == null)
                .Join(dbContext.Jewelries, ci => ci.JewelryId, jew => jew.JewelryId, (ci, jew) => new CartItemListModel { Jewelry = jew, CartItem = ci, CategoryName = null })
               .Join(dbContext.Categories, jew => jew.Jewelry.CategoryId, cat => cat.CategoryId, (jew, cat) => new CartItemListModel { Jewelry = jew.Jewelry, CartItem = jew.CartItem, CategoryName = cat.Name }).ToList();


            return View(jewelryModel);
        }


        [HttpPost]
        public ActionResult UpdateAll([ModelBinder(typeof(CartItemListModelBinder))] List<CartItem> items)
        {
            //try
            //{
                if (ModelState.IsValid)
                {
                    foreach (CartItem c in items)
                    {
                        CartItem cartitem = dbContext.CartItems.FirstOrDefault(ci => ci.CartItemId == c.CartItemId);
                        Jewelry jewelry = dbContext.Jewelries.FirstOrDefault(je => je.JewelryId == cartitem.JewelryId);

                        cartitem.Quantity = c.Quantity;
                        cartitem.Amount = cartitem.Quantity * jewelry.Price;
                    }

                 }

                dbContext.SaveChanges();

                return RedirectToAction("Index");            
        }

        public ActionResult Delete(int cartItemId)
        {
            try
            {
                CartItem cartitem = dbContext.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);
                dbContext.CartItems.Remove(cartitem);

                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        } 



    }
}
