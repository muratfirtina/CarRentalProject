using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace Business.Concrete;

public class CustomerManager : ICustomerService
{
    ICustomerDal _customerDal;

    public CustomerManager(ICustomerDal customerDal)
    {
        _customerDal = customerDal;
    }

    public IResult Add(Customer customer)
    {
        if (customer.CompanyName == null)
        {
            return new ErrorResult(Messages.CustomerNameInvalid);
        }
        else
        {
            _customerDal.Add(customer);
            return new SuccessResult(Messages.CustomerAdded);
        }
        
    }

    public IResult Delete(Customer customer)
    {
        _customerDal.Delete(customer);
        return new SuccessResult(Messages.CustomerDeleted);
    }

    public IResult Update(Customer customer)
    {
        if (customer.CompanyName == null)
        {
            return new ErrorResult(Messages.CustomerNameInvalid);
        }
        else
        {
            _customerDal.Update(customer);
            return new SuccessResult(Messages.CustomerUpdated);
        }
    }

    public IDataResult<List<Customer>> GetAll()
    {
        return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.CustomersListed);
    }

    public IDataResult<Customer> GetCustomerById(int customerId)
    {
        return new SuccessDataResult<Customer>(_customerDal.Get(c => c.UserId == customerId));
    }

    public IDataResult<Customer> GetCustomerByUserId(int userId)
    {
        return new SuccessDataResult<Customer>(_customerDal.Get(c => c.UserId == userId));
    }

    public IDataResult<List<CustomerDetailDto>> GetCustomerDetails()
    {
        return new SuccessDataResult<List<CustomerDetailDto>>(_customerDal.GetCustomerDetails());
    }

    public IDataResult<CustomerDetailDto> GetCustomerDetailsById(int userId)
    {
        return new SuccessDataResult<CustomerDetailDto>(_customerDal.GetCustomerDetailsById(userId));
    }
}