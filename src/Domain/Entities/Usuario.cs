using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario : Entity
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }

        private Usuario() { }

        public static Usuario Criar(string nome, string email, string senhaHash)
            => new() { Nome = nome, Email = email, SenhaHash = senhaHash, };
        
        public void Atualizar(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }
        public void AtualizarSenha(string senhaHash)
            => SenhaHash = senhaHash;
    }
}
