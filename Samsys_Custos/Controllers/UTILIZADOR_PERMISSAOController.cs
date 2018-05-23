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
    public class UTILIZADOR_PERMISSAOController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UTILIZADOR_PERMISSAOController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UTILIZADOR_PERMISSAO
        public async Task<IActionResult> Index()
        {
            return View(await _context.UTILIZADOR_PERMISSAO.ToListAsync());
        }

        // GET: UTILIZADOR_PERMISSAO/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uTILIZADOR_PERMISSAO = await _context.UTILIZADOR_PERMISSAO
                .SingleOrDefaultAsync(m => m.id_utilizador_permissao == id);
            if (uTILIZADOR_PERMISSAO == null)
            {
                return NotFound();
            }

            return View(uTILIZADOR_PERMISSAO);
        }

        // GET: UTILIZADOR_PERMISSAO/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UTILIZADOR_PERMISSAO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_utilizador_permissao,id_colaborador,id_permissao")] UTILIZADOR_PERMISSAO uTILIZADOR_PERMISSAO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uTILIZADOR_PERMISSAO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uTILIZADOR_PERMISSAO);
        }

        // GET: UTILIZADOR_PERMISSAO/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uTILIZADOR_PERMISSAO = await _context.UTILIZADOR_PERMISSAO.SingleOrDefaultAsync(m => m.id_utilizador_permissao == id);
            if (uTILIZADOR_PERMISSAO == null)
            {
                return NotFound();
            }
            return View(uTILIZADOR_PERMISSAO);
        }

        // POST: UTILIZADOR_PERMISSAO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_utilizador_permissao,id_colaborador,id_permissao")] UTILIZADOR_PERMISSAO uTILIZADOR_PERMISSAO)
        {
            if (id != uTILIZADOR_PERMISSAO.id_utilizador_permissao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uTILIZADOR_PERMISSAO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UTILIZADOR_PERMISSAOExists(uTILIZADOR_PERMISSAO.id_utilizador_permissao))
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
            return View(uTILIZADOR_PERMISSAO);
        }

        // GET: UTILIZADOR_PERMISSAO/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uTILIZADOR_PERMISSAO = await _context.UTILIZADOR_PERMISSAO
                .SingleOrDefaultAsync(m => m.id_utilizador_permissao == id);
            if (uTILIZADOR_PERMISSAO == null)
            {
                return NotFound();
            }

            return View(uTILIZADOR_PERMISSAO);
        }

        // POST: UTILIZADOR_PERMISSAO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uTILIZADOR_PERMISSAO = await _context.UTILIZADOR_PERMISSAO.SingleOrDefaultAsync(m => m.id_utilizador_permissao == id);
            _context.UTILIZADOR_PERMISSAO.Remove(uTILIZADOR_PERMISSAO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UTILIZADOR_PERMISSAOExists(int id)
        {
            return _context.UTILIZADOR_PERMISSAO.Any(e => e.id_utilizador_permissao == id);
        }
    }
}
