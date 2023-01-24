namespace Entities.Concrete.DTOs;

public class RentalDetailDto
{
    public int RentalId { get; set; }
    public string CompanyName { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string CarName { get; set; }
    public string BrandName { get; set; }
    public int ModelYear { get; set; }
    public decimal DailyPrice { get; set; }
    public string ColorName { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}