using RestaurantCrudApi.Dto;
using RestaurantCrudApi.Restaurants.Model;
using RestaurantCrudApi.Restaurants.Repository;
using RestaurantCrudApi.Restaurants.Repository.interfaces;
using RestaurantCrudApi.Restaurants.Service.interfaces;
using RestaurantCrudApi.System.Constant;
using RestaurantCrudApi.System.Exceptions;

namespace RestaurantCrudApi.Restaurants.Service
{
    public class RestaurantCommandService: IRestaurantCommandService
    {

        private IRestaurantRepository _repository;

        public RestaurantCommandService(RestaurantRepository repository)
        {
            _repository = repository;
        }

        public async Task<Restaurant> CreateRestaurant(CreateRestaurantRequest request)
        {
            Restaurant restaurant = await _repository.GetByLocationAsync(request.Location);

            if (restaurant!=null)
            {
                throw new ItemAlreadyExists(Constants.RESTAURANT_ALREADY_EXIST);
            }

            restaurant=await _repository.CreateRestaurant(request);
            return restaurant;
        }

        public async Task<Restaurant> DeleteRestaurant(int id)
        {
            Restaurant restaurant = await _repository.GetByIdAsync(id);

            if (restaurant==null)
            {
                throw new ItemDoesNotExist(Constants.RESTAURANT_DOES_NOT_EXIST);
            }

            await _repository.DeleteRestaurantById(id);
            return restaurant;
        }

        public async Task<Restaurant> UpdateRestaurant(int id, UpdateRestaurantRequest request)
        {
            Restaurant restaurant = await _repository.GetByIdAsync(id);

            if (restaurant==null)
            {
                throw new ItemDoesNotExist(Constants.RESTAURANT_DOES_NOT_EXIST);
            }

            restaurant = await _repository.UpdateRestaurant(id, request);
            return restaurant;
        }
    }
}
