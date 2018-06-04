using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Samsys_Custos.Data.Migrations
{
    public partial class teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {/*
            migrationBuilder.CreateTable(
                name: "CATEGORIA",
                columns: table => new
                {
                    id_categoria = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_pai = table.Column<int>(nullable: true),
                    nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA", x => x.id_categoria);
                });

            migrationBuilder.CreateTable(
                name: "GSM",
                columns: table => new
                {
                    id_gsm = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    numero = table.Column<int>(nullable: false),
                    equipamento = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GSM", x => x.id_gsm);
                });

            migrationBuilder.CreateTable(
                name: "PERFIL",
                columns: table => new
                {
                    id_perfil = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(nullable: true),
                    salario = table.Column<bool>(nullable: false),
                    premio = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERFIL", x => x.id_perfil);
                });

            migrationBuilder.CreateTable(
                name: "PERMISSAO",
                columns: table => new
                {
                    id_permissao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERMISSAO", x => x.id_permissao);
                });

            migrationBuilder.CreateTable(
                name: "SALARIO",
                columns: table => new
                {
                    id_salario = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    liquido = table.Column<decimal>(nullable: false),
                    subsidio_alimentacao = table.Column<decimal>(nullable: false),
                    outros = table.Column<decimal>(nullable: false),
                    seguranca_social = table.Column<decimal>(nullable: false),
                    irs = table.Column<decimal>(nullable: false),
                    outras_despesas = table.Column<decimal>(nullable: false),
                    outras_regalias = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SALARIO", x => x.id_salario);
                });

            migrationBuilder.CreateTable(
                name: "VIATURA",
                columns: table => new
                {
                    id_viatura = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    matricula = table.Column<string>(nullable: true),
                    marca = table.Column<string>(nullable: true),
                    modelo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VIATURA", x => x.id_viatura);
                });

            migrationBuilder.CreateTable(
                name: "FORNECEDOR",
                columns: table => new
                {
                    id_fornecedor = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    sugestao_categoria = table.Column<int>(nullable: true),
                    sugestao_custo = table.Column<bool>(nullable: true),
                    nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FORNECEDOR", x => x.id_fornecedor);
                    table.ForeignKey(
                        name: "FK_FORNECEDOR_CATEGORIA_sugestao_categoria",
                        column: x => x.sugestao_categoria,
                        principalTable: "CATEGORIA",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DADOS_PHC",
                columns: table => new
                {
                    id_phc = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_fornecedor = table.Column<int>(nullable: false),
                    custo_interno = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DADOS_PHC", x => x.id_phc);
                    table.ForeignKey(
                        name: "FK_DADOS_PHC_FORNECEDOR_id_fornecedor",
                        column: x => x.id_fornecedor,
                        principalTable: "FORNECEDOR",
                        principalColumn: "id_fornecedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CUSTO",
                columns: table => new
                {
                    id_custo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_colaborador = table.Column<int>(nullable: false),
                    id_categoria = table.Column<int>(nullable: false),
                    id_gsm = table.Column<int>(nullable: true),
                    id_phc = table.Column<int>(nullable: true),
                    id_viatura = table.Column<int>(nullable: true),
                    id_salario = table.Column<int>(nullable: true),
                    data = table.Column<DateTime>(nullable: false),
                    ano = table.Column<int>(nullable: false),
                    mes = table.Column<string>(nullable: true),
                    designacao = table.Column<string>(nullable: true),
                    valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTO", x => x.id_custo);
                    table.ForeignKey(
                        name: "FK_CUSTO_CATEGORIA_id_categoria",
                        column: x => x.id_categoria,
                        principalTable: "CATEGORIA",
                        principalColumn: "id_categoria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CUSTO_GSM_id_gsm",
                        column: x => x.id_gsm,
                        principalTable: "GSM",
                        principalColumn: "id_gsm",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CUSTO_DADOS_PHC_id_phc",
                        column: x => x.id_phc,
                        principalTable: "DADOS_PHC",
                        principalColumn: "id_phc",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CUSTO_SALARIO_id_salario",
                        column: x => x.id_salario,
                        principalTable: "SALARIO",
                        principalColumn: "id_salario",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CUSTO_VIATURA_id_viatura",
                        column: x => x.id_viatura,
                        principalTable: "VIATURA",
                        principalColumn: "id_viatura",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UTILIZADOR",
                columns: table => new
                {
                    id_colaborador = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(nullable: true),
                    id_perfil = table.Column<int>(nullable: false),
                    id_equipa = table.Column<string>(nullable: true),
                    data_admissao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UTILIZADOR", x => x.id_colaborador);
                    table.ForeignKey(
                        name: "FK_UTILIZADOR_PERFIL_id_perfil",
                        column: x => x.id_perfil,
                        principalTable: "PERFIL",
                        principalColumn: "id_perfil",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ATRIBUICAO",
                columns: table => new
                {
                    id_atribuicao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_viatura = table.Column<int>(nullable: true),
                    id_gsm = table.Column<int>(nullable: true),
                    id_colaborador = table.Column<int>(nullable: false),
                    data_inicio = table.Column<DateTime>(nullable: false),
                    data_fim = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATRIBUICAO", x => x.id_atribuicao);
                    table.ForeignKey(
                        name: "FK_ATRIBUICAO_UTILIZADOR_id_colaborador",
                        column: x => x.id_colaborador,
                        principalTable: "UTILIZADOR",
                        principalColumn: "id_colaborador",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ATRIBUICAO_GSM_id_gsm",
                        column: x => x.id_gsm,
                        principalTable: "GSM",
                        principalColumn: "id_gsm",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ATRIBUICAO_VIATURA_id_viatura",
                        column: x => x.id_viatura,
                        principalTable: "VIATURA",
                        principalColumn: "id_viatura",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EQUIPA",
                columns: table => new
                {
                    id_equipa = table.Column<string>(nullable: false),
                    id_lider = table.Column<int>(nullable: false),
                    nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EQUIPA", x => x.id_equipa);
                    table.ForeignKey(
                        name: "FK_EQUIPA_UTILIZADOR_id_lider",
                        column: x => x.id_lider,
                        principalTable: "UTILIZADOR",
                        principalColumn: "id_colaborador",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UTILIZADOR_PERMISSAO",
                columns: table => new
                {
                    id_utilizador_permissao = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    id_colaborador = table.Column<int>(nullable: false),
                    id_permissao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UTILIZADOR_PERMISSAO", x => x.id_utilizador_permissao);
                    table.ForeignKey(
                        name: "FK_UTILIZADOR_PERMISSAO_UTILIZADOR_id_colaborador",
                        column: x => x.id_colaborador,
                        principalTable: "UTILIZADOR",
                        principalColumn: "id_colaborador",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UTILIZADOR_PERMISSAO_PERMISSAO_id_permissao",
                        column: x => x.id_permissao,
                        principalTable: "PERMISSAO",
                        principalColumn: "id_permissao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ATRIBUICAO_id_colaborador",
                table: "ATRIBUICAO",
                column: "id_colaborador");

            migrationBuilder.CreateIndex(
                name: "IX_ATRIBUICAO_id_gsm",
                table: "ATRIBUICAO",
                column: "id_gsm");

            migrationBuilder.CreateIndex(
                name: "IX_ATRIBUICAO_id_viatura",
                table: "ATRIBUICAO",
                column: "id_viatura");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTO_id_categoria",
                table: "CUSTO",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTO_id_colaborador",
                table: "CUSTO",
                column: "id_colaborador");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTO_id_gsm",
                table: "CUSTO",
                column: "id_gsm");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTO_id_phc",
                table: "CUSTO",
                column: "id_phc");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTO_id_salario",
                table: "CUSTO",
                column: "id_salario");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTO_id_viatura",
                table: "CUSTO",
                column: "id_viatura");

            migrationBuilder.CreateIndex(
                name: "IX_DADOS_PHC_id_fornecedor",
                table: "DADOS_PHC",
                column: "id_fornecedor");

            migrationBuilder.CreateIndex(
                name: "IX_EQUIPA_id_lider",
                table: "EQUIPA",
                column: "id_lider",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FORNECEDOR_sugestao_categoria",
                table: "FORNECEDOR",
                column: "sugestao_categoria");

            migrationBuilder.CreateIndex(
                name: "IX_UTILIZADOR_id_equipa",
                table: "UTILIZADOR",
                column: "id_equipa");

            migrationBuilder.CreateIndex(
                name: "IX_UTILIZADOR_id_perfil",
                table: "UTILIZADOR",
                column: "id_perfil");

            migrationBuilder.CreateIndex(
                name: "IX_UTILIZADOR_PERMISSAO_id_colaborador",
                table: "UTILIZADOR_PERMISSAO",
                column: "id_colaborador");

            migrationBuilder.CreateIndex(
                name: "IX_UTILIZADOR_PERMISSAO_id_permissao",
                table: "UTILIZADOR_PERMISSAO",
                column: "id_permissao");

            migrationBuilder.AddForeignKey(
                name: "FK_CUSTO_UTILIZADOR_id_colaborador",
                table: "CUSTO",
                column: "id_colaborador",
                principalTable: "UTILIZADOR",
                principalColumn: "id_colaborador",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UTILIZADOR_EQUIPA_id_equipa",
                table: "UTILIZADOR",
                column: "id_equipa",
                principalTable: "EQUIPA",
                principalColumn: "id_equipa",
                onDelete: ReferentialAction.Restrict);
                */
        }
        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EQUIPA_UTILIZADOR_id_lider",
                table: "EQUIPA");

            migrationBuilder.DropTable(
                name: "ATRIBUICAO");

            migrationBuilder.DropTable(
                name: "CUSTO");

            migrationBuilder.DropTable(
                name: "UTILIZADOR_PERMISSAO");

            migrationBuilder.DropTable(
                name: "GSM");

            migrationBuilder.DropTable(
                name: "DADOS_PHC");

            migrationBuilder.DropTable(
                name: "SALARIO");

            migrationBuilder.DropTable(
                name: "VIATURA");

            migrationBuilder.DropTable(
                name: "PERMISSAO");

            migrationBuilder.DropTable(
                name: "FORNECEDOR");

            migrationBuilder.DropTable(
                name: "CATEGORIA");

            migrationBuilder.DropTable(
                name: "UTILIZADOR");

            migrationBuilder.DropTable(
                name: "EQUIPA");

            migrationBuilder.DropTable(
                name: "PERFIL");
        }
    }
    
}
