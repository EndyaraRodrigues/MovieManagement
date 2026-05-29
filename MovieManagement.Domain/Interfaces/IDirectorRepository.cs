using MovieManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain.Interfaces
{
    public interface IDirectorRepository
    {
        public void Adicionar(Director director);
        public List<Director> Listar();
        public Director? ObterPorNome(string nome);
        public bool Remover(int id);
        public bool ExistePorNome(string nome);
        public Director? ObterPorId(int id);
    }
}