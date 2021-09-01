using Ecommerce.BL.Repositories;
using Ecommerce.DAL.Entitites;
using Ecommerce.WebUI.Models;
using Ecommerce.WebUI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebUI.Controllers
{
    public class CartController : Controller
    {
        MSSQLRepo<Product> repoProduct;
        MSSQLRepo<Orders> repoOrders;
        MSSQLRepo<OrderDetail> repoOrderDetail;
        public CartController(MSSQLRepo<Product> _repoProduct, MSSQLRepo<Orders> _repoOrders, MSSQLRepo<OrderDetail> _repoOrderDetail)
        {
            repoProduct = _repoProduct;
            repoOrders = _repoOrders;
            repoOrderDetail = _repoOrderDetail;
        }

        [Route("/Sepetim")]
        public IActionResult Index()
        {
            CartVM cartVM = new CartVM
            {
                Carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["SepetCookie"]),
                Products = repoProduct.GetAll().Include(i => i.Pictures).OrderBy(o => Guid.NewGuid()).Take(4)
            };

            return View(cartVM);
        }

        public string AddProduct(int _productID, int _quantity)
        {
            Product product = repoProduct.GetAll(g => g.ID == _productID).Include(i => i.Pictures).FirstOrDefault() ?? null;
            if (product != null)
            {
                List<Cart> carts;
                string firstPicture = product.Pictures.FirstOrDefault().PicturePath;
                if (string.IsNullOrEmpty(firstPicture)) firstPicture = "/product/nopicture.jpg";
                Cart cart = new Cart { ProductID = product.ID, ProductName = product.Name, ProductPrice = product.Price, Quantity = _quantity, ProductPicture = firstPicture };
                if (Request.Cookies["SepetCookie"] == null)
                {
                    carts = new List<Cart>();
                    carts.Add(cart);
                }
                else
                {
                    carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["SepetCookie"]);
                    bool varmi = false;
                    foreach (Cart c in carts)
                    {
                        if (c.ProductID == _productID)
                        {
                            c.Quantity = _quantity;
                            varmi = true;
                        }
                    }
                    if (varmi == false) carts.Add(cart);
                }
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("SepetCookie", JsonConvert.SerializeObject(carts), cookieOptions);
                return product.Name;
            }
            else return "";
        }

        public int GetProductCount()
        {
            if (Request.Cookies["SepetCookie"] == null) return 0;
            else
            {
                List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["SepetCookie"]);
                return carts.Sum(s => s.Quantity);
            }
        }

        [Route("/Sepetim/SiparisiTamamla")]
        public IActionResult CheckOut()
        {
            CartCheckOutVM cartCheckOutVM = new CartCheckOutVM { 
                Carts= JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["SepetCookie"])
        };
            return View(cartCheckOutVM);
        }

        [Route("/Sepetim/SiparisiTamamla"),HttpPost]
        public IActionResult CheckOut(CartCheckOutVM model)
        {
            string lastID = "1";
            if (repoOrders.GetAll().Any()) lastID = repoOrders.GetAll().OrderByDescending(o=>o.ID).First().ID.ToString();
            model.Orders.OrderNumber = lastID + DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Hour.ToString();
            model.Orders.LastIPNO = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            model.Orders.RecDate = DateTime.Now;
            repoOrders.Add(model.Orders);

            List<Cart> carts = JsonConvert.DeserializeObject<List<Cart>>(Request.Cookies["SepetCookie"]);
            if(carts.Any())
            {
                foreach (Cart c in carts)
                {
                    OrderDetail orderDetail = new OrderDetail {
                        OrdersID = model.Orders.ID,
                        ProductID= c.ProductID,
                        ProductPicture=c.ProductPicture,
                        ProductName=c.ProductName,
                        Quantity=c.Quantity,
                        ProductPrice=c.ProductPrice                        
                    };
                    repoOrderDetail.Add(orderDetail);
                }
            }
            Response.Cookies.Delete("SepetCookie");
            return Redirect("/");
        }
    }
}
