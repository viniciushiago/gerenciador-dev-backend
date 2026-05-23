using Application.Commands.Estados;
using Domain.Commons;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Cidades
{
    public class CriarCidadeHandler : IRequestHandler<CriarCidadeCommand, Result<Cidade>>
    {
        private readonly ICidadeRepository _repository;
        private readonly IEstadoRepository _estadoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CriarCidadeHandler(ICidadeRepository repository, IEstadoRepository estadoRepository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _estadoRepository = estadoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Cidade>> Handle(CriarCidadeCommand command, CancellationToken cancellationToken)
        {
            var estadoExiste = await _estadoRepository.ExisteAsync(
                x => x.Id == command.EstadoId, cancellationToken);

            if (!estadoExiste)
                return Result<Cidade>.Failure("Estado não encontrado.");
            
            var cidade = Cidade.Criar(command.Nome, command.EstadoId);
            _repository.Adicionar(cidade);

            var sucesso = await _unitOfWork.CommitAsync();

            if (!sucesso)
                return Result<Cidade>.Failure("Não foi possível salvar a cidade.");

            return Result<Cidade>.Success(cidade);
        }
    }
}
