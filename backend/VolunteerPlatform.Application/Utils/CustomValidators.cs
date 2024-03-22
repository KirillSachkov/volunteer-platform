using CSharpFunctionalExtensions;
using FluentValidation;
using VolunteerPlatform.Domain.Common;

namespace VolunteerPlatform.Application.Utils;

public static class CustomValidators
{
    public static IRuleBuilderOptions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, Error>> factoryMethod)
        where TValueObject : ValueObject
    {
        return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject, Error> result = factoryMethod(value);

            if (result.IsFailure)
            {
                context.AddFailure(result.Error.Code);
            }
        });
    }
}