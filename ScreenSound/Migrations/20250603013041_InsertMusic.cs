using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class InsertMusic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Musicas", new string[]
           {"Nome", "ReleaseYear"},
           new object[] {"Musica Exemplo", 1997});

            migrationBuilder.InsertData("Musicas", new string[]
           {"Nome", "ReleaseYear"},
           new object[] { "Under The Bridge", 1999 });

            migrationBuilder.InsertData("Musicas", new string[]
           {"Nome", "ReleaseYear"},
           new object[] { "Yellow Submarine", 1968 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Musicas");
        }
    }
}
