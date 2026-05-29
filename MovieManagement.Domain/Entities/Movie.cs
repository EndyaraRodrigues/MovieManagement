namespace MovieManagement.Domain.Entities
{
    public class Movie
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public string Lingua { get; set; }
        public int Classificacao { get; set; }

        public Movie()
        {
        }
    }
}