using ErrorOr;
using FluentValidation;
using MediatR;

namespace GadgetLand.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) :
    IPipelineBehavior<TRequest, TResponse> where TRequest :
    IRequest<TResponse> where TResponse : IErrorOr
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(request, cancellationToken)));

        var failures = validationResults.SelectMany(result => result.Errors).Where(f => f != null).ToList();

        if (failures.Count == 0) return await next(cancellationToken);

        var errors = failures
            .Select(failure => Error.Validation(
                code: failure.PropertyName,
                description: failure.ErrorMessage))
            .ToList();

        return (TResponse)(dynamic)errors;
    }
}
