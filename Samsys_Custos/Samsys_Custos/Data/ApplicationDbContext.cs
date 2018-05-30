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

        public DbSet<Samsys_Custos.Models.VIATURA> VIATURA { get; set; }

        public DbSet<Samsys_Custos.Models.SALARIO> SALARIO { get; set; }

        public DbSet<Samsys_Custos.Models.PERMISSAO> PERMISSAO { get; set; }

        public DbSet<Samsys_Custos.Models.PERFIL> PERFIL { get; set; }

        public DbSet<Samsys_Custos.Data.CATEGORIA> CATEGORIA { get; set; }

        public DbSet<Samsys_Custos.Data.FORNECEDOR> FORNECEDOR { get; set; }

        public DbSet<Samsys_Custos.Models.UTILIZADOR> UTILIZADOR { get; set; }

        public DbSet<Samsys_Custos.Models.EQUIPA> EQUIPA { get; set; }

        public DbSet<Samsys_Custos.Data.UTILIZADOR_PERMISSAO> UTILIZADOR_PERMISSAO { get; set; }

        public DbSet<Samsys_Custos.Data.DADOS_PHC> DADOS_PHC { get; set; }

        public DbQuery<Samsys_Custos.Models.CUSTO_EQUIPA> CUSTO_EQUIPA { get; set; }
    }
}
