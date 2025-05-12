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
            .ForMember(dest => dest.Price, opt =>
                opt.MapFrom(src => src.Price.ParsePriceToString()))
            .ForMember(dest => dest.DiscountPrice, opt =>
                opt.MapFrom(src => src.DiscountPrice.ParsePriceToString()));

        CreateMap<Product, ProductDetailsResponse>()
            .ForMember(dest => dest.CategoryName, opt =>
                opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.BrandName, opt =>
                opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Price, opt =>
                opt.MapFrom(src => src.Price.ParsePriceToString()))
            .ForMember(dest => dest.DiscountPrice, opt =>
                opt.MapFrom(src => src.DiscountPrice.ParsePriceToString()))
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
    }
}
