using EMS.Shared;
using FluentValidation.Results;

namespace EMS.Application.Extensions
{
    public static class ValidationResultExtensions
    {
        public static void EnsureValidation(this ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                throw new HandledException(string.Join(Environment.NewLine, validationResult.Errors.Select(x => x.ErrorMessage)));
            }
        }

        //public static async Task EnsureValidationAsync<T>(this AbstractValidator<IRequest<T>> validator, IRequest<T> request, CancellationToken cancellationToken)
        //    where T : CQRSBase
        //{
        //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

        //    if (!validationResult.IsValid)
        //    {
        //        throw new HandledException(string.Join(Environment.NewLine, validationResult.Errors.Select(x => x.ErrorMessage)));
        //    }
        //}
    }
}
