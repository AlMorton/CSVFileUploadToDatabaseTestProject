﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSVUploadToDataTestProject.EntityFramework;
using CSVUploadToDataTestProject.EntityFramework.DomainModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using CSVUploadToDataProject.EntityFramework.Repository;

namespace CSVUploadToDataTestProject.Controllers
{
    [Authorize]
    public class CSVDataController : Controller
    {
        private readonly IRepostory<CSVData, int> _repostory;

        public CSVDataController(IRepostory<CSVData, int> repostory)
        {
            _repostory = repostory;
        }

        // GET: CSVData
        public async Task<IActionResult> Index()
        {
            var myDbContext = _repostory.DbContext().CSVData.Include(c => c.Client).Include(c => c.Site);
            return View(await myDbContext.ToListAsync());
        }        

        // GET: CSVData/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cSVData = await _repostory.DbContext().CSVData
                .Include(c => c.Client)
                .Include(c => c.Site)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cSVData == null)
            {
                return NotFound();
            }

            return View(cSVData);
        }

        // GET: CSVData/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_repostory.DbContext().Clients, "Id", "Id");
            ViewData["SiteId"] = new SelectList(_repostory.DbContext().Sites, "Id", "Id");
            return View();
        }

        // POST: CSVData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,FooData,ClientId,SiteId")] CSVData cSVData)
        {
            if (ModelState.IsValid)
            {
                _repostory.DbContext().Add(cSVData);
                await _repostory.DbContext().SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_repostory.DbContext().Clients, "Id", "Id", cSVData.ClientId);
            ViewData["SiteId"] = new SelectList(_repostory.DbContext().Sites, "Id", "Id", cSVData.SiteId);
            return View(cSVData);
        }

        // GET: CSVData/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cSVData = await _repostory.DbContext().CSVData.FindAsync(id);
            if (cSVData == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_repostory.DbContext().Clients, "Id", "Id", cSVData.ClientId);
            ViewData["SiteId"] = new SelectList(_repostory.DbContext().Sites, "Id", "Id", cSVData.SiteId);
            return View(cSVData);
        }

        // POST: CSVData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,FooData,ClientId,SiteId")] CSVData cSVData)
        {
            if (id != cSVData.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repostory.DbContext().Update(cSVData);
                    await _repostory.DbContext().SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CSVDataExists(cSVData.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_repostory.DbContext().Clients, "Id", "Id", cSVData.ClientId);
            ViewData["SiteId"] = new SelectList(_repostory.DbContext().Sites, "Id", "Id", cSVData.SiteId);
            return View(cSVData);
        }

        // GET: CSVData/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cSVData = await _repostory.DbContext().CSVData
                .Include(c => c.Client)
                .Include(c => c.Site)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cSVData == null)
            {
                return NotFound();
            }

            return View(cSVData);
        }

        // POST: CSVData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cSVData = await _repostory.DbContext().CSVData.FindAsync(id);
            _repostory.DbContext().CSVData.Remove(cSVData);
            await _repostory.DbContext().SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CSVDataExists(int id)
        {
            return _repostory.DbContext().CSVData.Any(e => e.Id == id);
        }
    }
}
