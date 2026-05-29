using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManagement.Domain.Entities
{
    public class Director
    {
        public int ID { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
    }


}