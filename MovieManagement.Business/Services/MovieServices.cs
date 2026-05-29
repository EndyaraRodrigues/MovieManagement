using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;

namespace MovieManagement.Business.Services
{
    public class MovieServices
    {
        private readonly IMovieRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDirectorRepository _directorRepository;

        // Agora recebe os 3 repositórios
        public MovieServices(IMovieRepository repository,
                             ICategoryRepository categoryRepository,
                             IDirectorRepository directorRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _directorRepository = directorRepository;
        }

        public void Adicionar(string titulo, int ano, string lingua,
                              int classificacao, int categoriaId, int realizadorId)
        {
            if (string.IsNullOrEmpty(titulo))
                throw new Exception("O título não pode estar vazio");

            if (_repository.ExistePorTitulo(titulo))
                throw new Exception("Já existe um filme com esse título");

            if (classificacao < 0 || classificacao > 5)
                throw new Exception("A classificação deve ser entre 0 e 5");

            // Valida se a categoria existe
            var categoria = _categoryRepository.ObterPorId(categoriaId);
            if (categoria == null)
                throw new Exception("Categoria não encontrada");

            // Valida se o realizador existe
            var realizador = _directorRepository.ObterPorId(realizadorId);
            if (realizador == null)
                throw new Exception("Realizador não encontrado");

            Movie novo = new Movie();
            novo.Titulo = titulo;
            novo.Ano = ano;
            novo.Lingua = lingua;
            novo.Classificacao = classificacao;
            novo.CategoriaID = categoriaId;
            novo.RealizadorID = realizadorId;

            _repository.Adicionar(novo);
        }

        public List<Movie> Listar() => _repository.Listar();

        public Movie? ProcurarPorTitulo(string titulo) => _repository.ObterPorTitulo(titulo);

        public void Remover(int id)
        {
            bool removido = _repository.Remover(id);
            if (!removido)
                throw new Exception("Filme não encontrado");
        }
    }
}