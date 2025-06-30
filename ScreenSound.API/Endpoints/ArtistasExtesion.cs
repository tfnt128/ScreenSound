using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.Data;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class ArtistasExtesion
    {

        public static void MapArtistasEndpoints(this WebApplication app)
        {
            app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) => {
                return Results.Ok(dal.Listar());
            });

            app.MapGet("/Artistas/{nome}", (string nome, [FromServices] DAL<Artista> dal) => {
                var artista = dal.Recuperar(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (artista == null)
                {
                    return Results.NotFound($"Artista '{nome}' não encontrado.");
                }
                return Results.Ok(artista);
            });

            app.MapPost("/Artistas", ([FromBody] ArtistaRequest artistaRequest, [FromServices] DAL<Artista> dal) => {
                var artista = new Artista(artistaRequest.Nome, artistaRequest.Bio);
                dal.Add(artista);
                return Results.Ok(artista);
            });

            app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) => {
                var artista = dal.Recuperar(a => a.Id == id);
                if (artista == null)
                {
                    return Results.NotFound($"Artista com o ID: '{id}' não encontrado.");
                }
                dal.Delete(artista);
                return Results.Ok(artista);
            });

            app.MapPut("/Artistas", ([FromBody] ArtistaRequestEdit artistaRequestEdit, [FromServices] DAL<Artista> dal) => {
                var artistaAtualizado = dal.Recuperar(a => a.Id == artistaRequestEdit.Id);
                if (artistaAtualizado == null)
                {
                    artistaAtualizado = dal.Recuperar(a => a.Nome.ToUpper().Equals(artistaRequestEdit.Nome.ToUpper()));
                    if (artistaAtualizado == null)
                    {
                        return Results.NotFound($"Artista com o ID: {artistaRequestEdit.Id} ou com o nome '{artistaRequestEdit.Nome}' não encontrado.");
                    }

                }
                artistaAtualizado.Nome = artistaRequestEdit.Nome;
                artistaAtualizado.Bio = artistaRequestEdit.Bio;

                dal.Update(artistaAtualizado);

                return Results.Ok();
            });
        }
    }
}
