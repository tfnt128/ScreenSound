using Microsoft.AspNetCore.Mvc;
using ScreenSound.Data;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class MusicasExtension
    {
        public static void MapMusicasEndpoints(this WebApplication app)
        {
            app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) => {
                return Results.Ok(dal.Listar());
            });

            app.MapGet("/Musicas/{nome}", (string nome, [FromServices] DAL<Musica> dal) => {
                var musica = dal.Recuperar(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (musica == null)
                {
                    return Results.NotFound($"Artista '{nome}' não encontrado.");
                }
                return Results.Ok(musica);
            });

            app.MapPost("/Musicas", ([FromBody] Musica musica, [FromServices] DAL<Musica> dal) => {
                dal.Add(musica);
                return Results.Ok(musica);
            });

            app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) => {
                var musica = dal.Recuperar(a => a.Id == id);
                if (musica == null)
                {
                    return Results.NotFound($"Música com o ID: '{id}' não encontrado.");
                }
                dal.Delete(musica);
                return Results.Ok(musica);
            });

            app.MapPut("/Musicas", ([FromBody] Musica musica, [FromServices] DAL<Musica> dal) => {
                var musicaAtualizado = dal.Recuperar(a => a.Id == musica.Id);
                if (musicaAtualizado == null)
                {
                    musicaAtualizado = dal.Recuperar(a => a.Nome.ToUpper().Equals(musica.Nome.ToUpper()));
                    if (musicaAtualizado == null)
                    {
                        return Results.NotFound($"Música com o ID: {musica.Id} ou com o nome '{musica.Nome}' não encontrado.");
                    }

                }
                musicaAtualizado.Nome = musica.Nome;
                musicaAtualizado.ReleaseYear = musica.ReleaseYear;

                dal.Update(musicaAtualizado);

                return Results.Ok(musica);
            });
        }
    }
}
