using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Samsys_Custos.Data;

namespace Samsys_Custos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Samsys_Custos.Data.CUSTO> CUSTO { get; set; }
        public DbSet<Samsys_Custos.Data.ATRIBUICAO> ATRIBUICAO { get; set; }

        public DbSet<Samsys_Custos.Data.GSM> GSM { get; set; }

        public DbSet<Samsys_Custos.Data.VIATURA> VIATURA { get; set; }

        public DbSet<Samsys_Custos.Data.SALARIO> SALARIO { get; set; }

        public DbSet<Samsys_Custos.Data.PERMISSAO> PERMISSAO { get; set; }


        public DbSet<Samsys_Custos.Data.FORNECEDOR> FORNECEDOR { get; set; }

        public DbSet<Samsys_Custos.Data.PERFIL> PERFIL { get; set; }

        public DbSet<Samsys_Custos.Data.CATEGORIA> CATEGORIA { get; set; }

        public DbSet<Samsys_Custos.Data.UTILIZADOR> UTILIZADOR { get; set; }

        public DbSet<Samsys_Custos.Data.EQUIPA> EQUIPA { get; set; }

        public DbSet<Samsys_Custos.Data.UTILIZADOR_PERMISSAO> UTILIZADOR_PERMISSAO { get; set; }

        public DbSet<Samsys_Custos.Data.DADOS_PHC> DADOS_PHC { get; set; }

        public DbQuery<Samsys_Custos.Models.CUSTOS_EQUIPA> CUSTOS_EQUIPA { get; set; }

        public DbQuery<Samsys_Custos.Models.DASHBOARD_CUSTOS_CATEGORIA> DASHBOARD_CUSTOS_CATEGORIA { get; set; }

        public DbQuery<Samsys_Custos.Models.COLABORADOR> COLABORADOR { get; set; }

        public DbQuery<Samsys_Custos.Models.CUSTOS_COLABORADOR> CUSTOS_COLABORADOR { get; set; }

        public DbQuery<Samsys_Custos.Models.CUSTOS_EQUIPA_RUBRICA> CUSTOS_EQUIPA_RUBRICA { get; set; }

        public DbQuery<Samsys_Custos.Models.CUSTOS_TOTAIS_ANO> CUSTOS_TOTAIS_ANO { get; set; }

        public DbQuery<Samsys_Custos.Models.CUSTOS_TOTAIS> CUSTOS_TOTAIS { get; set; }
    }
}
