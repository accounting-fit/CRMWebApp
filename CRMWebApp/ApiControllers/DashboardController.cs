using CRMWebApp.GlobalElemnts;
using CRMWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using ClosedXML.Excel;
using System.IO;

namespace CRMWebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            string SelectedAllDataQuery = @"SELECT 'Contacts' as name,  COUNT(1) as count FROM contacts UNION ALL SELECT 'Events' as name,  COUNT(1) as count FROM events UNION ALL SELECT 'Leads' as name,  COUNT(1) as count FROM leads UNION ALL SELECT 'Tasks' as name,  COUNT(1) as count FROM tasks";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var dataList = await con.QueryAsync<summary>(SelectedAllDataQuery);

                    return Ok(new { ok = false, AllDataList = dataList });
                }
                catch (Exception ex)
                {

                    return BadRequest(ex);
                }
                finally
                {
                    await con.CloseAsync();
                }
            }

        }


       
    }
}


