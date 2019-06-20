using JewelryStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JewelryStore.UI.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext dbContext = ApplicationDbContext.Create();

        // GET: Category
        public ActionResult Index()
        {    
            return View(dbContext.Categories.ToList());
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            int maxId = dbContext.Categories.Count() > 0 ? dbContext.Categories.Max(c => c.CategoryId.Value) : 0;
            category.CategoryId = maxId + 1;

            dbContext.Categories.Add(category);

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

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            Category category = dbContext.Categories.First(c => c.CategoryId == id);

            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Category category)
        {
            Category categoryOld = dbContext.Categories.First(c => c.CategoryId == id);

            //FakeRepository.Categories.Remove(categoryOld);
            //FakeRepository.Categories.Add(category);

            categoryOld.Name = category.Name;
            categoryOld.Description = category.Description;

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

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            Category category = dbContext.Categories.First(c => c.CategoryId == id);
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Category category = dbContext.Categories.First(c => c.CategoryId == id);
            dbContext.Categories.Remove(category);

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
