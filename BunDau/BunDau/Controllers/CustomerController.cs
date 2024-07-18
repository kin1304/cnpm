using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BunDau.Models;
using System.Net;

namespace BunDau.Controllers
{
    public class CustomerController : Controller
    {
        DoAn_BunDauEntities database = new DoAn_BunDauEntities();
        // GET: Customer
        public ActionResult Index()
        {
            var products = database.Products;
            return View(products.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new
               HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = database.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
    }
}