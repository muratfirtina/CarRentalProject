using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace Business.Concrete;

public class CarManager : ICarService
{
    ICarDal _carDal;

    public CarManager(ICarDal carDal)
    {
        _carDal = carDal;
    }

    public IDataResult<Car> GetById(int Id)
    {
        return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == Id), Messages.CarListed);

    }

    public IDataResult<List<Car>> GetAll()
    {
        return new SuccessDataResult<List<Car>>( _carDal.GetAll(), Messages.CarsListed);
    }

    public IDataResult<List<CarDetailDto>> GetCarsByBrandId(int brandId)
    {
        return new SuccessDataResult<List<CarDetailDto>>( _carDal.GetCarDetails(c=>c.BrandId==brandId));
    }

    public IDataResult<List<CarDetailDto>> GetCarsByColorId(int colorId)
    {
        return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c=>c.ColorId==colorId));
    }

    public IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max)
    {
        return new SuccessDataResult<List<Car>>( _carDal.GetAll(c=>c.DailyPrice>=min && c.DailyPrice<=max));
    }


    [SecuredOperation("admin,car.add")]
    [ValidationAspect(typeof(CarValidator))]
    public IResult Add(Car car)
    {
        // if (car.Description.Length >= 2 && car.DailyPrice > 0)
        // {
        //     _carDal.Add(car);
        //     return new SuccessResult(Messages.CarAdded);
        // }
        // else
        // {
        //     return new ErrorResult(Messages.CarNameInvalid);
        // }
        _carDal.Add(car);
        return new SuccessResult(Messages.CarAdded);
    }

    public IResult Update(Car car)
    {
        
        if (car.Description.Length >= 2 && car.DailyPrice > 0)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }
        else
        {
            return new ErrorResult(Messages.CarNameInvalid);
        }
        
    }

    public IResult Delete(Car car)
    {
        _carDal.Delete(car);
        return new SuccessResult(Messages.CarDeleted);
    }

    public IDataResult<List<CarDetailDto>> GetCarDetails()
    {
        return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
    }

    public IDataResult<CarDetailDto> GetCarDetailsById(int id)
    {
        return new SuccessDataResult<CarDetailDto>( _carDal.GetCarDetails(c => c.Id == id).SingleOrDefault());
    }

    public IDataResult<List<CarDetailDto>> GetCarsByBrandAndColor(int brandId, int colorId)
    {
        return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.BrandId == brandId && c.ColorId == colorId));
    }
}