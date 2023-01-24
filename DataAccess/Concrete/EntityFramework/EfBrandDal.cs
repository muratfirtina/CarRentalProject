using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;


namespace DataAccess.Concrete.EntityFramework;

public class EfBrandDal : EfEntityRepositoryBase<Brand,CarRentalContext>, IBrandDal
{
    public List<Brand> GetAll()
    {
        using (CarRentalContext context = new CarRentalContext())
        {
            return context.Set<Brand>().OrderBy(b =>b.BrandName ).ToList();
        }
    }
    
}