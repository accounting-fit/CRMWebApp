using CRMWebApp.GlobalElemnts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using CRMWebApp.Models;

namespace CRMWebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        //public BusinessDbContext _context;
        //public EventController(BusinessDbContext context)
        //{
        //    _context = context;
        //}

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IActionResult> GetAll()
        //{

        //    var dataList = await _context.events.ToListAsync();

        //    return Ok(new { ok = false, AllDataList = dataList });

        //}

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            string SelectedAllDataQuery = @"SELECT * FROM [events] ORDER BY event_id Desc";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var dataList = await con.QueryAsync<events>(SelectedAllDataQuery);

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


        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(events entity)
        {

            string inserQuery = @"INSERT INTO [dbo].[events]
                                                               ([topic]
                                                               ,[type]
                                                               ,[status]
                                                               ,[des]
                                                               ,[start_date]
                                                               ,[start_time]
                                                               ,[end_date]
                                                               ,[end_time])
                                                                VALUES
                                                               (@topic
                                                               ,@type
                                                               ,@status
                                                               ,@des
                                                               ,@start_date
                                                               ,@start_time
                                                               ,@end_date
                                                               ,@end_time)";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                using (var trn = await con.BeginTransactionAsync())
                {
                    try
                    {
                        int rowAffect = await con.ExecuteAsync(inserQuery, entity, trn);
                        await trn.CommitAsync();
                        if (rowAffect > 0)
                        {
                            return Ok(new { ok = true });
                        }
                        else
                        {
                            return Ok(new { ok = false });
                        }
                    }
                    catch (Exception ex)
                    {
                        await trn.RollbackAsync();
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
}
