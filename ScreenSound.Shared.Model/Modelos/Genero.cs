namespace ScreenSound.Modelos;

public class Genero
{
    public virtual ICollection<Musica> Musicas { get; set; } = new List<Musica>();
    public int Id { get; set; }
    public string? Nome { get; set; } 
    public string? Descricao { get; set; }

    public override string ToString()
    {
        return $"Nome: {Nome} - Descrição: {Descricao}";
    }
}

