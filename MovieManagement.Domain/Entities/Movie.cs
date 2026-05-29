namespace MovieManagement.Domain.Entities
{
    public class Movie
    {
        public int ID { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int Ano { get; set; }
        public string Lingua { get; set; } = string.Empty;
        public int Classificacao { get; set; }
        public int CategoriaID { get; set; }
        public int RealizadorID { get; set; }
    }
}