using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivrariaSouza.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class correcaoNomesColunasComValores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Livros",
                newName: "ValorVenda");

            migrationBuilder.RenameColumn(
                name: "Custo",
                table: "Livros",
                newName: "ValorCompra");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValorVenda",
                table: "Livros",
                newName: "Valor");

            migrationBuilder.RenameColumn(
                name: "ValorCompra",
                table: "Livros",
                newName: "Custo");
        }
    }
}
