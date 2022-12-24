using System.Threading.Channels;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

CarManager _carManager = new CarManager(new EfCarDal());

Console.WriteLine("------------------BrandId ye göre Araç listelemek------------------");
foreach (var car in _carManager.GetCarsByBrandId(2))
{
    Console.WriteLine(car.Description);
}

Console.WriteLine("------------------Sisteme araç eklemek------------------");
_carManager.Add(new Car { BrandId = 4, ColorId = 2, DailyPrice = 1000, Description = "Golf 1.6 GTI", ModelYear = 2016});


Console.WriteLine("---------------------ColorId ye göre listelemek----------------------------");
foreach(var car in _carManager.GetCarsByColorId(1))
{
    Console.WriteLine(car.Description);
    
}

Console.WriteLine("---------------------CarId ye göre Araç gösterimi----------------------------");
_carManager.GetById(1);
Console.WriteLine(_carManager.GetById(1).Description);

BrandManager _brandManager = new BrandManager(new EfBrandDal());
Console.WriteLine("---------------------BrandId ye göre Marka gösterimi----------------------------");
Console.WriteLine(_brandManager.GetByBrandId(1).BrandName);

ColorManager _colorManager = new ColorManager(new EfColorDal());
Console.WriteLine("---------------------ColorId göre Renk gösterimi----------------------------");
Console.WriteLine(_colorManager.GetByColorId(1).ColorName);