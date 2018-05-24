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
    public class SALARIOController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SALARIOController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SALARIO
        public async Task<IActionResult> Index()
        {
            return View(await _context.SALARIO.ToListAsync());
        }

        // GET: SALARIO/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sALARIO = await _context.SALARIO
                .SingleOrDefaultAsync(m => m.id_salario == id);
            if (sALARIO == null)
            {
                return NotFound();
            }

            return View(sALARIO);
        }

        // GET: SALARIO/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SALARIO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_salario,liquido,subsidio_alimentacao,outros,seguranca_social,irs,outras_despesas,outras_regalias")] SALARIO sALARIO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sALARIO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sALARIO);
        }

        // GET: SALARIO/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sALARIO = await _context.SALARIO.SingleOrDefaultAsync(m => m.id_salario == id);
            if (sALARIO == null)
            {
                return NotFound();
            }
            return View(sALARIO);
        }

        // POST: SALARIO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_salario,liquido,subsidio_alimentacao,outros,seguranca_social,irs,outras_despesas,outras_regalias")] SALARIO sALARIO)
        {
            if (id != sALARIO.id_salario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sALARIO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SALARIOExists(sALARIO.id_salario))
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
            return View(sALARIO);
        }

        // GET: SALARIO/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sALARIO = await _context.SALARIO
                .SingleOrDefaultAsync(m => m.id_salario == id);
            if (sALARIO == null)
            {
                return NotFound();
            }

            return View(sALARIO);
        }

        // POST: SALARIO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sALARIO = await _context.SALARIO.SingleOrDefaultAsync(m => m.id_salario == id);
            _context.SALARIO.Remove(sALARIO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SALARIOExists(int id)
        {
            return _context.SALARIO.Any(e => e.id_salario == id);
        }
    }
}
