using System.ComponentModel.DataAnnotations;

namespace ScreenSound.Modelos;

public class Musica
{

    public Musica() { }
    public Musica(string nome, int artistId, int releaseYear)
    {
        Nome = nome;
        ArtistId = artistId;
        ReleaseYear = releaseYear;
    }

    [Required] public string Nome { get; set; }
    public int Id { get; set; }
    public int? ReleaseYear { get; set; }
    public int? ArtistId { get; set; }
    public virtual Artista? Artist { get; set; }

    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"Nome: {Nome}");
      
    }

    public override string ToString()
    {
        return @$"Id: {Id}
        Nome: {Nome}";
    }
}