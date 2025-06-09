using AutoMapper;
using GadgetLand.Application.Common.Extensions;
using GadgetLand.Application.Features.Products.Commands.CreateProduct;
using GadgetLand.Application.Features.Products.Commands.UpdateProduct;
using GadgetLand.Contracts.Products;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Common.Mappings;

public class ProductMappings : Profile
{
    public ProductMappings()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.CategoryName, opt =>
                opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.BrandName, opt =>
                opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Price, opt =>
                opt.MapFrom(src => src.Price.ParsePriceToString()))
            .ForMember(dest => dest.DiscountPrice, opt =>
                opt.MapFrom(src => src.DiscountPrice.ParsePriceToString()));

        CreateMap<Product, ProductsForAdminTableResponse>()
            .ForMember(dest => dest.CategoryName, opt =>
                opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.BrandName, opt =>
                opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Price, opt =>
                opt.MapFrom(src => src.Price.ParsePriceToString()))
            .ForMember(dest => dest.DiscountPrice, opt =>
                opt.MapFrom(src => src.DiscountPrice.ParsePriceToString()));

        CreateMap<Product, ProductWithImagesResponse>()
            .ForMember(dest => dest.Images, opt =>
                opt.MapFrom(src => src.ProductImages.Select(x => x.Image)));

        CreateMap<CreateProductCommand, Product>()
            .ForMember(dest => dest.Price,
                opt => opt.MapFrom(src => src.Price.ParsePriceToLong()))
            .ForMember(dest => dest.DiscountPrice, opt => opt.MapFrom(src =>
                    string.IsNullOrWhiteSpace(src.DiscountPrice) ? null : src.DiscountPrice.ParsePriceToLong()));

        CreateMap<UpdateProductCommand, Product>()
            .ForMember(dest => dest.Image, opt => opt.Ignore())
            .ForMember(dest => dest.Price,
                opt => opt.MapFrom(src => src.Price.ParsePriceToLong()))
            .ForMember(dest => dest.DiscountPrice, opt => opt.MapFrom(src =>
                string.IsNullOrWhiteSpace(src.DiscountPrice) ? null : src.DiscountPrice.ParsePriceToLong()));

        CreateMap<Product, ProductsWithFiltersResponse>()
        .ForMember(dest => dest.Price, opt =>
            opt.MapFrom(src => src.Price.ParsePriceToString()))
        .ForMember(dest => dest.DiscountPrice, opt =>
            opt.MapFrom(src => src.DiscountPrice.ParsePriceToString()))
        .ForMember(dest => dest.DiscountPercent, opt =>
            opt.MapFrom(src =>
                src.DiscountPrice.HasValue ? Math.Round(((src.Price - src.DiscountPrice.Value) / (decimal)src.Price) * 100m, 1) : (decimal?)null
            ));

        CreateMap<Product, ProductDetailsResponse>()
            .ForMember(dest => dest.CategoryName, opt =>
                opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.CategorySlug, opt =>
                opt.MapFrom(src => src.Category.Slug))
            .ForMember(dest => dest.BrandName, opt =>
                opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.BrandSlug, opt =>
                opt.MapFrom(src => src.Brand.Slug))
            .ForMember(dest => dest.Price, opt =>
                opt.MapFrom(src => src.Price.ParsePriceToString()))
            .ForMember(dest => dest.DiscountPrice, opt =>
                opt.MapFrom(src => src.DiscountPrice.ParsePriceToString()))
            .ForMember(dest => dest.DiscountPercent, opt =>
                opt.MapFrom(src => src.DiscountPrice.HasValue
                    ? Math.Round(((src.Price - src.DiscountPrice.Value) / (decimal)src.Price) * 100m, 1)
                    : (decimal?)null))
            .ForMember(dest => dest.AverageRating, opt =>
                opt.MapFrom(src => src.Reviews.Any()
                    ? Math.Round(src.Reviews.Where(x => x.IsConfirmed).Average(x => x.Rating) * 2, MidpointRounding.AwayFromZero) / 2
                    : 0))
            .ForMember(dest => dest.TotalReviewsCount, opt =>
                opt.MapFrom(src => src.Reviews.Count()))
            .ForMember(dest => dest.ProductImages, opt =>
                opt.MapFrom(src => src.ProductImages.Select(x => x.Image).ToList()));

        CreateMap<Review, ProductDetailsReviewResponse>()
            .ForMember(dest => dest.FullName, opt =>
                opt.MapFrom(src => src.User.FullName))
            .ForMember(dest => dest.CreatedAt, opt =>
                opt.MapFrom(src => src.CreatedAt.ToPersianDateString()));

        CreateMap<Product, CartProductResponse>()
            .ForMember(dest => dest.StringPrice, opt =>
                opt.MapFrom(src => src.Price.ParsePriceToString()))
            .ForMember(dest => dest.StringDiscountPrice, opt =>
                opt.MapFrom(src => src.DiscountPrice.ParsePriceToString()));
    }
}
