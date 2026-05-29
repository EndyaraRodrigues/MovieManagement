using Microsoft.Data.Sqlite;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;

namespace MovieManagement.Repositories.SQLite
{
    public class DirectorSQLiteRepository : IDirectorRepository
    {
        private readonly string _connectionString;

        public DirectorSQLiteRepository(string dbPath = "moviemanagement.db")
        {
            _connectionString = $"Data Source={dbPath}";
            CriarTabela();
        }

        private void CriarTabela()
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Directors (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL,
                Pais TEXT NOT NULL)";
            cmd.ExecuteNonQuery();
        }

        public void Adicionar(Director director)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Directors (Nome, Pais) VALUES (@nome, @pais)";
            cmd.Parameters.AddWithValue("@nome", director.Nome);
            cmd.Parameters.AddWithValue("@pais", director.Pais);
            cmd.ExecuteNonQuery();
        }

        public List<Director> Listar()
        {
            var lista = new List<Director>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID, Nome, Pais FROM Directors";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(new Director { ID = reader.GetInt32(0), Nome = reader.GetString(1), Pais = reader.GetString(2) });
            return lista;
        }

        public Director? ObterPorNome(string nome)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID, Nome, Pais FROM Directors WHERE LOWER(Nome)=LOWER(@nome)";
            cmd.Parameters.AddWithValue("@nome", nome);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return new Director { ID = reader.GetInt32(0), Nome = reader.GetString(1), Pais = reader.GetString(2) };
            return null;
        }

        public Director? ObterPorId(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID, Nome, Pais FROM Directors WHERE ID=@id";
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return new Director { ID = reader.GetInt32(0), Nome = reader.GetString(1), Pais = reader.GetString(2) };
            return null;
        }

        public bool Remover(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Directors WHERE ID=@id";
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool ExistePorNome(string nome) => ObterPorNome(nome) != null;
    }
}