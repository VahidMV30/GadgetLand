using AutoMapper;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Common.Mappings;

public class OrderMappings : Profile
{
    public OrderMappings()
    {
        CreateMap<Payment, Order>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OrderItems, opt =>
                opt.MapFrom(src => src.PaymentItems));
    }
}
