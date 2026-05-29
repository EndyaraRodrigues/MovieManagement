using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Business.Services
{
    public class CategoryServices
    {
        private readonly ICategoryRepository _repository;

        public CategoryServices(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public void Adicionar(int id, string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new Exception("O nome da categoria não pode estar vazio");
            }
            if (_repository.ExistePorNome(nome))
            {
                throw new Exception("Já existe uma categoria com esse nome");
            }
            Category novo = new Category();
            novo.ID = id;
            novo.Nome = nome;
            _repository.Adicionar(novo);
        }

        public List<Category> Listar()
        {
            return _repository.Listar();
        }

        public void Remover(int id)
        {
            bool removido = _repository.Remover(id);
            if (!removido)
            {
                throw new Exception("Categoria não encontrada");
            }
        }
    }
}