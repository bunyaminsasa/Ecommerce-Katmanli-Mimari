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
    public class SlideController : Controller
    {
        MSSQLRepo<Slide> repoSlide;
        public SlideController(MSSQLRepo<Slide> _repoSlide)
        {
            repoSlide = _repoSlide;
        }

        public IActionResult Index()
        {
            return View(repoSlide.GetAll());
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(Slide model)
        {
            if(Request.Form.Files.Any())
            {
                string slidePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "slide");
                if (!Directory.Exists(slidePath)) Directory.CreateDirectory(slidePath);
                using (var stream=new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "slide", Request.Form.Files["Picture"].FileName),FileMode.Create))
                {
                    Request.Form.Files["Picture"].CopyTo(stream);
                }
                model.Picture = "/slide/" + Request.Form.Files["Picture"].FileName;
            }
          
            repoSlide.Add(model);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            return View(repoSlide.GetBy(g=>g.ID==id));
        }

        public IActionResult Update(Slide model)
        {
            if (Request.Form.Files.Any())
            {
                string slidePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "slide");
                if (!Directory.Exists(slidePath)) Directory.CreateDirectory(slidePath);
                using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "slide", Request.Form.Files["Picture"].FileName), FileMode.Create))
                {
                    Request.Form.Files["Picture"].CopyTo(stream);
                }
                model.Picture = "/slide/" + Request.Form.Files["Picture"].FileName;
            }           

            repoSlide.Update(model);
            return RedirectToAction("Index");
        }

        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }

        public string Delete(int _id)
        {
            string rtn = "";

            Slide slide = repoSlide.GetBy(g => g.ID == _id) ?? null;
            if(slide!=null)
            {
                repoSlide.Delete(slide);
                rtn = "OK";
            }

            return rtn;
        }
    }
}
