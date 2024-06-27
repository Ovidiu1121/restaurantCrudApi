using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantCrudApi.Data;
using RestaurantCrudApi.Dto;
using RestaurantCrudApi.Restaurants.Model;
using RestaurantCrudApi.Restaurants.Repository.interfaces;


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

        public async Task<RestaurantDto> GetByRatingAsync(int rating)
        {
            var restaurant = await _context.Restaurants.Where(r => r.Rating == rating).FirstOrDefaultAsync();
            
            return _mapper.Map<RestaurantDto>(restaurant);
        }

        public async Task<RestaurantDto> CreateRestaurant(CreateRestaurantRequest request)
        {
            var restaurants = _mapper.Map<Restaurant>(request);

            _context.Restaurants.Add(restaurants);

            await _context.SaveChangesAsync();

            return _mapper.Map<RestaurantDto>(restaurants);
        }

        public async Task<RestaurantDto> DeleteRestaurantById(int id)
        {
            var restaurants = await _context.Restaurants.FindAsync(id);

            _context.Restaurants.Remove(restaurants);

            await _context.SaveChangesAsync();

            return _mapper.Map<RestaurantDto>(restaurants);
        }

        public async Task<ListRestaurantDto> GetAllAsync()
        {
            List<Restaurant> result = await _context.Restaurants.ToListAsync();
            
            ListRestaurantDto listRestaurantrDto = new ListRestaurantDto()
            {
                restaurantList = _mapper.Map<List<RestaurantDto>>(result)
            };

            return listRestaurantrDto;
        }

        public async Task<RestaurantDto> GetByIdAsync(int id)
        {
            var restaurant = await _context.Restaurants.Where(r => r.Id == id).FirstOrDefaultAsync();
            
            return _mapper.Map<RestaurantDto>(restaurant);
        }

        public async Task<RestaurantDto> GetByNameAsync(string name)
        {
            var restaurant = await _context.Restaurants.Where(r => r.Name.Equals(name)).FirstOrDefaultAsync();
            
            return _mapper.Map<RestaurantDto>(restaurant);
        }

        public async Task<RestaurantDto> GetByLocationAsync(string location)
        {
            var restaurant = await _context.Restaurants.Where(r => r.Location.Equals(location)).FirstOrDefaultAsync();
            
            return _mapper.Map<RestaurantDto>(restaurant);
        }

        public async Task<RestaurantDto> UpdateRestaurant(int id, UpdateRestaurantRequest request)
        {
            var restaurants = await _context.Restaurants.FindAsync(id);

            restaurants.Name= request.Name ?? restaurants.Name;
            restaurants.Location= request.Location ?? restaurants.Location;
            restaurants.Rating=request.Rating ?? restaurants.Rating;

            _context.Restaurants.Update(restaurants);

            await _context.SaveChangesAsync();

            return _mapper.Map<RestaurantDto>(restaurants);
        }
    }
}
