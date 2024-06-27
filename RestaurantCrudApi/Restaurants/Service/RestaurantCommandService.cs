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

        public RestaurantCommandService(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public async Task<RestaurantDto> CreateRestaurant(CreateRestaurantRequest request)
        {
            RestaurantDto restaurant = await _repository.GetByLocationAsync(request.Location);

            if (restaurant!=null)
            {
                throw new ItemAlreadyExists(Constants.RESTAURANT_ALREADY_EXIST);
            }

            restaurant=await _repository.CreateRestaurant(request);
            return restaurant;
        }

        public async Task<RestaurantDto> DeleteRestaurant(int id)
        {
            RestaurantDto restaurant = await _repository.GetByIdAsync(id);

            if (restaurant==null)
            {
                throw new ItemDoesNotExist(Constants.RESTAURANT_DOES_NOT_EXIST);
            }

            await _repository.DeleteRestaurantById(id);
            return restaurant;
        }

        public async Task<RestaurantDto> UpdateRestaurant(int id, UpdateRestaurantRequest request)
        {
            RestaurantDto restaurant = await _repository.GetByIdAsync(id);

            if (restaurant==null)
            {
                throw new ItemDoesNotExist(Constants.RESTAURANT_DOES_NOT_EXIST);
            }

            restaurant = await _repository.UpdateRestaurant(id, request);
            return restaurant;
        }
    }
}
