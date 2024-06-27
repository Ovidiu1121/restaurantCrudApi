using RestaurantCrudApi.Dto;
using RestaurantCrudApi.Restaurants.Model;

namespace RestaurantCrudApi.Restaurants.Service.interfaces
{
    public interface IRestaurantQueryService
    {
        Task<ListRestaurantDto> GetAllRestaurants();
        Task<RestaurantDto> GetByLocation(string location);
        Task<RestaurantDto> GetById(int id);
        Task<RestaurantDto> GetByName(string name);
        Task<RestaurantDto> GetByRating(int rating);
    }
}
