using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JewelryStore.Models;

namespace JewelryStore.UI.Controllers
{
    public class CartItemController : Controller
    {
        ApplicationDbContext dbContext = ApplicationDbContext.Create();

        // GET: CartItem
        public ActionResult Index()
        {
            return View(dbContext.CartItems);
        }

        // GET: CartItem/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CartItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartItem/Create
        [HttpPost]
        public ActionResult Create(CartItem cartitem)
        {
            int maxId = dbContext.CartItems.Count() > 0 ? dbContext.CartItems.Max(ci => ci.CartItemId.Value) : 0;
            cartitem.CartItemId = maxId + 1;

            dbContext.CartItems.Add(cartitem);

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

        // GET: CartItem/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartItem/Edit/5
        [HttpPost]
        public ActionResult Edit(CartItem cartitem)
        {
           

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

        // GET: CartItem/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartItem/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
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
