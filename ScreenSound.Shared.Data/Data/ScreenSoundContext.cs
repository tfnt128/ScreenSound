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

        private string _connectionStringRemote = "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=ScreenSoundV0;Integrated Security=True;" +
            "Encrypt=False;Trust Server Certificate=False;" +
            "Application Intent=ReadWrite;Multi Subnet Failover=False";

        private string _connectionStringCloud = "Server=tcp:screen-sound-server.database.windows.net,1433;Initial Catalog=ScreenSoundV0;Persist Security Info=False;" +
            "User ID=thiago;Password=Azure546478!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_connectionStringCloud)
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
