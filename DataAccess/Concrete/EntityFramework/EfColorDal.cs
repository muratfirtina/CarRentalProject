using System.Linq.Expressions;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework;

public class EfColorDal : IColorDal
{
    DbContext _context= new CarRentalContext();        //Metodun içinde oluşturulduğu zaman performans kaybı yaşanır.
                                                      //Bu yüzden contexti classın içinde metodun dışında oluşturduk.

    public Color get(Expression<Func<Color, bool>> filter)
    {
        return _context.Set<Color>().SingleOrDefault(filter);
    }

    public List<Color> GetAll(Expression<Func<Color, bool>> filter = null)
    {
        return filter == null ?
            _context.Set<Color>().ToList() :
            _context.Set<Color>().Where(filter).ToList();
    }

    public void Add(Color entity)
    {
        var addedEntity = _context.Entry(entity);
        addedEntity.State = EntityState.Added;
        _context.SaveChanges();
    }

    public void Update(Color entity)
    {
        var updatedEntity = _context.Entry(entity);
        updatedEntity.State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(Color entity)
    {
        var deletedEntity = _context.Entry(entity);
        deletedEntity.State = EntityState.Deleted;
        _context.SaveChanges();
    }
}