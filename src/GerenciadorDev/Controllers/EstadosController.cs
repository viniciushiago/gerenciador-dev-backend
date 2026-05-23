using Application.Commands.Estados;
using Application.Queries.Estados;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EstadosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EstadosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarEstadoCommand command, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(command, cancellationToken);
            if(!resultado.IsSuccess)
                return BadRequest(resultado.Error);

            return  CreatedAtAction(nameof(ObterPorId), new { id = resultado.Value.Id }, resultado.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, AtualizarEstadoCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Id da rota diferente do Id do comando.");

            var resultado = await _mediator.Send(command, cancellationToken);
            if (!resultado.IsSuccess)
                return BadRequest(resultado.Error);

            return Ok(resultado.Value);
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos([FromQuery] string? nome, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ObterEstadosQuery(nome), cancellationToken);
            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id, CancellationToken ct)
        {
            var result = await _mediator.Send(new DeletarEstadoCommand(id), ct);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id, CancellationToken ct)
        {
            var result = await _mediator.Send(new ObterEstadoPorIdQuery(id), ct);
            if (!result.IsSuccess)
                return NotFound(result.Error);
            return Ok(result.Value);
        }
    }
}
