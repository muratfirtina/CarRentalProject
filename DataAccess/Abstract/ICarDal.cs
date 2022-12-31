using Core.DataAccess;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace DataAccess.Abstract;

public interface ICarDal : IEntityRepository<Car>
{
    List<CarDetailDto> GetCarDetails();
    CarDetailDto GetCarDetailsById(int Id);
}