using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CondominioAPI.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Condominio",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    COND_Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    COND_CNPJ = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    COND_Endereco = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    COND_NumeroUnidades = table.Column<int>(type: "int", nullable: false),
                    COND_NumeroBlocos = table.Column<int>(type: "int", nullable: false),
                    COND_DataFundacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Condominio", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_COND_Nome",
                table: "TB_Condominio",
                column: "COND_Nome",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Condominio");
        }
    }
}
