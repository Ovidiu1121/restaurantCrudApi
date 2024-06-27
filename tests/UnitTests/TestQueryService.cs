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

public class TestQueryService
{
    Mock<IRestaurantRepository> _mock;
    IRestaurantQueryService _service;

    public TestQueryService()
    {
        _mock=new Mock<IRestaurantRepository>();
        _service=new RestaurantQueryService(_mock.Object);
    }
    
    [Fact]
    public async Task GetAll_ItemsDoNotExist()
    {
        _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new ListRestaurantDto());

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetAllRestaurants());

        Assert.Equal(exception.Message, Constants.NO_RESTAURANTS_EXIST);       

    }

    [Fact]
    public async Task GetAll_ReturnAllRestaurants()
    {

        var restaurants = TestRestaurantFactory.CreateRestaurants(5);

        _mock.Setup(repo=>repo.GetAllAsync()).ReturnsAsync(restaurants);

        var result = await _service.GetAllRestaurants();

        Assert.NotNull(result);
        Assert.Contains(restaurants.restaurantList[1], result.restaurantList);

    }

    [Fact]
    public async Task GetById_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((RestaurantDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(()=>_service.GetById(1));

        Assert.Equal(Constants.RESTAURANT_DOES_NOT_EXIST, exception.Message);

    }
    
    [Fact]
    public async Task GetById_ReturnRestaurant()
    {

        var restaurant = TestRestaurantFactory.CreateRestaurant(2);

        _mock.Setup(repo => repo.GetByIdAsync(2)).ReturnsAsync(restaurant);

        var result = await _service.GetById(2);

        Assert.NotNull(result);
        Assert.Equal(restaurant, result);

    }

    [Fact]
    public async Task GetByName_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.GetByNameAsync("")).ReturnsAsync((RestaurantDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByName(""));

        Assert.Equal(Constants.RESTAURANT_DOES_NOT_EXIST, exception.Message);

    }

    [Fact]
    public async Task GetByName_ReturnRestaurant()
    {

        var restaurant = TestRestaurantFactory.CreateRestaurant(2);
        restaurant.Name="test";

        _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync(restaurant);

        var result = await _service.GetByName("test");

        Assert.NotNull(result);
        Assert.Equal(restaurant, result);

    }
    
    [Fact]
    public async Task GetByLocation_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.GetByLocationAsync("")).ReturnsAsync((RestaurantDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByLocation(""));

        Assert.Equal(Constants.RESTAURANT_DOES_NOT_EXIST, exception.Message);

    }

    [Fact]
    public async Task GetByLocation_ReturnRestaurant()
    {

        var restaurant = TestRestaurantFactory.CreateRestaurant(2);
        restaurant.Location="test";

        _mock.Setup(repo => repo.GetByLocationAsync("test")).ReturnsAsync(restaurant);

        var result = await _service.GetByLocation("test");

        Assert.NotNull(result);
        Assert.Equal(restaurant, result);
    }
    
    [Fact]
    public async Task GetByRating_ItemDoesNotExist()
    {

        _mock.Setup(repo => repo.GetByRatingAsync(4)).ReturnsAsync((RestaurantDto)null);

        var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(()=>_service.GetByRating(4));

        Assert.Equal(Constants.RESTAURANT_DOES_NOT_EXIST, exception.Message);

    }
    
    [Fact]
    public async Task GetByRating_ReturnRestaurant()
    {

        var restaurant = TestRestaurantFactory.CreateRestaurant(3);

        _mock.Setup(repo => repo.GetByRatingAsync(3)).ReturnsAsync(restaurant);
        var result = await _service.GetByRating(3);

        Assert.NotNull(result);
        Assert.Equal(restaurant, result);

    }
    
    
    
}