using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework;

public class CarRentalContext : DbContext
{
    override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=CarRentalProject;User Id=postgres;Password=1071");
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); 
        //bu satır PostgreSql'de datetime tipindeki verileri okumak için eklenmiştir.
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasNoKey();//Customer tablosu için bir primary key oluşturulmamıştır. //
                                                   //Bu satır bu durumu düzeltmek için eklenmiştir.
                                                   ////Çünkü CustomerId UserId ye bağlı.
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<CarImage> CarImages { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }

}