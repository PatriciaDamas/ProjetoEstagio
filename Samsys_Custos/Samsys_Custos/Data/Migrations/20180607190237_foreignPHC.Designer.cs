﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Samsys_Custos.Data;

namespace Samsys_Custos.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180607190237_foreignPHC")]
    partial class foreignPHC
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rc1-32029")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Samsys_Custos.Data.ATRIBUICAO", b =>
                {
                    b.Property<int>("id_atribuicao")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("data_fim");

                    b.Property<DateTime>("data_inicio");

                    b.Property<int>("id_colaborador");

                    b.Property<int?>("id_gsm");

                    b.Property<int?>("id_viatura");

                    b.HasKey("id_atribuicao");

                    b.HasIndex("id_colaborador");

                    b.HasIndex("id_gsm");

                    b.HasIndex("id_viatura");

                    b.ToTable("ATRIBUICAO");
                });

            modelBuilder.Entity("Samsys_Custos.Data.CATEGORIA", b =>
                {
                    b.Property<int>("id_categoria")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("id_pai");

                    b.Property<string>("nome");

                    b.HasKey("id_categoria");

                    b.ToTable("CATEGORIA");
                });

            modelBuilder.Entity("Samsys_Custos.Data.CUSTO", b =>
                {
                    b.Property<int>("id_custo")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ano");

                    b.Property<DateTime>("data");

                    b.Property<string>("designacao");

                    b.Property<int>("id_categoria");

                    b.Property<int>("id_colaborador");

                    b.Property<int?>("id_gsm");

                    b.Property<string>("id_phc");

                    b.Property<int?>("id_viatura");

                    b.Property<string>("mes");

                    b.Property<decimal>("valor");

                    b.HasKey("id_custo");

                    b.HasIndex("id_categoria");

                    b.HasIndex("id_colaborador");

                    b.HasIndex("id_gsm");

                    b.HasIndex("id_phc");

                    b.HasIndex("id_viatura");

                    b.ToTable("CUSTO");
                });

            modelBuilder.Entity("Samsys_Custos.Data.DADOS_PHC", b =>
                {
                    b.Property<string>("id_phc")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("custo_interno");

                    b.Property<string>("id_fornecedor");

                    b.HasKey("id_phc");

                    b.HasIndex("id_fornecedor");

                    b.ToTable("DADOS_PHC");
                });

            modelBuilder.Entity("Samsys_Custos.Data.EQUIPA", b =>
                {
                    b.Property<string>("id_equipa")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("id_lider");

                    b.Property<string>("nome");

                    b.HasKey("id_equipa");

                    b.HasIndex("id_lider")
                        .IsUnique();

                    b.ToTable("EQUIPA");
                });

            modelBuilder.Entity("Samsys_Custos.Data.FORNECEDOR", b =>
                {
                    b.Property<string>("id_fornecedor")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("nome");

                    b.Property<int?>("sugestao_categoria");

                    b.Property<bool?>("sugestao_custo");

                    b.HasKey("id_fornecedor");

                    b.HasIndex("sugestao_categoria");

                    b.ToTable("FORNECEDOR");
                });

            modelBuilder.Entity("Samsys_Custos.Data.GSM", b =>
                {
                    b.Property<int>("id_gsm")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("equipamento");

                    b.Property<int>("numero");

                    b.HasKey("id_gsm");

                    b.ToTable("GSM");
                });

            modelBuilder.Entity("Samsys_Custos.Data.PERFIL", b =>
                {
                    b.Property<int>("id_perfil")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("nome");

                    b.Property<bool>("premio");

                    b.Property<bool>("salario");

                    b.HasKey("id_perfil");

                    b.ToTable("PERFIL");
                });

            modelBuilder.Entity("Samsys_Custos.Data.PERMISSAO", b =>
                {
                    b.Property<int>("id_permissao")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("nome");

                    b.HasKey("id_permissao");

                    b.ToTable("PERMISSAO");
                });

            modelBuilder.Entity("Samsys_Custos.Data.SALARIO", b =>
                {
                    b.Property<int>("id_salario")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("id_custo");

                    b.Property<decimal>("irs");

                    b.Property<decimal>("liquido");

                    b.Property<decimal>("outras_despesas");

                    b.Property<decimal>("outras_regalias");

                    b.Property<decimal>("outros");

                    b.Property<decimal>("seguranca_social");

                    b.Property<decimal>("subsidio_alimentacao");

                    b.HasKey("id_salario");

                    b.HasIndex("id_custo");

                    b.ToTable("SALARIO");
                });

            modelBuilder.Entity("Samsys_Custos.Data.UTILIZADOR", b =>
                {
                    b.Property<int>("id_colaborador")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("data_admissao");

                    b.Property<string>("id_equipa");

                    b.Property<int>("id_perfil");

                    b.Property<string>("nome");

                    b.HasKey("id_colaborador");

                    b.HasIndex("id_equipa");

                    b.HasIndex("id_perfil");

                    b.ToTable("UTILIZADOR");
                });

            modelBuilder.Entity("Samsys_Custos.Data.UTILIZADOR_PERMISSAO", b =>
                {
                    b.Property<int>("id_utilizador_permissao")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("id_colaborador");

                    b.Property<int>("id_permissao");

                    b.HasKey("id_utilizador_permissao");

                    b.HasIndex("id_colaborador");

                    b.HasIndex("id_permissao");

                    b.ToTable("UTILIZADOR_PERMISSAO");
                });

            modelBuilder.Entity("Samsys_Custos.Data.VIATURA", b =>
                {
                    b.Property<int>("id_viatura")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("marca");

                    b.Property<string>("matricula");

                    b.Property<string>("modelo");

                    b.HasKey("id_viatura");

                    b.ToTable("VIATURA");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Samsys_Custos.Data.ATRIBUICAO", b =>
                {
                    b.HasOne("Samsys_Custos.Data.UTILIZADOR", "UTILIZADOR")
                        .WithMany()
                        .HasForeignKey("id_colaborador")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Samsys_Custos.Data.GSM", "GSM")
                        .WithMany()
                        .HasForeignKey("id_gsm");

                    b.HasOne("Samsys_Custos.Data.VIATURA", "VIATURA")
                        .WithMany()
                        .HasForeignKey("id_viatura");
                });

            modelBuilder.Entity("Samsys_Custos.Data.CUSTO", b =>
                {
                    b.HasOne("Samsys_Custos.Data.CATEGORIA", "CATEGORIA")
                        .WithMany()
                        .HasForeignKey("id_categoria")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Samsys_Custos.Data.UTILIZADOR", "UTILIZADOR")
                        .WithMany()
                        .HasForeignKey("id_colaborador")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Samsys_Custos.Data.GSM", "GSM")
                        .WithMany()
                        .HasForeignKey("id_gsm");

                    b.HasOne("Samsys_Custos.Data.DADOS_PHC", "DADOS_PHC")
                        .WithMany()
                        .HasForeignKey("id_phc");

                    b.HasOne("Samsys_Custos.Data.VIATURA", "VIATURA")
                        .WithMany()
                        .HasForeignKey("id_viatura");
                });

            modelBuilder.Entity("Samsys_Custos.Data.DADOS_PHC", b =>
                {
                    b.HasOne("Samsys_Custos.Data.FORNECEDOR", "FORNECEDOR")
                        .WithMany()
                        .HasForeignKey("id_fornecedor");
                });

            modelBuilder.Entity("Samsys_Custos.Data.EQUIPA", b =>
                {
                    b.HasOne("Samsys_Custos.Data.UTILIZADOR", "UTILIZADOR")
                        .WithOne()
                        .HasForeignKey("Samsys_Custos.Data.EQUIPA", "id_lider")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Samsys_Custos.Data.FORNECEDOR", b =>
                {
                    b.HasOne("Samsys_Custos.Data.CATEGORIA", "CATEGORIA")
                        .WithMany()
                        .HasForeignKey("sugestao_categoria");
                });

            modelBuilder.Entity("Samsys_Custos.Data.SALARIO", b =>
                {
                    b.HasOne("Samsys_Custos.Data.CUSTO", "CUSTO")
                        .WithMany()
                        .HasForeignKey("id_custo");
                });

            modelBuilder.Entity("Samsys_Custos.Data.UTILIZADOR", b =>
                {
                    b.HasOne("Samsys_Custos.Data.EQUIPA", "EQUIPA")
                        .WithMany()
                        .HasForeignKey("id_equipa");

                    b.HasOne("Samsys_Custos.Data.PERFIL", "PERFIL")
                        .WithMany()
                        .HasForeignKey("id_perfil")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Samsys_Custos.Data.UTILIZADOR_PERMISSAO", b =>
                {
                    b.HasOne("Samsys_Custos.Data.UTILIZADOR", "UTILIZADOR")
                        .WithMany()
                        .HasForeignKey("id_colaborador")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Samsys_Custos.Data.PERMISSAO", "PERMISSAO")
                        .WithMany()
                        .HasForeignKey("id_permissao")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
