using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivrariaSouza.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ajusteNaTabelaDetalhesVendaPt2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdRegistroVenda",
                table: "DetalhesVendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DetalhesVendas_IdRegistroVenda",
                table: "DetalhesVendas",
                column: "IdRegistroVenda");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalhesVendas_RegistroDeVendas_IdRegistroVenda",
                table: "DetalhesVendas",
                column: "IdRegistroVenda",
                principalTable: "RegistroDeVendas",
                principalColumn: "IdCompra",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalhesVendas_RegistroDeVendas_IdRegistroVenda",
                table: "DetalhesVendas");

            migrationBuilder.DropIndex(
                name: "IX_DetalhesVendas_IdRegistroVenda",
                table: "DetalhesVendas");

            migrationBuilder.DropColumn(
                name: "IdRegistroVenda",
                table: "DetalhesVendas");
        }
    }
}
