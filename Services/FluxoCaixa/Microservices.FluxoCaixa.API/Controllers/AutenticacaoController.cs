using MediatR;
using Microservices.FluxoCaixa.Application.Commands.Autenticar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microservices.FluxoCaixa.API.Configuracao;
using Microsoft.Extensions.Configuration;

namespace Microservices.FluxoCaixa.API.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOptions<Configurations> _configurations;

        public AutenticacaoController(IMediator mediator, IOptions<Configurations> configurations)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _configurations = configurations ?? throw new ArgumentNullException(nameof(configurations));
        }

        /// <summary>
        /// Autenticação de usuario
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("autorizar")]
        [ProducesResponseType(typeof(AutenticacaoRespose), 200)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get([FromBody] AutenticarCommand command)
        {
            var key = Encoding.ASCII.GetBytes(_configurations.Value.JwtSecretKey);

            var response = await _mediator.Send(command);

            if (response == null)
            {
                return Unauthorized();
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, response.ContaCorrenteId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(48),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            response.AccessToken = tokenString;

            return Ok(response);
        }
    }
}