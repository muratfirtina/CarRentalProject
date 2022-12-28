using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace Business.Abstract;

public interface ICarService
{
    IDataResult<Car> GetById(int id);
    IDataResult<List<Car>> GetAll();
    IDataResult<List<Car>> GetCarsByBrandId(int id);
    IDataResult<List<Car>> GetCarsByColorId(int id);
    
    IDataResult<List<Car>> GetByDailyPrice(decimal min, decimal max);
    IResult Add(Car car);
    IResult Update(Car car);
    IResult Delete(Car car);
    IDataResult<List<CarDetailDto>> GetCarDetails();
}