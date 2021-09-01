using Ecommerce.BL.Repositories;
using Ecommerce.DAL.Entitites;
using Ecommerce.WebUI.Areas.admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebUI.areas.admin.Controllers
{
    [Area("admin"),Authorize,Route("/admin/[controller]/[action]/{id?}")]
    public class ProductController : Controller
    {
        MSSQLRepo<Product> repoProduct;
        MSSQLRepo<Category> repoCategory;
        MSSQLRepo<Brand> repoBrand;
        public ProductController(MSSQLRepo<Product> _repoProduct, MSSQLRepo<Category> _repoCategory, MSSQLRepo<Brand> _repoBrand)
        {
            repoProduct = _repoProduct;
            repoCategory = _repoCategory;
            repoBrand = _repoBrand;
        }

        public IActionResult Index()
        {
            return View(repoProduct.GetAll().Include(i=>i.Category).Include(i=>i.Brand));
        }

        public IActionResult New()
        {
            ProductVM productVM = new ProductVM
            {
                Categories = repoCategory.GetAll().OrderBy(o => o.Name).ToList(),
                Brands = repoBrand.GetAll().OrderBy(o => o.Name).ToList()
            };
           // ViewBag.markalar = new SelectList(repoBrand.GetAll(), "ID", "Name");
            return View(productVM);
        }

        [HttpPost]
        public IActionResult New(ProductVM model)
        {
            repoProduct.Add(model.Product);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            ProductVM productVM = new ProductVM { 
                Product= repoProduct.GetBy(g => g.ID == id),
                Categories = repoCategory.GetAll().OrderBy(o => o.Name).ToList(),
                Brands = repoBrand.GetAll().OrderBy(o => o.Name).ToList()
            };
            return View(productVM);
        }

        public IActionResult Update(ProductVM model)
        {
            repoProduct.Update(model.Product);
            return RedirectToAction("Index");
        }

        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }

        public string Delete(int _id)
        {
            string rtn = "";

            Product product = repoProduct.GetBy(g => g.ID == _id) ?? null;
            if(product!=null)
            {
                repoProduct.Delete(product);
                rtn = "OK";
            }

            return rtn;
        }
    }
}
