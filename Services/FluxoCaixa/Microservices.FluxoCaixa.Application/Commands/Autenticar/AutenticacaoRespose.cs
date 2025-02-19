using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.FluxoCaixa.Application.Commands.Autenticar
{
    public class AutenticacaoRespose
    {
        public Guid ContaCorrenteId { get; set; }
        public string Nome { get; set; }

        public string? AccessToken { get; set; }
    }
}
