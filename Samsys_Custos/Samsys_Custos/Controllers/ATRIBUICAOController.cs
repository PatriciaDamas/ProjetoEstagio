﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        // GET: ATRIBUICAO
        public async Task<IActionResult> GSM()
        {
            var applicationDbContext = _context.ATRIBUICAO.Include(a => a.GSM).Include(a => a.UTILIZADOR).Where(c => c.id_gsm != null);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: ATRIBUICAO
        public async Task<IActionResult> Viatura()
        {
            var applicationDbContext = _context.ATRIBUICAO.Include(a => a.VIATURA).Include(a => a.UTILIZADOR).Where(c => c.id_viatura != null);
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
        public IActionResult CreateViatura()
        {
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "matricula");
            return View();
        }

        // GET: ATRIBUICAO/Create
        public IActionResult CreateGSM()
        {
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "numero");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            return View();
        }


        // POST: ATRIBUICAO/Create_viatura
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _context.Add(aTRIBUICAO);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome", aTRIBUICAO.id_colaborador);
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "matricula", aTRIBUICAO.id_viatura);
            return View(aTRIBUICAO);
        }
        // POST: ATRIBUICAO/Create_GSM
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _context.Add(aTRIBUICAO);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "numero", aTRIBUICAO.id_gsm);
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome", aTRIBUICAO.id_colaborador);
            return View(aTRIBUICAO);
        }

        
        // GET: ATRIBUICAO/Edit/5
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
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome", aTRIBUICAO.id_colaborador);
            return View(aTRIBUICAO);
        }
        // GET: ATRIBUICAO/Edit/5
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
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome", aTRIBUICAO.id_colaborador);
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "matricula", aTRIBUICAO.id_viatura);
            return View(aTRIBUICAO);
        }
      

        // POST: ATRIBUICAO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "numero", aTRIBUICAO.id_gsm);
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome", aTRIBUICAO.id_colaborador);
            return View(aTRIBUICAO);
        }
        // POST: ATRIBUICAO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome", aTRIBUICAO.id_colaborador);
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "matricula", aTRIBUICAO.id_viatura);
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
