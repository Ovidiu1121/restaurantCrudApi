
using RestaurantCrudApi.Dto;
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

        public RestaurantQueryService(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public async Task<ListRestaurantDto> GetAllRestaurants()
        {
            ListRestaurantDto restaurants = await _repository.GetAllAsync();

            if (restaurants.restaurantList.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_RESTAURANTS_EXIST);
            }

            return restaurants;
        }

        public async Task<RestaurantDto> GetById(int id)
        {
            RestaurantDto restaurants = await _repository.GetByIdAsync(id);

            if (restaurants == null)
            {
                throw new ItemDoesNotExist(Constants.RESTAURANT_DOES_NOT_EXIST);
            }

            return restaurants;
        }

        public async Task<RestaurantDto> GetByName(string name)
        {
            RestaurantDto restaurants = await _repository.GetByNameAsync(name);

            if (restaurants == null)
            {
                throw new ItemDoesNotExist(Constants.RESTAURANT_DOES_NOT_EXIST);
            }

            return restaurants;
        }

        public async Task<RestaurantDto> GetByRating(int rating)
        {
            RestaurantDto restaurants = await _repository.GetByRatingAsync(rating);

            if (restaurants == null)
            {
                throw new ItemDoesNotExist(Constants.RESTAURANT_DOES_NOT_EXIST);
            }

            return restaurants;
        }

        public async Task<RestaurantDto> GetByLocation(string location)
        {
            RestaurantDto restaurants = await _repository.GetByLocationAsync(location);

            if (restaurants == null)
            {
                throw new ItemDoesNotExist(Constants.RESTAURANT_DOES_NOT_EXIST);
            }

            return restaurants;
        }
    }
}
