using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace Business.Abstract;

public interface ICustomerService
{
    IResult Add(Customer customer);
    IResult Delete(Customer customer);
    IResult Update(Customer customer);
    IDataResult<List<Customer>> GetAll();
    IDataResult<Customer> GetCustomerById(int customerId);
    IDataResult<Customer> GetCustomerByUserId(int userId);
    IDataResult<List<CustomerDetailDto>> GetCustomerDetails();
    IDataResult<CustomerDetailDto> GetCustomerDetailsById(int userId);
}