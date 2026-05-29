using MovieManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        public void Adicionar(Category category);
        public List<Category> Listar();
        public Category? ObterPorNome(string nome);
        public bool Remover(int id);
        public bool ExistePorNome(string nome);
    }
}