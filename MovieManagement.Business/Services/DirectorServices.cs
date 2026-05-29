using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Business.Services
{
    public class DirectorServices
    {
        private readonly IDirectorRepository _repository;

        public DirectorServices(IDirectorRepository repository)
        {
            _repository = repository;
        }

        public void Adicionar(int id, string nome, string pais)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new Exception("O nome do realizador não pode estar vazio");
            }
            if (string.IsNullOrEmpty(pais))           // ← adiciona isto
        throw new Exception("O país é obrigatório");
            if (_repository.ExistePorNome(nome))
            {
                throw new Exception("Já existe um realizador com esse nome");
            }
           
            Director novo = new Director();
novo.ID = id;
novo.Nome = nome;
novo.Pais = pais;
_repository.Adicionar(novo); 
            
        }

        public List<Director> Listar()
        {
            return _repository.Listar();
        }

        public void Remover(int id)
        {
            bool removido = _repository.Remover(id);
            if (!removido)
            {
                throw new Exception("Realizador não encontrado");
            }
        }
    }
}