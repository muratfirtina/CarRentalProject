using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace Business.Abstract;

public interface IRentalService
{
    IResult Add(Rental rental);
    IResult Delete(Rental rental);
    IResult Update(Rental rental);
    IDataResult<List<Rental>> GetAll();
    IDataResult<Rental> GetById(int rentalId);
    IDataResult<List<Rental>>GetRentalsByCustomerId(int customerId);
    IDataResult<List<RentalDetailDto>>GetRentalDetails();
    IDataResult<RentalDetailDto>GetRentalDetailsById(int rentalId);
}