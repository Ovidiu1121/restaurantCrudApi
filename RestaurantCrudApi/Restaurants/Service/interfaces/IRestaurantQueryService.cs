using RestaurantCrudApi.Restaurants.Model;

namespace RestaurantCrudApi.Restaurants.Service.interfaces
{
    public interface IRestaurantQueryService
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurants();
        Task<Restaurant> GetByLocation(string location);
        Task<Restaurant> GetById(int id);
    }
}
