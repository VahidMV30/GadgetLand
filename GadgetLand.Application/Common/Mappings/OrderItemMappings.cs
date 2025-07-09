using AutoMapper;
using GadgetLand.Application.Common.Extensions;
using GadgetLand.Contracts.OrderItems;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Common.Mappings;

public class OrderItemMappings : Profile
{
    public OrderItemMappings()
    {
        CreateMap<PaymentItem, OrderItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<OrderItem, OrderItemResponse>()
            .ForMember(dest => dest.ProductName, opt =>
                opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ProductSlug, opt =>
                opt.MapFrom(src => src.Product.Slug))
            .ForMember(dest => dest.UnitPrice, opt =>
                opt.MapFrom(src => src.UnitPrice.ParsePriceToString()))
            .ForMember(dest => dest.UnitDiscount, opt =>
                opt.MapFrom(src => src.UnitDiscount.ParsePriceToString()))
            .ForMember(dest => dest.TotalDiscountAmount, opt =>
                opt.MapFrom(src => src.TotalDiscountAmount.ParsePriceToString()))
            .ForMember(dest => dest.SubtotalAmount, opt =>
                opt.MapFrom(src => src.SubtotalAmount.ParsePriceToString()))
            .ForMember(dest => dest.TotalAmount, opt =>
                opt.MapFrom(src => src.TotalAmount.ParsePriceToString()));
    }
}
