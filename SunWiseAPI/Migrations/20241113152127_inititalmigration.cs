using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SunWiseAPI.Migrations
{
    /// <inheritdoc />
    public partial class inititalmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(1)", nullable: false),
                    UserId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projetos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NomeProjeto = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Orçamento = table.Column<float>(type: "BINARY_FLOAT", nullable: false),
                    ConsumoMensalKwh = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false),
                    TarifaEnergia = table.Column<decimal>(type: "NUMBER(10,2)", nullable: false),
                    EconomiaMensal = table.Column<decimal>(type: "NUMBER(10,2)", nullable: true),
                    RetornoInvestimentoMeses = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    EconomiaAcumulada10Anos = table.Column<decimal>(type: "NUMBER(10,2)", nullable: true),
                    ImpactoAmbiental = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ClienteId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UserId = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projetos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Email = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    Uid = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NomeEmpresa = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Email);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Projetos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
