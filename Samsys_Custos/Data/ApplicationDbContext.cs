﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Samsys_Custos.Models;

namespace Samsys_Custos.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Samsys_Custos.Models.VIATURA> VIATURA { get; set; }

        public DbSet<Samsys_Custos.Models.UTILIZADOR> UTILIZADOR { get; set; }

        public DbSet<Samsys_Custos.Models.SALARIO> SALARIO { get; set; }

        public DbSet<Samsys_Custos.Models.PERMISSAO> PERMISSAO { get; set; }

        public DbSet<Samsys_Custos.Models.PERFIL> PERFIL { get; set; }
    }
}
