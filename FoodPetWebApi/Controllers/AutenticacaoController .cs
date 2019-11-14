using FoodPetWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace FoodPetWebApi.Controllers
{
    [AllowAnonymous] //Faz com que todos os metodos do controller estejam disponiveis para usuarios anonimos
    public class AutenticacaoController : Controller
    {
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (model.Login == "pamela" && model.Senha == "123")
            {
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name,"Pamela"),
                    new Claim(ClaimTypes.Email,"pamelawasen0@gmail.com"),
                    new Claim(ClaimTypes.Country,"Brasil")

                },
                "ApplicationCookie");
                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;
                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }
            ModelState.AddModelError("", "Usuario não localizado");
            return View();


        }
        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("index", "home");
        }
        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }
            return returnUrl;
        }
      
    }
}