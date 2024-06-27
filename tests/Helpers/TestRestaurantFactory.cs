using RestaurantCrudApi.Dto;

namespace tests.Helpers;

public class TestRestaurantFactory
{
    public static RestaurantDto CreateRestaurant(int id)
    {
        return new RestaurantDto
        {
            Id = id,
            Name="ElGringo"+id,
            Location="center"+id,
            Rating=2+id
        };
    }

    public static ListRestaurantDto CreateRestaurants(int count)
    {
        ListRestaurantDto doctors=new ListRestaurantDto();
            
        for(int i = 0; i<count; i++)
        {
            doctors.restaurantList.Add(CreateRestaurant(i));
        }
        return doctors;
    }
}