using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain.Entities
{
    public class Category
    {
        public int ID { get; set; }
        public string Nome { get; set; } = string.Empty;

        public Category() { }
    }
}