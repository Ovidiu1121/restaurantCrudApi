using RestaurantCrudApi.Dto;
using RestaurantCrudApi.Restaurants.Model;

namespace RestaurantCrudApi.Restaurants.Repository.interfaces
{
    public interface IRestaurantRepository
    {
        Task<ListRestaurantDto> GetAllAsync();
        Task<RestaurantDto> GetByIdAsync(int id);
        Task<RestaurantDto> GetByNameAsync(string name);
        Task<RestaurantDto> GetByLocationAsync(string location);
        Task<RestaurantDto> GetByRatingAsync(int rating);
        Task<RestaurantDto> CreateRestaurant(CreateRestaurantRequest request);
        Task<RestaurantDto> UpdateRestaurant(int id, UpdateRestaurantRequest request);
        Task<RestaurantDto> DeleteRestaurantById(int id);
    }
}
