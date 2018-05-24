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
    public class CUSTOController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CUSTOController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CUSTO
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CUSTO.Include(c => c.CATEGORIA).Include(c => c.DADOS_PHC).Include(c => c.GSM).Include(c => c.SALARIO).Include(c => c.UTILIZADOR).Include(c => c.VIATURA);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CUSTO/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cUSTO = await _context.CUSTO
                .Include(c => c.CATEGORIA)
                .Include(c => c.DADOS_PHC)
                .Include(c => c.GSM)
                .Include(c => c.SALARIO)
                .Include(c => c.UTILIZADOR)
                .Include(c => c.VIATURA)
                .SingleOrDefaultAsync(m => m.id_custo == id);
            if (cUSTO == null)
            {
                return NotFound();
            }

            return View(cUSTO);
        }

        // GET: CUSTO/Create
        public IActionResult Create()
        {
            ViewData["id_categoria"] = new SelectList(_context.CATEGORIA, "id_categoria", "id_categoria");
            ViewData["id_phc"] = new SelectList(_context.DADOS_PHC, "id_phc", "id_phc");
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "id_gsm");
            ViewData["id_salario"] = new SelectList(_context.SALARIO, "id_salario", "id_salario");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "id_colaborador");
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "id_viatura");
            return View();
        }

        // POST: CUSTO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_custo,id_colaborador,id_categoria,id_gsm,id_phc,id_viatura,id_salario,data,ano,mes,designacao,valor")] CUSTO cUSTO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cUSTO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_categoria"] = new SelectList(_context.CATEGORIA, "id_categoria", "id_categoria", cUSTO.id_categoria);
            ViewData["id_phc"] = new SelectList(_context.DADOS_PHC, "id_phc", "id_phc", cUSTO.id_phc);
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "id_gsm", cUSTO.id_gsm);
            ViewData["id_salario"] = new SelectList(_context.SALARIO, "id_salario", "id_salario", cUSTO.id_salario);
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "id_colaborador", cUSTO.id_colaborador);
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "id_viatura", cUSTO.id_viatura);
            return View(cUSTO);
        }

        // GET: CUSTO/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cUSTO = await _context.CUSTO.SingleOrDefaultAsync(m => m.id_custo == id);
            if (cUSTO == null)
            {
                return NotFound();
            }
            ViewData["id_categoria"] = new SelectList(_context.CATEGORIA, "id_categoria", "id_categoria", cUSTO.id_categoria);
            ViewData["id_phc"] = new SelectList(_context.DADOS_PHC, "id_phc", "id_phc", cUSTO.id_phc);
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "id_gsm", cUSTO.id_gsm);
            ViewData["id_salario"] = new SelectList(_context.SALARIO, "id_salario", "id_salario", cUSTO.id_salario);
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "id_colaborador", cUSTO.id_colaborador);
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "id_viatura", cUSTO.id_viatura);
            return View(cUSTO);
        }

        // POST: CUSTO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_custo,id_colaborador,id_categoria,id_gsm,id_phc,id_viatura,id_salario,data,ano,mes,designacao,valor")] CUSTO cUSTO)
        {
            if (id != cUSTO.id_custo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cUSTO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CUSTOExists(cUSTO.id_custo))
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
            ViewData["id_categoria"] = new SelectList(_context.CATEGORIA, "id_categoria", "id_categoria", cUSTO.id_categoria);
            ViewData["id_phc"] = new SelectList(_context.DADOS_PHC, "id_phc", "id_phc", cUSTO.id_phc);
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "id_gsm", cUSTO.id_gsm);
            ViewData["id_salario"] = new SelectList(_context.SALARIO, "id_salario", "id_salario", cUSTO.id_salario);
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "id_colaborador", cUSTO.id_colaborador);
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "id_viatura", cUSTO.id_viatura);
            return View(cUSTO);
        }

        // GET: CUSTO/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cUSTO = await _context.CUSTO
                .Include(c => c.CATEGORIA)
                .Include(c => c.DADOS_PHC)
                .Include(c => c.GSM)
                .Include(c => c.SALARIO)
                .Include(c => c.UTILIZADOR)
                .Include(c => c.VIATURA)
                .SingleOrDefaultAsync(m => m.id_custo == id);
            if (cUSTO == null)
            {
                return NotFound();
            }

            return View(cUSTO);
        }

        // POST: CUSTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cUSTO = await _context.CUSTO.SingleOrDefaultAsync(m => m.id_custo == id);
            _context.CUSTO.Remove(cUSTO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CUSTOExists(int id)
        {
            return _context.CUSTO.Any(e => e.id_custo == id);
        }
    }
}
