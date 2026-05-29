using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MovieManagement.Data.Repositories
{
    public class DirectorRepository: IDirectorRepository
    {
        private List<Director> _directors;
        private int _proximoID;

public Director? ObterPorId(int id)
{
    return _directors.FirstOrDefault(d => d.ID == id);
}
        public DirectorRepository()
        {
            _directors = new List<Director>();
            _proximoID = 1;
        }

        public void Adicionar(Director director)
        {
            director.ID = _proximoID;
            _directors.Add(director);
            _proximoID++;
        }

        public List<Director> Listar()
        {
            return _directors;
        }

        public Director? ObterPorNome(string nome)
        {
            foreach (Director d in _directors)
            {
                if (d.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
                {
                    return d;
                }
            }
            return null;
        }

        public bool Remover(int id)
        {
            Director? director = null;
            foreach (Director d in _directors)
            {
                if (d.ID == id)
                {
                    director= d;
                    break;
                }
            }
            if (director != null)
            {
                _directors.Remove(director);
                return true;
            }
            return false;
        }

        public bool ExistePorNome(string nome)
        {
            return _directors.Any(d => d.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }
    }
}