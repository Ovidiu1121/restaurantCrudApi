using Microsoft.EntityFrameworkCore;
using RestaurantCrudApi.Restaurants.Model;

namespace RestaurantCrudApi.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Restaurant> Restaurants { get; set; }

    }
}
