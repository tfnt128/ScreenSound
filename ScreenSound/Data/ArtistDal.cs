using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Data
{
    internal class ArtistDal
    {
        private readonly ScreenSoundContext _context;

        public ArtistDal(ScreenSoundContext context)
        {
            _context = context;
        }

        public IEnumerable<Artista> Listar()
        {
             return _context.Artistas.ToList();            
        }

        public void AddArtist(Artista artista)
        {   
            _context.Artistas.Add(artista);
            _context.SaveChanges();
        }

        public void DeleteArtist(Artista artista)
        {
            _context.Artistas.Remove(artista);
            _context.SaveChanges();
        }

        public void UpdateArtist(Artista artista)
        {
            _context.Artistas.Update(artista);
            _context.SaveChanges();
        }
        public IEnumerable<Artista> RecuperarPeloNome(string nome)
        {
            return _context.Artistas.Where(x => x.Nome == nome).ToList();
        }
    }
}
