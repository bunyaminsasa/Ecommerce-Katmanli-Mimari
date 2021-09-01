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
    public class BrandController : Controller
    {
        MSSQLRepo<Brand> repoBrand;
        public BrandController(MSSQLRepo<Brand> _repoBrand)
        {
            repoBrand = _repoBrand;
        }

        public IActionResult Index()
        {
            return View(repoBrand.GetAll());
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(Brand model)
        {
            repoBrand.Add(model);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(repoBrand.GetBy(g=>g.ID==id));
        }

        public IActionResult Update(Brand model)
        {
            repoBrand.Update(model);
            return RedirectToAction("Index");
        }

        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }

        public string Delete(int _id)
        {
            string rtn = "";

            Brand slide = repoBrand.GetBy(g => g.ID == _id) ?? null;
            if(slide!=null)
            {
                repoBrand.Delete(slide);
                rtn = "OK";
            }

            return rtn;
        }
    }
}
