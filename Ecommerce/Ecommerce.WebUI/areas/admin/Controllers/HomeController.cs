using Ecommerce.BL.Repositories;
using Ecommerce.DAL.Entitites;
using Ecommerce.Tools;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce.WebUI.areas.admin.Controllers
{
    [Area("admin"), Authorize]
    public class HomeController : Controller
    {
        MSSQLRepo<Admin> repoAdmin;
        public HomeController(MSSQLRepo<Admin> _repoAdmin)
        {
            repoAdmin = _repoAdmin;
        }

        [Route("/admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/admin/giris"),AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [Route("/admin/giris"), AllowAnonymous,HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string ReturnUrl, string username,string password,bool remember)
        {
            Admin admin = repoAdmin.GetBy(g => g.UserName == username && g.Password == GeneralTool.getMD5(password)) ?? null;
            if (admin != null)
            {

                ClaimsIdentity claimsIdentity = new ClaimsIdentity("Divisima");
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, admin.Name + " " + admin.Surname));
                claimsIdentity.AddClaim(new Claim(ClaimTypes.PrimarySid, admin.ID.ToString()));
                //claimsIdentity.AddClaim(new Claim("Iskonto", "20"));
              
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
                claimsPrincipal.AddIdentity(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties() { IsPersistent = remember });


                //ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.PrimarySid, admin.ID.ToString()), new Claim(ClaimTypes.Name, admin.Name + " " + admin.Surname) },, "Divisima"));

                //await HttpContext.SignInAsync(
                //CookieAuthenticationDefaults.AuthenticationScheme,
                //new ClaimsPrincipal(principal), new AuthenticationProperties() { IsPersistent = remember });




                if (string.IsNullOrEmpty(ReturnUrl)) return Redirect("/admin"); else return Redirect(ReturnUrl);
            }
            else ViewBag.mesaj = "Hatalı Kullanıcı Adı veya Şifre";
            return View();
        }

        [Route("/admin/cikis")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/admin");
        }
    }
}
