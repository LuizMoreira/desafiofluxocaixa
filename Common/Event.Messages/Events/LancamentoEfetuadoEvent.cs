namespace Event.Messages.Events
{
    public class LancamentoEfetuadoEvent : IntegrationBaseEvent
    {
        public Guid IdContaCorrente { get; set; }
        public string Nome { get; set; }
        public string TipoMovimentacao { get; set; }
        public decimal Saldo { get; set; }
        public decimal ValorTransacao { get; set; }
        public DateTime? Data { get; set; }
        
    }
}
