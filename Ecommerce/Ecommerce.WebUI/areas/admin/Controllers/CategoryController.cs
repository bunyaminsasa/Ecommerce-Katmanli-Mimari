using Ecommerce.BL.Repositories;
using Ecommerce.DAL.Entitites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebUI.areas.admin.Controllers
{
    [Area("admin"),Authorize,Route("/admin/[controller]/[action]/{id?}")]
    public class CategoryController : Controller
    {
        MSSQLRepo<Category> repoCategory;
        public CategoryController(MSSQLRepo<Category> _repoCategory)
        {
            repoCategory = _repoCategory;
        }

        public IActionResult Index()
        {
            return View(repoCategory.GetAll());
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(Category model)
        {
            repoCategory.Add(model);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(repoCategory.GetBy(g=>g.ID==id));
        }

        public IActionResult Update(Category model)
        {
            repoCategory.Update(model);
            return RedirectToAction("Index");
        }

        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }

        public string Delete(int _id)
        {
            string rtn = "";

            Category slide = repoCategory.GetBy(g => g.ID == _id) ?? null;
            if(slide!=null)
            {
                repoCategory.Delete(slide);
                rtn = "OK";
            }

            return rtn;
        }
    }
}
