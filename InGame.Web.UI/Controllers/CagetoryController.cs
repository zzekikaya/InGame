using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InGame.Core.Entities;
using InGame.Infrastructure.Data;

namespace InGame.Web.UI.Controllers
{
    public class CagetoryController : Controller
    {
        private readonly InGameContext _context;

        public CagetoryController(InGameContext context)
        {
            _context = context;
        }

        // GET: Cagetories
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Cagetories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cagetory = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cagetory == null)
            {
                return NotFound();
            }

            return View(cagetory);
        }

        // GET: Cagetories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cagetories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CagetoryId,CagetoryName,Uri,PictureUri,Description,Id")] Category cagetory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cagetory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cagetory);
        }

        // GET: Cagetories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cagetory = await _context.Categories.FindAsync(id);
            if (cagetory == null)
            {
                return NotFound();
            }
            return View(cagetory);
        }

        // POST: Cagetories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CagetoryId,CagetoryName,Uri,PictureUri,Description,Id")] Category cagetory)
        {
            if (id != cagetory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cagetory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CagetoryExists(cagetory.Id))
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
            return View(cagetory);
        }

        // GET: Cagetories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cagetory = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cagetory == null)
            {
                return NotFound();
            }

            return View(cagetory);
        }

        // POST: Cagetories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cagetory = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(cagetory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CagetoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
