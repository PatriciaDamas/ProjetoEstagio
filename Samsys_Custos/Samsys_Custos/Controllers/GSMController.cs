using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Samsys_Custos.Data;
using Samsys_Custos.Helpers;

namespace Samsys_Custos.Controllers
{
    public class GSMController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GSMController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GSM
        public async Task<IActionResult> Index(int? page)
        {
            var pageSize = 10;
            var applicationDbContext = _context.GSM;
            var count = applicationDbContext.Count();
            var gsm = await applicationDbContext.Skip(((page ?? 1) - 1) * pageSize).Take(pageSize).ToListAsync();
            return View(new PaginatedList<GSM>(gsm, count, page ?? 1, pageSize));

        }

        // GET: GSM/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gSM = await _context.GSM
                .SingleOrDefaultAsync(m => m.id_gsm == id);
            if (gSM == null)
            {
                return NotFound();
            }

            return View(gSM);
        }

        // GET: GSM/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GSM/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_gsm,numero,equipamento")] GSM gSM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gSM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gSM);
        }

        // GET: GSM/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gSM = await _context.GSM.SingleOrDefaultAsync(m => m.id_gsm == id);
            if (gSM == null)
            {
                return NotFound();
            }
            return View(gSM);
        }

        // POST: GSM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_gsm,numero,equipamento")] GSM gSM)
        {
            if (id != gSM.id_gsm)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gSM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GSMExists(gSM.id_gsm))
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
            return View(gSM);
        }

        // GET: GSM/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gSM = await _context.GSM
                .SingleOrDefaultAsync(m => m.id_gsm == id);
            if (gSM == null)
            {
                return NotFound();
            }

            return View(gSM);
        }

        // POST: GSM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gSM = await _context.GSM.SingleOrDefaultAsync(m => m.id_gsm == id);
            _context.GSM.Remove(gSM);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GSMExists(int id)
        {
            return _context.GSM.Any(e => e.id_gsm == id);
        }
    }
}
