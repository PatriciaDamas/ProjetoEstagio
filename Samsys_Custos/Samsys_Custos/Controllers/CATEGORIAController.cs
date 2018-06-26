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
    public class CATEGORIAController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CATEGORIAController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CATEGORIA
        public async Task<IActionResult> Index(int? page)
        {
            var pageSize = 10;
            var applicationDbContext = _context.CATEGORIA.OrderBy(a=>a.id_pai);
            var count = applicationDbContext.Count();
            var categorias = await applicationDbContext.Skip(((page ?? 1) - 1) * pageSize).Take(pageSize).ToListAsync();
            return View(new PaginatedList<CATEGORIA>(categorias, count, page ?? 1, pageSize));
        }

        // GET: CATEGORIA/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cATEGORIA = await _context.CATEGORIA
                .SingleOrDefaultAsync(m => m.id_categoria == id);
            if (cATEGORIA == null)
            {
                return NotFound();
            }

            return View(cATEGORIA);
        }

        // GET: CATEGORIA/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CATEGORIA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_categoria,id_pai,nome")] CATEGORIA cATEGORIA)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cATEGORIA);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cATEGORIA);
        }

        // GET: CATEGORIA/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cATEGORIA = await _context.CATEGORIA.SingleOrDefaultAsync(m => m.id_categoria == id);
            if (cATEGORIA == null)
            {
                return NotFound();
            }
            return View(cATEGORIA);
        }

        // POST: CATEGORIA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_categoria,id_pai,nome")] CATEGORIA cATEGORIA)
        {
            if (id != cATEGORIA.id_categoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cATEGORIA);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CATEGORIAExists(cATEGORIA.id_categoria))
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
            return View(cATEGORIA);
        }

        // GET: CATEGORIA/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cATEGORIA = await _context.CATEGORIA
                .SingleOrDefaultAsync(m => m.id_categoria == id);
            if (cATEGORIA == null)
            {
                return NotFound();
            }

            return View(cATEGORIA);
        }

        // POST: CATEGORIA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cATEGORIA = await _context.CATEGORIA.SingleOrDefaultAsync(m => m.id_categoria == id);
            _context.CATEGORIA.Remove(cATEGORIA);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CATEGORIAExists(int id)
        {
            return _context.CATEGORIA.Any(e => e.id_categoria == id);
        }
    }
}
