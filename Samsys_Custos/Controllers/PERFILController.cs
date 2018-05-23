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
    public class PERFILController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PERFILController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PERFIL
        public async Task<IActionResult> Index()
        {
            return View(await _context.PERFIL.ToListAsync());
        }

        // GET: PERFIL/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pERFIL = await _context.PERFIL
                .SingleOrDefaultAsync(m => m.id_perfil == id);
            if (pERFIL == null)
            {
                return NotFound();
            }

            return View(pERFIL);
        }

        // GET: PERFIL/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PERFIL/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_perfil,nome,salario,premio")] PERFIL pERFIL)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pERFIL);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pERFIL);
        }

        // GET: PERFIL/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pERFIL = await _context.PERFIL.SingleOrDefaultAsync(m => m.id_perfil == id);
            if (pERFIL == null)
            {
                return NotFound();
            }
            return View(pERFIL);
        }

        // POST: PERFIL/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_perfil,nome,salario,premio")] PERFIL pERFIL)
        {
            if (id != pERFIL.id_perfil)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pERFIL);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PERFILExists(pERFIL.id_perfil))
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
            return View(pERFIL);
        }

        // GET: PERFIL/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pERFIL = await _context.PERFIL
                .SingleOrDefaultAsync(m => m.id_perfil == id);
            if (pERFIL == null)
            {
                return NotFound();
            }

            return View(pERFIL);
        }

        // POST: PERFIL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pERFIL = await _context.PERFIL.SingleOrDefaultAsync(m => m.id_perfil == id);
            _context.PERFIL.Remove(pERFIL);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PERFILExists(int id)
        {
            return _context.PERFIL.Any(e => e.id_perfil == id);
        }
    }
}
