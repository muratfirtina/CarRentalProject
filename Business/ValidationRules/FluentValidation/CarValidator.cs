using Business.Constants;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class CarValidator : AbstractValidator<Car>
{
    public CarValidator()
    {
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Description).MinimumLength(2).WithMessage(Messages.CarNameInvalid);
        RuleFor(c => c.DailyPrice).NotEmpty();
        RuleFor(c => c.DailyPrice).GreaterThan(0);
        RuleFor(c => c.DailyPrice).GreaterThanOrEqualTo(1000).When(c => c.BrandId == 1);
    }
    
}