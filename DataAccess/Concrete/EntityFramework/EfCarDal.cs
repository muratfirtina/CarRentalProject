using System.Linq.Expressions;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework;

public class EfCarDal : ICarDal
{
    DbContext _context = new CarRentalContext();

    public Car get(Expression<Func<Car, bool>> filter)
    {
        return _context.Set<Car>().SingleOrDefault(filter);
    }

    public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
    {
        return filter == null ? 
            _context.Set<Car>().ToList() 
            : _context.Set<Car>().Where(filter).ToList();
    }

    public void Add(Car entity)
    {
        var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            _context.SaveChanges();
    }

    public void Update(Car entity)
    {
        var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            _context.SaveChanges();
    }

    public void Delete(Car entity)
    {
        var deletedEntity = _context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            _context.SaveChanges();
    }
}