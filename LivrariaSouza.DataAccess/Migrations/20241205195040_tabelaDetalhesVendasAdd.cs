using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivrariaSouza.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class tabelaDetalhesVendasAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapaLivro",
                table: "RegistroDeVendas");

            migrationBuilder.DropColumn(
                name: "LivroId",
                table: "RegistroDeVendas");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "RegistroDeVendas");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "RegistroDeVendas");

            migrationBuilder.DropColumn(
                name: "ValorUnit",
                table: "RegistroDeVendas");

            migrationBuilder.CreateTable(
                name: "DetalhesVendas",
                columns: table => new
                {
                    IdDetalhe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    LivroId = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapaLivro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorUnit = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalhesVendas", x => x.IdDetalhe);
                    table.ForeignKey(
                        name: "FK_DetalhesVendas_Livros_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livros",
                        principalColumn: "LivroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalhesVendas_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalhesVendas_IdUsuario",
                table: "DetalhesVendas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_DetalhesVendas_LivroId",
                table: "DetalhesVendas",
                column: "LivroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalhesVendas");

            migrationBuilder.AddColumn<string>(
                name: "CapaLivro",
                table: "RegistroDeVendas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LivroId",
                table: "RegistroDeVendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "RegistroDeVendas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "RegistroDeVendas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ValorUnit",
                table: "RegistroDeVendas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
