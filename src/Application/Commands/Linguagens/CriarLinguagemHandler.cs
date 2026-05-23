using Domain.Commons;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Linguagens
{
    public class CriarLinguagemHandler : IRequestHandler<CriarLinguagemCommand, Result<Linguagem>>
    {
        private readonly ILinguagemRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CriarLinguagemHandler(ILinguagemRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Linguagem>> Handle(CriarLinguagemCommand command, CancellationToken cancellationToken)
        {
            var linguagem = Linguagem.Criar(command.Nome, command.TipoLinguagem);
            _repository.Adicionar(linguagem);

            var sucesso = await _unitOfWork.CommitAsync();

            if (!sucesso)
                return Result<Linguagem>.Failure("Não foi possível salvar a linguagem.");

            return Result<Linguagem>.Success(linguagem);

        }
    }
}
