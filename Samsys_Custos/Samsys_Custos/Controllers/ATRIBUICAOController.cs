using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Samsys_Custos.Data;
using Samsys_Custos.Helpers;

namespace Samsys_Custos.Controllers
{
    public class ATRIBUICAOController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ATRIBUICAOController(ApplicationDbContext context)
        {
            _context = context;
        }

        /*// GET: ATRIBUICAO
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ATRIBUICAO.Include(a => a.GSM).Include(a => a.COLABORADOR).Include(a => a.VIATURA);
            return View(await applicationDbContext.ToListAsync());
        }*/

        //Obter json das atribuições de gsm e obter json das atribuições de gsm de um determinado colaborador
        public JsonResult AtribuicaoGsmJson(int? id)
        {
            if(id != null)
            {
                var applicationDbContext = _context.ATRIBUICAO.ToList().Where(a => a.id_gsm == id & a.data_fim == null);
                return Json(applicationDbContext);
            }
            else
            {
                var applicationDbContext = _context.ATRIBUICAO.ToList().Where(a => a.id_gsm != null);
                return Json(applicationDbContext);
            }
            
           
        }

        //Obter json das atribuições de viaturas e obter json das atribuições de uma viatura de um determinado colaborador
        public JsonResult AtribuicaoViaturaJson(int? id)
        {
            if (id != null)
            {
                var applicationDbContext = _context.ATRIBUICAO.ToList().Where(a => a.id_viatura == id & a.data_fim == null);
                return Json(applicationDbContext);
            }
            else
            {
                var applicationDbContext = _context.ATRIBUICAO.ToList().Where(a => a.id_viatura != null);
                return Json(applicationDbContext);
            }


        }
        // GET: ATRIBUICAO
        public async Task<IActionResult> GSM( int? page)
        {
            var pageSize = 10;
            var applicationDbContext = _context.ATRIBUICAO.Include(a => a.GSM).Include(a => a.COLABORADOR).Where(c => c.id_gsm != null);
            var count = applicationDbContext.Count();
            var users = await applicationDbContext.Skip(((page ?? 1) - 1) * pageSize).Take(pageSize).ToListAsync();
            return View(new PaginatedList<ATRIBUICAO>(users, count, page ?? 1, pageSize));
        }
        // GET: ATRIBUICAO
        public async Task<IActionResult> Viatura( int? page)
        {
            int pageSize = 10;
            var applicationDbContext = _context.ATRIBUICAO.Include(a => a.VIATURA).Include(a => a.COLABORADOR).Where(c => c.id_viatura != null);
            var count = applicationDbContext.Count();
            var users = await applicationDbContext.Skip(((page ?? 1) - 1) * pageSize).Take(pageSize).ToListAsync();
            return View(new PaginatedList<ATRIBUICAO>(users,count,page ?? 1, pageSize));
        }

       /* // GET: ATRIBUICAO/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aTRIBUICAO = await _context.ATRIBUICAO
                .Include(a => a.GSM)
                .Include(a => a.COLABORADOR)
                .Include(a => a.VIATURA)
                .SingleOrDefaultAsync(m => m.id_atribuicao == id);
            if (aTRIBUICAO == null)
            {
                return NotFound();
            }

            return View(aTRIBUICAO);
        }*/


        // GET: ATRIBUICAO/Create
        [Authorize(Roles = "Atribuições,Gestor,SuperAdmin")]
        public IActionResult CreateViatura()
        {
            ViewData["id_colaborador"] = new SelectList(_context.COLABORADOR, "id_colaborador", "nome");
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "matricula");
            return View();
        }

        // GET: ATRIBUICAO/Create
        [Authorize(Roles = "Atribuições,Gestor,SuperAdmin")]
        public IActionResult CreateGSM()
        {
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "numero");
            ViewData["id_colaborador"] = new SelectList(_context.COLABORADOR, "id_colaborador", "nome");
            return View();
        }


        // POST: ATRIBUICAO/Create_viatura
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Viaturas,Gestor,SuperAdmin")]
        public async Task<IActionResult> CreateViatura([Bind("id_atribuicao,id_viatura,id_colaborador,data_inicio,data_fim")] ATRIBUICAO aTRIBUICAO)
        {
            if (ModelState.IsValid)
            {
                var aTRIBUICAORESERVA = await _context.ATRIBUICAO.SingleOrDefaultAsync((c => c.data_fim == null && c.id_viatura == aTRIBUICAO.id_viatura));

                if (aTRIBUICAORESERVA != null)
                {
                    //copia de aTRIBUICAO para nao atrapalhar na criação da nova atribuição
                    aTRIBUICAORESERVA.data_fim = DateTime.Now;
                    // edição da atribuição antiga
                    await EditViatura(aTRIBUICAORESERVA.id_atribuicao, aTRIBUICAORESERVA);
                    // criar atribuição
                    _context.Add(aTRIBUICAO);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Viatura));
                }
                else
                {
                    _context.Add(aTRIBUICAO);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Viatura));
                }
            }

            ViewData["id_colaborador"] = new SelectList(_context.COLABORADOR, "id_colaborador", "nome", aTRIBUICAO.id_colaborador);
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "matricula", aTRIBUICAO.id_viatura);
            return View(aTRIBUICAO);
        }
        // POST: ATRIBUICAO/Create_GSM
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gsm,Gestor,SuperAdmin")]
        public async Task<IActionResult> CreateGSM([Bind("id_atribuicao,id_gsm,id_colaborador,data_inicio,data_fim")] ATRIBUICAO aTRIBUICAO)
        {
            if (ModelState.IsValid)
            {
                var aTRIBUICAORESERVA = await _context.ATRIBUICAO.SingleOrDefaultAsync((c => c.data_fim == null && c.id_gsm == aTRIBUICAO.id_gsm));

                if (aTRIBUICAORESERVA != null)
                {
                    //copia de aTRIBUICAO para nao atrapalhar na criação da nova atribuição
                    aTRIBUICAORESERVA.data_fim = DateTime.Now;
                    // edição da atribuição antiga
                    await EditGSM(aTRIBUICAORESERVA.id_atribuicao, aTRIBUICAORESERVA);
                    // criar atribuição
                    _context.Add(aTRIBUICAO);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(GSM));
                }
                else
                {
                    _context.Add(aTRIBUICAO);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(GSM));
                }
            }
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "numero", aTRIBUICAO.id_gsm);
            ViewData["id_colaborador"] = new SelectList(_context.COLABORADOR, "id_colaborador", "nome", aTRIBUICAO.id_colaborador);
            return View(aTRIBUICAO);
        }


        // GET: ATRIBUICAO/Edit/5
        [Authorize(Roles = "Gsm,Gestor,SuperAdmin")]
        public async Task<IActionResult> EditGSM(int? id)
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
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "numero", aTRIBUICAO.id_gsm);
            ViewData["id_colaborador"] = new SelectList(_context.COLABORADOR, "id_colaborador", "nome", aTRIBUICAO.id_colaborador);
            return View(aTRIBUICAO);
        }
        // GET: ATRIBUICAO/Edit/5
        [Authorize(Roles = "Viaturas,Gestor,SuperAdmin")]
        public async Task<IActionResult> EditViatura(int? id)
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
            ViewData["id_colaborador"] = new SelectList(_context.COLABORADOR, "id_colaborador", "nome", aTRIBUICAO.id_colaborador);
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "matricula", aTRIBUICAO.id_viatura);
            return View(aTRIBUICAO);
        }
      

        // POST: ATRIBUICAO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gsm,Gestor,SuperAdmin")]
        public async Task<IActionResult> EditGSM(int id, [Bind("id_atribuicao,id_gsm,id_colaborador,data_inicio,data_fim")] ATRIBUICAO aTRIBUICAO)
        {
            if (id != aTRIBUICAO.id_atribuicao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var aTRIBUICAORESERVA = await _context.ATRIBUICAO.SingleOrDefaultAsync(c => c.data_fim == null && c.id_gsm == aTRIBUICAO.id_gsm);
                    if (aTRIBUICAORESERVA.data_fim == null)
                    {
                        aTRIBUICAORESERVA.data_fim = DateTime.Now;
                        _context.Update(aTRIBUICAORESERVA);
                        await _context.SaveChangesAsync();
                    }

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
                return RedirectToAction(nameof(GSM));
            }
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "numero", aTRIBUICAO.id_gsm);
            ViewData["id_colaborador"] = new SelectList(_context.COLABORADOR, "id_colaborador", "nome", aTRIBUICAO.id_colaborador);
            return View(aTRIBUICAO);
        }
        // POST: ATRIBUICAO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Viaturas,Gestor,SuperAdmin")]
        public async Task<IActionResult> EditViatura(int id, [Bind("id_atribuicao,id_viatura,id_colaborador,data_inicio,data_fim")] ATRIBUICAO aTRIBUICAO)
        {
            if (id != aTRIBUICAO.id_atribuicao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var aTRIBUICAORESERVA = await _context.ATRIBUICAO.SingleOrDefaultAsync(c => c.data_fim == null && c.id_viatura == aTRIBUICAO.id_viatura);
                    if(aTRIBUICAORESERVA.data_fim == null)
                    {
                        aTRIBUICAORESERVA.data_fim = DateTime.Now;
                        _context.Update(aTRIBUICAORESERVA);
                        await _context.SaveChangesAsync();
                    }

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
                return RedirectToAction(nameof(Viatura));
            }
            ViewData["id_colaborador"] = new SelectList(_context.COLABORADOR, "id_colaborador", "nome", aTRIBUICAO.id_colaborador);
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "matricula", aTRIBUICAO.id_viatura);
            return View(aTRIBUICAO);
        }
        // GET: ATRIBUICAO/Delete/5
        [Authorize(Roles = "Viaturas,Gestor,SuperAdmin")]
        public async Task<IActionResult> DeleteViatura(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aTRIBUICAO = await _context.ATRIBUICAO
                .Include(a => a.COLABORADOR)
                .Include(a => a.VIATURA)
                .SingleOrDefaultAsync(m => m.id_atribuicao == id);
            if (aTRIBUICAO == null)
            {
                return NotFound();
            }

            return View(aTRIBUICAO);
        }

        // POST: ATRIBUICAO/DeleteViatura/5
        [HttpPost, ActionName("DeleteViatura")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Viaturas,Gestor,SuperAdmin")]
        public async Task<IActionResult> DeleteViaturaConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var aTRIBUICAO = await _context.ATRIBUICAO.SingleOrDefaultAsync(m => m.id_atribuicao == id);
            _context.ATRIBUICAO.Remove(aTRIBUICAO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Viatura));
        }

        // GET: ATRIBUICAO/DeleteGSM/5
        [Authorize(Roles = "Gsm,Gestor,SuperAdmin")]
        public async Task<IActionResult> DeleteGSM(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aTRIBUICAO = await _context.ATRIBUICAO
                .Include(a => a.GSM)
                .Include(a => a.COLABORADOR)
                .SingleOrDefaultAsync(m => m.id_atribuicao == id);
            if (aTRIBUICAO == null)
            {
                return NotFound();
            }

            return View(aTRIBUICAO);
        }

        // POST: ATRIBUICAO/DeleteGSM/5
        [HttpPost, ActionName("DeleteGSM")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gsm,Gestor,SuperAdmin")]
        public async Task<IActionResult> DeleteGSMConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var aTRIBUICAO = await _context.ATRIBUICAO.SingleOrDefaultAsync(m => m.id_atribuicao == id);
            _context.ATRIBUICAO.Remove(aTRIBUICAO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GSM));
        }

        private bool ATRIBUICAOExists(int id)
        {
            return _context.ATRIBUICAO.Any(e => e.id_atribuicao == id);
        }
    }
}
