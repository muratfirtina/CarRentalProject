using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class UserManager : IUserService
{
    IUserDal _userDal;

    public UserManager(IUserDal userDal)
    {
        _userDal = userDal;
    }

    public IResult Add(User user)
    {
        if (user.Email == "" || user.FirstName == "" || user.LastName == "" || user.Password == "")
        {
            return new ErrorResult(Messages.UsersNotAdded, Messages.UsersInformationNotNull);
        }
        else
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }
        
    }

    public IResult Delete(User user)
    {
        _userDal.Delete(user);
        return new SuccessResult(Messages.UserDeleted);
    }

    public IResult Update(User user)
    {
        if (user.Email == "" || user.FirstName == "" || user.LastName == "" || user.Password == "")
        {
            return new ErrorResult(Messages.UsersNotUpdated, Messages.UsersInformationNotNull);
        }
        else
        {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }
    }

    public IDataResult<List<User>> GetAll()
    {
        return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.UsersListed);
    }

    public IDataResult<User> GetById(int userId)
    {
        return new SuccessDataResult<User>(_userDal.get(u => u.Id == userId), Messages.UserListed);
    }
}