using RestaurantCrudApi.Dto;
using RestaurantCrudApi.Restaurants.Model;

namespace RestaurantCrudApi.Restaurants.Service.interfaces
{
    public interface IRestaurantCommandService
    {
        Task<RestaurantDto> CreateRestaurant(CreateRestaurantRequest request);
        Task<RestaurantDto> UpdateRestaurant(int id, UpdateRestaurantRequest request);
        Task<RestaurantDto> DeleteRestaurant(int id);
    }
}
