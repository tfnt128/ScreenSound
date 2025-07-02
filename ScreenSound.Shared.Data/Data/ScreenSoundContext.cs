using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Data
{
    public class ScreenSoundContext : DbContext
    {
        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Genero> Generos { get; set; }

        private string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=ScreenSoundV0;Integrated Security=True;" +
            "Encrypt=False;Trust Server Certificate=False;" +
            "Application Intent=ReadWrite;Multi Subnet Failover=False";


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_connectionString)
                .UseLazyLoadingProxies();
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Musica>()
                .HasMany(m => m.Generos)
                .WithMany(g => g.Musicas);
        }
    }
}
