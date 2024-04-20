using AutoMapper;
using RestaurantCrudApi.Data;
using RestaurantCrudApi.Dto;
using RestaurantCrudApi.Restaurants.Model;
using RestaurantCrudApi.Restaurants.Repository.interfaces;
using System.Data.Entity;

namespace RestaurantCrudApi.Restaurants.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RestaurantRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Restaurant> CreateRestaurant(CreateRestaurantRequest request)
        {
            var restaurants = _mapper.Map<Restaurant>(request);

            _context.Restaurants.Add(restaurants);

            await _context.SaveChangesAsync();

            return restaurants;
        }

        public async Task<Restaurant> DeleteRestaurantById(int id)
        {
            var restaurants = await _context.Restaurants.FindAsync(id);

            _context.Restaurants.Remove(restaurants);

            await _context.SaveChangesAsync();

            return restaurants;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant> GetByIdAsync(int id)
        {
            return await _context.Restaurants.FirstOrDefaultAsync(obj => obj.Id.Equals(id));
        }

        public async Task<Restaurant> GetByLocationAsync(string location)
        {
            return await _context.Restaurants.FirstOrDefaultAsync(obj => obj.Location.Equals(location));
        }

        public async Task<Restaurant> UpdateRestaurant(int id, UpdateRestaurantRequest request)
        {
            var restaurants = await _context.Restaurants.FindAsync(id);

            restaurants.Name= request.Name ?? restaurants.Name;
            restaurants.Location= request.Location ?? restaurants.Location;
            restaurants.Rating=request.Rating ?? restaurants.Rating;

            _context.Restaurants.Update(restaurants);

            await _context.SaveChangesAsync();

            return restaurants;
        }
    }
}
