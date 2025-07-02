using AutoMapper;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Common.Mappings;

public class OrderItemMappings : Profile
{
    public OrderItemMappings()
    {
        CreateMap<PaymentItem, OrderItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
