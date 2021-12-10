using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Controllers
{
    public class ManageEmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Main/Manage/ManageEmployee/Index.cshtml");
        }

        public IActionResult Create()
        {
            return View("/Views/Main/Manage/ManageEmployee/Create.cshtml");
        }

        public IActionResult Edit(string id)
        {
            ViewData["Action"] = "Edit Contact";
            ViewBag.Id = id;
            return View("/Views/Main/Manage/ManageEmployee/Edit.cshtml");
        }

        public IActionResult Delete(string id)
        {
            ViewData["Action"] = "Delete Contact";
            ViewBag.Id = id;
            return View("/Views/Main/Manage/ManageEmployee/Delete.cshtml");
        }
    }
}
