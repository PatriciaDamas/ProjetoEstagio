using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Samsys_Custos.Data;
using Samsys_Custos.Helpers;
using Samsys_Custos.Models;

namespace Samsys_Custos.Controllers
{
    public class EQUIPAController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EQUIPAController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET Custo colaborador
        public IActionResult Colaborador()
        {

            var applicationDbContext = _context.COLABORADOR.Include(a=>a.EQUIPA).ToList();
            return View(applicationDbContext);
        }

        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10;
            var applicationDbContext = _context.EQUIPA;
            var count = applicationDbContext.Count();
            var equipas = await applicationDbContext.Skip(((page ?? 1) - 1) * pageSize).Take(pageSize).ToListAsync();
            return View(new PaginatedList<EQUIPA>(equipas, count, page ?? 1, pageSize));

        }

        // GET: EQUIPA/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eQUIPA = await _context.EQUIPA
                .Include(e => e.COLABORADOR)
                .SingleOrDefaultAsync(m => m.id_equipa == id);
            if (eQUIPA == null)
            {
                return NotFound();
            }

            return View(eQUIPA);
        }

        // GET: EQUIPA/Create
        public IActionResult Create()
        {
            ViewData["id_lider"] = new SelectList(_context.COLABORADOR, "id_colaborador", "nome");
            return View();
        }

        // POST: EQUIPA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_equipa,id_lider,nome")] EQUIPA eQUIPA)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eQUIPA);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Colaborador));
            }
            ViewData["id_lider"] = new SelectList(_context.COLABORADOR, "id_colaborador", "nome", eQUIPA.id_lider);
            return View(eQUIPA);
        }

        // GET: EQUIPA/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eQUIPA = await _context.EQUIPA.SingleOrDefaultAsync(m => m.id_equipa == id);
            if (eQUIPA == null)
            {
                return NotFound();
            }
            ViewData["id_lider"] = new SelectList(_context.COLABORADOR, "id_colaborador", "nome", eQUIPA.id_lider);
            return View(eQUIPA);
        }

        // POST: EQUIPA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id_equipa,id_lider,nome")] EQUIPA eQUIPA)
        {
            if (id != eQUIPA.id_equipa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eQUIPA);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EQUIPAExists(eQUIPA.id_equipa))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Colaborador));
            }
            ViewData["id_lider"] = new SelectList(_context.COLABORADOR, "id_colaborador", "nome", eQUIPA.id_lider);
            return View(eQUIPA);
        }

        // GET: EQUIPA/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eQUIPA = await _context.EQUIPA
                .Include(e => e.COLABORADOR)
                .SingleOrDefaultAsync(m => m.id_equipa == id);
            if (eQUIPA == null)
            {
                return NotFound();
            }

            return View(eQUIPA);
        }

        // POST: EQUIPA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var eQUIPA = await _context.EQUIPA.SingleOrDefaultAsync(m => m.id_equipa == id);
            _context.EQUIPA.Remove(eQUIPA);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Colaborador));
        }

        private bool EQUIPAExists(string id)
        {
            return _context.EQUIPA.Any(e => e.id_equipa == id);
        }
    }
}
