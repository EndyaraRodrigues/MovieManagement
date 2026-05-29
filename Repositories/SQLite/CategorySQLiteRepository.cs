using Microsoft.Data.Sqlite;
using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;

namespace MovieManagement.Repositories.SQLite
{
    public class CategorySQLiteRepository : ICategoryRepository
    {
        private readonly string _connectionString;

        public CategorySQLiteRepository(string dbPath = "moviemanagement.db")
        {
            _connectionString = $"Data Source={dbPath}";
            CriarTabela();
        }

        private void CriarTabela()
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Categories (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL)";
            cmd.ExecuteNonQuery();
        }

        public void Adicionar(Category category)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Categories (Nome) VALUES (@nome)";
            cmd.Parameters.AddWithValue("@nome", category.Nome);
            cmd.ExecuteNonQuery();
        }

        public List<Category> Listar()
        {
            var lista = new List<Category>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID, Nome FROM Categories";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                lista.Add(new Category { ID = reader.GetInt32(0), Nome = reader.GetString(1) });
            return lista;
        }

        public Category? ObterPorNome(string nome)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID, Nome FROM Categories WHERE LOWER(Nome)=LOWER(@nome)";
            cmd.Parameters.AddWithValue("@nome", nome);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return new Category { ID = reader.GetInt32(0), Nome = reader.GetString(1) };
            return null;
        }

        public Category? ObterPorId(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID, Nome FROM Categories WHERE ID=@id";
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return new Category { ID = reader.GetInt32(0), Nome = reader.GetString(1) };
            return null;
        }

        public bool Remover(int id)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Categories WHERE ID=@id";
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool ExistePorNome(string nome) => ObterPorNome(nome) != null;
    }
}