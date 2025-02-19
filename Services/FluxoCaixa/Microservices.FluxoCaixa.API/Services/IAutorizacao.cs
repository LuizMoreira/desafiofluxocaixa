namespace Microservices.FluxoCaixa.API.Services
{
    public interface IAutorizacao
    {
        string GetJWTTokenClaim(string? token, string claimName);
    }
}
