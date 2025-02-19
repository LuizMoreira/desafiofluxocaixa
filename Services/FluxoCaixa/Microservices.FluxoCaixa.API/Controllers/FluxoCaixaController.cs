using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microservices.FluxoCaixa.Application.Commands.Depositar;
using Microservices.FluxoCaixa.Application.Commands.Sacar;
using Microservices.FluxoCaixa.Application.Dtos;
using Microservices.FluxoCaixa.Application.Queries.Extrato;
using Microsoft.AspNetCore.Authorization;
using Microservices.FluxoCaixa.API.Services;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Microservices.FluxoCaixa.API.Controllers
{
    [Route("api/v1/fluxocaixa/")]
    [ApiController]
    [Authorize]
    public class FluxoCaixaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAutorizacao _autorizacao;

        public FluxoCaixaController(IMediator mediator, IAutorizacao autorizacao)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _autorizacao = autorizacao ?? throw new ArgumentNullException(nameof(autorizacao));
        }
        
        // testing purpose
        [HttpPost("depositar")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<decimal>> Depositar([FromBody] DepositarContaCorrenteCommand command)
        {
            command.ContaCorrenteId = new Guid(_autorizacao.GetJWTTokenClaim(await HttpContext.GetTokenAsync("access_token"), "name"));
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("sacar")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Sacar([FromBody] SacarContaCorrenteCommand command)
        {
            command.ContaCorrenteId = new Guid(_autorizacao.GetJWTTokenClaim(await HttpContext.GetTokenAsync("access_token"), "name"));
            var result = await _mediator.Send(command);
            return Ok(result);
        }

       
        [HttpGet("extrato")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ExtratoDto>>> ObterExtratoPorContaCorrenteComData([FromQuery] ExtratoQuery extratoQuery)
        {
            extratoQuery.ContaCorrenteId = _autorizacao.GetJWTTokenClaim(await HttpContext.GetTokenAsync("access_token"), "name");
            var extrato = await _mediator.Send(extratoQuery);
            return Ok(extrato);
        }

    }
}
