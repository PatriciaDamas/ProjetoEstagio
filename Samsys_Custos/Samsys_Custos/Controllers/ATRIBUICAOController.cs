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
    public class ATRIBUICAOController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ATRIBUICAOController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ATRIBUICAO
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ATRIBUICAO.Include(a => a.GSM).Include(a => a.UTILIZADOR).Include(a => a.VIATURA);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ATRIBUICAO/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aTRIBUICAO = await _context.ATRIBUICAO
                .Include(a => a.GSM)
                .Include(a => a.UTILIZADOR)
                .Include(a => a.VIATURA)
                .SingleOrDefaultAsync(m => m.id_atribuicao == id);
            if (aTRIBUICAO == null)
            {
                return NotFound();
            }

            return View(aTRIBUICAO);
        }

        // GET: ATRIBUICAO/Create
        public IActionResult Create()
        {
            List<SelectListItem> tipo = new List<SelectListItem>();
            tipo.Add(new SelectListItem() { Text = "Gsm", Value = "1" });
            tipo.Add(new SelectListItem() { Text = "Viatura", Value = "2" });

            ViewData["teste"] = new SelectList(tipo, "Text", "Value");

            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "id_gsm");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "id_colaborador");
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "id_viatura");
            return View();
        }



        // POST: ATRIBUICAO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_atribuicao,id_viatura,id_gsm,id_colaborador,data_inicio,data_fim")] ATRIBUICAO aTRIBUICAO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aTRIBUICAO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "id_gsm", aTRIBUICAO.id_gsm);
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "id_colaborador", aTRIBUICAO.id_colaborador);
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "id_viatura", aTRIBUICAO.id_viatura);
            return View(aTRIBUICAO);
        }

        // GET: ATRIBUICAO/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aTRIBUICAO = await _context.ATRIBUICAO.SingleOrDefaultAsync(m => m.id_atribuicao == id);
            if (aTRIBUICAO == null)
            {
                return NotFound();
            }
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "id_gsm", aTRIBUICAO.id_gsm);
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "id_colaborador", aTRIBUICAO.id_colaborador);
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "id_viatura", aTRIBUICAO.id_viatura);
            return View(aTRIBUICAO);
        }

        // POST: ATRIBUICAO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_atribuicao,id_viatura,id_gsm,id_colaborador,data_inicio,data_fim")] ATRIBUICAO aTRIBUICAO)
        {
            if (id != aTRIBUICAO.id_atribuicao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aTRIBUICAO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ATRIBUICAOExists(aTRIBUICAO.id_atribuicao))
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
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "id_gsm", aTRIBUICAO.id_gsm);
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "id_colaborador", aTRIBUICAO.id_colaborador);
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "id_viatura", aTRIBUICAO.id_viatura);
            return View(aTRIBUICAO);
        }

        // GET: ATRIBUICAO/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aTRIBUICAO = await _context.ATRIBUICAO
                .Include(a => a.GSM)
                .Include(a => a.UTILIZADOR)
                .Include(a => a.VIATURA)
                .SingleOrDefaultAsync(m => m.id_atribuicao == id);
            if (aTRIBUICAO == null)
            {
                return NotFound();
            }

            return View(aTRIBUICAO);
        }

        // POST: ATRIBUICAO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aTRIBUICAO = await _context.ATRIBUICAO.SingleOrDefaultAsync(m => m.id_atribuicao == id);
            _context.ATRIBUICAO.Remove(aTRIBUICAO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ATRIBUICAOExists(int id)
        {
            return _context.ATRIBUICAO.Any(e => e.id_atribuicao == id);
        }
    }
}
