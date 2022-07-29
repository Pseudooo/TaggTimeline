
using FluentValidation;
using MediatR;
using TaggTimeline.Service.Exceptions;

namespace TaggTimeline.Service.PipelineBehaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {

        var context = new ValidationContext<TRequest>(request);
        var failures = _validators.Select(x => x.Validate(context))
                                  .SelectMany(x => x.Errors)
                                  .Where(x => x is not null)
                                  .ToList();

        if(failures.Any())
            throw new ValidationFailedException(string.Join(", ", failures.Select(f => f.ToString())));

        return next();
    }
}
