using Microsoft.AspNetCore.Mvc;
using RestaurantCrudApi.Dto;
using RestaurantCrudApi.Restaurants.Model;

namespace RestaurantCrudApi.Restaurants.Controller.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class RestaurantApiController:ControllerBase
    {
        [HttpGet("all")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Restaurant>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<ListRestaurantDto>> GetAll();

        [HttpPost("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(Restaurant))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<RestaurantDto>> CreateRestaurant([FromBody] CreateRestaurantRequest request);

        [HttpPut("update/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Restaurant))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<RestaurantDto>> UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantRequest request);

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Restaurant))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<RestaurantDto>> DeleteRestaurant([FromRoute] int id);

        [HttpGet("id/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Restaurant))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<RestaurantDto>> GetByIdRoute([FromRoute] int id);
        
        [HttpGet("name/{name}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Restaurant))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<RestaurantDto>> GetByNameRoute([FromRoute] string name);
        
        [HttpGet("location/{location}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Restaurant))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<RestaurantDto>> GetByLocationRoute([FromRoute] string location);

        [HttpGet("rating/{rating}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Restaurant))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<RestaurantDto>> GetByRatingRoute([FromRoute] int rating);

    }
}
