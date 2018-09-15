using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSVUploadToDataTestProject.EntityFramework;
using CSVUploadToDataTestProject.EntityFramework.DomainModel;

namespace CSVUploadToDataTestProject.Controllers
{
    public class CSVDatasController : Controller
    {
        private readonly MyDbContext _context;

        public CSVDatasController(MyDbContext context)
        {
            _context = context;
        }

        // GET: CSVDatas
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.CSVData.Include(c => c.Client).Include(c => c.Site);
            return View(await myDbContext.ToListAsync());
        }

        // GET: CSVDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cSVData = await _context.CSVData
                .Include(c => c.Client)
                .Include(c => c.Site)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cSVData == null)
            {
                return NotFound();
            }

            return View(cSVData);
        }

        // GET: CSVDatas/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id");
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Id");
            return View();
        }

        // POST: CSVDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,FooData,ClientId,SiteId")] CSVData cSVData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cSVData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", cSVData.ClientId);
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Id", cSVData.SiteId);
            return View(cSVData);
        }

        // GET: CSVDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cSVData = await _context.CSVData.FindAsync(id);
            if (cSVData == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", cSVData.ClientId);
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Id", cSVData.SiteId);
            return View(cSVData);
        }

        // POST: CSVDatas/Edit/5
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
                    _context.Update(cSVData);
                    await _context.SaveChangesAsync();
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", cSVData.ClientId);
            ViewData["SiteId"] = new SelectList(_context.Sites, "Id", "Id", cSVData.SiteId);
            return View(cSVData);
        }

        // GET: CSVDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cSVData = await _context.CSVData
                .Include(c => c.Client)
                .Include(c => c.Site)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cSVData == null)
            {
                return NotFound();
            }

            return View(cSVData);
        }

        // POST: CSVDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cSVData = await _context.CSVData.FindAsync(id);
            _context.CSVData.Remove(cSVData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CSVDataExists(int id)
        {
            return _context.CSVData.Any(e => e.Id == id);
        }
    }
}
