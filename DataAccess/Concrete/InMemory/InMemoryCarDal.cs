using System.Linq.Expressions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace DataAccess.Concrete.InMemory;

public class InMemoryCarDal : ICarDal
{
    List<Car> _cars;

    public InMemoryCarDal()
    {
        _cars = new List<Car>()
        {
            new Car{Id = 1, BrandId  = 1, ColorId = 1, ModelYear = 2020, DailyPrice = 175, Description = "1.araba"},
            new Car{Id = 2, BrandId  = 1, ColorId = 2, ModelYear = 2021, DailyPrice = 200, Description = "2.araba"},
            new Car{Id = 3, BrandId  = 2, ColorId = 2, ModelYear = 2019, DailyPrice = 550, Description = "3.araba"},
            new Car{Id = 4, BrandId  = 2, ColorId = 4, ModelYear = 2020, DailyPrice = 600, Description = "4.araba."},
            new Car{Id = 5, BrandId  = 3, ColorId = 4, ModelYear = 2019, DailyPrice = 500, Description = "5.araba"},
            new Car{Id = 6, BrandId  = 3, ColorId = 2, ModelYear = 2021, DailyPrice = 550, Description = "6.araba "},
            new Car{Id = 7, BrandId  = 4, ColorId = 1, ModelYear = 2021, DailyPrice = 250, Description = "7.araba"},
            new Car{Id = 8, BrandId  = 4, ColorId = 3, ModelYear = 2017, DailyPrice = 200, Description = "8.araba"},
            new Car{Id = 9, BrandId  = 5, ColorId = 1, ModelYear = 2021, DailyPrice = 220, Description = "9.araba"},
            new Car{Id = 10, BrandId  = 5, ColorId = 4, ModelYear = 2020, DailyPrice = 200, Description = "10.araba"},

        };
    }

    public List<Car> GetById(int id)
    {
        return new List<Car> { _cars.SingleOrDefault(c => c.Id == id) };
    }

    public List<Car> GetAll()
    {
        return _cars;
    }

    public Car get(Expression<Func<Car, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
    {
        throw new NotImplementedException();
    }

    public void Add(Car car)
    {
        _cars.Add(car);
    }

    public void Update(Car car)
    {
        Car carToUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
        carToUpdate.Description = car.Description;
    }

    public void Delete(Car car)
    {
        Car carToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
        _cars.Remove(carToDelete);
    }

    public List<CarDetailDto> GetCarDetails()
    {
        throw new NotImplementedException();
    }
}