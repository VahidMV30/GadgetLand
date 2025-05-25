using FluentValidation;
using GadgetLand.Application.Common.Extensions;

namespace GadgetLand.Application.Features.Products.Commands.ModifyProductImages;

public class ModifyProductImagesCommandValidator : AbstractValidator<ModifyProductImagesCommand>
{
    public ModifyProductImagesCommandValidator()
    {
        RuleForEach(x => x.NewImages)
            .MustBeValidImage()
            .MustBeUnder1MB();
    }
}
