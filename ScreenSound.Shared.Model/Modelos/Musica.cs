using System.ComponentModel.DataAnnotations;

namespace ScreenSound.Modelos;

public class Musica
{
    public virtual ICollection<Genero> Generos { get; set; } = new List<Genero>();
    public Musica() { }
    public Musica(string nome)
    {
        Nome = nome;
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