using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Main/Report/Index.cshtml");
        }
    }
}
