using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivrariaSouza.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mudancaColunaDeVendaTabelaDetalhesVenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalhesVendas_Usuarios_IdUsuario",
                table: "DetalhesVendas");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "DetalhesVendas",
                newName: "RegistroVendaIdCompra");

            migrationBuilder.RenameIndex(
                name: "IX_DetalhesVendas_IdUsuario",
                table: "DetalhesVendas",
                newName: "IX_DetalhesVendas_RegistroVendaIdCompra");

            migrationBuilder.AddColumn<int>(
                name: "IdRegistroVenda",
                table: "DetalhesVendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_DetalhesVendas_RegistroDeVendas_RegistroVendaIdCompra",
                table: "DetalhesVendas",
                column: "RegistroVendaIdCompra",
                principalTable: "RegistroDeVendas",
                principalColumn: "IdCompra",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalhesVendas_RegistroDeVendas_RegistroVendaIdCompra",
                table: "DetalhesVendas");

            migrationBuilder.DropColumn(
                name: "IdRegistroVenda",
                table: "DetalhesVendas");

            migrationBuilder.RenameColumn(
                name: "RegistroVendaIdCompra",
                table: "DetalhesVendas",
                newName: "IdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_DetalhesVendas_RegistroVendaIdCompra",
                table: "DetalhesVendas",
                newName: "IX_DetalhesVendas_IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalhesVendas_Usuarios_IdUsuario",
                table: "DetalhesVendas",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
