using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
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
        _userDal.Add(user);
        return new SuccessResult(Messages.UserAdded);
        
    }

    public IResult Delete(User user)
    {
        _userDal.Delete(user);
        return new SuccessResult(Messages.UserDeleted);
    }

    public IResult Update(User user)
    {
        var _user = _userDal.Get(u => u.Id == user.Id);
        _user.FirstName = user.FirstName;
        _user.LastName = user.LastName;
        _user.Status = user.Status;

        _userDal.Update(_user);
        return new SuccessResult();
    }

    public IDataResult<List<User>> GetAll()
    {
        return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.UsersListed);
    }

    public IDataResult<User> GetById(int userId)
    {
        return new SuccessDataResult<User>(_userDal.Get(u => u.Id == userId), Messages.UserListed);
    }

    public IDataResult<User> GetByMail(string email)
    {
        return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
    }

    public IDataResult<List<OperationClaim>> GetClaims(User user)
    {
        return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
    }
}