using System;

namespace Event.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            DataCriacaoEvento = DateTime.UtcNow;
        }
        
        public IntegrationBaseEvent(Guid id, DateTime createDate)
        {
            Id = id;
            DataCriacaoEvento = createDate;
        }
        
        public Guid Id { get; private set; }
        
        public DateTime DataCriacaoEvento { get; private set; }
    }
}
