using System.Linq.Expressions;
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
    public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto,bool>>filter=null)
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
                             BrandId = b.BrandId,
                                ColorId = co.ColorId,
                             BrandName = b.BrandName,
                             ColorName = co.ColorName,
                             DailyPrice = c.DailyPrice,
                             ModelYear = c.ModelYear,
                             CarName = c.Description,
                             CarImages = ((from ci in  context.CarImages.Where(ci => ci.CarId == c.Id)
                                 select new CarImage
                                 {
                                     Id = ci.Id,
                                     CarId = ci.CarId,
                                     ImagePath = ci.ImagePath,
                                     Date = ci.Date
                                 }).ToList()).Count == 0 
                                 ? new List<CarImage> {new CarImage {Id = -1, CarId = c.Id, Date = DateTime.Now ,ImagePath = "defaultcar.png"}} 
                                 : (from ci in  context.CarImages.Where(ci => ci.CarId == c.Id)
                                     select new CarImage
                                     {
                                         Id = ci.Id,
                                         CarId = ci.CarId,
                                         ImagePath = ci.ImagePath,
                                         Date = ci.Date
                                     }).ToList()
                             
                         };
            
            return filter == null
                ? result.ToList()
                : result.Where(filter).ToList();
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
                                BrandId = b.BrandId,
                                ColorId = co.ColorId,
                             BrandName = b.BrandName,
                             ColorName = co.ColorName,
                             DailyPrice = c.DailyPrice,
                             CarName = c.Description,
                             ModelYear = c.ModelYear,
                             CarImages = ((from ci in  context.CarImages.Where(ci => ci.CarId == c.Id)
                                 select new CarImage
                                 {
                                     Id = ci.Id,
                                     CarId = ci.CarId,
                                     ImagePath = ci.ImagePath,
                                     Date = ci.Date
                                 }).ToList()).Count == 0 
                                 ? new List<CarImage> {new CarImage {Id = -1, CarId = c.Id, Date = DateTime.Now ,ImagePath = "defaultcar.png"}} 
                                 : (from ci in  context.CarImages.Where(ci => ci.CarId == c.Id)
                                     select new CarImage
                                     {
                                         Id = ci.Id,
                                         CarId = ci.CarId,
                                         ImagePath = ci.ImagePath,
                                         Date = ci.Date
                                     }).ToList()
                         };
            return result.SingleOrDefault();
        }
    }
    
}