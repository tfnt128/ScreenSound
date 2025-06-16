using Microsoft.AspNetCore.Mvc;
using ScreenSound.Data;
using ScreenSound.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure
    <Microsoft.AspNetCore.Http.Json.JsonOptions>
    (options => options.SerializerOptions.ReferenceHandler
    = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

app.MapGet("/Artistas", () => { 
    var dal = new  DAL<Artista>(new ScreenSoundContext());
    return Results.Ok(dal.Listar());
});

app.MapGet("/Artistas/{nome}", (string nome) => {
    var dal = new DAL<Artista>(new ScreenSoundContext());
    var artista = dal.Recuperar(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
    if (artista == null)
    {
        return Results.NotFound($"Artista {nome} não encontrado.");
    }
    return Results.Ok(artista);
});

app.MapPost("/Artistas", ([FromBody]Artista artista) => {
    var dal = new DAL<Artista>(new ScreenSoundContext());
    dal.Add(artista);
    return Results.Ok(artista);
});

app.Run();
