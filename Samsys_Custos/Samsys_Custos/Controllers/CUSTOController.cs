using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Samsys_Custos.Controllers { 
    public class CUSTOController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CUSTOController(ApplicationDbContext context)
        {
            _context = context;
        }

        #region DASHBOARD

        // GET: RETORNA DASHBOARD
        public IActionResult Grafico_Gerais()
        {
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 2006; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            Years.OrderByDescending(x => x.Value);

            ViewData["ano"] = new SelectList(Years.OrderByDescending(x => x.Value), "Value", "Value");
            return View();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: FILTRO ANO GRÁFICOS CUSTOS TOTAIS
        public IActionResult Custos_Filtro(string categoria, int ano, string mes)
        {
            if (mes == null)
            {
                var applicationDbContext = _context.CUSTOS_TOTAIS.Where(a => a.NomeCompleto == categoria & a.ano == ano).ToList();
                ViewBag.nomeCategoria = categoria;
                return View(applicationDbContext);
            }
            else
            {
                var applicationDbContext = _context.CUSTOS_TOTAIS.Where(a => a.NomeCompleto == categoria & a.ano == ano & a.mes == mes).ToList();
                ViewBag.nomeCategoria = categoria;
                return View(applicationDbContext);

            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA INFORMAÇÃO PARA O GRÁFICO DE CUSTOS AO LONGO DO ANO
        public JsonResult GeralJson(int? ano)
        {
            if (ano == null)
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

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA INFORMAÇÃO PARA O GRÁFICO DE CUSTOS ANUAIS
        public JsonResult CustoTotalAnoJson(int? ano)
        {
            if (ano == null)
            {
                var applicationDbContext = _context.CUSTOS_TOTAIS_ANO.ToList().Where(a => a.ano == Int32.Parse(DateTime.Now.Year.ToString()));
                return Json(applicationDbContext);
            }
            else
            {
                var applicationDbContext = _context.CUSTOS_TOTAIS_ANO.ToList().Where(a => a.ano == ano);
                return Json(applicationDbContext);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        #endregion

        #region EQUIPA

        // GET: FILTRO POR ANO,MES E EQUIPA DE CUSTO EQUIPA
        public IActionResult CustoEquipaDetalhe(int? ano, string mes, string equipa)
        {
            if (mes == null)
            {
                var applicationDbContext = _context.CUSTOS_EQUIPA_DETALHE.ToList();
                ViewBag.nomeEquipa = equipa;
                return View(applicationDbContext);
            }
            else
            {
                var applicationDbContext = _context.CUSTOS_EQUIPA_DETALHE.Where(a => a.Equipas == equipa & a.ano == ano & a.mes == mes).ToList();
                ViewBag.nomeEquipa = equipa;
                return View(applicationDbContext);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA INFORMAÇÃO PARA O GRÁFICO DE CUSTOS DE EQUIPA AO LONGO DO ANO
        public JsonResult CustoEquipaJson(int? ano)
        {
            if (ano == null)
            {
                var applicationDbContext = _context.CUSTOS_EQUIPA.ToList().Where(a => a.ano == Int32.Parse(DateTime.Now.Year.ToString()));
                return Json(applicationDbContext);
            }
            else
            {
                var applicationDbContext = _context.CUSTOS_EQUIPA.ToList().Where(a => a.ano == ano);
                return Json(applicationDbContext);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA GRÁFICO DE CUSTO EQUIPAS
        public IActionResult Grafico_Equipa()
        {
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 2006; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            Years.OrderByDescending(x => x.Value);

            ViewData["ano"] = new SelectList(Years.OrderByDescending(x => x.Value), "Value", "Value");
            return View();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA GRÁFICO DE MÉDIA CUSTOS EQUIPAS
        public IActionResult Grafico_Equipa_Media()
        {
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 2018; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            Years.OrderByDescending(x => x.Value);

            ViewData["ano"] = new SelectList(Years.OrderByDescending(x => x.Value), "Value", "Value");
            return View();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA INFORMAÇÃO PARA O GRÁFICO DE MÉDIA DE CUSTOS DE EQUIPA AO LONGO DO ANO
        public JsonResult CustoEquipaMediaJson(string ano)
        {
            //chamar o stored proc para a tabela MEDIA_CUSTOS
            _context.Database.ExecuteSqlCommand("exec dbo.MEDIA_CUSTO_PROC");
            if (ano == null)
            {
                var applicationDbContext = _context.MEDIA_CUSTOS.ToList().Where(a => a.ano == DateTime.Now.Year.ToString());
                return Json(applicationDbContext);
            }
            else
            {
                var applicationDbContext = _context.MEDIA_CUSTOS.ToList().Where(a => a.ano == ano);
                return Json(applicationDbContext);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        #endregion

        #region LISTAGENS

        // GET: RETORNA TODOS OS CUSTOS
        public IActionResult Geral(int? page)
        {
            int pageSize = 10;
            var applicationDbContext = _context.DASHBOARD_CUSTOS_CATEGORIA;
            var count = applicationDbContext.Count();
            var gerais = applicationDbContext.Skip(((page ?? 1) - 1) * pageSize).Take(pageSize).ToList();
            return View(new PaginatedList<Samsys_Custos.Models.DASHBOARD_CUSTOS_CATEGORIA>(gerais, count, page ?? 1, pageSize));
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA EQUIPA
        public IActionResult Equipa(string equipa, string mes, int ano)
        {
            var applicationDbContext = _context.CUSTOS_EQUIPA.ToList();
            return View(applicationDbContext);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA VIEW DE SELEÇÃO PARA CUSTOS DE COLABORADOR
        public IActionResult Colaborador()
        {
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 2006; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            Years.OrderByDescending(x => x.Value);

            ViewData["ano"] = new SelectList(Years.OrderByDescending(x => x.Value), "Value", "Value");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            return View();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA INFORMAÇÃO PARA DETALHE DO CUSTO COLABORADOR
        public JsonResult CustoColaboradorJson(int? ano, int? id)
        {
            var applicationDbContext = _context.CUSTOS_COLABORADOR.Where(a => a.Ano == ano.ToString() && a.Colaborador.Equals(id.ToString()));
            return Json(applicationDbContext);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA TABELA SALARIO
        [Authorize(Roles = "Salário,Gestor,SuperAdmin,Financeiro")]
        public async Task<IActionResult> Salario(int? page)
        {
            int pageSize = 10;
            var applicationDbContext = _context.SALARIO.Include(c => c.CUSTO).Include(c => c.CUSTO.CATEGORIA).Include(c => c.CUSTO.UTILIZADOR);
            var count = applicationDbContext.Count();
            var salarios = await applicationDbContext.Skip(((page ?? 1) - 1) * pageSize).Take(pageSize).ToListAsync();
            return View(new PaginatedList<SALARIO>(salarios, count, page ?? 1, pageSize));
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA ECONOMATO
        public async Task<IActionResult> Economato(int? page)
        {
            int pageSize = 10;
            var applicationDbContext = _context.CUSTO.Include(c => c.CATEGORIA).Include(c => c.UTILIZADOR).Include(c => c.VIATURA).Where(c => c.CATEGORIA.id_categoria == 32);
            var count = applicationDbContext.Count();
            var users = await applicationDbContext.Skip(((page ?? 1) - 1) * pageSize).Take(pageSize).ToListAsync();
            return View(new PaginatedList<CUSTO>(users, count, page ?? 1, pageSize));
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA PREMIOS
        [Authorize(Roles = "Prémios,Gestor,SuperAdmin,Financeiro")]
        public IActionResult Premio()
        {
            var applicationDbContext = _context.CUSTOS_PREMIOS.ToList();
            return View(applicationDbContext);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA EQUIPAMENTOS GSM
        public async Task<IActionResult> Gsm(int? page)
        {
            int pageSize = 10;
            var applicationDbContext = _context.CUSTO.Include(c => c.CATEGORIA).Include(c => c.GSM).Include(c => c.UTILIZADOR).Where(c => c.id_gsm != null);
            var count = applicationDbContext.Count();
            var gsm = await applicationDbContext.Skip(((page ?? 1) - 1) * pageSize).Take(pageSize).ToListAsync();
            return View(new PaginatedList<CUSTO>(gsm, count, page ?? 1, pageSize));
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA VALIDAÇÃO DE DADOS PHC
        public async Task<IActionResult> Validacao(int? page)
        {
            int pageSize = 5;
            var applicationDbContext = _context.CUSTO.Include(c => c.CATEGORIA).Include(c => c.DADOS_PHC).Include(c => c.GSM).Include(c => c.UTILIZADOR).Include(c => c.VIATURA).Where(c => c.id_phc != null);
            var count = applicationDbContext.Count();
            var validacao = await applicationDbContext.Skip(((page ?? 1) - 1) * pageSize).Take(pageSize).ToListAsync();
            ViewData["id_categoria"] = new SelectList(_context.CATEGORIA.Where(a => a.id_pai != null), "id_categoria", "nome");
            return View(new PaginatedList<CUSTO>(validacao, count, page ?? 1, pageSize));
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA VIATURAS
        public async Task<IActionResult> Viatura(int? page)
        {
            int pageSize = 10;
            var applicationDbContext = _context.CUSTO.Include(c => c.CATEGORIA).Include(c => c.DADOS_PHC).Include(c => c.GSM).Include(c => c.UTILIZADOR).Include(c => c.VIATURA).Where(c => c.id_viatura != null);
            var count = applicationDbContext.Count();
            var viatura = await applicationDbContext.Skip(((page ?? 1) - 1) * pageSize).Take(pageSize).ToListAsync();
            return View(new PaginatedList<CUSTO>(viatura, count, page ?? 1, pageSize));
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------
        #endregion

        #region CREATE

        // GET: RETORNA VIEW DE REGISTO SALARIO
        [Authorize(Roles = "Salário,Gestor,SuperAdmin")]
        public IActionResult CriarSalario()
        {
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 2006; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            Years.OrderByDescending(x => x.Value);

            ViewData["ano"] = new SelectList(Years.OrderByDescending(x => x.Value), "Value", "Value");
            return View();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // POST: CREATE REGISTO SALARIO
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Salário,Gestor,SuperAdmin")]
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
            for (int i = 2006; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            Years.OrderByDescending(x => x.Value);

            ViewData["ano"] = new SelectList(Years.OrderByDescending(x => x.Value), "Value", "Value");
            return View(sALARIO);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA VIEW DE REGISTO PREMIO
        [Authorize(Roles = "Prémios,Gestor,SuperAdmin")]
        public IActionResult CriarPremio()
        {
            ViewData["tipo_premio"] = new SelectList(_context.CATEGORIA.Where(a => a.nome == "Comercial" || a.nome == "Campanha" || a.nome == "Operacional"), "id_categoria", "nome");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 2006; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            Years.OrderByDescending(x => x.Value);

            ViewData["ano"] = new SelectList(Years.OrderByDescending(x => x.Value), "Value", "Value");
            return View();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // POST: CREATE REGISTO PREMIO
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Prémios,Gestor,SuperAdmin")]
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
            for (int i = 2006; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            Years.OrderByDescending(x => x.Value);

            ViewData["ano"] = new SelectList(Years.OrderByDescending(x => x.Value), "Value", "Value");
            return View(cUSTO);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------


        // GET: RETORNA VIEW DE REGISTO GSM
        public IActionResult CriarGsm()
        {
            var temp2 = _context.CATEGORIA.Where(a => a.nome == "Moveis").FirstOrDefault();
            var gsm2 = _context.CATEGORIA.Where(a => a.nome == "Comunicações").FirstOrDefault();


            ViewData["id_categoria"] = new SelectList(_context.CATEGORIA.Where(a => a.id_pai == temp2.id_categoria || a.id_pai == gsm2.id_categoria && a.nome != "Moveis"), "id_categoria", "nome");
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "numero");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 2006; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            Years.OrderByDescending(x => x.Value);

            ViewData["ano"] = new SelectList(Years.OrderByDescending(x => x.Value), "Value", "Value");
            return View();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // POST: CREATE REGISTO GSM
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gsm,Gestor,SuperAdmin")]
        public async Task<IActionResult> CriarGsm([Bind("id_colaborador,id_categoria,id_gsm,ano,mes,designacao,valor")] CUSTO cUSTO)
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
                return RedirectToAction(nameof(Gsm));
            }
            var temp2 = _context.CATEGORIA.Where(a => a.nome == "Moveis").FirstOrDefault();
            var gsm2 = _context.CATEGORIA.Where(a => a.nome == "Comunicações").FirstOrDefault();
            ViewData["id_categoria"] = new SelectList(_context.CATEGORIA.Where(a => a.id_pai == temp2.id_categoria || a.id_pai == gsm2.id_categoria && a.nome != "Moveis"), "id_categoria", "nome");
            ViewData["id_gsm"] = new SelectList(_context.GSM, "id_gsm", "numero");
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 2006; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            Years.OrderByDescending(x => x.Value);

            ViewData["ano"] = new SelectList(Years.OrderByDescending(x => x.Value), "Value", "Value");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            return View(cUSTO);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA VIEW DE REGISTO VIATURA
        [Authorize(Roles = "Viaturas,Gestor,SuperAdmin")]
        public IActionResult CriarViatura()
        {
            var viatura = _context.CATEGORIA.Where(a => a.nome == "Viaturas").FirstOrDefault();
            ViewData["id_categoria"] = new SelectList(_context.CATEGORIA.Where(a => a.id_pai == viatura.id_categoria), "id_categoria", "nome");
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome");
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "matricula");
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 2006; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            Years.OrderByDescending(x => x.Value);

            ViewData["ano"] = new SelectList(Years.OrderByDescending(x => x.Value), "Value", "Value");
            return View();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // POST: CREATE REGISTO VIATURA
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Viaturas,Gestor,SuperAdmin")]
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
            for (int i = 2006; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            Years.OrderByDescending(x => x.Value);

            ViewData["ano"] = new SelectList(Years.OrderByDescending(x => x.Value), "Value", "Value");
            return View(cUSTO);
        }
        #endregion

        #region EDIT

        // GET: EDIT PREMIO
        [Authorize(Roles = "Prémios,Gestor,SuperAdmin")]
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
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome", premio.id_colaborador);

            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 2006; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            Years.OrderByDescending(x => x.Value);

            ViewData["ano"] = new SelectList(Years.OrderByDescending(x => x.Value), "Value", "Value", premio.ano);
            return View(premio);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // POST: EDIT PREMIO
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Prémios,Gestor,SuperAdmin")]
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
                    var catname = _context.CATEGORIA.Where(a => a.id_categoria == custo.id_categoria).FirstOrDefault();
                    custo.designacao = catname.nome;
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

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: RETORNA EDIT GSM
        [Authorize(Roles = "Gsm,Gestor,SuperAdmin")]
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
            for (int i = 2006; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            Years.OrderByDescending(x => x.Value);
            ViewData["ano"] = new SelectList(Years.OrderByDescending(x => x.Value), "Value", "Value", gsm.ano);
            return View(gsm);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // POST: EDIT GSM
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gsm,Gestor,SuperAdmin")]
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

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: EDIT VALIDAÇÃO DADOS PHC
        [Authorize(Roles = "Atribuições,Gestor,SuperAdmin")]
        public async Task<DADOS_PHC> EditValidacao(int? id, bool flag, int? cat)
        {
            var validaçao = await _context.DADOS_PHC.SingleOrDefaultAsync(n => n.id_phc == id.ToString());
            try
            {
                validaçao.custo_interno = flag;
                if (cat != null)
                {
                    var custo = _context.CUSTO.Where(a => a.id_phc == validaçao.id_phc).FirstOrDefault();
                    custo.id_categoria = (int)cat;
                }
                _context.Update(validaçao);
                await _context.SaveChangesAsync();
                var validaçao2 = await _context.DADOS_PHC.SingleOrDefaultAsync(n => n.id_phc == id.ToString());
                return validaçao2;
            }
            catch
            {
                return validaçao;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: EDIT VIATURA
        [Authorize(Roles = "Viatura,Gestor,SuperAdmin")]
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
            for (int i = 2006; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            Years.OrderByDescending(x => x.Value);

            ViewData["ano"] = new SelectList(Years.OrderByDescending(x => x.Value), "Value", "Value", viatura.ano);
            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome", viatura.id_colaborador);
            ViewData["id_viatura"] = new SelectList(_context.VIATURA, "id_viatura", "matricula", viatura.id_viatura);
            return View(viatura);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // POST: EDIT VIATURA
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Viatura,Gestor,SuperAdmin")]
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

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: EDIT SALARIO
        [Authorize(Roles = "Salários,Gestor,SuperAdmin")]
        public async Task<IActionResult> EditSalario(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salario = await _context.SALARIO.SingleOrDefaultAsync(m => m.id_salario == id);
            var custo = await _context.CUSTO.SingleOrDefaultAsync(m => m.id_custo == salario.id_custo);
            if (salario == null)
            {
                return NotFound();
            }

            ViewData["id_colaborador"] = new SelectList(_context.UTILIZADOR, "id_colaborador", "nome", custo.id_colaborador);
            List<SelectListItem> Years = new List<SelectListItem>();
            for (int i = 2006; i <= Int32.Parse(DateTime.Now.Year.ToString()); i++)
            {
                Years.Add(new SelectListItem() { Text = "", Value = i.ToString() });
            }
            Years.OrderByDescending(x => x.Value);

            ViewData["ano"] = new SelectList(Years.OrderByDescending(x => x.Value), "Value", "Value", custo.ano);
            return View(salario);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // POST: EDIT SALARIO
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Salários,Gestor,SuperAdmin")]
        public async Task<IActionResult> EditSalario(int id, CUSTO cUSTO, SALARIO sALARIO)
        {
            if (id != sALARIO.id_salario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    cUSTO.valor = sALARIO.irs + sALARIO.liquido + sALARIO.outras_despesas + sALARIO.outras_regalias + sALARIO.outros + sALARIO.seguranca_social + sALARIO.subsidio_alimentacao;
                    sALARIO.CUSTO = cUSTO;
                    _context.Update(sALARIO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CUSTOExists(sALARIO.id_custo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Salario));
            }
            return View(sALARIO);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        #endregion

        #region DELETE

        // GET: DELETE GLOBAL PARA CADA TIPO DE CUSTOS
        public async Task<IActionResult> Delete(int? id, string page)
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

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // POST: DELETE GLOBAL PARA CADA TIPO DE CUSTOS
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

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: DELETE SALÁRIO
        [Authorize(Roles = "Salários,Gestor,SuperAdmin")]
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

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // POST: DELETE SALARIO
        [HttpPost, ActionName("DeleteSalario")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Salários,Gestor,SuperAdmin")]
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

        #endregion

        #region HELPERS

        //var teste = _context.Database.ExecuteSqlCommand( "exec dbo.ANALISES_TO_CUSTO");

        // GET: DEVOLVE O NOME DA CATEGORIA PAI DA SUB-CATEGORIA RECEBIDA
        public JsonResult getPai(int id)
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

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // GET: DEVOLVE A LISTA DE CATEGORIAS QUEM TEM COMO ID_PAI O ID RECEBIDO
        public JsonResult getRubrica(int id)
        {
            return Json(JsonConvert.SerializeObject(new SelectList(_context.CATEGORIA.Where(a => a.id_pai == id), "id_categoria", "nome")));
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // VARIAVEL GLOBAL PARA DETERMINAR PAGINA PARA O DELETE
        public static class GlobalVariables
        {
            public static string PageServer { get; set; }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

        // VERIFICA SE O CUSTO EXISTE
        private bool CUSTOExists(int id)
        {
            return _context.CUSTO.Any(e => e.id_custo == id);
        }

        #endregion


    }
}
