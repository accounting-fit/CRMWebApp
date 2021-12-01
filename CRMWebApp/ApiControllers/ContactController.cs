using CRMWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        public BusinessDbContext _context;
        public ContactController(BusinessDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public async  Task<IActionResult> GetAll()
        {

            var dataList = await _context.contacts.ToListAsync();

            return Ok(new { ok = false, AllDataList = dataList});               

        }

    }
}
