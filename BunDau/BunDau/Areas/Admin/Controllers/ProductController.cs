using BunDau.Models;
using Fluent.Infrastructure.FluentModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BunDau.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        private DoAn_BunDauEntities database = new DoAn_BunDauEntities();
      

        public ActionResult Index(int? page , string id)
        {
            var productList = database.Products.OrderByDescending(x => x.NamePro);
            
            var pageSize=10;
            if (page == null)
            {
                page = 1;
                
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            productList.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(productList.ToPagedList(pageIndex, pageSize));
        }
        public ActionResult Add(Product pro)
        {
            ViewBag.ProductCategory = new SelectList(database.ProductCategories.ToList(), "ID", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product pro,List<string> Image,List<int> rDefault )
        {
            try
            {
                int x = 1;
                var list = database.Products.ToList();
                for (int i = 0; i < list.Count(); i++)
                {
                    if (list[i].ProductID != i + 1)
                    {
                        x = i + 1;
                        break;
                    }
                }
                if (Image != null) { pro.ImagePro = Image[0]; }

                pro.ProductID = x;
                pro.Alias = BunDau.Models.Common.Filter.ChuyenCoDauThanhKhongDau(pro.NamePro);
                database.Products.Add(pro);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch { return View(pro); }
            
        }
        public ActionResult Edit(int id)
        {
            ViewBag.ProductCategory = new SelectList(database.ProductCategories.ToList(), "ID", "Title");
            return View(database.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product pro, int id)
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
            ViewBag.ProductCategory = new SelectList(database.ProductCategories.ToList(), "ID", "Title");
            return View(database.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(Product pro, int id)
        {
            try
            {
                pro = database.Products.Where(s => s.ProductID == id).FirstOrDefault();
                database.Products.Remove(pro);
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
            return View(database.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }
    }
}