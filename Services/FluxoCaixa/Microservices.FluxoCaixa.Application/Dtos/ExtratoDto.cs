using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Microservices.FluxoCaixa.Application.Dtos
{
    public class ExtratoDto
    {
        public string Id { get; set; }
        public string IdContaCorrente { get; set; }
        public string Nome { get; set; }
        public string TipoMovimentacao { get; set; }
        public decimal Saldo { get; set; }
        public decimal ValorTransacao { get; set; }
        public DateTime? Data { get; set; }
        public DateTime DataCriacaoEvento { get; set; }
    }
}
