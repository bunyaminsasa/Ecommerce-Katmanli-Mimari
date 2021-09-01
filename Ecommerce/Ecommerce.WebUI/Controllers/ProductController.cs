using Ecommerce.BL.Repositories;
using Ecommerce.DAL.Entitites;
using Ecommerce.Tools;
using Ecommerce.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebUI.Controllers
{
    public class ProductController : Controller
    {
        MSSQLRepo<Product> repoProduct;
        public ProductController(MSSQLRepo<Product> _repoProduct)
        {
            repoProduct = _repoProduct;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/urun/{name}/{id}")]
        public IActionResult Detail(string name,int id)
        {
            //Product product = repoProduct.GetAll().ToList().FirstOrDefault(g => GeneralTool.UrlConvert(g.Name) == name) ?? null;
            Product product = repoProduct.GetAll(g=>g.ID==id).Include(i=>i.Category).Include(i=>i.Pictures).FirstOrDefault() ?? null;
            if (product != null)
            {
                ProductVM productVM = new ProductVM();
                productVM.Product = product;
                productVM.Products = repoProduct.GetAll(g => g.ID!=product.ID && g.CategoryID == product.CategoryID).Include(i=>i.Pictures).Take(8).ToList();
                return View(productVM);
            }
            else return Redirect("/");
        }
    }
}
