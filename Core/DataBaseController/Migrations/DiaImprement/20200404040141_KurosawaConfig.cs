﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace DataBaseController.Migrations.DiaImprement
{
    public partial class KurosawaConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfiguracoesServidores",
                columns: table => new
                {
                    cod = table.Column<int>(type: "int", nullable: false),
                    key = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracoesServidores", x => x.cod);
                });

            migrationBuilder.CreateTable(
                name: "Servidores",
                columns: table => new
                {
                    codigo_servidor = table.Column<long>(type: "bigint", nullable: false),
                    id_servidor = table.Column<long>(type: "bigint", nullable: false),
                    nome_servidor = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8"),
                    especial_servidor = table.Column<sbyte>(type: "tinyint", nullable: false, defaultValue: (sbyte)0),
                    prefix_servidor = table.Column<string>(type: "varchar(25)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servidores", x => x.codigo_servidor);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    codigo_usuario = table.Column<long>(type: "bigint", nullable: false),
                    id_usuario = table.Column<long>(type: "bigint", nullable: false),
                    nome_usuario = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.codigo_usuario);
                });

            migrationBuilder.CreateTable(
                name: "Canais",
                columns: table => new
                {
                    cod = table.Column<long>(type: "bigint", nullable: false),
                    TipoCanal = table.Column<byte>(nullable: false),
                    nome = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8"),
                    id = table.Column<long>(type: "bigint", nullable: false),
                    codigo_servidor = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canais", x => x.cod);
                    table.ForeignKey(
                        name: "FK_Canais_Servidores_codigo_servidor",
                        column: x => x.codigo_servidor,
                        principalTable: "Servidores",
                        principalColumn: "codigo_servidor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    cod = table.Column<long>(type: "bigint", nullable: false),
                    tipoCargos = table.Column<sbyte>(type: "tinyint", nullable: false, defaultValue: (sbyte)0),
                    nome = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8"),
                    id = table.Column<long>(type: "bigint", nullable: false),
                    codigo_servidor = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.cod);
                    table.ForeignKey(
                        name: "FK_Cargos_Servidores_codigo_servidor",
                        column: x => x.codigo_servidor,
                        principalTable: "Servidores",
                        principalColumn: "codigo_servidor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracoesServidoresAplicada",
                columns: table => new
                {
                    cod = table.Column<long>(type: "bigint", nullable: false),
                    servidor = table.Column<long>(nullable: true),
                    configuracoes = table.Column<int>(nullable: true),
                    valor = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracoesServidoresAplicada", x => x.cod);
                    table.ForeignKey(
                        name: "FK_ConfiguracoesServidoresAplicada_ConfiguracoesServidores_conf~",
                        column: x => x.configuracoes,
                        principalTable: "ConfiguracoesServidores",
                        principalColumn: "cod",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConfiguracoesServidoresAplicada_Servidores_servidor",
                        column: x => x.servidor,
                        principalTable: "Servidores",
                        principalColumn: "codigo_servidor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomReactions",
                columns: table => new
                {
                    cod_cr = table.Column<long>(type: "bigint", nullable: false),
                    trigger_cr = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8"),
                    resposta_cr = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8"),
                    modo_cr = table.Column<bool>(type: "bool", nullable: false),
                    servidor_cr = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomReactions", x => x.cod_cr);
                    table.ForeignKey(
                        name: "FK_CustomReactions_Servidores_servidor_cr",
                        column: x => x.servidor_cr,
                        principalTable: "Servidores",
                        principalColumn: "codigo_servidor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdmsBot",
                columns: table => new
                {
                    cod = table.Column<long>(type: "bigint", nullable: false),
                    usuario = table.Column<long>(nullable: true),
                    permissao = table.Column<sbyte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmsBot", x => x.cod);
                    table.ForeignKey(
                        name: "FK_AdmsBot_Usuarios_usuario",
                        column: x => x.usuario,
                        principalTable: "Usuarios",
                        principalColumn: "codigo_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fuck",
                columns: table => new
                {
                    cod = table.Column<long>(type: "bigint", nullable: false),
                    codigo_usuario = table.Column<long>(nullable: true),
                    urlImage = table.Column<string>(type: "varchar(255)", nullable: false),
                    explicitImage = table.Column<bool>(type: "bool", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuck", x => x.cod);
                    table.ForeignKey(
                        name: "FK_Fuck_Usuarios_codigo_usuario",
                        column: x => x.codigo_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "codigo_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Insultos",
                columns: table => new
                {
                    cod = table.Column<long>(type: "bigint", nullable: false),
                    usuario = table.Column<long>(nullable: true),
                    insulto = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insultos", x => x.cod);
                    table.ForeignKey(
                        name: "FK_Insultos_Usuarios_usuario",
                        column: x => x.usuario,
                        principalTable: "Usuarios",
                        principalColumn: "codigo_usuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Servidores_Usuarios",
                columns: table => new
                {
                    Servidores_codigo_servidor = table.Column<long>(type: "bigint", nullable: false),
                    Usuarios_codigo_usuario = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servidores_Usuarios", x => new { x.Servidores_codigo_servidor, x.Usuarios_codigo_usuario });
                    table.ForeignKey(
                        name: "FK_Servidores_Usuarios_Servidores_Servidores_codigo_servidor",
                        column: x => x.Servidores_codigo_servidor,
                        principalTable: "Servidores",
                        principalColumn: "codigo_servidor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servidores_Usuarios_Usuarios_Usuarios_codigo_usuario",
                        column: x => x.Usuarios_codigo_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "codigo_usuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdmsBot_usuario",
                table: "AdmsBot",
                column: "usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Canais_codigo_servidor",
                table: "Canais",
                column: "codigo_servidor");

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_codigo_servidor",
                table: "Cargos",
                column: "codigo_servidor");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracoesServidores_key",
                table: "ConfiguracoesServidores",
                column: "key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracoesServidoresAplicada_configuracoes",
                table: "ConfiguracoesServidoresAplicada",
                column: "configuracoes");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracoesServidoresAplicada_servidor",
                table: "ConfiguracoesServidoresAplicada",
                column: "servidor");

            migrationBuilder.CreateIndex(
                name: "IX_CustomReactions_servidor_cr",
                table: "CustomReactions",
                column: "servidor_cr");

            migrationBuilder.CreateIndex(
                name: "IX_Fuck_codigo_usuario",
                table: "Fuck",
                column: "codigo_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Insultos_usuario",
                table: "Insultos",
                column: "usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Servidores_id_servidor",
                table: "Servidores",
                column: "id_servidor",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servidores_Usuarios_Usuarios_codigo_usuario",
                table: "Servidores_Usuarios",
                column: "Usuarios_codigo_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_id_usuario",
                table: "Usuarios",
                column: "id_usuario",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdmsBot");

            migrationBuilder.DropTable(
                name: "Canais");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "ConfiguracoesServidoresAplicada");

            migrationBuilder.DropTable(
                name: "CustomReactions");

            migrationBuilder.DropTable(
                name: "Fuck");

            migrationBuilder.DropTable(
                name: "Insultos");

            migrationBuilder.DropTable(
                name: "Servidores_Usuarios");

            migrationBuilder.DropTable(
                name: "ConfiguracoesServidores");

            migrationBuilder.DropTable(
                name: "Servidores");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
