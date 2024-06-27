using System.Threading.Tasks;
using Moq;
using RestaurantCrudApi.Dto;
using RestaurantCrudApi.Restaurants.Repository.interfaces;
using RestaurantCrudApi.Restaurants.Service;
using RestaurantCrudApi.Restaurants.Service.interfaces;
using RestaurantCrudApi.System.Constant;
using RestaurantCrudApi.System.Exceptions;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests;

public class TestCommandService
{
    Mock<IRestaurantRepository> _mock;
    IRestaurantCommandService _service;

    public TestCommandService()
    {
        _mock = new Mock<IRestaurantRepository>();
        _service = new RestaurantCommandService(_mock.Object);
    }
    
    [Fact]
    public async Task Create_InvalidData()
    {
        var create = new CreateRestaurantRequest()
        {
            Name="Test",
            Location= "test",
            Rating= 0
        };

        var restaurant = TestRestaurantFactory.CreateRestaurant(2);

        _mock.Setup(repo => repo.GetByLocationAsync("test")).ReturnsAsync(restaurant);
                
        var exception=  await Assert.ThrowsAsync<ItemAlreadyExists>(()=>_service.CreateRestaurant(create));

        Assert.Equal(Constants.RESTAURANT_ALREADY_EXIST, exception.Message);
    }

    [Fact]
    public async Task Create_ReturnDoctor()
    {

        var create = new CreateRestaurantRequest()
        {
            Name="Test",
            Location= "test",
            Rating= 4
        };

        var restaurant = TestRestaurantFactory.CreateRestaurant(2);

        restaurant.Name=create.Name;
        restaurant.Location=create.Location;
        restaurant.Rating=create.Rating;

        _mock.Setup(repo => repo.CreateRestaurant(It.IsAny<CreateRestaurantRequest>())).ReturnsAsync(restaurant);

        var result = await _service.CreateRestaurant(create);

        Assert.NotNull(result);
        Assert.Equal(result, restaurant);
    }

    [Fact]
    public async Task Update_ItemDoesNotExist()
    {
        var update = new UpdateRestaurantRequest()
        {
            Name="Test",
            Location= "test",
            Rating= 0
        };

        _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((RestaurantDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateRestaurant(1, update));

        Assert.Equal(Constants.RESTAURANT_DOES_NOT_EXIST, exception.Message);

    }
    
    [Fact]
    public async Task Update_InvalidData()
    {
        var update = new UpdateRestaurantRequest()
        {
            Name="Test",
            Location= "test",
            Rating= 2
        };

        _mock.Setup(repo=>repo.GetByIdAsync(1)).ReturnsAsync((RestaurantDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateRestaurant(5, update));

        Assert.Equal(Constants.RESTAURANT_DOES_NOT_EXIST, exception.Message);

    }

    [Fact]
    public async Task Update_ValidData()
    {
        var update = new UpdateRestaurantRequest()
        {
            Name="Test",
            Location= "test",
            Rating= 2
        };

        var restaurant = TestRestaurantFactory.CreateRestaurant(2);
        restaurant.Name=update.Name;
        restaurant.Location=update.Location;
        restaurant.Rating=update.Rating.Value;

        _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(restaurant);
        _mock.Setup(repoo => repoo.UpdateRestaurant(It.IsAny<int>(), It.IsAny<UpdateRestaurantRequest>())).ReturnsAsync(restaurant);

        var result = await _service.UpdateRestaurant(5, update);

        Assert.NotNull(result);
        Assert.Equal(restaurant, result);

    }

    [Fact]
    public async Task Delete_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.DeleteRestaurantById(It.IsAny<int>())).ReturnsAsync((RestaurantDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteRestaurant(5));

        Assert.Equal(exception.Message, Constants.RESTAURANT_DOES_NOT_EXIST);

    }

    [Fact]
    public async Task Delete_ValidData()
    {
        var restaurant = TestRestaurantFactory.CreateRestaurant(2);

        _mock.Setup(repo => repo.GetByIdAsync(2)).ReturnsAsync(restaurant);

        var result= await _service.DeleteRestaurant(2);

        Assert.NotNull(result);
        Assert.Equal(restaurant, result);


    }
    
    
}