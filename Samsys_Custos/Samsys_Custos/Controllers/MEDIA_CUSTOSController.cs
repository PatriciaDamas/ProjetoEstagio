using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Samsys_Custos.Data;
using Samsys_Custos.Models;

namespace Samsys_Custos.Controllers
{
    public class MEDIA_CUSTOSController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MEDIA_CUSTOSController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MEDIA_CUSTOS
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MEDIA_CUSTOS.Include(m => m.EQUIPA);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MEDIA_CUSTOS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mEDIA_CUSTOS = await _context.MEDIA_CUSTOS
                .Include(m => m.EQUIPA)
                .FirstOrDefaultAsync(m => m.id_media == id);
            if (mEDIA_CUSTOS == null)
            {
                return NotFound();
            }

            return View(mEDIA_CUSTOS);
        }

        // GET: MEDIA_CUSTOS/Create
        public IActionResult Create()
        {
            ViewData["id_equipa"] = new SelectList(_context.EQUIPA, "id_equipa", "id_equipa");
            return View();
        }

        // POST: MEDIA_CUSTOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_media,id_equipa,nome,qtd_colaboradores,Total,mes,ano")] MEDIA_CUSTOS mEDIA_CUSTOS)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mEDIA_CUSTOS);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_equipa"] = new SelectList(_context.EQUIPA, "id_equipa", "id_equipa", mEDIA_CUSTOS.id_equipa);
            return View(mEDIA_CUSTOS);
        }

        // GET: MEDIA_CUSTOS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mEDIA_CUSTOS = await _context.MEDIA_CUSTOS.FindAsync(id);
            if (mEDIA_CUSTOS == null)
            {
                return NotFound();
            }
            ViewData["id_equipa"] = new SelectList(_context.EQUIPA, "id_equipa", "id_equipa", mEDIA_CUSTOS.id_equipa);
            return View(mEDIA_CUSTOS);
        }

        // POST: MEDIA_CUSTOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_media,id_equipa,nome,qtd_colaboradores,Total,mes,ano")] MEDIA_CUSTOS mEDIA_CUSTOS)
        {
            if (id != mEDIA_CUSTOS.id_media)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mEDIA_CUSTOS);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MEDIA_CUSTOSExists(mEDIA_CUSTOS.id_media))
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
            ViewData["id_equipa"] = new SelectList(_context.EQUIPA, "id_equipa", "id_equipa", mEDIA_CUSTOS.id_equipa);
            return View(mEDIA_CUSTOS);
        }

        // GET: MEDIA_CUSTOS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mEDIA_CUSTOS = await _context.MEDIA_CUSTOS
                .Include(m => m.EQUIPA)
                .FirstOrDefaultAsync(m => m.id_media == id);
            if (mEDIA_CUSTOS == null)
            {
                return NotFound();
            }

            return View(mEDIA_CUSTOS);
        }

        // POST: MEDIA_CUSTOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mEDIA_CUSTOS = await _context.MEDIA_CUSTOS.FindAsync(id);
            _context.MEDIA_CUSTOS.Remove(mEDIA_CUSTOS);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MEDIA_CUSTOSExists(int id)
        {
            return _context.MEDIA_CUSTOS.Any(e => e.id_media == id);
        }
    }
}
