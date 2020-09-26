using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HansViolinWebApp.Models;
using HansViolinWebApp.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace HansViolinWebApp.Controllers
{
    public class AccountController : Controller
    {
        private AdminUserRepository adminUserRepository;

        public AccountController (HansViolinWebContext context)
        {
            adminUserRepository = new AdminUserRepository(context);
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<ActionResult> ValidateAsync(AdminUser admin)
        {
            var _admin = await adminUserRepository.getUser();
            if (_admin != null)
            {
                if (_admin.username == admin.username && Crypto.VerifyHashedPassword(_admin.password, admin.password))
                {

                    return Json(new { status = true, message = "Login Successfull!" });
                }
                else
                {
                    return Json(new { status = false, message = "Invalid Username or Password!" });
                }
            }
            else
            {
                var user = new AdminUser
                {
                    username = "adm1n_h10v",
                    password = Crypto.HashPassword("Passw0rd8vh1!"),
                    Role = "Admin"
                };
                await adminUserRepository.createUser(user);
                return Json(new { status = false, message = "No User available, created a new one. Please try login again" });

            }
        }

        [Authorize]
        public IActionResult LoginRedirect()
        {
            return RedirectToAction("index","admin");
        }
    }
}