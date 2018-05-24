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
    public class UTILIZADORController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UTILIZADORController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UTILIZADOR
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UTILIZADOR.Include(u => u.EQUIPA).Include(u => u.PERFIL);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UTILIZADOR/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uTILIZADOR = await _context.UTILIZADOR
                .Include(u => u.EQUIPA)
                .Include(u => u.PERFIL)
                .SingleOrDefaultAsync(m => m.id_colaborador == id);
            if (uTILIZADOR == null)
            {
                return NotFound();
            }

            return View(uTILIZADOR);
        }

        // GET: UTILIZADOR/Create
        public IActionResult Create()
        {
            ViewData["id_equipa"] = new SelectList(_context.Set<EQUIPA>(), "id_equipa", "id_equipa");
            ViewData["id_perfil"] = new SelectList(_context.PERFIL, "id_perfil", "id_perfil");
            return View();
        }

        // POST: UTILIZADOR/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_colaborador,nome,id_perfil,id_equipa,data_admissao")] UTILIZADOR uTILIZADOR)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uTILIZADOR);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_equipa"] = new SelectList(_context.Set<EQUIPA>(), "id_equipa", "id_equipa", uTILIZADOR.id_equipa);
            ViewData["id_perfil"] = new SelectList(_context.PERFIL, "id_perfil", "id_perfil", uTILIZADOR.id_perfil);
            return View(uTILIZADOR);
        }

        // GET: UTILIZADOR/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uTILIZADOR = await _context.UTILIZADOR.SingleOrDefaultAsync(m => m.id_colaborador == id);
            if (uTILIZADOR == null)
            {
                return NotFound();
            }
            ViewData["id_equipa"] = new SelectList(_context.Set<EQUIPA>(), "id_equipa", "id_equipa", uTILIZADOR.id_equipa);
            ViewData["id_perfil"] = new SelectList(_context.PERFIL, "id_perfil", "id_perfil", uTILIZADOR.id_perfil);
            return View(uTILIZADOR);
        }

        // POST: UTILIZADOR/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_colaborador,nome,id_perfil,id_equipa,data_admissao")] UTILIZADOR uTILIZADOR)
        {
            if (id != uTILIZADOR.id_colaborador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uTILIZADOR);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UTILIZADORExists(uTILIZADOR.id_colaborador))
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
            ViewData["id_equipa"] = new SelectList(_context.Set<EQUIPA>(), "id_equipa", "id_equipa", uTILIZADOR.id_equipa);
            ViewData["id_perfil"] = new SelectList(_context.PERFIL, "id_perfil", "id_perfil", uTILIZADOR.id_perfil);
            return View(uTILIZADOR);
        }

        // GET: UTILIZADOR/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uTILIZADOR = await _context.UTILIZADOR
                .Include(u => u.EQUIPA)
                .Include(u => u.PERFIL)
                .SingleOrDefaultAsync(m => m.id_colaborador == id);
            if (uTILIZADOR == null)
            {
                return NotFound();
            }

            return View(uTILIZADOR);
        }

        // POST: UTILIZADOR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uTILIZADOR = await _context.UTILIZADOR.SingleOrDefaultAsync(m => m.id_colaborador == id);
            _context.UTILIZADOR.Remove(uTILIZADOR);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UTILIZADORExists(int id)
        {
            return _context.UTILIZADOR.Any(e => e.id_colaborador == id);
        }
    }
}
