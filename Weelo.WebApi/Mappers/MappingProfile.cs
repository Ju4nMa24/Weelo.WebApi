using AutoMapper;
using Weelo.Business.Commands.Authentication;
using Weelo.Business.Commands.Owner;
using Weelo.Business.Commands.Property;
using Weelo.Common.Types.Authentication;
using Weelo.Common.Types.Owners;
using Weelo.Common.Types.Properties;

namespace Weelo.WebApi.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Property entities.
            CreateMap<Property, PropertyCommand>();
            CreateMap<PropertyCommand, Property>();
            CreateMap<Property, PropertyFiltersCommand>();
            CreateMap<PropertyFiltersCommand, Property>();
            CreateMap<PropertyTrace, PropertyPurchaseCommand>();
            CreateMap<PropertyPurchaseCommand, PropertyTrace>();
            //Owner entities.
            CreateMap<Owner, OwnerCommand>();
            CreateMap<OwnerCommand, Owner>();
            //Authentication entities.
            CreateMap<AuthenticationCommand, Auth>();
            CreateMap<Auth, AuthenticationCommand>();

        }
    }
}
