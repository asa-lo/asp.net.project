using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Caser.Data;
using Caser.Models;
using Microsoft.AspNetCore.Authorization;
using ClosedXML.Excel;
using OfficeOpenXml;
using System.Data;
using OfficeOpenXml.Style;
using System.Drawing;
using Microsoft.AspNetCore.Http;

namespace Caser_CaseManagement.Controllers
{
    public class CasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// ***************
        ///  GET OPEN CASES
        /// ***************
        [Authorize]
        public async Task<IActionResult> Index(string searchString)
        {
            var cases = from c in _context.Case select c;
            var ApplicationDbContext = _context.Case.Include(item => item.Customers);

            if (!String.IsNullOrEmpty(searchString)) //Search for cases based on subject
            {
                ApplicationDbContext = cases.Where(item => item.CaseSubject.Contains(searchString)).Include(item => item.Customers);
            }
            
            return View(await ApplicationDbContext.ToListAsync());
        }

        /// **********
        /// CLOSE CASE
        /// **********

        [Authorize]
        public async Task<IActionResult> CloseCases(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @cases = await _context.Case
                .FirstOrDefaultAsync(m => m.CaseId == id);
            _context.Update(@cases);

            if (@cases == null)
            {
                return NotFound();
            }

            return View(@cases);
        }

        /// *************************
        /// GET FINISHED/CLOSED CASES
        /// *************************

        [HttpPost, ActionName("CloseCases")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CloseCaseConfirmed(int id)
        {
            var cases = await _context.Case.FindAsync(id);
            cases.CaseIsFinished = true;
            _context.Case.Update(@cases);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// ***************
        /// CREATE NEW CASE
        /// ***************
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustName"); //Select list of existing customers
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("CaseId,CaseSubject,CaseDesc,CaseContactName,CaseContactPhone,CaseIsFinished,CustId,CustName")] Cases @cases)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@cases);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustName", @cases.CustId); //Select list of existing customers
            return View(@cases);
        }

        /// *************
        /// EDIT AND STAY
        /// *************
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var @cases = await _context.Case.FindAsync(id);
            if (@cases == null)
            {
                return NotFound();
            }
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustName", @cases.CustId); //Select list of existing customers
            return View(@cases);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("CaseId,CaseSubject,CaseDesc,CaseContactName,CaseContactPhone,CaseIsFinished,CustId,CustName,CaseIntComment")] Cases @cases)
        {
            if (id != @cases.CaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Update(@cases);
                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CasesExists(@cases.CaseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["Success"] = "Changes were saved successfully!"; //Display success message on successful save
                return RedirectToAction("Edit", "Cases", @cases.CaseId);

            }
            ViewData["CustId"] = new SelectList(_context.Customer, "CustId", "CustName", @cases.CustId); //Select list of existing customers
            return View(@cases);
        }

        /// ***********
        /// DELETE CASE
        /// ***********

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @cases = await _context.Case
                .FirstOrDefaultAsync(m => m.CaseId == id);
            if (@cases == null)
            {
                return NotFound();
            }

            return View(@cases);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cases = await _context.Case.FindAsync(id);
            _context.Case.Remove(@cases);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CasesExists(int id)
        {
            return _context.Case.Any(e => e.CaseId == id);
        }


        /// ******************************
        /// GET DETAILS FROM A CLOSED CASE
        /// ******************************

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @cases = await _context.Case
                .Include(item => item.Customers)
                .FirstOrDefaultAsync(m => m.CaseId == id);
            if (@cases == null)
            {
                return NotFound();
            }

            return View(@cases);
        }
        /// ************************************
        /// GET FINISHED/CLOSED CASES
        /// ************************************
        public async Task<IActionResult> FinishedCases(string searchString)
        {
            var ApplicationDbContext = _context.Case.Include(item => item.Customers);
            var cases = from c in _context.Case select c;
           

            if (!String.IsNullOrEmpty(searchString)) //search for cases
            {
                ApplicationDbContext = cases.Where(item => item.CaseSubject.Contains(searchString)).Include(item => item.Customers);
            }
            return View(await ApplicationDbContext.ToListAsync());
        }

        private void ExcelExport(DataTable table)
        {
            using (ExcelPackage packge = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = packge.Workbook.Worksheets.Add("Demo");

                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells["A1"].LoadFromDataTable(table, true);

                //Format the header for column 1-3
                using (ExcelRange range = ws.Cells["A1:C1"])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189)); //Set color to dark blue
                    range.Style.Font.Color.SetColor(Color.White);
                }

                //Example how to Format Column 1 as numeric 
                using (ExcelRange col = ws.Cells[2, 1, 2 + table.Rows.Count, 1])
                {
                    col.Style.Numberformat.Format = "#,##0.00";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }

                //Write it back to the client
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Headers.Add("content-disposition", "attachment;  filename=ExcelExport.xlsx");
                Response.Body.WriteAsync(packge.GetAsByteArray());
                
            }
        }
    }
}
