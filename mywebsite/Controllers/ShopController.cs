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

namespace JewelryStore.UI.Controllers
{
    public class ShopController : Controller
    {
        ApplicationDbContext dbContext = ApplicationDbContext.Create();

        public string ShoppingCartId { get; set; }

        public const string CartSessionKey = null;        

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

        // GET: Shop
        public ActionResult Index(/*string message*/ )
        {
            var jewelryModel = dbContext.Jewelries.Where(j => j.IsAvailable )
                .Join(dbContext.Categories, jew => jew.CategoryId, cat => cat.CategoryId, (jew, cat) => new JewelryListModel { Jewelry = jew, CategoryName = cat.Name }).ToList();

            //ViewBag.Message = message;

            return View(jewelryModel);
            //return View()
        }


        //public void Buy(int jewelryId)
        //{
        //    CartItem shoppingCartId = new CartItem();
        //    shoppingCartId.ShoppingCartId = GetCartId();


        //    var cartItem = dbContext.CartItems.SingleOrDefault(c => c.CartId == shoppingCartId.ShoppingCartId && c.JewelryId == jewelryId);
        //    if (cartItem == null)
        //    {
        //        cartItem = new CartItem
        //        {
        //            //ItemId = Guid.NewGuid().ToString(),
        //            JewelryId = jewelryId,
        //            CartId = shoppingCartId.ShoppingCartId,
        //            Quantity = 1,
        //            Date = DateTime.Now,
        //            SessionId = Session.SessionID                   
        //    };
        //        dbContext.CartItems.Add(cartItem);
        //    }
        //    else
        //    {
        //        cartItem.Quantity++;
        //    }


        //    dbContext.SaveChanges();

        //}

        //public string GetCartId()
        //{
        //    if (System.Web.HttpContext.Current.Session[CartSessionKey] == null)
        //    {
        //        if (!string.IsNullOrWhiteSpace(System.Web.HttpContext.Current.User.Identity.Name))
        //        {
        //            System.Web.HttpContext.Current.Session[CartSessionKey] = System.Web.HttpContext.Current.User.Identity.Name;
        //        }
        //        else
        //        {
        //            // Generate a new random GUID using System.Guid class.     
        //            Guid tempCartId = Guid.NewGuid();
        //            System.Web.HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
        //        }
        //    }
        //    return System.Web.HttpContext.Current.Session[CartSessionKey].ToString();
        //}



        public ActionResult Buy(int jewelryId)
        {
            ShoppingCartId = GetCartId();


            var cartItem = dbContext.CartItems.SingleOrDefault(c => c.CartId == ShoppingCartId && c.JewelryId == jewelryId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    //ItemId = Guid.NewGuid().ToString(),                    
                    CartId = ShoppingCartId,
                    JewelryId = jewelryId,
                    Quantity = 1,
                    Date = DateTime.Now,
                    SessionId = Session.SessionID                   

                };
                dbContext.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }

            if (Request.IsAuthenticated)
            {
                cartItem.UserId = new Guid(ControllerContext.HttpContext.User.Identity.GetUserId());
            }            

            dbContext.SaveChanges();

            return RedirectToAction("Index");

        }

        public string GetCartId()
        {
            if (System.Web.HttpContext.Current.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(System.Web.HttpContext.Current.User.Identity.Name))
                {
                    System.Web.HttpContext.Current.Session[CartSessionKey] = System.Web.HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class.     
                    Guid tempCartId = Guid.NewGuid();
                    System.Web.HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return System.Web.HttpContext.Current.Session[CartSessionKey].ToString();
        }

        //public ActionResult Buy(int jewelryId)
        //{
        //    Jewelry jewelry = dbContext.Jewelries.FirstOrDefault(j => j.JewelryId == jewelryId);

        //    CartItem cartitem = new CartItem();
        //    cartitem.JewelryId = jewelryId;
        //    cartitem.SessionId = Session.SessionID;
        //    cartitem.Quantity = 1;
        //    cartitem.Date = DateTime.Now;
        //    cartitem.Amount = jewelry.Price * cartitem.Quantity;



        //    if (Request.IsAuthenticated)
        //    {
        //        cartitem.UserId = new Guid(ControllerContext.HttpContext.User.Identity.GetUserId());
        //    }
        //    else
        //    {   
        //        //cartitem.SessionId = Guid.NewGuid().ToString();

        //    }

        //    dbContext.CartItems.Add(cartitem);
        //    dbContext.SaveChanges();

        //    return RedirectToAction("Index");
        //} 
    }
}