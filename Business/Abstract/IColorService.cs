using Entities.Concrete;

namespace Business.Abstract;

public interface IColorService
{
    List<Color>GetAll();
    List<Color>GetByColorId(int colorId);
    void Add(Color color);
    void Update(Color color);
    void Delete(Color color);
    
}