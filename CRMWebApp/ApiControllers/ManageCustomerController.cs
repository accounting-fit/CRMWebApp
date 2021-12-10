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
            string SelectedAllDataQuery = @"SELECT c.*,Convert(bit,IIF(cu.contact_id>0,1,0)) as issupported FROM [contacts] c  left join customers cu on c.contact_id=cu.contact_id   ORDER BY c.contact_id Desc";
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
                                                               ,[type]
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
                                                               ,@type
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

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(contacts entity)
        {

            string updateQuery = @"UPDATE [dbo].[contacts]
                                                           SET [fname] = @fname
                                                              ,[lname] = @lname
                                                              ,[mname] = @mname
                                                              ,[email] = @email
                                                              ,[mobile] = @mobile
                                                              ,[phone] = @phone
                                                              ,[website] = @website
                                                              ,[address1] = @address1
                                                              ,[address2] = @address2
                                                              ,[type] = @type
                                                              ,[des] = @des
                                                              ,[other] = @other
                                                         WHERE contact_id=@contact_id";
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

            string deleteQuery = @"Delete  [dbo].[contacts] where contact_id='" + id + "'" + "";
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
            string selectMaterialGoods = @"SELECT * FROM [contacts] ORDER BY contact_id Desc";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    DataTable table = new DataTable();
                    table.Load(await con.ExecuteReaderAsync(selectMaterialGoods));


                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("ContactList");
                        var currentRow = 1;

                        worksheet.Cell(currentRow, 1).Value = "Id";
                        worksheet.Cell(currentRow, 2).Value = "First Name";
                        worksheet.Cell(currentRow, 3).Value = "Last Name";
                        worksheet.Cell(currentRow, 4).Value = "Middle Name";
                        worksheet.Cell(currentRow, 5).Value = "Email";
                        worksheet.Cell(currentRow, 6).Value = "Mobile";
                        worksheet.Cell(currentRow, 7).Value = "Phone";
                        worksheet.Cell(currentRow, 8).Value = "Website";
                        worksheet.Cell(currentRow, 9).Value = "Primary Address";
                        worksheet.Cell(currentRow, 10).Value = "Secondary Address";
                        worksheet.Cell(currentRow, 11).Value = "Type";
                        worksheet.Cell(currentRow, 12).Value = "Description";
                        worksheet.Cell(currentRow, 13).Value = "Other Information";

                        worksheet.Row(currentRow).Cells(1, 13).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 13).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 13).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Row(currentRow).Cells(1, 13).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        worksheet.Row(1).Cells(1, 13).Style.Fill.SetBackgroundColor(XLColor.Yellow);
                        worksheet.Row(1).Cells(1, 13).Style.Font.Bold = true;

                        foreach (DataRow row in table.Rows)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = row["contact_id"]?.ToString();
                            worksheet.Cell(currentRow, 2).Value = row["fname"]?.ToString();
                            worksheet.Cell(currentRow, 3).Value = row["lname"]?.ToString();
                            worksheet.Cell(currentRow, 4).Value = row["mname"]?.ToString();
                            worksheet.Cell(currentRow, 5).Value = row["email"]?.ToString();
                            worksheet.Cell(currentRow, 6).Value = row["mobile"]?.ToString();
                            worksheet.Cell(currentRow, 7).Value = row["phone"]?.ToString();
                            worksheet.Cell(currentRow, 8).Value = row["website"]?.ToString();
                            worksheet.Cell(currentRow, 9).Value = row["address1"]?.ToString();
                            worksheet.Cell(currentRow, 10).Value = row["address2"]?.ToString();
                            worksheet.Cell(currentRow, 11).Value =Convert.ToBoolean(row["type"])==false?"Customer":"Employeee";
                            worksheet.Cell(currentRow, 12).Value = row["des"]?.ToString();
                            worksheet.Cell(currentRow, 13).Value = row["other"]?.ToString();

                            worksheet.Row(currentRow).Cells(1, 13).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 13).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 13).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            worksheet.Row(currentRow).Cells(1, 13).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        }
                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();

                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "ContactList.xlsx");
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

        [HttpGet]
        [Route("GetAllContacts/{type}")]
        public async Task<IActionResult> GetAllContacts(string type)
        {
            string SelectedAllDataQuery = @"SELECT c.contact_id as id,ISNULL(c.fname,'')+' '+ISNULL(c.mname,'')+' '+ISNULL(c.lname,'') as text FROM  [contacts] c where c.type='" + type + "' ORDER BY c.contact_id Desc";
            using (var con = new SqlConnection(GlobalClass.ConnectionString))
            {
                await con.OpenAsync();
                try
                {
                    var dataList = await con.QueryAsync<dropdown>(SelectedAllDataQuery);

                    return Ok(new { ok = false, GetAllContacts = dataList.ToList() });
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


