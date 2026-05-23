using Application.Commands.Cidades;
using Application.Commands.Desenvolvedores;
using Application.Queries.Desenvolvedores;
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
    public class DesenvolvedoresController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DesenvolvedoresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Criar(CriarDesenvolvedorCommand command, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(command, cancellationToken);
            if (!resultado.IsSuccess)
                return BadRequest(resultado.Error);

            return CreatedAtAction(nameof(ObterPorId), new { id = resultado.Value.Id }, resultado.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, AtualizarDesenvolvedorCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest("Id da rota diferente do Id do comando.");

            var resultado = await _mediator.Send(command, cancellationToken);
            if (!resultado.IsSuccess)
                return BadRequest(resultado.Error);

            return Ok(resultado.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(new DeletarDesenvolvedorCommand(id), cancellationToken);
            if (!resultado.IsSuccess)
                return BadRequest(resultado?.Error);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos([FromQuery] string? nome, [FromQuery] string? senioridade, [FromQuery] int? linguagemId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ObterDesenvolvedoresQuery(nome, senioridade, linguagemId), cancellationToken);
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ObterDesenvolvedorPorIdQuery(id), cancellationToken);
            if (!result.IsSuccess)
                return NotFound(result.Error);
            return Ok(result.Value);
        }
    }
}
