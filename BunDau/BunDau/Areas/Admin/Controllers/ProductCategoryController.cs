using BunDau.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace BunDau.Areas.Admin.Controllers
{

    public class ProductCategoryController : Controller
    {
        private DoAn_BunDauEntities database = new DoAn_BunDauEntities();
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {

            return View(database.ProductCategories.ToList());
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductCategory procate)
        {
            try
            {
                int x = 1;
                var list = database.ProductCategories.ToList();
                for (int i = 0; i < list.Count(); i++)
                {
                    if (list[i].ID != i + 1)
                    {
                        x = i + 1;
                        break;
                    }
                }

                procate.ID = x;
                procate.Alias = BunDau.Models.Common.Filter.ChuyenCoDauThanhKhongDau(procate.Title);
                database.ProductCategories.Add(procate);
                database.SaveChanges();
                return RedirectToAction("Index");
            }

            catch
            {
                return Content(" Lỗi ");
            }
        }
        public ActionResult Edit(int id)
        {
            
            return View(database.ProductCategories.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory pro, int id)
        {
            try
            {
                database.Entry(pro).State = System.Data.Entity.EntityState.Modified;
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Lỗi");
            }
        }
        public ActionResult Delete(int id)
        {
            
            return View(database.ProductCategories.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(ProductCategory pro, int id)
        {
            try
            {
                pro = database.ProductCategories.Where(s => s.ID == id).FirstOrDefault();
                database.ProductCategories.Remove(pro);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Lỗi");
            }
        }
        public ActionResult Detail(int id)
        {
            return View(database.ProductCategories.Where(s => s.ID == id).FirstOrDefault());
        }
    }
    
}