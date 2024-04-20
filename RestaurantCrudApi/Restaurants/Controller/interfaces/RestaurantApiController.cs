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
        public abstract Task<ActionResult<IEnumerable<Restaurant>>> GetAll();

        [HttpPost("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(Restaurant))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Restaurant>> CreateRestaurant([FromBody] CreateRestaurantRequest request);

        [HttpPut("update/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Restaurant))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Restaurant>> UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantRequest request);

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Restaurant))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Restaurant>> DeleteRestaurant([FromRoute] int id);

        [HttpGet("{location}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Restaurant))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Restaurant>> GetByLocationRoute([FromRoute] string location);


    }
}
