using Microsoft.AspNetCore.Mvc;
using ScreenSound.Data;
using ScreenSound.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();

builder.Services.Configure
    <Microsoft.AspNetCore.Http.Json.JsonOptions>
    (options => options.SerializerOptions.ReferenceHandler
    = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) => { 
    return Results.Ok(dal.Listar());
});

app.MapGet("/Artistas/{nome}", (string nome, [FromServices] DAL<Artista> dal) => {
    var artista = dal.Recuperar(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
    if (artista == null)
    {
        return Results.NotFound($"Artista '{nome}' n�o encontrado.");
    }
    return Results.Ok(artista);
});

app.MapPost("/Artistas", ([FromBody]Artista artista, [FromServices] DAL<Artista> dal) => {
    dal.Add(artista);
    return Results.Ok(artista);
});

app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) => {
    var artista = dal.Recuperar(a => a.Id == id);
    if (artista == null)
    {
        return Results.NotFound($"Artista com o ID: '{id}' n�o encontrado.");
    }
    dal.Delete(artista);
    return Results.Ok(artista);
});

app.MapPut("/Artistas", ([FromBody] Artista artista, [FromServices] DAL<Artista> dal) => {
    var artistaAtualizado = dal.Recuperar(a => a.Id == artista.Id);
    if (artistaAtualizado == null)
    {
        artistaAtualizado = dal.Recuperar(a => a.Nome.ToUpper().Equals(artista.Nome.ToUpper()));
        if (artistaAtualizado == null)
        {
            return Results.NotFound($"Artista com o ID: {artista.Id} ou com o nome '{artista.Nome}' n�o encontrado.");
        }
            
    }
    artistaAtualizado.Nome = artista.Nome;
    artistaAtualizado.Bio = artista.Bio;
    artistaAtualizado.FotoPerfil = artista.FotoPerfil;
    artistaAtualizado.Id = artista.Id;

    dal.Update(artistaAtualizado);

    return Results.Ok(artista);
});



app.Run();
