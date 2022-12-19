using Entities.Concrete;

namespace Business.Abstract;

public interface IBrandService
{
    List<Brand>GetAll();
    List<Brand>GetByBrandId(int brandId);
    void Add(Brand brand);
    void Update(Brand brand);
    void Delete(Brand brand);
}