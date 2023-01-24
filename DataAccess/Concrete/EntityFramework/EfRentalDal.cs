using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace DataAccess.Concrete.EntityFramework;

public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalContext>, IRentalDal
{
    public List<RentalDetailDto> GetRentalDetails()
    {
        using (CarRentalContext context = new CarRentalContext())
        {
            var result = from r in context.Rentals
                join c in context.Cars on r.CarId equals c.Id
                join cu in context.Customers on r.CustomerId equals cu.UserId
                join b in context.Brands on c.BrandId equals b.BrandId
                join cl in context.Colors on c.ColorId equals cl.ColorId
                join u in context.Users on cu.UserId equals u.Id
                select new RentalDetailDto()
            {
                RentalId = r.Id,
                CompanyName = cu.CompanyName,
                UserFirstName = u.FirstName,
                UserLastName = u.LastName,
                CarName = c.Description,
                BrandName = b.BrandName,
                ColorName = cl.ColorName,
                ModelYear = c.ModelYear,
                DailyPrice = c.DailyPrice,
                RentDate = r.RentDate,
                ReturnDate = r.ReturnDate
            };
            return result.ToList();
        }
    }

    public RentalDetailDto GetRentalDetailsById(int rentalId)
    {
        using (CarRentalContext context = new CarRentalContext())
        {
            var result = from r in context.Rentals
                join c in context.Cars on r.CarId equals c.Id
                join cu in context.Customers on r.CustomerId equals cu.UserId
                join b in context.Brands on c.BrandId equals b.BrandId
                join cl in context.Colors on c.ColorId equals cl.ColorId
                join u in context.Users on cu.UserId equals u.Id
                select new RentalDetailDto()
                {
                    RentalId = r.Id,
                    CompanyName = cu.CompanyName,
                    UserFirstName = u.FirstName,
                    UserLastName = u.LastName,
                    CarName = c.Description,
                    BrandName = b.BrandName,
                    ColorName = cl.ColorName,
                    ModelYear = c.ModelYear,
                    DailyPrice = c.DailyPrice,
                    RentDate = r.RentDate,
                    ReturnDate = r.ReturnDate
                };
            return result.SingleOrDefault();
        }
    }
}