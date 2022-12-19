using System.Linq.Expressions;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework;

public class EfBrandDal : IBrandDal
{
    DbContext _context = new CarRentalContext();

    public Brand get(Expression<Func<Brand, bool>> filter)
    {
        return _context.Set<Brand>().SingleOrDefault(filter);
    }

    public List<Brand> GetAll(Expression<Func<Brand, bool>> filter = null)
    {
        return filter == null ? 
            _context.Set<Brand>().ToList() 
            : _context.Set<Brand>().Where(filter).ToList();
    }

    public void Add(Brand entity)
    {
        var addedEntity = _context.Entry(entity);
        addedEntity.State = EntityState.Added;
        _context.SaveChanges();
    }

    public void Update(Brand entity)
    {
        var updatedEntity = _context.Entry(entity);
        updatedEntity.State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(Brand entity)
    {
        var deletedEntity = _context.Entry(entity);
        deletedEntity.State = EntityState.Deleted;
        _context.SaveChanges();
    }
}