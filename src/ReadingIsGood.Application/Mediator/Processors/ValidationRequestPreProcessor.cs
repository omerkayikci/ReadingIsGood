using FluentValidation;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using ReadingIsGood.Common.ExceptionHandling;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReadingIsGood.Application.Mediator.Processors
{
    public class ValidationRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
        where TRequest : notnull
    {

        private readonly IEnumerable<IValidator<TRequest>> validators;
        public ValidationRequestPreProcessor(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<object>(request);

            List<FluentValidation.Results.ValidationFailure> failures = validators
                 .Select(v => v.Validate(context))
                 .SelectMany(result => result.Errors)
                 .Where(f => f != null)
                 .ToList();

            if (failures.Count > 0)
            {
                string message = string.Join(", ", failures.Select(x => x.ErrorMessage));
                throw new ReadingIsGoodException(message, System.Net.HttpStatusCode.BadRequest, logLevel: LogLevel.Information);
            }

            return Task.CompletedTask;
        }
    }
}
