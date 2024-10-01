using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TemDeTudo.Data;
using TemDeTudo.Models;

namespace TemDeTudo.Controllers
{
    public class DepatmentsController : Controller
    {
        private readonly TemDeTudoContext _context;

        public DepatmentsController(TemDeTudoContext context)
        {
            _context = context;
        }

        // GET: Depatments
        public async Task<IActionResult> Index()
        {
              return _context.Depatment != null ? 
                          View(await _context.Depatment.ToListAsync()) :
                          Problem("Entity set 'TemDeTudoContext.Depatment'  is null.");
        }

        // GET: Depatments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Depatment == null)
            {
                return NotFound();
            }

            var depatment = await _context.Depatment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depatment == null)
            {
                return NotFound();
            }

            return View(depatment);
        }

        // GET: Depatments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Depatments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Depatment depatment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(depatment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(depatment);
        }

        // GET: Depatments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Depatment == null)
            {
                return NotFound();
            }

            var depatment = await _context.Depatment.FindAsync(id);
            if (depatment == null)
            {
                return NotFound();
            }
            return View(depatment);
        }

        // POST: Depatments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Depatment depatment)
        {
            if (id != depatment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depatment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepatmentExists(depatment.Id))
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
            return View(depatment);
        }

        // GET: Depatments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Depatment == null)
            {
                return NotFound();
            }

            var depatment = await _context.Depatment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depatment == null)
            {
                return NotFound();
            }

            return View(depatment);
        }

        // POST: Depatments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Depatment == null)
            {
                return Problem("Entity set 'TemDeTudoContext.Depatment'  is null.");
            }
            var depatment = await _context.Depatment.FindAsync(id);
            if (depatment != null)
            {
                _context.Depatment.Remove(depatment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepatmentExists(int id)
        {
          return (_context.Depatment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
