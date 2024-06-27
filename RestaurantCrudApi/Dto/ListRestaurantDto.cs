namespace RestaurantCrudApi.Dto;

public class ListRestaurantDto
{
    public ListRestaurantDto()
    {
        restaurantList = new List<RestaurantDto>();
    }
    
    public List<RestaurantDto> restaurantList { get; set; }
}