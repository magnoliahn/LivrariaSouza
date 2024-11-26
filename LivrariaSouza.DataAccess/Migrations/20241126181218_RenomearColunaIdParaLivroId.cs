using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivrariaSouza.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenomearColunaIdParaLivroId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Renomeando a coluna "Id" para "LivroId" na tabela Livros
            migrationBuilder.RenameColumn(
                name: "Id",  // Nome antigo da coluna
                table: "Livros",  // Nome da tabela
                newName: "LivroId"  // Novo nome da coluna
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Caso precise desfazer a migração, renomeamos novamente para o nome antigo
            migrationBuilder.RenameColumn(
                name: "LivroId",  // Novo nome da coluna
                table: "Livros",  // Nome da tabela
                newName: "Id"  // Nome antigo da coluna
            );
        }
    }

}
