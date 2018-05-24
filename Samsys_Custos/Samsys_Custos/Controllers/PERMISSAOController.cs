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
    public class PERMISSAOController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PERMISSAOController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PERMISSAO
        public async Task<IActionResult> Index()
        {
            return View(await _context.PERMISSAO.ToListAsync());
        }

        // GET: PERMISSAO/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pERMISSAO = await _context.PERMISSAO
                .SingleOrDefaultAsync(m => m.id_permissao == id);
            if (pERMISSAO == null)
            {
                return NotFound();
            }

            return View(pERMISSAO);
        }

        // GET: PERMISSAO/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PERMISSAO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_permissao,nome")] PERMISSAO pERMISSAO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pERMISSAO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pERMISSAO);
        }

        // GET: PERMISSAO/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pERMISSAO = await _context.PERMISSAO.SingleOrDefaultAsync(m => m.id_permissao == id);
            if (pERMISSAO == null)
            {
                return NotFound();
            }
            return View(pERMISSAO);
        }

        // POST: PERMISSAO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_permissao,nome")] PERMISSAO pERMISSAO)
        {
            if (id != pERMISSAO.id_permissao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pERMISSAO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PERMISSAOExists(pERMISSAO.id_permissao))
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
            return View(pERMISSAO);
        }

        // GET: PERMISSAO/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pERMISSAO = await _context.PERMISSAO
                .SingleOrDefaultAsync(m => m.id_permissao == id);
            if (pERMISSAO == null)
            {
                return NotFound();
            }

            return View(pERMISSAO);
        }

        // POST: PERMISSAO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pERMISSAO = await _context.PERMISSAO.SingleOrDefaultAsync(m => m.id_permissao == id);
            _context.PERMISSAO.Remove(pERMISSAO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PERMISSAOExists(int id)
        {
            return _context.PERMISSAO.Any(e => e.id_permissao == id);
        }
    }
}
