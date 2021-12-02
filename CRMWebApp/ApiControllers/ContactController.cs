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

namespace CRMWebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        //public BusinessDbContext _context;
        //public ContactController(BusinessDbContext context)
        //{
        //    _context = context;
        //}

        //[HttpGet]
        //[Route("GetAll")]
        //public async  Task<IActionResult> GetAll()
        //{

        //    var dataList = await _context.contacts.ToListAsync();

        //    return Ok(new { ok = false, AllDataList = dataList});               

        //}

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            string SelectedAllDataQuery = @"SELECT * FROM [contacts] ORDER BY contact_id Desc";
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


        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(contacts entity)
        {

            string inserQuery = @"INSERT INTO [dbo].[contacts]
                                                               ([fname]
                                                               ,[lname]
                                                               ,[mname]
                                                               ,[email]
                                                               ,[mobile]
                                                               ,[phone]
                                                               ,[website]
                                                               ,[address1]
                                                               ,[address2]
                                                               ,[des]
                                                               ,[other])
                                                         VALUES
                                                               (@fname
                                                               ,@lname
                                                               ,@mname
                                                               ,@email
                                                               ,@mobile
                                                               ,@phone
                                                               ,@website
                                                               ,@address1
                                                               ,@address2
                                                               ,@des
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
