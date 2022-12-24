using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

namespace Business.Abstract;

public interface ICarService
{
    Car GetById(int id);
    List<Car> GetAll();
    List<Car> GetCarsByBrandId(int id);
    List<Car> GetCarsByColorId(int id);
    
    List<Car> GetByDailyPrice(decimal min, decimal max);
    void Add(Car car);
    void Update(Car car);
    void Delete(Car car);
    
}