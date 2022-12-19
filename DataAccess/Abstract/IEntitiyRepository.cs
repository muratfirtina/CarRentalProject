using System.Linq.Expressions;
using Entities.Abstract;

namespace DataAccess.Abstract;

public interface IEntitiyRepository<T> where T:class, IEntity, new()

{
    T get(Expression<Func<T, bool>> filter);
    List<T> GetAll(Expression<Func<T,bool>>filter=null);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}