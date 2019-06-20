using JewelryStore.Models;
using JewelryStore.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JewelryStore.UI.Emails;
using System.Net.Mail;

namespace JewelryStore.UI.Controllers
{
    public class OrderController : Controller
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
        

        // GET: Order
        //[Authorize]
        [HttpGet]
        public ActionResult Index(int? orderId)
        {
            var userId = Request.IsAuthenticated ? (Guid?)(new Guid(ControllerContext.HttpContext.User.Identity.GetUserId())) : null;

            var order = dbContext.Orders.Where(o => o.OrderId == orderId 
                    && ((userId == null && o.SessionId == Session.SessionID) 
                            || (userId != null && o.UserId == userId))).First();
            
            return View(order);            
        }

        [HttpGet]
        public ActionResult MyIndex(int? orderId)
        {
            var userId = Request.IsAuthenticated ? (Guid?)(new Guid(ControllerContext.HttpContext.User.Identity.GetUserId())) : null;

            var order = dbContext.Orders.Where(o => o.OrderId == orderId
                    && ((userId == null && o.SessionId == Session.SessionID)
                            || (userId != null && o.UserId == userId))).First();

            return View(order);
        }

        // GET: Order
        [Authorize]
        [HttpGet]
        public ActionResult MyOrders()
        {
            var userId = new Guid(ControllerContext.HttpContext.User.Identity.GetUserId());

            var orders = dbContext.Orders.Where(o => o.UserId == userId).ToList();

            return View(orders);

        }
        //#region AdminRules
        //// GET: AdminOrder, Admin can see list of all orders
        //[Authorize(Roles ="Admin")]
        //[HttpGet]
        //public ActionResult AdminOrder()
        //{         
        //    var orders = dbContext.Orders.Select(o => o.OrderId);
        //    var orderListModel = dbContext.Orders.Where(o => orders.Contains(o.OrderId));
        //    var orderList = dbContext.Orders.OrderByDescending(o => o.OrderId).ToList();                   
            
        //    return View(orderListModel);
            
        //}

        //// GET: AdminIndex, Admin can see customer orders and chage status
        //[Authorize(Roles ="Admin")]        
        //public ActionResult AdminIndex(int? orderId)
        //{
        //    var userId = Request.IsAuthenticated ? (Guid?)(new Guid(ControllerContext.HttpContext.User.Identity.GetUserId())) : null;
        //    var order = dbContext.Orders.Where(o => o.OrderId == orderId).First();                     

        //    return View(order);


        //}
        //[Authorize(Roles = "Admin")]
        //[HttpPost]
        //public ActionResult AdminIndex(int? orderId, OrderStatus status)
        //{            
        //    var order = dbContext.Orders.Where(o => o.OrderId == orderId).First();
        //    order.Status = status;
        //    dbContext.SaveChanges();

        //    return View(order);


        //}
        //#endregion


        #region Checkout
        
        public ActionResult Checkout()
        {         
            Order order = new Order();
            order.SessionId = Session.SessionID;
            
            order.Date = DateTime.Now;

            if (Request.IsAuthenticated)
            {
                var userId = new Guid(ControllerContext.HttpContext.User.Identity.GetUserId());
                order.UserId = userId;

                var customer = dbContext.Customers.FirstOrDefault(c => c.UserId == userId);
                order.FirstName = customer.FirstName;
                order.LastName = customer.LastName;
                order.Delivery = customer.Delivery;
                order.Discount = customer.Discount;
                order.Phone = customer.Phone;
            }

            dbContext.Orders.Add(order);
            //dbContext.SaveChanges();

            var cartItems = dbContext.CartItems.
                Where(ci => ((!Request.IsAuthenticated && ci.SessionId == Session.SessionID) || (Request.IsAuthenticated && ci.UserId == order.UserId))
                                && ci.OrderId == null).ToList();

            foreach (CartItem ci in cartItems)
            {
                ci.OrderId = order.OrderId;
            }

            order.Price = cartItems.Sum(ci => ci.Amount);

            dbContext.SaveChanges();

            //return View();
            return RedirectToAction("Index", new { orderId = order.OrderId });

        }

        
        [HttpPost]
        public ActionResult ConfirmCheckout(FormCollection collection, int? orderId)
        {

            var userId = Request.IsAuthenticated ? (Guid?)(new Guid(ControllerContext.HttpContext.User.Identity.GetUserId())) : null;
            var order = dbContext.Orders.Where(o => o.OrderId == orderId
                    && ((userId == null && o.SessionId == Session.SessionID)
                            || (userId != null && o.UserId == userId))).First();

            if (ModelState.IsValid)
            {
                order.FirstName = collection["FirstName"];
                order.LastName = collection["LastName"];                
                order.Delivery = collection["Delivery"];
                order.Comment = collection["Comment"];
                order.Phone = collection["Phone"];
                order.Email = collection["Email"];
                order.Status = OrderStatus.Placed;

                dbContext.SaveChanges();
            }           
            

            Emails.Emails emailSending = new Emails.Emails();
            emailSending.EmailSending(order);                       

            return RedirectToAction("Index", new { orderId = order.OrderId });
        }
        #endregion

        //public void EmptyCart(int? orderId)
        //{
        //    var cartItems = dbContext.Orders.Where(
        //        cart => cart.CartItems == );

        //    foreach (var cartItem in cartItems)
        //    {
        //        dbContext.Carts.Remove(cartItem);
        //    }
        //    // Save changes
        //    dbContext.SaveChanges();
        //}
    }
}
