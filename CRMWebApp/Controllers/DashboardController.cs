using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Main/Dashboard/Index.cshtml");
        }
    }
}
