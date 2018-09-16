using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSVUploadToDataTestProject.EntityFramework;
using CSVUploadToDataTestProject.EntityFramework.DomainModel;
using Microsoft.AspNetCore.Authorization;
using CSVUploadToDataProject.EntityFramework.Repository;

namespace CSVUploadToDataProject.Controllers
{
    [Authorize]
    public class SitesController : Controller
    {
        private readonly IRepostory<Site, int> _repostory;

        public SitesController(IRepostory<Site, int> repostory)
        {
            _repostory = repostory;
        }



        // GET: Sites
        public async Task<IActionResult> Index()
        {
            var myDbContext = _repostory.Query().Include(s => s.Client);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Sites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _repostory.DbContext().Sites
                .Include(s => s.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (site == null)
            {
                return NotFound();
            }

            return View(site);
        }

        // GET: Sites/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_repostory.DbContext().Clients, "Id", "Name");

            return View();
        }

        // POST: Sites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ClientId")] Site site)
        {
            if (ModelState.IsValid)
            {
                //_repostory.DbContext().Add(site);
                //await _repostory.DbContext().SaveChangesAsync();
                await _repostory.SaveAsync(site);
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClientId"] = new SelectList(_repostory.DbContext().Clients, "Id", "Id", site.ClientId);

            return View(site);
        }

        // GET: Sites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _repostory.DbContext().Sites.FindAsync(id);
            if (site == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_repostory.DbContext().Clients, "Id", "Name", site.ClientId);
            return View(site);
        }

        // POST: Sites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ClientId")] Site site)
        {
            if (id != site.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repostory.SaveAsync(site);
                    //_repostory.DbContext().Update(site);
                    //await _repostory.DbContext().SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteExists(site.Id))
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
            ViewData["ClientId"] = new SelectList(_repostory.DbContext().Clients, "Id", "Id", site.ClientId);
            return View(site);
        }

        // GET: Sites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var site = await _repostory.DbContext().Sites
                .Include(s => s.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (site == null)
            {
                return NotFound();
            }

            return View(site);
        }

        // POST: Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var site = await _repostory.DbContext().Sites.FindAsync(id);
            _repostory.DbContext().Sites.Remove(site);
            await _repostory.DbContext().SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteExists(int id)
        {
            return _repostory.DbContext().Sites.Any(e => e.Id == id);
        }
    }
}
