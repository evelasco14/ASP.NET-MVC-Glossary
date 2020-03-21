using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GlossaryData;

namespace GlossaryWeb.Controllers
{
    public class GlossaryTermsController : Controller
    {
        private readonly GlossaryContext _context;

        public GlossaryTermsController(GlossaryContext context)
        {
            _context = context;
        }

        // GET: GlossaryTerms
        public async Task<IActionResult> Index()
        {
            return View(await _context.GlossaryTerms.OrderBy(g => g.Term).ToListAsync());
        }

        // GET: GlossaryTerms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glossaryTerm = await _context.GlossaryTerms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (glossaryTerm == null)
            {
                return NotFound();
            }

            return View(glossaryTerm);
        }

        // GET: GlossaryTerms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GlossaryTerms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Term,Definition,Created,Updated")] GlossaryTerm glossaryTerm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(glossaryTerm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(glossaryTerm);
        }

        // GET: GlossaryTerms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glossaryTerm = await _context.GlossaryTerms.FindAsync(id);
            if (glossaryTerm == null)
            {
                return NotFound();
            }
            return View(glossaryTerm);
        }

        // POST: GlossaryTerms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Term,Definition,Created,Updated")] GlossaryTerm glossaryTerm)
        {
            if (id != glossaryTerm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(glossaryTerm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GlossaryTermExists(glossaryTerm.Id))
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
            return View(glossaryTerm);
        }

        // GET: GlossaryTerms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glossaryTerm = await _context.GlossaryTerms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (glossaryTerm == null)
            {
                return NotFound();
            }

            return View(glossaryTerm);
        }

        // POST: GlossaryTerms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var glossaryTerm = await _context.GlossaryTerms.FindAsync(id);
            _context.GlossaryTerms.Remove(glossaryTerm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GlossaryTermExists(int id)
        {
            return _context.GlossaryTerms.Any(e => e.Id == id);
        }
    }
}
