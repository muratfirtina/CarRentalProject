using Core.Entities;

namespace Entities.Concrete.DTOs;

public class CarDetailDto : IDto
{
    public int Id { get; set; }
    public string CarName { get; set; }
    public string BrandName { get; set; }
    public string ColorName { get; set; }
    public int ModelYear { get; set; }
    
    public decimal DailyPrice { get; set; }
    public List<CarImage> CarImages { get; set; }
}