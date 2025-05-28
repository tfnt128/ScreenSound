using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Data
{
    internal class MusicDal
    {
        private readonly ScreenSoundContext _context;

        public MusicDal(ScreenSoundContext context)
        {
            _context = context;
        }

        public IEnumerable<Musica> Listar()
        {
            return _context.Musicas.ToList();
        }

        public void AddMusic(Musica musica)
        {
            _context.Musicas.Add(musica);
            _context.SaveChanges();
        }

        public void DeleteMusic(Musica musica)
        {
            _context.Musicas.Remove(musica);
            _context.SaveChanges();
        }

        public void UpdateArtist(Musica musica)
        {
            _context.Musicas.Update(musica);
            _context.SaveChanges();
        }

        public Musica? RecuperarPeloNome(string nome)
        {
            return _context.Musicas.FirstOrDefault(a => a.Nome.Equals(nome));
        }
    }
}
