using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using CRMWebApp.GlobalElemnts;
using CRMWebApp.Models;
using System;
using Dapper;

namespace CRMWebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        //public BusinessDbContext _context;
        //public TaskController(BusinessDbContext context)
        //{
        //    _context = context;
        //}

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IActionResult> GetAll()
        //{

        //    var dataList = await _context.tasks.ToListAsync();

        //    return Ok(new { ok = false, AllDataList = dataList });

        //}

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            string SelectedAllDataQuery = @"SELECT * FROM [tasks] ORDER BY task_id Desc";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var dataList = await con.QueryAsync<tasks>(SelectedAllDataQuery);

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
        public async Task<IActionResult> Save(tasks entity)
        {           

            string inserQuery = @"INSERT INTO [dbo].[tasks]
                                                           ([task_name]
                                                           ,[status]
                                                           ,[refer_type]
                                                           ,[refer_name]
                                                           ,[priority]
                                                           ,[des])
                                                            VALUES
                                                           (@task_name
                                                           ,@status
                                                           ,@refer_type
                                                           ,@refer_name
                                                           ,@priority
                                                           ,@des)";
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
