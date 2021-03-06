using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Main/Task/Index.cshtml");
        }

        public IActionResult Create()
        {
            return View("/Views/Main/Task/Create.cshtml");
        }

        public IActionResult Edit(string id)
        {
            ViewData["Action"] = "Edit Task";
            ViewBag.Id = id;
            return View("/Views/Main/Task/Edit.cshtml");
        }

        public IActionResult Delete(string id)
        {
            ViewData["Action"] = "Delete Task";
            ViewBag.Id = id;
            return View("/Views/Main/Task/Delete.cshtml");
        }
    }
}
