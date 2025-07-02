using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Data;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class MusicasExtension
    {
        public static void MapMusicasEndpoints(this WebApplication app)
        {
            app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) => {
                var listaDeMusicas = dal.Listar();
                if (listaDeMusicas is null)
                {
                    return Results.NotFound();
                }
                var listaDeMusicaResponse = EntityListToResponseList(listaDeMusicas);
                return Results.Ok(listaDeMusicaResponse);
            });

            app.MapGet("/Musicas/{nome}", (string nome, [FromServices] DAL<Musica> dal) => {
                var musica = dal.Recuperar(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (musica == null)
                {
                    return Results.NotFound($"Artista '{nome}' não encontrado.");
                }
                return Results.Ok(EntityToResponse(musica));
            });

            app.MapPost("/Musicas", ([FromBody] MusicaRequest musicaRequest, [FromServices] DAL<Musica> dalMusica,
                [FromServices] DAL<Genero> dalGen) => {
                var musica = new Musica(musicaRequest.Nome)
                {
                    ArtistId = musicaRequest.ArtistaId,
                    ReleaseYear = musicaRequest.ReleaseYear,
                    Generos = musicaRequest.Generos is not null?
                    GeneroRequestConverter(musicaRequest.Generos, dalGen): new List<Genero>()   
                };
                dalMusica.Add(musica);
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

            app.MapPut("/Musicas", ([FromBody] MusicaRequestEdit musicaRequestEdit, [FromServices] DAL<Musica> dal) => {
                var musicaAtualizado = dal.Recuperar(a => a.Id == musicaRequestEdit.Id);
                if (musicaAtualizado == null)
                {
                    musicaAtualizado = dal.Recuperar(a => a.Nome.ToUpper().Equals(musicaRequestEdit.Nome.ToUpper()));
                    if (musicaAtualizado == null)
                    {
                        return Results.NotFound($"Música com o ID: {musicaRequestEdit.Id} ou com o nome '{musicaRequestEdit.Nome}' não encontrado.");
                    }

                }
                musicaAtualizado.Nome = musicaRequestEdit.Nome;
                musicaAtualizado.ReleaseYear = musicaRequestEdit.ReleaseYear;

                dal.Update(musicaAtualizado);

                return Results.Ok();
            });
        }

        private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos, DAL<Genero> dalGen)
        {
            var listaDeGeneros = new List<Genero>();
            foreach(var item in generos)
            {
                var entity = EntityToRequest(item);
                var genero = dalGen.Recuperar(a => a.Nome.ToUpper().Equals(item.Nome.ToUpper()));
                if (genero != null)
                    listaDeGeneros.Add(genero);
                else
                    listaDeGeneros.Add(entity);
            }

            return listaDeGeneros;
        }

        private static ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> listaDeMusicas)
        {
            return listaDeMusicas.Select(a => EntityToResponse(a)).ToList();
        }

        private static MusicaResponse EntityToResponse(Musica musica)
        {
            return new MusicaResponse(musica.Id, musica.Nome!, musica.Artist!.Id, musica.Artist.Nome);
        }
        private static Genero EntityToRequest(GeneroRequest genero)
        {
            return new Genero() 
            {
                Nome = genero.Nome,
                Descricao = genero.Descricao,
            };
        }
    }
}
