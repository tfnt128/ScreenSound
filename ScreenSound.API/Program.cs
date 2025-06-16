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
        return Results.NotFound($"Artista {nome} não encontrado.");
    }
    return Results.Ok(artista);
});

app.MapPost("/Artistas", ([FromBody]Artista artista, [FromServices] DAL<Artista> dal) => {
    dal.Add(artista);
    return Results.Ok(artista);
});

app.Run();
