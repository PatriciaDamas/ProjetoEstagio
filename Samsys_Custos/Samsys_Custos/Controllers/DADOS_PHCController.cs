using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Samsys_Custos.Data;

namespace Samsys_Custos.Controllers
{
    public class DADOS_PHCController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DADOS_PHCController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DADOS_PHC
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DADOS_PHC.Include(d => d.FORNECEDOR);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DADOS_PHC/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dADOS_PHC = await _context.DADOS_PHC
                .Include(d => d.FORNECEDOR)
                .SingleOrDefaultAsync(m => m.id_phc == id);
            if (dADOS_PHC == null)
            {
                return NotFound();
            }

            return View(dADOS_PHC);
        }

        // GET: DADOS_PHC/Create
        public IActionResult Create()
        {
            ViewData["id_fornecedor"] = new SelectList(_context.FORNECEDOR, "id_fornecedor", "id_fornecedor");
            return View();
        }

        // POST: DADOS_PHC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_phc,id_fornecedor,custo_interno")] DADOS_PHC dADOS_PHC)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dADOS_PHC);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_fornecedor"] = new SelectList(_context.FORNECEDOR, "id_fornecedor", "id_fornecedor", dADOS_PHC.id_fornecedor);
            return View(dADOS_PHC);
        }

        // GET: DADOS_PHC/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dADOS_PHC = await _context.DADOS_PHC.SingleOrDefaultAsync(m => m.id_phc == id);
            if (dADOS_PHC == null)
            {
                return NotFound();
            }
            ViewData["id_fornecedor"] = new SelectList(_context.FORNECEDOR, "id_fornecedor", "id_fornecedor", dADOS_PHC.id_fornecedor);
            return View(dADOS_PHC);
        }

        // POST: DADOS_PHC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_phc,id_fornecedor,custo_interno")] DADOS_PHC dADOS_PHC)
        {
            if (id != dADOS_PHC.id_phc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dADOS_PHC);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DADOS_PHCExists(dADOS_PHC.id_phc))
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
            ViewData["id_fornecedor"] = new SelectList(_context.FORNECEDOR, "id_fornecedor", "id_fornecedor", dADOS_PHC.id_fornecedor);
            return View(dADOS_PHC);
        }

        // GET: DADOS_PHC/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dADOS_PHC = await _context.DADOS_PHC
                .Include(d => d.FORNECEDOR)
                .SingleOrDefaultAsync(m => m.id_phc == id);
            if (dADOS_PHC == null)
            {
                return NotFound();
            }

            return View(dADOS_PHC);
        }

        // POST: DADOS_PHC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dADOS_PHC = await _context.DADOS_PHC.SingleOrDefaultAsync(m => m.id_phc == id);
            _context.DADOS_PHC.Remove(dADOS_PHC);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DADOS_PHCExists(int id)
        {
            return _context.DADOS_PHC.Any(e => e.id_phc == id);
        }
    }
}
