using ScreenSound.Modelos;
using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Requests
{
    public record MusicaRequest([Required] string Nome, [Required] int ArtistaId, int ReleaseYear, ICollection<GeneroRequest> Generos=null);
}
