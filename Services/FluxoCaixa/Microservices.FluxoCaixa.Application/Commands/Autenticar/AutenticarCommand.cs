using Microservices.FluxoCaixa.Application.Messaging;

namespace Microservices.FluxoCaixa.Application.Commands.Autenticar
{
    public class AutenticarCommand : ICommand<AutenticacaoRespose>
    {
        public string Nome{ get; set; }
        public string Senha { get; set; }
    }
}
