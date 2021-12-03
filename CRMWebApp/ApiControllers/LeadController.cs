using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using CRMWebApp.GlobalElemnts;
using CRMWebApp.Models;
using System;
using Dapper;
using System.Linq;
using System.Data;
using ClosedXML.Excel;
using System.IO;

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

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            string selectedData = @"select * from [leads] where lead_id='" + id + "'" + "";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var singleData = await con.QueryAsync<leads>(selectedData);
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
        public async Task<IActionResult> Update(leads entity)
        {

            string updateQuery = @"UPDATE [dbo].[leads]
                                                           SET [fname] = @fname
                                                              ,[lname] = @lname
                                                              ,[mname] = @mname
                                                              ,[sales_person] = @sales_person
                                                              ,[dep] = @dep
                                                              ,[comp] = @comp
                                                              ,[industry] = @industry
                                                              ,[lead_source] = @lead_source
                                                              ,[lead_status] = @lead_status
                                                              ,[no_empl] = @no_empl
                                                              ,[revenue] = @revenue
                                                              ,[des] = @des
                                                              ,[referred] = @referred
                                                              ,[address1] = @address1
                                                              ,[address2] = @address2
                                                              ,[email] = @email
                                                              ,[mobile] = @mobile
                                                              ,[phone] = @phone
                                                              ,[website] = @website
                                                              ,[other] = @other
                                                         WHERE lead_id= @lead_id";
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

            string deleteQuery = @"Delete  [dbo].[leads] where lead_id='" + id + "'" + "";
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
            string selectMaterialGoods = @"SELECT * FROM [leads] ORDER BY lead_id Desc";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    DataTable table = new DataTable();
                    table.Load(await con.ExecuteReaderAsync(selectMaterialGoods));


                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("LeadList");
                        var currentRow = 1;

                        worksheet.Cell(currentRow, 1).Value = "Id";
                        worksheet.Cell(currentRow, 2).Value = "First Name";
                        worksheet.Cell(currentRow, 3).Value = "Last Name";
                        worksheet.Cell(currentRow, 4).Value = "Middle Name";
                        worksheet.Cell(currentRow, 5).Value = "Sales Person";
                        worksheet.Cell(currentRow, 6).Value = "Department";
                        worksheet.Cell(currentRow, 7).Value = "Company";
                        worksheet.Cell(currentRow, 8).Value = "Industry";
                        worksheet.Cell(currentRow, 9).Value = "Lead Source";
                        worksheet.Cell(currentRow, 10).Value = "Lead Status";
                        worksheet.Cell(currentRow, 11).Value = "No of Employees";
                        worksheet.Cell(currentRow, 12).Value = "Revenue";
                        worksheet.Cell(currentRow, 13).Value = "Description";
                        worksheet.Cell(currentRow, 14).Value = "Referred By";
                        worksheet.Cell(currentRow, 15).Value = "Email";
                        worksheet.Cell(currentRow, 16).Value = "Mobile";
                        worksheet.Cell(currentRow, 17).Value = "Phone";
                        worksheet.Cell(currentRow, 18).Value = "Website";
                        worksheet.Cell(currentRow, 19).Value = "Address1";
                        worksheet.Cell(currentRow, 20).Value = "Address2";
                        worksheet.Cell(currentRow, 21).Value = "Other";

                        worksheet.Row(currentRow).Cells(1, 21).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 21).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 21).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 21).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        worksheet.Row(1).Cells(1, 21).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                        worksheet.Row(1).Cells(1, 21).Style.Font.Bold = true;

                        foreach (DataRow row in table.Rows)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = row["lead_id"]?.ToString();
                            worksheet.Cell(currentRow, 2).Value = row["fname"]?.ToString();
                            worksheet.Cell(currentRow, 3).Value = row["lname"]?.ToString();
                            worksheet.Cell(currentRow, 4).Value = row["mname"]?.ToString();
                            worksheet.Cell(currentRow, 5).Value = row["sales_person"]?.ToString();
                            worksheet.Cell(currentRow, 6).Value = row["dep"]?.ToString();
                            worksheet.Cell(currentRow, 7).Value = row["comp"]?.ToString();
                            worksheet.Cell(currentRow, 8).Value = row["industry"]?.ToString();
                            worksheet.Cell(currentRow, 9).Value = row["lead_source"]?.ToString();
                            worksheet.Cell(currentRow, 10).Value = row["lead_status"]?.ToString();
                            worksheet.Cell(currentRow, 11).Value = row["no_empl"]?.ToString();
                            worksheet.Cell(currentRow, 12).Value = row["revenue"]?.ToString();
                            worksheet.Cell(currentRow, 13).Value = row["des"]?.ToString();
                            worksheet.Cell(currentRow, 14).Value = row["referred"]?.ToString();
                            worksheet.Cell(currentRow, 15).Value = row["address1"]?.ToString();
                            worksheet.Cell(currentRow, 16).Value = row["address2"]?.ToString();
                            worksheet.Cell(currentRow, 17).Value = row["email"]?.ToString();
                            worksheet.Cell(currentRow, 18).Value = row["mobile"]?.ToString();
                            worksheet.Cell(currentRow, 19).Value = row["phone"]?.ToString();
                            worksheet.Cell(currentRow, 20).Value = row["website"]?.ToString();
                            worksheet.Cell(currentRow, 21).Value = row["other"]?.ToString();

                            worksheet.Row(currentRow).Cells(1, 21).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 21).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 21).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 21).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        }
                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "LeadList.xlsx");
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

