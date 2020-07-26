using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApplication.Models;
using System.IO;


namespace TestApplication.Controllers
{
    public class HomeController : Controller
    {
        private ShopDBEntities _db = new ShopDBEntities();    

        // GET: Home
        public ActionResult Index()
        {
            return View(_db.Products.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            var originalProduct = (from p in _db.Products
                                   where p.Id == id
                                   select p).First();
            return View(originalProduct);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(Product productToAdd)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(productToAdd.ImageFile.FileName);
                string extension = Path.GetExtension(productToAdd.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                productToAdd.FileName = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                productToAdd.ImageFile.SaveAs(fileName);

                _db.Products.Add(productToAdd);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            var productToEdit = (from p in _db.Products
                                 where p.Id == id
                                 select p).First();
            return View(productToEdit);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product productToEdit)
        {
            try
            {

                // TODO: Add update logic here
                var originalProduct = (from p in _db.Products
                                       where p.Id == productToEdit.Id
                                       select p).First();

                string fileName = Path.GetFileNameWithoutExtension(productToEdit.ImageFile.FileName);
                string extension = Path.GetExtension(productToEdit.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                productToEdit.FileName = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                productToEdit.ImageFile.SaveAs(fileName);

                _db.Entry(originalProduct).CurrentValues.SetValues(productToEdit);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(productToEdit);
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            var originalProduct = (from p in _db.Products
                                   where p.Id == id
                                   select p).First();
            return View(originalProduct);
        }

        //// POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Product productToDelete)
        {
            try
            {
                // TODO: Add delete logic here
                var originalProduct = (from p in _db.Products
                                       where p.Id == productToDelete.Id
                                       select p).First();
                _db.Products.Remove(originalProduct);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
