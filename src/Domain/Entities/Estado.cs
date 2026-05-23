using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Estado : Entity
    {
        public string Nome { get; private set; }
        public string Uf { get; private set; }
        public IReadOnlyCollection<Cidade> Cidades => _cidades.AsReadOnly();
        private readonly List<Cidade> _cidades = new();

        private Estado() { }

        public static Estado Criar(string nome, string uf)
        => new() { Nome = nome, Uf = uf };

        public void Atualizar(string nome, string uf)
        {
            Nome = nome;
            Uf = uf;
        }
    }
}
