using AutoMapper;
using GadgetLand.Application.Common.Extensions;
using GadgetLand.Contracts.Orders;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Common.Mappings;

public class OrderMappings : Profile
{
    public OrderMappings()
    {
        CreateMap<Payment, Order>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.PaymentItems));

        CreateMap<Order, OrderDetailsResponse>()
            .ForMember(dest => dest.DiscountAmount, opt =>
                opt.MapFrom(src => src.DiscountAmount.ParsePriceToString()))
            .ForMember(dest => dest.ShippingCost, opt =>
                opt.MapFrom(src => src.ShippingCost.ParsePriceToString()))
            .ForMember(dest => dest.SubtotalAmount, opt =>
                opt.MapFrom(src => src.SubtotalAmount.ParsePriceToString()))
            .ForMember(dest => dest.TotalPayableAmount, opt =>
                opt.MapFrom(src => src.TotalPayableAmount.ParsePriceToString()))
            .ForMember(dest => dest.OrderDate, opt =>
                opt.MapFrom(src => src.OrderDate.ToPersianDateTimeString()));

        CreateMap<Order, OrderResponse>()
            .ForMember(dest => dest.TotalPayableAmount, opt =>
                opt.MapFrom(src => src.TotalPayableAmount.ParsePriceToString()))
            .ForMember(dest => dest.OrderDate, opt =>
                opt.MapFrom(src => src.OrderDate.ToPersianDateTimeString()));

        CreateMap<Order, OrderForUserPanelResponse>()
            .ForMember(dest => dest.DiscountAmount, opt =>
                opt.MapFrom(src => src.DiscountAmount.ParsePriceToString()))
            .ForMember(dest => dest.ShippingCost, opt =>
                opt.MapFrom(src => src.ShippingCost.ParsePriceToString()))
            .ForMember(dest => dest.SubtotalAmount, opt =>
                opt.MapFrom(src => src.SubtotalAmount.ParsePriceToString()))
            .ForMember(dest => dest.TotalPayableAmount, opt =>
                opt.MapFrom(src => src.TotalPayableAmount.ParsePriceToString()))
            .ForMember(dest => dest.OrderDate, opt =>
                opt.MapFrom(src => src.OrderDate.ToPersianDateTimeString()));
    }
}
