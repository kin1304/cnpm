using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BunDau.Models;
namespace BunDau.Controllers
{
    public class AccController : Controller
    {
        DoAn_BunDauEntities database = new DoAn_BunDauEntities();
        // GET: Account


        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Customer user, FormCollection form)
        {
            Customer checkUser = database.Customers.SingleOrDefault(x => x.NameCus == user.NameCus);
            if (checkUser != null)
            {
                ViewBag.Message = "UserName already exists, please choose another UserName.";
                return View(user);
            }
            else
            {
                if (user.Password != form["txtConfirmPassword"])
                {
                    ViewBag.Message = "Password and Confirm Password do not match.";
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    
                    database.Customers.Add(user);
                    database.SaveChanges();
                    ViewBag.Message = "Register success!";
                    return RedirectToAction("Index","Customer");
                }
            }
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Customer user)
        {
            Customer checkUser = database.Customers.SingleOrDefault(x=> x.NameCus == user.NameCus &&  x.Password == user.Password);
            if (checkUser == null)
            {
                ViewBag.Message = "UserName or Password is incorrect";
                return View();
            }
            else
            {
                Session["User"] = checkUser;
                return RedirectToAction("Index");
            }
        }

    }
}