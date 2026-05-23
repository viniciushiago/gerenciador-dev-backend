using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cidade : Entity
    {
        public string Nome { get; private set; }
        public int EstadoId { get; private set; }

        private Cidade() { }

        public static Cidade Criar(string nome, int estadoId)
            => new() { Nome = nome, EstadoId = estadoId };

        public void Atualizar(string nome, int estadoId)
        {
            Nome = nome;
            EstadoId = estadoId;
        }
    }
}
