using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract;

public interface ICustomerService
{
    IResult Add(Customer customer);
    IResult Delete(Customer customer);
    IResult Update(Customer customer);
    IDataResult<List<Customer>> GetAll();
    IDataResult<Customer> GetCustomerById(int customerId);
    IDataResult<Customer> GetCustomerByUserId(int userId);
}