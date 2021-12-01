using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CRMWebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        public BusinessDbContext _context;
        public TaskController(BusinessDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            var dataList = await _context.tasks.ToListAsync();

            return Ok(new { ok = false, AllDataList = dataList });

        }

    }
}
