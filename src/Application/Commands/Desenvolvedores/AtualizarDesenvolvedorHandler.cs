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
    public class AtualizarDesenvolvedorHandler : IRequestHandler<AtualizarDesenvolvedorCommand, Result<Desenvolvedor>>
    {
        private readonly IDesenvolvedorRepository _desenvolvedorRepository;
        private readonly ILinguagemRepository _linguagemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AtualizarDesenvolvedorHandler(IDesenvolvedorRepository desenvolvedorRepository, IUnitOfWork unitOfWork, ILinguagemRepository linguagemRepository)
        {
            _desenvolvedorRepository = desenvolvedorRepository;
            _unitOfWork = unitOfWork;
            _linguagemRepository = linguagemRepository;
        }

        public async Task<Result<Desenvolvedor>> Handle(AtualizarDesenvolvedorCommand command, CancellationToken cancellationToken)
        {
            var emailExiste = await _desenvolvedorRepository.ExisteAsync(
                x => x.Email == command.Email && x.Id != command.Id, cancellationToken);

            if (emailExiste)
                return Result<Desenvolvedor>.Failure("Já existe um desenvolvedor com esse E-mail.");

            var dev = await _desenvolvedorRepository.ObterPorIdAsync(command.Id, cancellationToken);

            if (dev is null)
                return Result<Desenvolvedor>.Failure("Desenvolvedor não encontrado.");

            await _desenvolvedorRepository.RemoverLinguagensAsync(command.Id, cancellationToken);
            await _unitOfWork.CommitAsync();

            foreach (var id in command.LinguagensId)
            {
                var linguagem = await _linguagemRepository.ObterPorIdAsync(id, cancellationToken);
                if (linguagem is null)
                    return Result<Desenvolvedor>.Failure($"Linguagem {id} não encontrada.");
                dev.AdicionarLinguagem(linguagem);
            }

            dev.Atualizar(command.Nome, command.Email, command.Senioridade, command.CidadeId, command.Observacoes);
            _desenvolvedorRepository.Atualizar(dev);

            var sucesso = await _unitOfWork.CommitAsync();

            if (!sucesso)
                return Result<Desenvolvedor>.Failure("Não foi possível atualizar o desenvolvedor");

            return Result<Desenvolvedor>.Success(dev);
        }
    }
}
