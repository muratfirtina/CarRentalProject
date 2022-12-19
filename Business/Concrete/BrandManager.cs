using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class BrandManager : IBrandService

{
    IBrandDal _brandDal;

    public BrandManager(IBrandDal brandDal)
    {
        _brandDal = brandDal;
    }

    public List<Brand> GetAll()
    {
        return _brandDal.GetAll();
    }

    public List<Brand> GetByBrandId(int brandId)
    {
        return _brandDal.GetAll(b => b.BrandId == brandId);
    }

    public void Add(Brand brand)
    {
        _brandDal.Add(brand);
    }

    public void Update(Brand brand)
    {
        _brandDal.Update(brand);
    }

    public void Delete(Brand brand)
    {
        _brandDal.Delete(brand);
    }
}