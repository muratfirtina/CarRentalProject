using Core.DataAccess;
using Entities.Concrete;
using Entities.Concrete.DTOs;

namespace DataAccess.Abstract;

public interface ICustomerDal:IEntityRepository<Customer>
{
    List<CustomerDetailDto> GetCustomerDetails();
    CustomerDetailDto GetCustomerDetailsById(int userId);
}
