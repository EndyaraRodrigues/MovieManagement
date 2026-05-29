using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
namespace MovieManagement.Business.Services
{
    public class MovieServices
    {
        private readonly IMovieRepository _repository; 

        public MovieServices(IMovieRepository repository)
        {
            _repository = repository;
        }

        public void Adicionar(int id,string titulo, int ano,string lingua, int classificacao)
        {
            if (string.IsNullOrEmpty(titulo))
            {
                throw new Exception("O título não pode estar vazio");
            }
            if (_repository.ExistePorTitulo(titulo))
            {
                throw new Exception("Já existe um filme com esse título");
            }
            if (classificacao < 0 || classificacao > 5)
            {
                throw new Exception("A classificação deve ser entre 0 e 5");
            }
            Movie novo = new Movie();
            novo.ID = id;
            novo.Titulo = titulo;
            novo.Ano = ano;
            novo.Lingua = lingua;
            novo.Classificacao = classificacao;
            _repository.Adicionar(novo);
        }

        public List<Movie> Listar()
        {
            return _repository.Listar();
        }

        public void Remover(int id)
        {
            bool removido = _repository.Remover(id);
            if (!removido)
            {
                throw new Exception("Filme não encontrado");
            }
        }


    }
}