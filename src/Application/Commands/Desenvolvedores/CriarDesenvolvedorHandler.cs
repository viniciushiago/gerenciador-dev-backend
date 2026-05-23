using Domain.Commons;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Desenvolvedores
{
    public  class CriarDesenvolvedorHandler : IRequestHandler<CriarDesenvolvedorCommand, Result<Desenvolvedor>>
    {
        private readonly IDesenvolvedorRepository _desenvolvedorRepository;
        private readonly ILinguagemRepository _linguagemRepository;
        private readonly ICidadeRepository _cidadeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CriarDesenvolvedorHandler(IDesenvolvedorRepository desenvolvedorRepository, IUnitOfWork unitOfWork, 
            ILinguagemRepository linguagemRepository, ICidadeRepository cidadeRepository)
        {
            _desenvolvedorRepository = desenvolvedorRepository;
            _unitOfWork = unitOfWork;
            _linguagemRepository = linguagemRepository;
            _cidadeRepository = cidadeRepository;
        }

        public async Task<Result<Desenvolvedor>> Handle(CriarDesenvolvedorCommand command, CancellationToken cancellationToken)
        {
            var emailDev = await _desenvolvedorRepository.ExisteAsync(x => x.Email == command.Email, cancellationToken);

            if (emailDev)
                return Result<Desenvolvedor>.Failure("Já existe um desenvolvedor com esse E-mail.");

            var linguagens = new List<Linguagem>();
            foreach (var id in command.LinguagensId)
            {
                var linguagem = await _linguagemRepository.ObterPorIdAsync(id, cancellationToken);
                if (linguagem is null)
                    return Result<Desenvolvedor>.Failure($"Linguagem {id} não encontrada.");
                linguagens.Add(linguagem);
            }

            var cidadeExiste = await _cidadeRepository.ExisteAsync(
                x => x.Id == command.CidadeId, cancellationToken);

            if (!cidadeExiste)
                return Result<Desenvolvedor>.Failure("Cidade não encontrada.");

            var dev = Desenvolvedor.Criar(command.Nome, command.Email, command.Senioridade, command.CidadeId, command.Observacoes);

            foreach (var linguagem in linguagens)
                dev.AdicionarLinguagem(linguagem);

            _desenvolvedorRepository.Adicionar(dev);

            var sucesso = await _unitOfWork.CommitAsync();

            if (!sucesso)
                return Result<Desenvolvedor>.Failure("Não foi possível salvar o desenvolvedor.");

            return Result<Desenvolvedor>.Success(dev);
        }
    }
}
