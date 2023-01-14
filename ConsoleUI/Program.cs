using System.Threading.Channels;
using Business.Concrete;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

CarManager _carManager = new CarManager(new EfCarDal());

var result = _carManager.GetCarDetails();
if (result.Success == true)
{
    foreach (var car in result.Data)
    {
        Console.WriteLine(car.BrandName + "/" + car.CarName + "/" + car.ColorName + "/" + car.DailyPrice);
    }
}
else
{
    Console.WriteLine(result.Message);
}

Console.WriteLine("------------------BrandId ye göre Araç listelemek------------------");
foreach (var car in _carManager.GetCarsByBrandId(2).Data)
{
    Console.WriteLine(car.Description);
}

Console.WriteLine("------------------Sisteme araç eklemek------------------");
//_carManager.Add(new Car { BrandId = 4, ColorId = 2, DailyPrice = 1000, Description = "Golf 1.6 GTI", ModelYear = 2016});

Console.WriteLine("------------------Sistemde ki aracı güncellemek------------------");
//_carManager.Update(new Car { Id = 10, BrandId = 4, ColorId = 2, DailyPrice = 1200, Description = "Golf 1.6 GTI", ModelYear = 2016});

Console.WriteLine("------------------Sistemden araç silmek------------------");
//_carManager.Delete(new Car { Id = 15});


Console.WriteLine("---------------------ColorId ye göre listelemek----------------------------");
foreach(var car in _carManager.GetCarsByColorId(1).Data)
{
    Console.WriteLine(car.Description);
    
}

Console.WriteLine("---------------------CarId ye göre Araç gösterimi----------------------------");
_carManager.GetById(1);
Console.WriteLine(_carManager.GetById(1).Data.Description);

BrandManager _brandManager = new BrandManager(new EfBrandDal());
Console.WriteLine("---------------------BrandId ye göre Marka gösterimi----------------------------");
Console.WriteLine(_brandManager.GetByBrandId(1).Data.BrandName);

ColorManager _colorManager = new ColorManager(new EfColorDal());
Console.WriteLine("---------------------ColorId göre Renk gösterimi----------------------------");
Console.WriteLine(_colorManager.GetByColorId(1).Data.ColorName);

Console.WriteLine("---------------------Kullanıcı eklemek----------------------------");
//AddUser();
/*void AddUser()
{
    UserManager _userManager = new UserManager(new EfUserDal());
    var result = _userManager.Add(new User()
        { FirstName = "Emel", LastName = "Fırtına", Email = "emel@firtina.com", Password = "123456" });
    if (result.Success == true)
    {
        
        Console.WriteLine( result.Message);
    }
    else
    {
        Console.WriteLine(result.Message);
    }
}*/

Console.WriteLine("---------------------Kullanıcı silmek----------------------------");
//DeleteUser();

void DeleteUser()
{
    UserManager _userManager = new UserManager(new EfUserDal());
    var deleteUser = _userManager.Delete(new User() { Id = 1 });
    if (deleteUser.Success == true)
    {
        Console.WriteLine(deleteUser.Message);
    }
    else
    {
        Console.WriteLine(deleteUser.Message);
    }
}
Console.WriteLine("---------------------Müşteri eklemek----------------------------");
//AddCustomer();

void AddCustomer()
{
    CustomerManager _customerManager = new CustomerManager(new EfCustomerDal());
    var addCustomer = _customerManager.Add(new Customer() { UserId = 5, CompanyName = "Fırtına" });
    if (addCustomer.Success == true)
    {
        Console.WriteLine(addCustomer.Message);
    }
    else
    {
        Console.WriteLine(addCustomer.Message);
    }
}

Console.WriteLine("---------------------Araç kiralamak----------------------------");
//AddRental();

void AddRental()
{
    RentalManager _rentalManager = new RentalManager(new EfRentalDal());
    var addRental = _rentalManager.Add(new Rental()
        { CarId = 1, CustomerId = 4, RentDate = DateTime.Now, ReturnDate = DateTime.Now.AddDays(1) });
    if (addRental.Success == true)
    {
        Console.WriteLine(addRental.Message);
    }
    else
    {
        Console.WriteLine(addRental.Message);
    }
}
