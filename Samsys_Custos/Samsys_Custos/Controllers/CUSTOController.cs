using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
      
        
        //------------------------------------------------------------------
        // GET: Custos Gerais
        public IActionResult Geral() {

            var applicationDbContext = _context.DASHBOARD_CUSTOS_CATEGORIA.ToList();
            return View(applicationDbContext);
        }
        
        
        //------------------------------------------------------------------
        // GET: Custos Gerais Grafico
        public IActionResult Grafico()
        {
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 1990; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            ViewData["ano"] = new SelectList(Years, "Value", "Value");
            return View();
        }        
        public JsonResult GeralJson(int? ano)
        {

            if(ano==null)
            {
                var applicationDbContext = _context.DASHBOARD_CUSTOS_CATEGORIA.Where(a => a.ano == Int32.Parse(DateTime.Now.Year.ToString()));
                return Json(applicationDbContext);
            }
            else
            {
                var applicationDbContext = _context.DASHBOARD_CUSTOS_CATEGORIA.Where(a => a.ano == ano);
                return Json(applicationDbContext);
            }
            
        }
        
        
        //------------------------------------------------------------------
        // GET: Custos Equipa
        public IActionResult Equipa()
        {
            var applicationDbContext = _context.CUSTOS_EQUIPA.ToList();
            return View(applicationDbContext);
        }
        //------------------------------------------------------------------
        //get Custos Colaborador ???
        public IActionResult Colaborador(int? ano)
        {   
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 1990; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
            }
            ViewData["ano"] = new SelectList(Years, "Value", "Text");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "nome", "nome");


            if (ano == null)
            {
                var applicationDbContext = _context.CUSTOS_COLABORADOR.Where(a => a.Ano == DateTime.Now.Year.ToString());
                return View(applicationDbContext.ToList());
            }
            else
            {
                var applicationDbContext = _context.CUSTOS_COLABORADOR.Where(a => a.Ano == ano.ToString() );
                return View( applicationDbContext.ToList());
            }
            
        }

        //------------------------------------------------------------------
        // GET: SALARIO
        public async Task<IActionResult> Salario()
        {
            //AUTH FOR PROFILE
            var applicationDbContext = _context.SALARIO.Include(c => c.CUSTO).Include(c => c.CUSTO.CATEGORIA).Include(c => c.CUSTO.UTILIZADOR);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: Criar SALARIO (CUSTO)
        public IActionResult CriarSalario()
        {
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 1990; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            ViewData["ano"] = new SelectList(Years, "Value", "Value");
            return View();
        }
        // POST: Criar Salario
        [HttpPost]
        [ValidateAntiForgeryToken]
            public async Task<IActionResult> CriarSalario(Samsys_Custos.Data.SALARIO sALARIO)
            {
                if (ModelState.IsValid)
            {
               
                if (sALARIO.CUSTO.designacao == null)
                {
                    sALARIO.CUSTO.designacao = _context.CATEGORIA.Where(a => a.id_categoria == 16).FirstOrDefault().nome;
                }
                sALARIO.CUSTO.id_categoria = 16;
                sALARIO.CUSTO.valor = sALARIO.irs + sALARIO.liquido + sALARIO.outras_despesas + sALARIO.outras_regalias + sALARIO.outros + sALARIO.seguranca_social + sALARIO.subsidio_alimentacao;
                sALARIO.CUSTO.data = DateTime.Now;
                _context.SALARIO.Add(sALARIO);
                var id = sALARIO.id_salario;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Salario));
            }
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 1990; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            ViewData["ano"] = new SelectList(Years, "Value", "Value");
            return View(sALARIO);
        }
        
        //------------------------------------------------------------------
        // GET: ECONOMATO
        public async Task<IActionResult> Economato()
        {
            //AUTH FOR PROFILE
            var applicationDbContext = _context.CUSTO.Include(c => c.CATEGORIA).Include(c => c.UTILIZADOR).Include(c => c.VIATURA).Where(c => c.CATEGORIA.id_categoria == 32);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PREMIOS
        public async Task<IActionResult> Premio()
        {
            //AUTH FOR PROFILE
            var applicationDbContext = _context.CUSTO.Include(c => c.CATEGORIA).Include(c => c.UTILIZADOR).Include(c => c.VIATURA).Where(c => c.id_gsm == null && c.id_viatura == null && c.id_phc == null);
            return View(await applicationDbContext.ToListAsync());
        }
        public IActionResult CriarPremio()
        {
            
            ViewData["tipo_premio"] = new SelectList(_context.CATEGORIA.Where(a=>a.nome == "Comercial" || a.nome=="Campanha" || a.nome=="Operacional"),"id_categoria","nome");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 1990; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            ViewData["ano"] = new SelectList(Years, "Value", "Value");
            return View();
        }
        public JsonResult getPai(int id)//22
        {
            if (id != 0)
            {
                var tempID = _context.CATEGORIA.Where(a => a.id_categoria == id).FirstOrDefault();
                int myPai = 0;
                if (tempID != null)
                {
                    myPai = (int)tempID.id_pai;
                    Debug.WriteLine(myPai);
                }
                var tempID2 = _context.CATEGORIA.Where(a => a.id_categoria == myPai).FirstOrDefault();

                return Json(tempID2.nome);

            }
            return Json("Empty");

        }
        public JsonResult getRubrica(int id)
        {
          
            return Json(JsonConvert.SerializeObject(new SelectList(_context.CATEGORIA.Where(a => a.id_pai == id), "id_categoria", "nome")));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarPremio([Bind("id_colaborador,id_categoria,ano,mes,designacao,valor")] CUSTO cUSTO)
        {
            if (ModelState.IsValid)
            {
                if (cUSTO.designacao == null)
                {
                    cUSTO.designacao = _context.CATEGORIA.Where(a => a.id_categoria == cUSTO.id_categoria).FirstOrDefault().nome;
                }

                cUSTO.data = DateTime.Now;
                _context.Add(cUSTO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Premio));
            }
            ViewData["tipo_premio"] = new SelectList(_context.CATEGORIA.Where(a => a.nome == "Comercial" || a.nome == "Campanha" || a.nome == "Operacional"), "id_categoria", "nome");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 1990; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            ViewData["ano"] = new SelectList(Years, "Value", "Value");
            return View(cUSTO);
        }

        // GET: GSM
        public async Task<IActionResult> Gsm()
        {
            var applicationDbContext = _context.CUSTO.Include(c => c.CATEGORIA).Include(c => c.GSM).Include(c => c.UTILIZADOR).Where(c => c.id_gsm != null);
            return View(await applicationDbContext.ToListAsync());
        }
        public IActionResult CriarGsm()
        {
            var temp2 = _context.CATEGORIA.Where(a => a.nome == "Moveis").FirstOrDefault();
            var gsm2 = _context.CATEGORIA.Where(a => a.nome == "Comunicações").FirstOrDefault();

        
            ViewData["id_categoria"] = new SelectList(_context.CATEGORIA.Where(a => a.id_pai == temp2.id_categoria || a.id_pai == gsm2.id_categoria && a.nome != "Moveis"), "id_categoria", "nome");
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "numero");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 1990; i<=Int32.Parse(DateTime.Now.Year.ToString());i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            ViewData["ano"] = new SelectList(Years,"Value","Value");
            return View();
        }
        // POST: GSM
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarGsm([Bind("id_colaborador,id_categoria,id_gsm,ano,mes,designacao,valor")] CUSTO cUSTO)
        {
            if (ModelState.IsValid)
            {
                if (cUSTO.designacao == null)
                {
                    cUSTO.designacao = _context.CATEGORIA.Where(a=> a.id_categoria == cUSTO.id_categoria).FirstOrDefault().nome;
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
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 1990; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            ViewData["ano"] = new SelectList(Years, "Value", "Value");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            return View(cUSTO);
        }

        //------------------------------------------------------------------
        // GET: Viaturas
        public async Task<IActionResult> Viatura()
        {

            var applicationDbContext = _context.CUSTO.Include(c => c.CATEGORIA).Include(c => c.DADOS_PHC).Include(c => c.GSM).Include(c => c.UTILIZADOR).Include(c => c.VIATURA).Where(c => c.id_viatura != null);
            return View(await applicationDbContext.ToListAsync());
        }
        public IActionResult CriarViatura()
        {
            var viatura = _context.CATEGORIA.Where(a => a.nome == "Viaturas").FirstOrDefault();
            ViewData["id_categoria"] = new SelectList(_context.CATEGORIA.Where(a=> a.id_pai == viatura.id_categoria), "id_categoria", "nome");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "matricula");
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 1990; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            ViewData["ano"] = new SelectList(Years, "Value", "Value");
            return View();
        }


        // POST: CUSTO/viatura
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarViatura([Bind("id_colaborador,id_categoria,id_viatura,ano,mes,designacao,valor")] CUSTO cUSTO)
        {
            if (ModelState.IsValid)
            {
                if (cUSTO.designacao == null)
                {
                    cUSTO.designacao = _context.CATEGORIA.Where(a => a.id_categoria == cUSTO.id_categoria).FirstOrDefault().nome;
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
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 1990; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            ViewData["ano"] = new SelectList(Years, "Value", "Value");
            return View(cUSTO);
        }
        //------------------------------------------------------------------
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
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "id_colaborador", cUSTO.id_colaborador);
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "id_viatura", cUSTO.id_viatura);
            return View(cUSTO);
        }

        //EDITAR PRÉMIOS ------------------------
        // GET: CUSTO/Edit/5
        public async Task<IActionResult> EditPremio(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var premio = await _context.CUSTO.SingleOrDefaultAsync(m => m.id_custo == id);
            if (premio == null)
            {
                return NotFound();
            }
            ViewData["tipo_premio"] = new SelectList(_context.CATEGORIA.Where(a => a.nome == "Comercial" || a.nome == "Campanha" || a.nome == "Operacional"), "id_categoria", "nome");
            //ViewData["id_categoria"] = new SelectList(_context.CATEGORIA, "id_categoria", "nome", premio.id_categoria);
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome", premio.id_colaborador);
            
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 1990; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            ViewData["ano"] = new SelectList(Years, "Value", "Value", premio.ano);
            return View(premio);
        }

        // POST: GSM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPremio(int id, [Bind("id_custo, id_colaborador, id_categoria, id_gsm, id_phc, id_viatura, id_salario, data, ano, mes, designacao, valor")] CUSTO custo)
        {
            if (id != custo.id_custo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(custo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CUSTOExists(custo.id_custo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Premio));
            }

            return View(custo);
        }

        //EDITAR GSM ------------------------
        // GET: CUSTO/Edit/5
        public async Task<IActionResult> EditGsm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gsm = await _context.CUSTO.SingleOrDefaultAsync(m => m.id_custo == id);
            if (gsm == null)
            {
                return NotFound();
            }
            var temp2 = _context.CATEGORIA.Where(a => a.nome == "Moveis").FirstOrDefault();
            var gsm2 = _context.CATEGORIA.Where(a => a.nome == "Comunicações").FirstOrDefault();


            ViewData["id_categoria"] = new SelectList(_context.CATEGORIA.Where(a => a.id_pai == temp2.id_categoria || a.id_pai == gsm2.id_categoria && a.nome != "Moveis"), "id_categoria", "nome", gsm.id_categoria);
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome", gsm.id_colaborador);
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "numero", gsm.id_gsm);
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 1990; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            ViewData["ano"] = new SelectList(Years, "Value", "Value",gsm.ano);
            return View(gsm);
        }

        // POST: GSM/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGsm(int id, [Bind("id_custo, id_colaborador, id_categoria, id_gsm, id_phc, id_viatura, id_salario, data, ano, mes, designacao, valor")] CUSTO custo)
        {
            if (id != custo.id_custo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    custo.data = DateTime.Now;
                    _context.Update(custo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CUSTOExists(custo.id_custo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Gsm));
            }

            return View(custo);
        }


        //EDITAR VIATURA ------------------------
        // GET: CUSTO/Edit/5
        public async Task<IActionResult> EditViatura(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viatura = await _context.CUSTO.SingleOrDefaultAsync(m => m.id_custo == id);
            if (viatura == null)
            {
                return NotFound();
            }
            var viatura_categoria = _context.CATEGORIA.Where(a => a.nome == "Viaturas").FirstOrDefault();
            ViewData["id_categoria"] = new SelectList(_context.CATEGORIA.Where(a => a.id_pai == viatura_categoria.id_categoria), "id_categoria", "nome", viatura.id_categoria);
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 1990; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            ViewData["ano"] = new SelectList(Years, "Value", "Value", viatura.ano);
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome", viatura.id_colaborador);
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "matricula", viatura.id_viatura);
            return View(viatura);
        }

     


        // POST: VIATURASEdit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditViatura(int id, [Bind("id_custo, id_colaborador, id_categoria, id_gsm, id_phc, id_viatura, id_salario, data, ano, mes, designacao, valor")] CUSTO custo)
        {
            if (id != custo.id_custo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    custo.data = DateTime.Now;
                    _context.Update(custo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CUSTOExists(custo.id_custo))
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

            
            return View(custo);
        }


        public static class GlobalVariables
        {
            public static string PageServer { get; set; }
        }

        // GET: CUSTO/Delete
        public async Task<IActionResult> Delete(int? id,string page)
        {
            GlobalVariables.PageServer = page;
            if (id == null)
            {
                return NotFound();
            }

            var cUSTO = await _context.CUSTO
                .Include(c => c.CATEGORIA)
                .Include(c => c.DADOS_PHC)
                .Include(c => c.GSM)
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
            if (GlobalVariables.PageServer == "Viatura")
            {
                return RedirectToAction(nameof(Viatura));

            }
            if (GlobalVariables.PageServer == "Salario")
            {
                return RedirectToAction(nameof(Salario));

            }
            if (GlobalVariables.PageServer == "Gsm")
            {
                return RedirectToAction(nameof(Gsm));

            }
            if (GlobalVariables.PageServer == "Premio")
            {
                return RedirectToAction(nameof(Premio));

            }
            return RedirectToAction(nameof(Geral));
        }

        // GET: CUSTO/Delete
        public async Task<IActionResult> DeleteSalario(int? id, string page)
        {
            GlobalVariables.PageServer = page;
            if (id == null)
            {
                return NotFound();
            }

            var sALARIO = await _context.SALARIO
                .Include(c => c.CUSTO)
                .Include(c => c.CUSTO.CATEGORIA)
                .Include(c => c.CUSTO.UTILIZADOR)
                .SingleOrDefaultAsync(m => m.id_salario == id);
            if (sALARIO == null)
            {
                return NotFound();
            }
            return View(sALARIO);
        }

        // POST: CUSTO/Delete/5
        [HttpPost, ActionName("DeleteSalario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSalarioConfimed(int id)
        {
            var sALARIO = await _context.SALARIO.SingleOrDefaultAsync(m => m.id_salario == id);
            var cUSTO = await _context.CUSTO.SingleOrDefaultAsync(m => m.id_custo == sALARIO.id_custo);
            _context.SALARIO.Remove(sALARIO);
            _context.CUSTO.Remove(cUSTO);
            await _context.SaveChangesAsync();
            
            if (GlobalVariables.PageServer == "Salario")
            {
                return RedirectToAction(nameof(Salario));
            }
            return RedirectToAction(nameof(Geral));
        }

        private bool CUSTOExists(int id)
        {
            return _context.CUSTO.Any(e => e.id_custo == id);
        }
    }
}
