﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Samsys_Custos.Data;

namespace Samsys_Custos.Controllers
{
    public class EQUIPAController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EQUIPAController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EQUIPA
        public async Task<IActionResult> Index()
        {
            return View(await _context.EQUIPA.ToListAsync());
        }

        // GET: EQUIPA/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eQUIPA = await _context.EQUIPA
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
                return RedirectToAction(nameof(Index));
            }
            return View(eQUIPA);
        }

        // GET: EQUIPA/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            return View(eQUIPA);
        }

        // POST: EQUIPA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_equipa,id_lider,nome")] EQUIPA eQUIPA)
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
                return RedirectToAction(nameof(Index));
            }
            return View(eQUIPA);
        }

        // GET: EQUIPA/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eQUIPA = await _context.EQUIPA
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eQUIPA = await _context.EQUIPA.SingleOrDefaultAsync(m => m.id_equipa == id);
            _context.EQUIPA.Remove(eQUIPA);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EQUIPAExists(int id)
        {
            return _context.EQUIPA.Any(e => e.id_equipa == id);
        }
    }
}
