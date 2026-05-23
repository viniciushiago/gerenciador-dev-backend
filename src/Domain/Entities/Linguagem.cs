using Domain.Commons;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Linguagem : Entity
    {
        public string Nome { get; private set; }
        public TipoLinguagem Tipo { get; set; }
        private Linguagem() { }

        public static Linguagem Criar(string nome, TipoLinguagem tipo)
            => new() { Nome = nome, Tipo = tipo };

        public void Atualizar(string nome, TipoLinguagem tipo)
        {
            Nome = nome;
            Tipo = tipo;
        }
    }
}
