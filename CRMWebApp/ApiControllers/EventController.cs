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
using System.Data;
using ClosedXML.Excel;
using System.IO;

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


        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            string selectedData = @"select * from [events] where event_id='" + id + "'" + "";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var singleData = await con.QueryAsync<events>(selectedData);
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
        public async Task<IActionResult> Update(events entity)
        {

            string updateQuery = @"UPDATE [dbo].[events]
                                                       SET [topic] = @topic
                                                          ,[type] = @type
                                                          ,[status] = @status
                                                          ,[des] = @des
                                                          ,[start_date] = @start_date
                                                          ,[start_time] = @start_time
                                                          ,[end_date] = @end_date
                                                          ,[end_time] = @end_time
                                                     WHERE event_id=@event_id";
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

            string deleteQuery = @"Delete  [dbo].[events] where event_id='" + id + "'" + "";
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
            string selectMaterialGoods = @"SELECT * FROM [events] ORDER BY event_id Desc";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    DataTable table = new DataTable();
                    table.Load(await con.ExecuteReaderAsync(selectMaterialGoods));

                    using (var workbook = new XLWorkbook())
                    {

                        var worksheet = workbook.Worksheets.Add("EventList");
                        var currentRow = 1;
     
                        worksheet.Cell(currentRow, 1).Value = "Id";
                        worksheet.Cell(currentRow, 2).Value = "Topic ";
                        worksheet.Cell(currentRow, 3).Value = "Type";
                        worksheet.Cell(currentRow, 4).Value = "Status";
                        worksheet.Cell(currentRow, 5).Value = "Description";
                        worksheet.Cell(currentRow, 6).Value = "Start Date";
                        worksheet.Cell(currentRow, 7).Value = "Start Time";
                        worksheet.Cell(currentRow, 8).Value = "End Date";
                        worksheet.Cell(currentRow, 9).Value = "End Time";

                        worksheet.Row(currentRow).Cells(1, 9).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 9).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 9).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        worksheet.Row(1).Cells(1, 9).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                        worksheet.Row(1).Cells(1, 9).Style.Font.Bold = true;

                        foreach (DataRow row in table.Rows)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = row["event_id"]?.ToString();
                            worksheet.Cell(currentRow, 2).Value = row["topic"]?.ToString();
                            worksheet.Cell(currentRow, 3).Value = row["type"]?.ToString();
                            worksheet.Cell(currentRow, 4).Value = row["status"]?.ToString();
                            worksheet.Cell(currentRow, 5).Value = row["des"]?.ToString();
                            worksheet.Cell(currentRow, 6).Value = row["start_date"]?.ToString();
                            worksheet.Cell(currentRow, 7).Value = row["start_time"]?.ToString();
                            worksheet.Cell(currentRow, 8).Value = row["end_date"]?.ToString();
                            worksheet.Cell(currentRow, 9).Value = row["end_time"]?.ToString();

                            worksheet.Row(currentRow).Cells(1, 9).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 9).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 9).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 9).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                        }
                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "EventList.xlsx");
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

