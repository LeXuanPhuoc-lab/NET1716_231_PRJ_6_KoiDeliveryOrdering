using KoiDeliveryOrdering.Data.Dtos;
using KoiDeliveryOrdering.Data.Entities;
using Mapster;

namespace KoiDeliveryOrdering.Business.Mapping;

public class MappingRegistration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserDto>();
        // More mapping configuration here...
    }
}