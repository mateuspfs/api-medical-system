using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaMedico.Migrations
{
    public partial class AddCodigoEspecialidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Especialidades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Especialidades");
        }
    }
}
