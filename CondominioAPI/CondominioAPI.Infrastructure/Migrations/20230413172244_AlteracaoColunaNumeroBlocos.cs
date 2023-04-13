using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CondominioAPI.Infrastructure.Migrations
{
    public partial class AlteracaoColunaNumeroBlocos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TB_Condominio",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "COND_NumeroBlocos",
                table: "TB_Condominio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TB_Condominio",
                newName: "ID");

            migrationBuilder.AlterColumn<int>(
                name: "COND_NumeroBlocos",
                table: "TB_Condominio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
