using MovieManagement.Domain.Entities;
using MovieManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MovieManagement.Data.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private List<Category> _categories;
        private int _proximoID;

public Category? ObterPorId(int id)
{
    return _categories.FirstOrDefault(c => c.ID == id);
}
        public CategoryRepository()
        {
            _categories = new List<Category>();
            _proximoID = 1;
        }

        public void Adicionar(Category category)
        {
            category.ID = _proximoID;
            _categories.Add(category);
            _proximoID++;
        }

        public List<Category> Listar()
        {
            return _categories;
        }

        public Category? ObterPorNome(string nome)
        {
            foreach (Category c in _categories)
            {
                if (c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
                {
                    return c;
                }
            }
            return null;
        }

        public bool Remover(int id)
        {
            Category? category= null;
            foreach (Category c in _categories)
            {
                if (c.ID == id)
                {
                    category = c;
                    break;
                }
            }
            if (category != null)
            {
                _categories.Remove(category);
                return true;
            }
            return false;
        }

        public bool ExistePorNome(string nome)
        {
            return _categories.Any(c => c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        }
    }

    
}