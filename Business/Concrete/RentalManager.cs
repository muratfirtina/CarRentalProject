using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

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
        var result = RulesForAdding(rental);
        if (result != null)
        {
            return result;
        }
        _rentalDal.Add(rental);
        return new SuccessResult(Messages.RentalSuccess);
    }

    public IResult Delete(Rental rental)
    {
        _rentalDal.Delete(rental);
        return new SuccessResult(Messages.RentalDeleted);
    }

    public IResult Update(Rental rental)
    {
        var result = RulesForAdding(rental);
        if (result != null)
        {
            return result;
        }
        _rentalDal.Update(rental);
        return new SuccessResult(Messages.RentalUpdated);

    }

    public IDataResult<List<Rental>> GetAll()
    {
        return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalListed);
    }

    public IDataResult<Rental> GetById(int rentalId)
    {
        return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == rentalId));
    }

    public IDataResult<List<Rental>> GetAllByCarId(int carId)
    {
        return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CarId == carId));
    }

    public IDataResult<List<Rental>> GetRentalsByCustomerId(int customerId)
    {
        return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CustomerId == customerId));
    }

    public IDataResult<List<RentalDetailDto>> GetRentalDetails()
    {
        return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
    }

    public IDataResult<RentalDetailDto> GetRentalDetailsById(int rentalId)
    {
        return new SuccessDataResult<RentalDetailDto>(_rentalDal.GetRentalDetailsById(rentalId));
    }



    private IResult CheckIfRentDateIsBeforeToday(DateTime rentDate)
    {
        if (rentDate.Date < DateTime.Now.Date)
        {
            return new ErrorResult(Messages.RentDateCannotBeBeforeToday);
        }

        return new SuccessResult();
    }
    
    private IResult CheckIfThisCarIsRentedAtALaterDateWhileReturnDateIsNull(Rental rent)
    {
        var result = _rentalDal.Get(r =>
            r.CarId == rent.CarId
            && rent.ReturnDate == null
            && r.RentDate.Date > rent.RentDate.Date
        );

        if (result != null)
        {
            return new ErrorResult(Messages.ReturnDateCannotBeLeftBlankAsThisCarWasAlsoRentedAtALaterDate);
        }
        return new SuccessResult();
    }
    
    private IResult CheckIfThisCarHasBeenReturned(Rental rent)
    {
        var result = _rentalDal.Get(r => r.CarId == rent.CarId && r.ReturnDate == null);

        if (result != null)
        {
            if (rent.ReturnDate==null || rent.ReturnDate>result.RentDate)
            {
                return new ErrorResult(Messages.ThisCarHasNotBeenReturned);
            }
        }
        return new SuccessResult();
    }
    
    private IResult CheckIfReturnDateIsBeforeThanRentDate(DateTime? returnDate, DateTime rentDate)
    {
        if (returnDate != null)
        {
            if (returnDate < rentDate)
            {
                return new ErrorResult(Messages.ReturnDateCannotBeEarlierThanRentDate);
            }
        }
        return new SuccessResult();
    }
    
    private IResult CheckIfThisCarIsAlreadyRentedInSelectedDateRange(Rental rent)
    {
        var result = _rentalDal.Get(r =>
            r.CarId == rent.CarId
            && (r.RentDate.Date == rent.RentDate.Date
                || (r.RentDate.Date < rent.RentDate.Date
                    && (r.ReturnDate == null
                        || ((DateTime)r.ReturnDate).Date > rent.RentDate.Date))));

        if (result != null)
        {
            return new ErrorResult(Messages.ThisCarIsAlreadyRentedInSelectedDateRange);
        }
        return new SuccessResult();
    }
    
    public IResult RulesForAdding(Rental rent)
    {
        var result = BusinessRules.Run(
            CheckIfRentDateIsBeforeToday(rent.RentDate),
            CheckIfReturnDateIsBeforeThanRentDate(rent.ReturnDate, rent.RentDate),
            CheckIfThisCarIsAlreadyRentedInSelectedDateRange(rent),
            CheckIfThisCarIsRentedAtALaterDateWhileReturnDateIsNull(rent),
            CheckIfThisCarHasBeenReturned(rent));
            
        if (result != null)
        {
            return result;
        }
        return new SuccessResult(Messages.YouAreDirectedToPaymentPage);
    }
    
    
    


    
}