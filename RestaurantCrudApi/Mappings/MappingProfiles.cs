using AutoMapper;
using RestaurantCrudApi.Dto;
using RestaurantCrudApi.Restaurants.Model;

namespace RestaurantCrudApi.Mappings
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateRestaurantRequest, Restaurant>();
            CreateMap<UpdateRestaurantRequest, Restaurant>();
        }

    }
}
