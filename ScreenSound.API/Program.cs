using Microsoft.AspNetCore.Mvc;
using ScreenSound.Data;
using ScreenSound.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();

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
        return Results.NotFound($"Artista '{nome}' não encontrado.");
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
        return Results.NotFound($"Artista com o ID: '{id}' não encontrado.");
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
            return Results.NotFound($"Artista com o ID: {artista.Id} ou com o nome '{artista.Nome}' não encontrado.");
        }
            
    }
    artistaAtualizado.Nome = artista.Nome;
    artistaAtualizado.Bio = artista.Bio;
    artistaAtualizado.FotoPerfil = artista.FotoPerfil;

    dal.Update(artistaAtualizado);

    return Results.Ok(artista);
});


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
    musicaAtualizado.Artist = musica.Artist;

    dal.Update(musicaAtualizado);

    return Results.Ok(musica);
});





app.Run();
