using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract;

public interface IUserService
{
    IResult Add(User user);
    
    IResult Delete(User user);
    IResult Update(User user);
    IDataResult<List<User>> GetAll();
    IDataResult<User> GetById(int userId);
    IDataResult<User> GetByMail(string email);
    IDataResult<List<OperationClaim>> GetClaims(User user);
    
    
    
    
}