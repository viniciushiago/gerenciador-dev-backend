using Application.DTOs;
using Domain.Commons;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Queries.Estados
{
    public class ObterEstadosHandler : IRequestHandler<ObterEstadosQuery, Result<IReadOnlyCollection<Estado>>>
    {
        private readonly IEstadoRepository _repository;

        public ObterEstadosHandler(IEstadoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IReadOnlyCollection<Estado>>> Handle(ObterEstadosQuery command, CancellationToken cancellationToken)
        {
            var estados = await _repository.ObterTodosComFiltroAsync(command.Nome, cancellationToken);

            return Result<IReadOnlyCollection<Estado>>.Success(estados);
        }
    }
}
