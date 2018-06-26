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
    public class FORNECEDORController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FORNECEDORController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FORNECEDOR
        public async Task<IActionResult> Index(int? page)
        {
            var pageSize = 10;
            var applicationDbContext = _context.FORNECEDOR.Include(f => f.CATEGORIA);
            var count = applicationDbContext.Count();
            var fornecedores = await applicationDbContext.Skip(((page ?? 1) - 1) * pageSize).Take(pageSize).ToListAsync();
            return View(new PaginatedList<FORNECEDOR>(fornecedores, count, page ?? 1, pageSize));
        }

        // GET: FORNECEDOR/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fORNECEDOR = await _context.FORNECEDOR
                .Include(f => f.CATEGORIA)
                .FirstOrDefaultAsync(m => m.id_fornecedor == id);
            if (fORNECEDOR == null)
            {
                return NotFound();
            }

            return View(fORNECEDOR);
        }

        // GET: FORNECEDOR/Create
        public IActionResult Create()
        {
            ViewData["sugestao_categoria"] = new SelectList(_context.CATEGORIA, "id_categoria", "id_categoria");
            return View();
        }

        // POST: FORNECEDOR/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_fornecedor,sugestao_categoria,sugestao_custo,nome")] FORNECEDOR fORNECEDOR)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fORNECEDOR);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["sugestao_categoria"] = new SelectList(_context.CATEGORIA, "id_categoria", "id_categoria", fORNECEDOR.sugestao_categoria);
            return View(fORNECEDOR);
        }

        // GET: FORNECEDOR/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fORNECEDOR = await _context.FORNECEDOR.FindAsync(id);
            if (fORNECEDOR == null)
            {
                return NotFound();
            }
            ViewData["sugestao_categoria"] = new SelectList(_context.CATEGORIA.Where(a=>a.id_pai != null), "id_categoria", "nome", fORNECEDOR.sugestao_categoria);
           
            return View(fORNECEDOR);
        }

        // POST: FORNECEDOR/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id_fornecedor,sugestao_categoria,sugestao_custo,nome")] FORNECEDOR fORNECEDOR)
        {
            if (id != fORNECEDOR.id_fornecedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fORNECEDOR);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FORNECEDORExists(fORNECEDOR.id_fornecedor))
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
            ViewData["sugestao_categoria"] = new SelectList(_context.CATEGORIA, "id_categoria", "id_categoria", fORNECEDOR.sugestao_categoria);
            
            return View(fORNECEDOR);
        }

        // GET: FORNECEDOR/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fORNECEDOR = await _context.FORNECEDOR
                .Include(f => f.CATEGORIA)
                .FirstOrDefaultAsync(m => m.id_fornecedor == id);
            if (fORNECEDOR == null)
            {
                return NotFound();
            }

            return View(fORNECEDOR);
        }

        // POST: FORNECEDOR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var fORNECEDOR = await _context.FORNECEDOR.FindAsync(id);
            _context.FORNECEDOR.Remove(fORNECEDOR);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FORNECEDORExists(string id)
        {
            return _context.FORNECEDOR.Any(e => e.id_fornecedor == id);
        }
    }
}
