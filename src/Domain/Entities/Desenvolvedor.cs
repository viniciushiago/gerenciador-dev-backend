using Domain.Commons;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Desenvolvedor : Entity
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public Senioridade Senioridade { get; private set; }
        public int CidadeId { get; private set; }
        public IReadOnlyCollection<Linguagem> Linguagens
            => _linguagens.AsReadOnly();

        private readonly List<Linguagem> _linguagens = new();
        public string Observacoes { get; private set; }

        private Desenvolvedor() { }

        public static Desenvolvedor Criar(
            string nome,
            string email,
            Senioridade senioridade,
            int cidadeId,
            string? observacoes = null)
        {
            return new Desenvolvedor
            {
                Nome = nome,
                Email = email,
                Senioridade = senioridade,
                CidadeId = cidadeId,
                Observacoes = observacoes,
            };
        }

        public void Atualizar(
            string nome,
            string email,
            Senioridade senioridade,
            int cidadeId,
            string? observacoes)
        {
            Nome = nome;
            Email = email;
            Senioridade = senioridade;
            CidadeId = cidadeId;
            Observacoes = observacoes;
        }

        public void AtualizarObservacoes(string observacoes)
            => Observacoes = observacoes;

        public void AdicionarLinguagem(Linguagem linguagem)
        {
            if (_linguagens.Any(l => l.Id == linguagem.Id))
                return;

            _linguagens.Add(linguagem);
        }

        public void RemoverLinguagem(Linguagem linguagem)
             => _linguagens.RemoveAll(l => l.Id == linguagem.Id);
        }
}
