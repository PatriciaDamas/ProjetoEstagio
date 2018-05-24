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
    public class VIATURAController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VIATURAController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VIATURA
        public async Task<IActionResult> Index()
        {
            return View(await _context.VIATURA.ToListAsync());
        }

        // GET: VIATURA/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vIATURA = await _context.VIATURA
                .SingleOrDefaultAsync(m => m.id_viatura == id);
            if (vIATURA == null)
            {
                return NotFound();
            }

            return View(vIATURA);
        }

        // GET: VIATURA/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VIATURA/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_viatura,matricula,marca,modelo")] VIATURA vIATURA)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vIATURA);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vIATURA);
        }

        // GET: VIATURA/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vIATURA = await _context.VIATURA.SingleOrDefaultAsync(m => m.id_viatura == id);
            if (vIATURA == null)
            {
                return NotFound();
            }
            return View(vIATURA);
        }

        // POST: VIATURA/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_viatura,matricula,marca,modelo")] VIATURA vIATURA)
        {
            if (id != vIATURA.id_viatura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vIATURA);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VIATURAExists(vIATURA.id_viatura))
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
            return View(vIATURA);
        }

        // GET: VIATURA/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vIATURA = await _context.VIATURA
                .SingleOrDefaultAsync(m => m.id_viatura == id);
            if (vIATURA == null)
            {
                return NotFound();
            }

            return View(vIATURA);
        }

        // POST: VIATURA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vIATURA = await _context.VIATURA.SingleOrDefaultAsync(m => m.id_viatura == id);
            _context.VIATURA.Remove(vIATURA);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VIATURAExists(int id)
        {
            return _context.VIATURA.Any(e => e.id_viatura == id);
        }
    }
}
