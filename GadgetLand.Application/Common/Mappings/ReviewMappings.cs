using AutoMapper;
using GadgetLand.Application.Features.Reviews.Commands;
using GadgetLand.Domain.Entities;

namespace GadgetLand.Application.Common.Mappings;

public class ReviewMappings : Profile
{
    public ReviewMappings()
    {
        CreateMap<CreateReviewCommand, Review>();
    }
}
