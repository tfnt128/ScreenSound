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
        public IEnumerable<Artista> Listar()
        {
            var lista = new List<Artista>();
            using var connection = new Connection().GetConnection();
            connection.Open();

            string sql = "SELECT * FROM Artistas";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var id = Convert.ToInt32(reader["ID"]);
                var nome = Convert.ToString(reader["NOME"]);
                var bio = Convert.ToString(reader["BIO"]);
                var fotoPerfil = Convert.ToString(reader["FOTOPERFIL"]);

                Artista artistaObj = new Artista(nome, bio, id)
                {
                    Id = id,
                    FotoPerfil = fotoPerfil
                };

                lista.Add(artistaObj);
            }
            return lista;
        }

        public void AddArtist(Artista artista)
        {
            using var connection = new Connection().GetConnection();
            connection.Open();

            string sql = "INSERT INTO Artistas (NOME, BIO, FOTOPERFIL) VALUES (@nome, @bio, @fotoPerfil)";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@nome", artista.Nome);
            command.Parameters.AddWithValue("@bio", artista.Bio);
            command.Parameters.AddWithValue("@fotoPerfil", artista.FotoPerfil);

            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {rowsAffected} ");
        }

        public void DeleteArtist(Artista artista)
        {
            using var connection = new Connection().GetConnection();
            connection.Open();

            string sql = $"DELETE FROM Artistas WHERE Id = @id";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", artista.Id);

            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {rowsAffected} ");
        }

        public void UpdateArtist(Artista artista)
        {
            using var connection = new Connection().GetConnection();
            connection.Open();

            string sql = $"UPDATE Artistas SET Nome = @nome, Bio = @bio WHERE Id = @id";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@nome", artista.Nome);
            command.Parameters.AddWithValue("@bio", artista.Bio);
            command.Parameters.AddWithValue("@fotoPerfil", artista.FotoPerfil);
            command.Parameters.AddWithValue("@id", artista.Id);

            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {rowsAffected} ");
        }
    }
}
