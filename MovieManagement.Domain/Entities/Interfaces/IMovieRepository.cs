using MovieManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain.Interfaces
{
    public interface IMovieRepository
    {
        public void Adicionar(Movie movie);
        public List<Movie> Listar();
        public Movie? ObterPorTitulo(string titulo);
        public bool Remover(int id);
        public bool ExistePorTitulo(string titulo);
    }
}