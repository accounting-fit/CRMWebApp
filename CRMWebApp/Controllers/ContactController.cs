using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Main/Contact/Index.cshtml");
        }

        public IActionResult Create()
        {
            return View("/Views/Main/Contact/Create.cshtml");
        }
    }
}
