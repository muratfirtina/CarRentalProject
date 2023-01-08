using Business.Abstract;
using Business.Concrete;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class CarImageValidator : AbstractValidator<CarImage>
{
    ICarImageService _carImageService;
    public CarImageValidator(ICarImageService carImageService)
    {
        _carImageService = carImageService;
        RuleFor(c => c.CarId).NotEmpty();
        RuleFor(c => c.ImagePath).NotEmpty();
        
    }
    
}
