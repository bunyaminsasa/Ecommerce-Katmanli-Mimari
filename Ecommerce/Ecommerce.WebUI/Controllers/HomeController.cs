using Ecommerce.BL.Repositories;
using Ecommerce.DAL.Entitites;
using Ecommerce.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebUI.Controllers
{
    public class HomeController : Controller
    {
        MSSQLRepo<Slide> repoSlide;
        MSSQLRepo<Product> repoProduct;
        public HomeController(MSSQLRepo<Slide> _repoSlide, MSSQLRepo<Product> _repoProduct)
        {
            repoSlide = _repoSlide;
            repoProduct = _repoProduct;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM();
            homeVM.Slides = repoSlide.GetAll().OrderBy(o => o.DisplayIndex);
            homeVM.LastProducts = repoProduct.GetAll().Include(i=>i.Pictures).OrderByDescending(o => o.RecDate).Take(5);// select top 5 * from product order by recdate desc
            //select top 5 * from product join picture on picture.ProductID=Product.ID order by Product.recdate desc
            homeVM.SalesProducts = repoProduct.GetAll().Include(i => i.Pictures).OrderByDescending(o => o.RecDate).Skip(5).Take(8);
           // homeVM.SalesProducts = homeVM.SalesProducts.OrderBy(o => Guid.NewGuid());
            return View(homeVM);
        }
    }
}