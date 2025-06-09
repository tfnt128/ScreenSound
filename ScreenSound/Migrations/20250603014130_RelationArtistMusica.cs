using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class RelationArtistMusica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "Musicas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musicas_ArtistId",
                table: "Musicas",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Musicas_Artistas_ArtistId",
                table: "Musicas",
                column: "ArtistId",
                principalTable: "Artistas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musicas_Artistas_ArtistId",
                table: "Musicas");

            migrationBuilder.DropIndex(
                name: "IX_Musicas_ArtistId",
                table: "Musicas");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Musicas");
        }
    }
}
