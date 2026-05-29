using Microsoft.Data.Sqlite;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;

namespace MovieManagement.Repositories.SQLite
{
    public class MovieSQLiteRepository : IMovieRepository
    {
        private readonly string _connectionString;

        public MovieSQLiteRepository(string dbPath = "moviemanagement.db")
        {
            _connectionString = $"Data Source={dbPath}";
            CriarTabela();
        }

        private void CriarTabela()
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Movies (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Titulo TEXT NOT NULL,
                Ano INTEGER,
                Lingua TEXT,
                Classificacao INTEGER,
                CategoriaID INTEGER,
                RealizadorID INTEGER)";
            cmd.ExecuteNonQuery();
        }

        public void Adicionar(Movie movie)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO Movies (Titulo,Ano,Lingua,Classificacao,CategoriaID,RealizadorID)
                                VALUES (@t,@a,@l,@c,@cid,@rid)";
            cmd.Parameters.AddWithValue("@t", movie.Titulo);
            cmd.Parameters.AddWithValue("@a", movie.Ano);
            cmd.Parameters.AddWithValue("@l", movie.Lingua);
            cmd.Parameters.AddWithValue("@c", movie.Classificacao);
            cmd.Parameters.AddWithValue("@cid", movie.CategoriaID);
            cmd.Parameters.AddWithValue("@rid", movie.RealizadorID);
            cmd.ExecuteNonQuery();
        }

        public List<Movie> Listar()
        {
            var lista = new List<Movie>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID,Titulo,Ano,Lingua,Classificacao,CategoriaID,RealizadorID FROM Movies";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(new Movie {
                    ID = reader.GetInt32(0), Titulo = reader.GetString(1),
                    Ano = reader.GetInt32(2), Lingua = reader.GetString(3),
                    Classificacao = reader.GetInt32(4), CategoriaID = reader.GetInt32(5),
                    RealizadorID = reader.GetInt32(6)
                });
            return lista;
        }

        public Movie? ObterPorTitulo(string titulo)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID,Titulo,Ano,Lingua,Classificacao,CategoriaID,RealizadorID FROM Movies WHERE LOWER(Titulo)=LOWER(@t)";
            cmd.Parameters.AddWithValue("@t", titulo);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return new Movie {
                    ID = reader.GetInt32(0), Titulo = reader.GetString(1),
                    Ano = reader.GetInt32(2), Lingua = reader.GetString(3),
                    Classificacao = reader.GetInt32(4), CategoriaID = reader.GetInt32(5),
                    RealizadorID = reader.GetInt32(6)
                };
            return null;
        }

        public Movie? ObterPorId(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID,Titulo,Ano,Lingua,Classificacao,CategoriaID,RealizadorID FROM Movies WHERE ID=@id";
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return new Movie {
                    ID = reader.GetInt32(0), Titulo = reader.GetString(1),
                    Ano = reader.GetInt32(2), Lingua = reader.GetString(3),
                    Classificacao = reader.GetInt32(4), CategoriaID = reader.GetInt32(5),
                    RealizadorID = reader.GetInt32(6)
                };
            return null;
        }

        public bool Remover(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Movies WHERE ID=@id";
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool ExistePorTitulo(string titulo) => ObterPorTitulo(titulo) != null;
    }
}