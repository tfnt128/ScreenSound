using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Data;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class GenerosExtension
    {
        public static void MapGenerosEndpoints(this WebApplication app)
        {
            app.MapGet("/Generos", ([FromServices] DAL<Genero> dal) => {
                var listaDeGeneros = dal.Listar();
                if (listaDeGeneros is null)
                {
                    return Results.NotFound();
                }
                var listaDeGenerosResponse = EntityListToResponseList(listaDeGeneros);
                return Results.Ok(listaDeGenerosResponse);
            });

            app.MapPost("/Generos", ([FromBody] GeneroRequest generoRequest, [FromServices] DAL<Genero> dal) => {
                var genero = new Genero()
                {
                    Nome = generoRequest.Nome,
                    Descricao = generoRequest.Descricao
                };
                dal.Add(genero);
                return Results.Ok(genero);
            });

            app.MapGet("/Generos/{nome}", (string nome, [FromServices] DAL<Genero> dal) => {
                var genero = dal.Recuperar(a => a.Nome!.ToUpper().Equals(nome.ToUpper()));
                if (genero == null)
                {
                    return Results.NotFound($"Gênero '{nome}' não encontrado.");
                }
                return Results.Ok(EntityToResponse(genero));
            });

            app.MapDelete("/Generos/{id}", ([FromServices] DAL<Genero> dal, int id) => {
                var genero = dal.Recuperar(a => a.Id == id);
                if (genero == null)
                {
                    return Results.NotFound($"Gênero com o ID: '{id}' não encontrado.");
                }
                dal.Delete(genero);
                return Results.Ok(genero);
            });         
        }


        private static ICollection<GeneroResponse> EntityListToResponseList(IEnumerable<Genero> listaDeGeneros)
        {
            return listaDeGeneros.Select(a => EntityToResponse(a)).ToList();
        }

        private static GeneroResponse EntityToResponse(Genero genero)
        {
            return new GeneroResponse(genero.Id, genero.Nome!, genero.Descricao!);
        }

    }
}
