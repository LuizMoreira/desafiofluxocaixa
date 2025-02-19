using System;

namespace Microservices.FluxoCaixa.Core.Common
{
    public abstract class EntityBase
    {
        public Guid Id { get; protected set; }
        public string CriadoPor { get; set; }
        public DateTime DataCriacao { get; set; }
        public string UltimaModificacaoPor { get; set; }
        public DateTime? DataUltimaAlteracao { get; set; }
    }
}
