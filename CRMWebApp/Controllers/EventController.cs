using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Main/Event/Index.cshtml");
        }

        public IActionResult Create()
        {
            return View("/Views/Main/Event/Create.cshtml");
        }

        public IActionResult Edit(string id)
        {
            ViewData["Action"] = "Edit Event";
            ViewBag.Id = id;
            return View("/Views/Main/Event/Edit.cshtml");
        }

        public IActionResult Delete(string id)
        {
            ViewData["Action"] = "Delete Event";
            ViewBag.Id = id;
            return View("/Views/Main/Event/Delete.cshtml");
        }
    }
}
