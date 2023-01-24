using Core.DataAccess;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace DataAccess.Abstract;

public interface IRentalDal : IEntityRepository<Rental>
{
    List<RentalDetailDto>GetRentalDetails();
    RentalDetailDto GetRentalDetailsById(int rentalId);
}