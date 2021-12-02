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
    public class LeadController : ControllerBase
    {
        //public BusinessDbContext _context;
        //public LeadController(BusinessDbContext context)
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
            string SelectedAllDataQuery = @"SELECT * FROM [leads] ORDER BY lead_id Desc";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var dataList = await con.QueryAsync<leads>(SelectedAllDataQuery);

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
        public async Task<IActionResult> Save(leads entity)
        {           

            string inserQuery = @"INSERT INTO [dbo].[leads]
                                                           ([fname]
                                                           ,[lname]
                                                           ,[mname]
                                                           ,[sales_person]
                                                           ,[dep]
                                                           ,[comp]
                                                           ,[industry]
                                                           ,[lead_source]
                                                           ,[lead_status]
                                                           ,[no_empl]
                                                           ,[revenue]
                                                           ,[des]
                                                           ,[referred]
                                                           ,[address1]
                                                           ,[address2]
                                                           ,[email]
                                                           ,[mobile]
                                                           ,[phone]
                                                           ,[website]
                                                           ,[other])
                                                     VALUES
                                                           (@fname
                                                           ,@lname
                                                           ,@mname
                                                           ,@sales_person
                                                           ,@dep
                                                           ,@comp
                                                           ,@industry
                                                           ,@lead_source
                                                           ,@lead_status
                                                           ,@no_empl
                                                           ,@revenue
                                                           ,@des
                                                           ,@referred
                                                           ,@address1
                                                           ,@address2
                                                           ,@email
                                                           ,@mobile
                                                           ,@phone
                                                           ,@website
                                                           ,@other)";
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
