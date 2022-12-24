using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class CarManager : ICarService
{
    ICarDal _carDal;

    public CarManager(ICarDal carDal)
    {
        _carDal = carDal;
    }

    public Car GetById(int Id)
    {
        return _carDal.get(c => c.Id == Id);

    }

    public List<Car> GetAll()
    {
        return _carDal.GetAll();
    }

    public List<Car> GetCarsByBrandId(int id)
    {
        return _carDal.GetAll(c=>c.BrandId==id);
    }

    public List<Car> GetCarsByColorId(int id)
    {
        return _carDal.GetAll(c=>c.ColorId==id);
    }

    public List<Car> GetByDailyPrice(decimal min, decimal max)
    {
        return _carDal.GetAll(c=>c.DailyPrice>=min && c.DailyPrice<=max);
    }


    public void Add(Car car)
    {
        if (car.Description.Length >= 2 && car.DailyPrice > 0)
        {
            _carDal.Add(car);
            Console.WriteLine("Araba eklendi");
        }
        else
        {
            Console.WriteLine("Araba eklenemedi");
        }
    }

    public void Update(Car car)
    {
        
        if (car.Description.Length >= 2 && car.DailyPrice > 0)
        
        {
            _carDal.Update(car);
            Console.WriteLine(car.Id + " ID 'li Araç bilgisi güncellendi");
        }
        else
        {
            throw new Exception("araç bilgisi boş olamaz veya günlük fiyatı 0'dan küçük olamaz");
        }
        
    }

    public void Delete(Car car)
    {
        _carDal.Delete(car);
        Console.WriteLine(car.Id + " Id li araç silinmiştir.");
    }
    
}