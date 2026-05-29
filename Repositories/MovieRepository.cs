using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System.Collections;

namespace MovieManagement.Data.Repositories
{
    public class MovieRepository: IMovieRepository
    {
        private List<Movie> _movies;
        private int _proximoID;

        public MovieRepository()
        {
            _movies = new List<Movie>();
            _proximoID = 1;
        }

        public void Adicionar(Movie movie)
        {
            movie.ID = _proximoID;
            _movies.Add(movie);
            _proximoID++;
        }

        public List<Movie> Listar()
        {
            return _movies;
        }

        public Movie? ObterPorTitulo(string titulo)
        {
            foreach (Movie m in _movies)
            {
                if (m.Titulo.Equals(titulo,StringComparison.OrdinalIgnoreCase))
                {
                    return m;
                }
            }
            return null;
        }

        public bool Remover(int id)
        {
            Movie? movie = null;
            foreach (Movie m in _movies)
            {
                if (m.ID==id)
                {
                    movie = m;
                    break;
                }
            }
            if (movie!=null)
            {
                _movies.Remove(movie);
                return true;
            }
            return false;
        }

        public bool ExistePorTitulo(string titulo)
        {
            return _movies.Any(m => m.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
        }



    }
}