using RestaurantCrudApi.Dto;
using RestaurantCrudApi.Restaurants.Model;

namespace RestaurantCrudApi.Restaurants.Service.interfaces
{
    public interface IRestaurantCommandService
    {
        Task<Restaurant> CreateRestaurant(CreateRestaurantRequest request);
        Task<Restaurant> UpdateRestaurant(int id, UpdateRestaurantRequest request);
        Task<Restaurant> DeleteRestaurant(int id);

    }
}
