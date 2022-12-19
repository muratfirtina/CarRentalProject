using System.Threading.Channels;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

CarManager _carManager = new CarManager(new EfCarDal());

Console.WriteLine("------------------BrandId ye göre Araç seçmek------------------");
foreach (var car in _carManager.GetCarsByBrandId(2))
{
    Console.WriteLine(car.Description);
}

Console.WriteLine("------------------Sisteme araç eklemek------------------");
_carManager.Add(new Car { BrandId = 4, ColorId = 2, DailyPrice = 1000, Description = "Golf 1.6 GTI", ModelYear = 2016});


Console.WriteLine("---------------------ColorId ye göre listeleme----------------------------");
foreach(var car in _carManager.GetCarsByColorId(1))
{
    Console.WriteLine(car.Description);
    
}

Console.WriteLine("---------------------CarId ye göre Araç gösterimi----------------------------");
foreach (var car in _carManager.GetById(1))
{
    Console.WriteLine(car.Description);
}

BrandManager _brandManager = new BrandManager(new EfBrandDal());
Console.WriteLine("---------------------BrandId ye göre Marka gösterimi----------------------------");
foreach (var brand in _brandManager.GetByBrandId(7))
{
    Console.WriteLine(brand.BrandName);
}

ColorManager _colorManager = new ColorManager(new EfColorDal());
Console.WriteLine("---------------------ColorId göre Renk gösterimi----------------------------");
foreach (var color in _colorManager.GetByColorId(2))
{
    Console.WriteLine(color.ColorName);
}