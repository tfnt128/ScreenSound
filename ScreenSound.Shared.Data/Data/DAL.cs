using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Data
{
    public class DAL<T> where T : class
    {

        private readonly ScreenSoundContext _context;

        public DAL(ScreenSoundContext context)
        {
            _context = context;
        }
        public IEnumerable<T> Listar()
        {
            return _context.Set<T>().ToList();
        }
        public void Add(T obj)
        {
            _context.Set<T>().Add(obj);
            _context.SaveChanges();
        }
        public void Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
            _context.SaveChanges();
        }
        public void Update(T obj)
        {
            _context.Set<T>().Update(obj);
            _context.SaveChanges();
        }
        public T? Recuperar(Func<T, bool> condicao)
        {
            return _context.Set<T>().FirstOrDefault(condicao);
        }
    }
}
