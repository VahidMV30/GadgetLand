using AutoMapper;
using GadgetLand.Application.Common.Extensions;
using GadgetLand.Application.Features.Reviews.Commands.CreateReview;
using GadgetLand.Contracts.Reviews;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Common.Mappings;

public class ReviewMappings : Profile
{
    public ReviewMappings()
    {
        CreateMap<CreateReviewCommand, Review>();

        CreateMap<Review, ReviewResponse>()
            .ForMember(dest => dest.UserFullName, opt =>
                opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.ProductName, opt =>
                opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.CreatedAt, opt =>
                opt.MapFrom(src => src.CreatedAt.ToPersianDateString()));

        CreateMap<Review, ReviewDetailsResponse>()
            .ForMember(dest => dest.UserFullName, opt =>
                opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.ProductName, opt =>
                opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.CreatedAt, opt =>
                opt.MapFrom(src => src.CreatedAt.ToPersianDateTimeString()));
    }
}
