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
    public class ManageCustomerController : ControllerBase
    {


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            string SelectedAllDataQuery = @"SELECT c.*,Convert(bit,IIF(cu.contact_id>0,1,0)) as issupported FROM [contacts] c  left join customers cu on c.contact_id=cu.contact_id where c.type=0  ORDER BY c.contact_id Desc";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var dataList = await con.QueryAsync<contacts>(SelectedAllDataQuery);

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


        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            string selectedData = @"select * from [contacts] where contact_id='" + id + "'" + "";
            string subData = @"select ce.contact_id,e.* from [dbo].[customerEvent]  ce inner join [events] e on ce.event_id=e.event_id where ce.contact_id='" + id + "'" + "";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var singleData = await con.QueryAsync<contacts>(selectedData);
                    var subDataList = await con.QueryAsync<events>(subData);

                    return Ok(new { ok = false, SingleData = singleData.FirstOrDefault(),SubDataList= subDataList.ToList() });
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


