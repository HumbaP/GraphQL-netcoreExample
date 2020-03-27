using CarvedRock.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarvedRock.Data
{
    public class CarvedRockDbContext : DbContext
    {
        public CarvedRockDbContext(DbContextOptions<CarvedRockDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products {get;set;}
        public  DbSet<ProductReview> ProductReviews {get;set;}

    }
}