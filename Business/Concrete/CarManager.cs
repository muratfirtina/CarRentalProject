using Business.Abstract;
using Business.Constants;
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
        return new SuccessDataResult<Car>(_carDal.get(c => c.Id == Id), Messages.CarListed);

    }

    public IDataResult<List<Car>> GetAll()
    {
        return new SuccessDataResult<List<Car>>( _carDal.GetAll(), Messages.CarsListed);
    }

    public IDataResult<List<Car>> GetCarsByBrandId(int id)
    {
        return new SuccessDataResult<List<Car>>( _carDal.GetAll(c=>c.BrandId==id));
    }

    public IDataResult<List<Car>> GetCarsByColorId(int id)
    {
        return new SuccessDataResult<List<Car>>(_carDal.GetAll(c=>c.ColorId==id));
    }

    public IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max)
    {
        return new SuccessDataResult<List<Car>>( _carDal.GetAll(c=>c.DailyPrice>=min && c.DailyPrice<=max));
    }


    public IResult Add(Car car)
    {
        if (car.Description.Length >= 2 && car.DailyPrice > 0)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }
        else
        {
            return new ErrorResult(Messages.CarNameInvalid);
        }
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
        if (DateTime.Now.Hour == 15)
        {
            return new ErrorDataResult<List<CarDetailDto>>(Messages.MaintenanceTime);
        }
        {
            return new SuccessDataResult<List<CarDetailDto>>( _carDal.GetCarDetails());
        }
        
    }

    public IDataResult<CarDetailDto> GetCarDetailsById(int id)
    {
        return new SuccessDataResult<CarDetailDto>( _carDal.GetCarDetailsById(id));
    }
}