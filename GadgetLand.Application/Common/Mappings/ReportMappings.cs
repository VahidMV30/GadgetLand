using AutoMapper;
using GadgetLand.Application.Common.Extensions;
using GadgetLand.Application.Features.Reports.Models;
using GadgetLand.Contracts.Reports;

namespace GadgetLand.Application.Common.Mappings;

public class ReportMappings : Profile
{
    public ReportMappings()
    {
        CreateMap<AdminDashboardWidgetsData, AdminDashboardWidgetsResponse>()
            .ForMember(dest => dest.TotalSales, opt =>
                opt.MapFrom(src => src.TotalSales.ParsePriceToString()))
            .ForMember(dest => dest.CurrentMonthSales, opt =>
                opt.MapFrom(src => src.CurrentMonthSales.ParsePriceToString()))
            .ForMember(dest => dest.TodaySales, opt =>
                opt.MapFrom(src => src.TodaySales.ParsePriceToString()));

        CreateMap<SalesByPersianMonthOfYearData, SalesByPersianMonthOfYearResponse>();

        CreateMap<TopFiveCitiesBySalesOfYearData, TopFiveCitiesBySalesOfYearResponse>();

        CreateMap<TopFiveProvincesBySalesOfYearData, TopFiveProvincesBySalesOfYearResponse>();

        CreateMap<UserDashboardWidgetsData, UserDashboardWidgetsResponse>()
            .ForMember(dest => dest.TotalPurchase, opt =>
                opt.MapFrom(src => src.TotalPurchase.ParsePriceToString()))
            .ForMember(dest => dest.CurrentMonthPurchase, opt =>
            opt.MapFrom(src => src.CurrentMonthPurchase.ParsePriceToString()));
    }
}
