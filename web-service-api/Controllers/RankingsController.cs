using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web_service_api;

namespace web_service_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RankingsController : Controller
    {
        private readonly WebServiceContext _context;

        public RankingsController(WebServiceContext context)
        {
            _context = context;
        }

        // GET: Rankings
        [HttpGet]
        public async Task<IActionResult> Index()
        {
              return _context.Ranking != null ? 
                          View(await _context.Ranking.ToListAsync()) :
                          Problem("Entity set 'WebServiceContext.Ranking'  is null.");
        }

        // GET: Rankings/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ranking == null)
            {
                return NotFound();
            }

            var ranking = await _context.Ranking
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ranking == null)
            {
                return NotFound();
            }

            return View(ranking);
        }

        // GET: Rankings/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rankings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create"),ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Created,Name,Text,Rank")] Ranking ranking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ranking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ranking);
        }

        // GET: Rankings/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ranking == null)
            {
                return NotFound();
            }

            var ranking = await _context.Ranking.FindAsync(id);
            if (ranking == null)
            {
                return NotFound();
            }
            return View(ranking);
        }

        // POST: Rankings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Created,Name,Text,Rank")] Ranking ranking)
        {
            if (id != ranking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ranking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RankingExists(ranking.Id))
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
            return View(ranking);
        }

        // GET: Rankings/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ranking == null)
            {
                return NotFound();
            }

            var ranking = await _context.Ranking
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ranking == null)
            {
                return NotFound();
            }

            return View(ranking);
        }

        // POST: Rankings/Delete/5
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ranking == null)
            {
                return Problem("Entity set 'WebServiceContext.Ranking'  is null.");
            }
            var ranking = await _context.Ranking.FindAsync(id);
            if (ranking != null)
            {
                _context.Ranking.Remove(ranking);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RankingExists(int id)
        {
          return (_context.Ranking?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
