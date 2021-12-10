using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Controllers
{
    public class ManageCustomerController : Controller
    {
        public IActionResult Index()
        {
            return View("/Views/Main/Manage/ManageCustomer/Index.cshtml");
        }

        public IActionResult CreateEvent(string id)
        {
            ViewBag.Id = id;
            return View("/Views/Main/Manage/ManageCustomer/CreateEvent.cshtml");
        }

        public IActionResult Edit(string id)
        {
            ViewData["Action"] = "Edit Contact";
            ViewBag.Id = id;
            return View("/Views/Main/Manage/ManageCustomer/Edit.cshtml");
        }

       
    }
}
