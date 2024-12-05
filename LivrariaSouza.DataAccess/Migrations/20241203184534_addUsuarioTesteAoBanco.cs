using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivrariaSouza.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addUsuarioTesteAoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "IdUsuario", "Email", "Nome", "Senha", "Telefone" },
                values: new object[] { 1, "Eduardo.Azoia@valtech.com", "Eduardo Azoia", "123456", "(48) 91234-8754" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1);
        }
    }
}
