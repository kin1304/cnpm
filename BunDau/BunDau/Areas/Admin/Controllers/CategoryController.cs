using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BunDau.Models;

namespace BunDau.Areas.Admin.Controllers
{
    
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        private DoAn_BunDauEntities database = new DoAn_BunDauEntities();

        public ActionResult Index()
        {
            
            return View(database.Categories.ToList());
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category cate)
        {
            try
            {
                int x = 1;
                var list = database.Categories.ToList();
                for(int i = 0; i < list.Count();i++)
                {
                    if (list[i].Id != i+1)
                    {
                        x = i+1;
                        break;
                    }
                }
                
                cate.Id = x;
                cate.Alias = BunDau.Models.Common.Filter.ChuyenCoDauThanhKhongDau(cate.NameCate);
                database.Categories.Add(cate);
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
            return View(database.Categories.Where(s => s.Id == id).FirstOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category cate, int id)
        {
            try
            {
                database.Entry(cate).State = System.Data.Entity.EntityState.Modified;
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
            return View(database.Categories.Where(s => s.Id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(Category cate,int id)
        {
            try
            {
                cate = database.Categories.Where(s => s.Id == id).FirstOrDefault();
                database.Categories.Remove(cate);
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
            return View(database.Categories.Where(s => s.Id == id).FirstOrDefault());
        }
    }
}