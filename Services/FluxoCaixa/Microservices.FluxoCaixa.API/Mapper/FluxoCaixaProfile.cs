using AutoMapper;
using Event.Messages.Events;
using Microservices.FluxoCaixa.Application.Commands.LancamentoEventStore;

namespace Microservices.FluxoCaixa.API.Mapper
{
    public class FluxoCaixaProfile : Profile
	{
		public FluxoCaixaProfile()
		{
			CreateMap<LancamentoEventStoreCommand, LancamentoEfetuadoEvent>().ReverseMap();
		}
	}
}
