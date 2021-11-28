using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View("/Views/Main/Authentication/Login.cshtml");
        }
        public IActionResult Password()
        {
            return View("/Views/Main/Authentication/Password.cshtml");
        }
        public IActionResult Register()
        {
            return View("/Views/Main/Authentication/Register.cshtml");
        }
    }
}
