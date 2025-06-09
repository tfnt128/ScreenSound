using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class InsertTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Artistas", new string[]
            {"Nome", "FotoPerfil", "Bio" }, 
            new object[] {"Artista Exemplo", "https://example.com/foto.jpg", 
            "Esta é uma biografia de exemplo." });

            migrationBuilder.InsertData("Artistas", new string[]
            {"Nome", "FotoPerfil", "Bio" },
            new object[] {"´Red Hot", "https://example.com/foto.jpg",
            "Red hot chilli peppers biografia insira aqui" });

            migrationBuilder.InsertData("Artistas", new string[]
            {"Nome", "FotoPerfil", "Bio" },
            new object[] {"´Beatles", "https://example.com/foto.jpg",
            "Beatles biografia insira aqui" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Artistas");
        }
    }
}
