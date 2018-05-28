using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        // GET: Custo Viaturas
        public async Task<IActionResult> Viatura()
        {
            var applicationDbContext = _context.CUSTO.Include(c => c.CATEGORIA).Include(c => c.DADOS_PHC).Include(c => c.GSM).Include(c => c.SALARIO).Include(c => c.UTILIZADOR).Include(c => c.VIATURA).Where(c=> c.id_viatura != null);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CUSTO
        public async Task<IActionResult> Gsm()
        {
            var applicationDbContext = _context.CUSTO.Include(c => c.CATEGORIA).Include(c => c.GSM).Include(c => c.UTILIZADOR).Where(c => c.id_gsm != null);
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

        //GET: 
        public IActionResult CriarGsm()
        {
            var temp2 = _context.CATEGORIA.Where(a => a.nome == "Moveis").FirstOrDefault();
            var gsm2 = _context.CATEGORIA.Where(a => a.nome == "Comunicações").FirstOrDefault();
            Debug.WriteLine("-------------" + temp2.nome);
            ViewData["id_categoria"] = new SelectList(_context.CATEGORIA.Where(a => a.id_pai == temp2.id_categoria || a.id_pai == gsm2.id_categoria && a.nome != "Moveis"), "id_categoria", "nome");
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "numero");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");

            return View();
        }

        // GET: CUSTO/viatura
        public IActionResult CriarViatura()
        {
            var viatura = _context.CATEGORIA.Where(a => a.nome == "Viaturas").FirstOrDefault();
            ViewData["id_categoria"] = new SelectList(_context.CATEGORIA.Where(a=> a.id_pai == viatura.id_categoria), "id_categoria", "nome");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "matricula");
            return View();
        }

        // GET: CUSTO/Create
        public IActionResult Create()
        {
            ViewData["id_categoria"] = new SelectList(_context.CATEGORIA, "id_categoria", "nome");
            ViewData["id_phc"] = new SelectList(_context.DADOS_PHC, "id_phc", "id_phc");
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "id_gsm");
            ViewData["id_salario"] = new SelectList(_context.SALARIO, "id_salario", "id_salario");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "id_colaborador");
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "matricula");
            return View();
        }

        // POST: CUSTO/viatura
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // "id_custo,id_colaborador,id_categoria,id_gsm,id_phc,id_viatura,id_salario,data,ano,mes,designacao,valor")
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardarViatura([Bind("id_colaborador,id_categoria,id_viatura,ano,mes,designacao,valor")] CUSTO cUSTO)
        {
            if (ModelState.IsValid)
            {
                if (cUSTO.designacao == null)// temos de esperimentar se vier com valor null, se implementa na mesma na BD, se der tira-se este if
                {
                    cUSTO.designacao = "";
                }

                cUSTO.data = DateTime.Now;
                _context.Add(cUSTO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Viatura));
            }
            var viatura = _context.CATEGORIA.Where(a => a.nome == "Viaturas").FirstOrDefault();
            ViewData["id_categoria"] = new SelectList(_context.CATEGORIA.Where(a => a.id_pai == viatura.id_categoria), "id_categoria", "nome");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "matricula");
            return View(cUSTO);
        }


        // POST: CUSTO/gsm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarGsm([Bind("id_colaborador,id_categoria,id_gsm,ano,mes,designacao,valor")] CUSTO cUSTO)
        {
            if (ModelState.IsValid)
            {
                if (cUSTO.designacao == null)// temos de experimentar se vier com valor null, se implementa na mesma na BD, se der tira-se este if
                {
                    cUSTO.designacao = "";
                }

                cUSTO.data = DateTime.Now;
                _context.Add(cUSTO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Gsm));
            }
            var temp2 = _context.CATEGORIA.Where(a => a.nome == "Moveis").FirstOrDefault();
            var gsm2 = _context.CATEGORIA.Where(a => a.nome == "Comunicações").FirstOrDefault();
            ViewData["id_categoria"] = new SelectList(_context.CATEGORIA.Where(a => a.id_pai == temp2.id_categoria || a.id_pai == gsm2.id_categoria && a.nome != "Moveis"), "id_categoria", "nome");
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "numero");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            return View(cUSTO);
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
                cUSTO.data = DateTime.Now;
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
