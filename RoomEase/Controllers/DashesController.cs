using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomEase.Data;
using RoomEase.Models;

namespace RoomEase.Controllers
{
    public class DashesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]

        // GET: Dashes
        public async Task<IActionResult> Index()
        {
            return _context.Dash != null ?
                        View(await _context.Dash.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Dash'  is null.");
        }



        // GET: Dashes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dash == null)
            {
                return NotFound();
            }

            var dash = await _context.Dash
                .FirstOrDefaultAsync(m => m.id == id);
            if (dash == null)
            {
                return NotFound();
            }

            return View(dash);
        }



        // GET: Dashes/Create

        [Authorize]
        public IActionResult Create()
        {
            return View();

        }

        // GET: Dashes/ShowSearchForm

        public IActionResult ShowSearchForm()
        {
            return View();
        }



        // POST: Dashes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,description,Amount")] Dash dash)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dash);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dash);
        }

        // GET: Dashes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Dash == null)
            {
                return NotFound();
            }

            var dash = await _context.Dash.FindAsync(id);
            if (dash == null)
            {
                return NotFound();
            }
            return View(dash);
        }

        // POST: Dashes/Edit/5

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,description,Amount")] Dash dash)
        {
            if (id != dash.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dash);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DashExists(dash.id))
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
            return View(dash);
        }

        // GET: Dashes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dash == null)
            {
                return NotFound();
            }

            var dash = await _context.Dash
                .FirstOrDefaultAsync(m => m.id == id);
            if (dash == null)
            {
                return NotFound();
            }

            return View(dash);
        }

        // POST: Dashes/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dash == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Dash'  is null.");
            }
            var dash = await _context.Dash.FindAsync(id);
            if (dash != null)
            {
                _context.Dash.Remove(dash);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // POST: Dash/ShowSearchResults
        public async Task<IActionResult> ShowSearchresult(String SearchPhrase)
        {
            return View("Index", await _context.Dash.Where(j => j.name.Contains(SearchPhrase)).ToListAsync());
        }

        private bool DashExists(int id)
        {
            return (_context.Dash?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
