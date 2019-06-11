using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.Core.Models;
using Webshop.DataAccess.InMemory.Repositories;

namespace Webshop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        public ProductRepository context { get; set; }

        public ProductManagerController()
        {
            context = new ProductRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();

            return View(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid == false)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string Id)
        {
            var productToDelete = context.FindProduct(Id);
            return View(productToDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            var productToDelete = context.Delete(Id);
            if (productToDelete == false)
            {
                return HttpNotFound();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}