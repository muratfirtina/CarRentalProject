using Microsoft.AspNetCore.Http;

namespace Entities.Concrete.DTOs;

public class CarImageUploadDto
{
    public IFormFile file { get; set; }
    public int CarId { get; set; }
}