using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JewelryStore.Models;
using JewelryStore.UI;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace JewelryStore.UI.Controllers
{
    public class CustomerController : Controller
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
               

        // GET: Customers
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            var userId = new Guid(ControllerContext.HttpContext.User.Identity.GetUserId());
            var customer = dbContext.Customers.FirstOrDefault(c => c.UserId == userId);
            if (customer == null)
            {
                customer = new Customer();
                customer.UserId = userId;

                int maxId = dbContext.Customers.Count() > 0 ? dbContext.Customers.Max(c => c.CustomerId.Value) : 0;
                customer.CustomerId = maxId + 1;

                dbContext.Customers.Add(customer);

            }

            customer.Email = ControllerContext.HttpContext.User.Identity.Name;

            return View(customer);
        }


        public ActionResult Edit()
        {
            var userId = new Guid(ControllerContext.HttpContext.User.Identity.GetUserId());
            var customer = dbContext.Customers.FirstOrDefault(cu => cu.UserId == userId);


            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult Edit(/*int? id,*/ FormCollection collection)
        {
            var userId = new Guid(ControllerContext.HttpContext.User.Identity.GetUserId());
            var customer = dbContext.Customers.FirstOrDefault(c => c.UserId == userId);        
            
            if (customer == null)
            {
                customer = new Customer();
                customer.UserId = userId;

                int maxId = dbContext.Customers.Count() > 0 ? dbContext.Customers.Max(c => c.CustomerId.Value) : 0;
                customer.CustomerId = maxId + 1;

                dbContext.Customers.Add(customer);               

                
            }

            customer.FirstName = collection["FirstName"];
            customer.LastName = collection["LastName"];
            customer.Phone = collection["Phone"];
            customer.Delivery = collection["Delivery"];
            customer.Comment = collection["Comment"];

            dbContext.SaveChanges();




            return RedirectToAction("Index");

        }

       
    }
}
