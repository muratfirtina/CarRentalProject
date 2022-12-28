using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class RentalManager : IRentalService
{
    IRentalDal _rentalDal;

    public RentalManager(IRentalDal rentalDal)
    {
        _rentalDal = rentalDal;
    }

    public IResult Add(Rental rental)
    {
        if (rental.ReturnDate == null)
        {
            return new ErrorResult(Messages.RentalNotAdded);
        }
        else
        {
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalSuccess);
        }
    }

    public IResult Delete(Rental rental)
    {
        _rentalDal.Delete(rental);
        return new SuccessResult(Messages.RentalDeleted);
    }

    public IResult Update(Rental rental)
    {
        if (rental.ReturnDate == null)
        {
            return new SuccessResult(Messages.RentDateUpdated);
        }
        else
        {
            _rentalDal.Update(rental);
            return new ErrorResult(Messages.RentalNotUpdated);
        }
        
    }

    public IDataResult<List<Rental>> GetAll()
    {
        return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalListed);
    }

    public IDataResult<Rental> GetById(int rentalId)
    {
        return new SuccessDataResult<Rental>(_rentalDal.get(r => r.Id == rentalId));
    }

    public IDataResult<List<Rental>> GetRentalsByCustomerId(int customerId)
    {
        return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CustomerId == customerId));
    }
}