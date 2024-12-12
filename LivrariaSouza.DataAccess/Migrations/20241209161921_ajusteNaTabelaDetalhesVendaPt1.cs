using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivrariaSouza.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ajusteNaTabelaDetalhesVendaPt1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalhesVendas_RegistroDeVendas_RegistroVendaIdCompra",
                table: "DetalhesVendas");

            migrationBuilder.DropIndex(
                name: "IX_DetalhesVendas_RegistroVendaIdCompra",
                table: "DetalhesVendas");

            migrationBuilder.DropColumn(
                name: "IdRegistroVenda",
                table: "DetalhesVendas");

            migrationBuilder.DropColumn(
                name: "RegistroVendaIdCompra",
                table: "DetalhesVendas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdRegistroVenda",
                table: "DetalhesVendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RegistroVendaIdCompra",
                table: "DetalhesVendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DetalhesVendas_RegistroVendaIdCompra",
                table: "DetalhesVendas",
                column: "RegistroVendaIdCompra");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalhesVendas_RegistroDeVendas_RegistroVendaIdCompra",
                table: "DetalhesVendas",
                column: "RegistroVendaIdCompra",
                principalTable: "RegistroDeVendas",
                principalColumn: "IdCompra",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
