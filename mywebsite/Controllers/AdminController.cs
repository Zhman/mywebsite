using JewelryStore.Models;
using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JewelryStore.UI.Controllers
{
    public class AdminController : Controller
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

        #region AdminRules
        // GET: AdminOrder, Admin can see list of all orders
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AdminOrder()
        {
            var orders = dbContext.Orders.Select(o => o.OrderId);
            var orderListModel = dbContext.Orders.Where(o => orders.Contains(o.OrderId));
            var orderList = dbContext.Orders.OrderByDescending(o => o.OrderId).ToList();

            return View(orderListModel);

        }

        // GET: AdminIndex, Admin can see customer orders and chage status
        [Authorize(Roles = "Admin")]
        public ActionResult AdminIndex(int? orderId)
        {
            var userId = Request.IsAuthenticated ? (Guid?)(new Guid(ControllerContext.HttpContext.User.Identity.GetUserId())) : null;
            var order = dbContext.Orders.Where(o => o.OrderId == orderId).First();

            return View(order);


        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AdminIndex(int? orderId, OrderStatus status)
        {
            var order = dbContext.Orders.Where(o => o.OrderId == orderId).First();
            order.Status = status;
            dbContext.SaveChanges();

            return View(order);


        }
        #endregion
    }
}