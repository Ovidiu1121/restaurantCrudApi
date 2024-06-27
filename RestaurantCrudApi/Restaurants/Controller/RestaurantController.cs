using Microsoft.AspNetCore.Mvc;
using RestaurantCrudApi.Dto;
using RestaurantCrudApi.Restaurants.Controller.interfaces;
using RestaurantCrudApi.Restaurants.Model;
using RestaurantCrudApi.Restaurants.Repository.interfaces;
using RestaurantCrudApi.Restaurants.Service.interfaces;
using RestaurantCrudApi.System.Exceptions;

namespace RestaurantCrudApi.Restaurants.Controller
{
    public class RestaurantController: RestaurantApiController
    {
        private IRestaurantCommandService _restaurantCommandService;
        private IRestaurantQueryService _restaurantQueryService;

        public RestaurantController(IRestaurantCommandService restaurantCommandService, IRestaurantQueryService restaurantQueryService)
        {
           _restaurantCommandService = restaurantCommandService;
           _restaurantQueryService = restaurantQueryService;
        }

        public override async Task<ActionResult<RestaurantDto>> CreateRestaurant([FromBody] CreateRestaurantRequest request)
        {
            try
            {
                var restaurants = await _restaurantCommandService.CreateRestaurant(request);

                return Created("Restaurantul a fost adaugat",restaurants);
            }
            catch (ItemAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<RestaurantDto>> DeleteRestaurant([FromRoute] int id)
        {
            try
            {
                var restaurants = await _restaurantCommandService.DeleteRestaurant(id);

                return Accepted("", restaurants);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<RestaurantDto>> GetByIdRoute(int id)
        {
            try
            {
                var restaurants = await _restaurantQueryService.GetById(id);
                return Ok(restaurants);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<RestaurantDto>> GetByNameRoute(string name)
        {
            try
            {
                var restaurants = await _restaurantQueryService.GetByName(name);
                return Ok(restaurants);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ListRestaurantDto>> GetAll()
        {
            try
            {
                var restaurants = await _restaurantQueryService.GetAllRestaurants();
                return Ok(restaurants);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<RestaurantDto>> GetByLocationRoute([FromRoute] string location)
        {
            try
            {
                var restaurants = await _restaurantQueryService.GetByLocation(location);
                return Ok(restaurants);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<RestaurantDto>> GetByRatingRoute(int rating)
        {
            try
            {
                var restaurants = await _restaurantQueryService.GetByRating(rating);
                return Ok(restaurants);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<RestaurantDto>> UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantRequest request)
        {
            try
            {
                var restaurants = await _restaurantCommandService.UpdateRestaurant(id, request);

                return Ok(restaurants);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
