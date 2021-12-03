using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using CRMWebApp.GlobalElemnts;
using CRMWebApp.Models;
using System;
using Dapper;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using System.Linq;

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


        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            string selectedData = @"select * from [tasks] where task_id='" + id + "'" + "";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var singleData = await con.QueryAsync<tasks>(selectedData);
                    return Ok(new { ok = false, SingleData = singleData.FirstOrDefault() });
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
        [Route("Update")]
        public async Task<IActionResult> Update(tasks entity)        {         

            string updateQuery = @"UPDATE [dbo].[tasks]
                                   SET [task_name] = @task_name
                                      ,[status] = @status
                                      ,[refer_type] = @refer_type
                                      ,[refer_name] = @refer_name
                                      ,[priority] = @priority
                                      ,[des] = @des
                                 WHERE task_id=@task_id";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                using (var trn = await con.BeginTransactionAsync())
                {
                    try
                    {
                        int rowAffect = await con.ExecuteAsync(updateQuery, entity, trn);
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


        [HttpPost]
        [Route("DeleteById/{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {

            string deleteQuery = @"Delete  [dbo].[tasks] where task_id='" + id + "'" + "";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                using (var trn = await con.BeginTransactionAsync())
                {
                    try
                    {
                        int rowAffect = await con.ExecuteAsync(deleteQuery, null, trn);
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

        [HttpGet]
        [Route("ExportExcel")]
        public async Task<FileResult> ExportExcel()
        {
            string selectMaterialGoods = @"SELECT * FROM [tasks] ORDER BY task_id Desc";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    DataTable table = new DataTable();
                    table.Load(await con.ExecuteReaderAsync(selectMaterialGoods));


                    using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("TaskList");
                        var currentRow = 1;

                        worksheet.Cell(currentRow, 1).Value = "Id";
                        worksheet.Cell(currentRow, 2).Value = "Task ";
                        worksheet.Cell(currentRow, 3).Value = "Status";
                        worksheet.Cell(currentRow, 4).Value = "Refer Type";
                        worksheet.Cell(currentRow, 5).Value = "Refer Name";
                        worksheet.Cell(currentRow, 6).Value = "Priority";
                        worksheet.Cell(currentRow, 7).Value = "Description";

                        worksheet.Row(currentRow).Cells(1, 7).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                                  
                        worksheet.Row(1).Cells(1, 7).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                        worksheet.Row(1).Cells(1, 7).Style.Font.Bold = true;

                        foreach (DataRow row in table.Rows)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = row["task_id"]?.ToString();
                            worksheet.Cell(currentRow, 2).Value = row["task_name"]?.ToString();
                            worksheet.Cell(currentRow, 3).Value = row["status"]?.ToString();
                            worksheet.Cell(currentRow, 4).Value = row["refer_type"]?.ToString();
                            worksheet.Cell(currentRow, 5).Value = row["refer_name"]?.ToString();
                            worksheet.Cell(currentRow, 6).Value = row["priority"]?.ToString();
                            worksheet.Cell(currentRow, 7).Value = row["des"]?.ToString();                        

                            worksheet.Row(currentRow).Cells(1,7).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1,7).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1,7).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1,7).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                        }
                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "TaskList.xlsx");
                        }
                    }
                }
                catch (Exception ex)
                {

                    return null;
                }
                finally
                {
                    await con.CloseAsync();
                }
            }

        }


    }
}
