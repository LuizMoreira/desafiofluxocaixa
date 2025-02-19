using AutoMapper;

namespace Microservices.FluxoCaixa.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            //CreateMap<ContaCorrenteRoot, DepositarContaCorrenteCommand>().ReverseMap();
            //CreateMap<ContaCorrenteRoot, MovimentacaoEfetuadaEvent>().ReverseMap();
            //CreateMap<MovimentarEventStoreCommand, ExtratoDto>().ReverseMap();
        }
    }
}
