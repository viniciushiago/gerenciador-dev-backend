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
    public class DeletarDesenvolvedorHandler : IRequestHandler<DeletarDesenvolvedorCommand, Result<bool>>
    {
        private readonly IDesenvolvedorRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletarDesenvolvedorHandler(IDesenvolvedorRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeletarDesenvolvedorCommand command, CancellationToken cancellationToken)
        {
            var dev = await _repository.ObterPorIdAsync(command.Id, cancellationToken);

            if (dev is null)
                return Result<bool>.Failure("Desenvolvedor não encontrado.");

            _repository.Remover(dev);

            var sucesso = await _unitOfWork.CommitAsync();

            if (!sucesso)
                return Result<bool>.Failure("Não foi possível deletar o desenvolvedor.");

            return Result<bool>.Success(true);
        }
    }
}
