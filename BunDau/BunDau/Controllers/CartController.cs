using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BunDau.Models;

namespace BunDau.Controllers
{
    public class CartController : Controller
    {
        DoAn_BunDauEntities database = new DoAn_BunDauEntities();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
        public List<CartItem> GetCart()
        {
            List<CartItem> myCart = Session["GioHang"] as List<CartItem>;
            //Nếu giỏ hàng chưa tồn tại thì tạo mới và đưa vào Session
            if (myCart == null)
            {
                myCart = new List<CartItem>();
                Session["GioHang"] = myCart;
            }


            return myCart;
        }
        public ActionResult AddToCart(int id)
        {
            //Lấy giỏ hàng hiện tại
            List<CartItem> myCart = GetCart();
            CartItem currentProduct = myCart.FirstOrDefault(p => p.ProductID == id);
            if (currentProduct == null)
            {
                currentProduct = new CartItem(id);
                myCart.Add(currentProduct);
            }
            else
            {
                currentProduct.Number++;
            }
            return RedirectToAction("Index", "Customer", new 
            {
                id = id
            });
        }
        private int GetTotalNumber()
        {
            int totalNumber = 0;
            List<CartItem> myCart = GetCart();
            if (myCart != null)
                totalNumber = myCart.Sum(sp => sp.Number);
            return totalNumber;
        }
        private decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            List<CartItem> myCart = GetCart();
            if (myCart != null)
                totalPrice = myCart.Sum(sp => sp.FinalPrice());


            return totalPrice;
        }
        public ActionResult GetCartInfo()
        {
            List<CartItem> myCart = GetCart();
            if (myCart == null || myCart.Count == 0)
            {
                return RedirectToAction("Index", "Customer");
            }
            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return View(myCart);
        }
        public ActionResult CartPartial()
        {
            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return PartialView();
        }
        public ActionResult DeleteCartItem(int id)
        {
            List<CartItem> myCart = GetCart();
            //Lấy sản phẩm trong giỏ hàng
            var currentProduct = myCart.FirstOrDefault(p => p.ProductID == id);
            if (currentProduct != null)
            {
                myCart.RemoveAll(p => p.ProductID == id);
                return RedirectToAction("GetCartInfo");
            }
            if (myCart.Count == 0)
                return RedirectToAction("Index", "CustomerProducts");


            return RedirectToAction("GetCartInfo");
        }
        public ActionResult UpdateCartItem(int id, int Number)
        {
            List<CartItem> myCart = GetCart();
            var currentProduct = myCart.FirstOrDefault(p => p.ProductID == id);
            if (currentProduct != null)
            {
                currentProduct.Number = Number;
            }


            return RedirectToAction("GetCartInfo");
        }
        public ActionResult ConfirmCart()
        {
            if (Session["TaiKhoan"] == null)
                return RedirectToAction("Login", "Users");
            List<CartItem> myCart = GetCart();
            if (myCart == null || myCart.Count == 0)
                return RedirectToAction("Index", "CustomerProducts");
            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return View(myCart);
        }
        
        public ActionResult AgreeCart()
        {
            Customer khach = Session["TaiKhoan"] as Customer; //Khách
            List<CartItem> myCart = GetCart(); //Giỏ hàng
            OrderPro DonHang = new OrderPro(); //Tạo mới đơn đặt hàng
            DonHang.IDCus = khach.IDCus;
            DonHang.DateOrder = DateTime.Now;
            DonHang.AddressDelivery = "PLEASE CONTACT CUSTOMER";
            database.OrderProes.Add(DonHang);
            database.SaveChanges();
            //Lần lượt thêm từng chi tiết cho đơn hàng trên
            foreach (var product in myCart)
            {
                OrderDetail chitiet = new OrderDetail();
                chitiet.IDOrder = DonHang.ID;
                chitiet.IDProduct = product.ProductID;
                chitiet.Quantity = product.Number;
                chitiet.UnitPrice = (double)product.Price;
                database.OrderDetails.Add(chitiet);
            }
            database.SaveChanges();

            //Xóa giỏ hàng
            Session["GioHang"] = null;
            return RedirectToAction("Index", "Customer");
        }
    }
}