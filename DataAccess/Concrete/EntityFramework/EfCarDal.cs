using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace DataAccess.Concrete.EntityFramework;

public class EfCarDal : EfEntitiyRepositoryBase<Car,CarRentalContext>, ICarDal
{
    public List<CarDetailDto> GetCarDetails()
    {
        using (CarRentalContext context = new CarRentalContext())
        {
            var result = from c in context.Cars
                join b in context.Brands on c.BrandId equals b.BrandId
                join co in context.Colors on c.ColorId equals co.ColorId
                select new CarDetailDto
                         {
                             CarId = c.Id,
                             BrandName = b.BrandName,
                             ColorName = co.ColorName,
                             DailyPrice = c.DailyPrice,
                             CarName = c.Description,
                         };
            return result.ToList();
        }
        {
            
        }
    }
}