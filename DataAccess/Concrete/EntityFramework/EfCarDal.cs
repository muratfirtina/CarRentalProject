using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace DataAccess.Concrete.EntityFramework;

public class EfCarDal : EfEntityRepositoryBase<Car,CarRentalContext>, ICarDal
{
    public List<Car> GetAll()
    {
        using (CarRentalContext context = new CarRentalContext())
        {
            return context.Set<Car>().OrderBy(c=>c.Id).ToList();

        }
    }
    public List<CarDetailDto> GetCarDetails()
    {
        using (CarRentalContext context = new CarRentalContext())
        {
            var result = from c in context.Cars
                join b in context.Brands on c.BrandId equals b.BrandId
                join co in context.Colors on c.ColorId equals co.ColorId
                orderby c.Id
                select new CarDetailDto
                         {
                             Id = c.Id,
                             BrandName = b.BrandName,
                             ColorName = co.ColorName,
                             DailyPrice = c.DailyPrice,
                             ModelYear = c.ModelYear,
                             CarName = c.Description,
                         };
            return result.ToList();
        }
        
    }
    public CarDetailDto GetCarDetailsById(int Id)
    {
        using (CarRentalContext context = new CarRentalContext())
        {
            var result = from c in context.Cars
                join b in context.Brands on c.BrandId equals b.BrandId
                join co in context.Colors on c.ColorId equals co.ColorId
                where c.Id == Id
                select new CarDetailDto
                         {
                             Id = c.Id,
                             BrandName = b.BrandName,
                             ColorName = co.ColorName,
                             DailyPrice = c.DailyPrice,
                             CarName = c.Description,
                             ModelYear = c.ModelYear,
                             CarImages = context.CarImages.Where(i => i.CarId == c.Id).ToList()
                         };
            return result.SingleOrDefault();
        }
    }
    
}