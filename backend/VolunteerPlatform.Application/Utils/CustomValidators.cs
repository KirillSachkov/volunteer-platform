using CSharpFunctionalExtensions;
using FluentValidation;

namespace VolunteerPlatform.Application.Utils;

public static class CustomValidators
{
    public static IRuleBuilderOptions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject>> factoryMethod)
        where TValueObject : ValueObject
    {
        return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject> result = factoryMethod(value);

            if (result.IsFailure)
            {
                context.AddFailure("");
            }
        });
    }
}
