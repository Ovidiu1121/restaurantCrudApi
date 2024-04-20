using RestaurantCrudApi.Dto;
using RestaurantCrudApi.Restaurants.Model;

namespace RestaurantCrudApi.Restaurants.Repository.interfaces
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant> GetByIdAsync(int id);
        Task<Restaurant> GetByLocationAsync(string location);
        Task<Restaurant> CreateRestaurant(CreateRestaurantRequest request);
        Task<Restaurant> UpdateRestaurant(int id, UpdateRestaurantRequest request);
        Task<Restaurant> DeleteRestaurantById(int id);
    }
}
