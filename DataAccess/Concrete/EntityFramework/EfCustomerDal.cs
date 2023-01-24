using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace DataAccess.Concrete.EntityFramework;

public class EfCustomerDal : EfEntityRepositoryBase<Customer, CarRentalContext>, ICustomerDal
{
    public List<CustomerDetailDto> GetCustomerDetails()
    {
        using (CarRentalContext context = new CarRentalContext())
        {
            var result = from c in context.Customers
                         join u in context.Users
                         on c.UserId equals u.Id
                         select new CustomerDetailDto
                         {
                             UserId = u.Id,
                             FirstName = u.FirstName,
                             LastName = u.LastName,
                             CompanyName = c.CompanyName
                         };
            return result.ToList();
        }
    }

    public CustomerDetailDto GetCustomerDetailsById(int userId)
    {
        using (CarRentalContext context = new CarRentalContext())
        {
            var result = from c in context.Customers
                join u in context.Users
                    on c.UserId equals u.Id
                select new CustomerDetailDto
                {
                    UserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    CompanyName = c.CompanyName
                };
            return result.SingleOrDefault();
        }
    }
}