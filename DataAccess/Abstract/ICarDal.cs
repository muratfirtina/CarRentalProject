using System.Linq.Expressions;
using Core.DataAccess;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace DataAccess.Abstract;

public interface ICarDal : IEntityRepository<Car>
{
    List<Car> GetAll();
    List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto,bool>>filter=null);
    CarDetailDto GetCarDetailsById(int Id);
}