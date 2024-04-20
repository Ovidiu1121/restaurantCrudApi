
using RestaurantCrudApi.Restaurants.Model;
using RestaurantCrudApi.Restaurants.Repository;
using RestaurantCrudApi.Restaurants.Repository.interfaces;
using RestaurantCrudApi.Restaurants.Service.interfaces;
using RestaurantCrudApi.System.Constant;
using RestaurantCrudApi.System.Exceptions;

namespace RestaurantCrudApi.Restaurants.Service
{
    public class RestaurantQueryService: IRestaurantQueryService
    {
        private IRestaurantRepository _repository;

        public RestaurantQueryService(RestaurantRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            IEnumerable<Restaurant> restaurants = await _repository.GetAllAsync();

            if (restaurants.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_RESTAURANTS_EXIST);
            }

            return restaurants;
        }

        public async Task<Restaurant> GetById(int id)
        {
            Restaurant restaurants = await _repository.GetByIdAsync(id);

            if (restaurants == null)
            {
                throw new ItemDoesNotExist(Constants.RESTAURANT_DOES_NOT_EXIST);
            }

            return restaurants;
        }

        public async Task<Restaurant> GetByLocation(string location)
        {
            Restaurant restaurants = await _repository.GetByLocationAsync(location);

            if (restaurants == null)
            {
                throw new ItemDoesNotExist(Constants.RESTAURANT_DOES_NOT_EXIST);
            }

            return restaurants;
        }
    }
}
