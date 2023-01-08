using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete;

public class CarImageManager : ICarImageService
{
    ICarImageDal _carImageDal;

    public CarImageManager(ICarImageDal carImageDal)
    {
        _carImageDal = carImageDal;
    }

    [ValidationAspect(typeof(CarImageValidator))]
    public IResult Add(IFormFile file,CarImage carImage)
    {
        IResult result = BusinessRules.Run(CheckIfImageLimitExceeded(carImage.CarId));

        if (result != null)
        {
            return result;
        }

        carImage.ImagePath = ImageHelper.Add(file);
        carImage.Date = DateTime.Now;
        _carImageDal.Add(carImage);
        return new SuccessResult(Messages.CarImageAdded);
    }

    public IResult Delete(CarImage carImage)
    {
        File.Delete(carImage.ImagePath);
        _carImageDal.Delete(carImage);
        return new SuccessResult(Messages.CarImageDeleted);
    }

    public IResult Update(IFormFile file,CarImage carImage)
    {
        carImage.ImagePath = ImageHelper.Update(_carImageDal.get(p => p.Id == carImage.Id).ImagePath, file);
        carImage.Date = DateTime.Now;
        _carImageDal.Update(carImage);
        return new SuccessResult(Messages.CarImageUpdated);
    }
    
    public IDataResult<List<CarImage>> GetAll()
    {
        return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
    }

    public IDataResult<CarImage> GetById(int carImageId)
    {
        return new SuccessDataResult<CarImage>(_carImageDal.get(c => c.Id == carImageId));
    }

    public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
    {
        return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c=>c.CarId==carId));
        ;
    }

    private IResult CheckIfImageLimitExceeded(int carId)
    {
        var carImageCount = _carImageDal.GetAll(c => c.CarId == carId).Count;
        if (carImageCount >= 5)
        {
            return new ErrorResult(Messages.CarImageLimitExceeded);
        }

        return new SuccessResult();
    }
}