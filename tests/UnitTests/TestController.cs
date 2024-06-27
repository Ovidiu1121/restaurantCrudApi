using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestaurantCrudApi.Dto;
using RestaurantCrudApi.Restaurants.Controller;
using RestaurantCrudApi.Restaurants.Controller.interfaces;
using RestaurantCrudApi.Restaurants.Service.interfaces;
using RestaurantCrudApi.System.Constant;
using RestaurantCrudApi.System.Exceptions;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests;

public class TestController
{
    Mock<IRestaurantCommandService> _command;
    Mock<IRestaurantQueryService> _query;
    RestaurantApiController _controller;

    public TestController()
    {
        _command = new Mock<IRestaurantCommandService>();
        _query = new Mock<IRestaurantQueryService>();
        _controller = new RestaurantController(_command.Object, _query.Object);
    }
    
    [Fact]
    public async Task GetAll_ItemsDoNotExist()
    {

        _query.Setup(repo => repo.GetAllRestaurants()).ThrowsAsync(new ItemDoesNotExist(Constants.RESTAURANT_DOES_NOT_EXIST));
           
        var result = await _controller.GetAll();

        var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Equal(404, notFound.StatusCode);
        Assert.Equal(Constants.RESTAURANT_DOES_NOT_EXIST, notFound.Value);

    }

    [Fact]
    public async Task GetAll_ValidData()
    {

        var restaurants = TestRestaurantFactory.CreateRestaurants(5);

        _query.Setup(repo => repo.GetAllRestaurants()).ReturnsAsync(restaurants);

        var result = await _controller.GetAll();
        var okresult = Assert.IsType<OkObjectResult>(result.Result);
        var restaurantsAll = Assert.IsType<ListRestaurantDto>(okresult.Value);

        Assert.Equal(5, restaurantsAll.restaurantList.Count);
        Assert.Equal(200, okresult.StatusCode);
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

        _command.Setup(repo => repo.CreateRestaurant(It.IsAny<CreateRestaurantRequest>())).ThrowsAsync(new ItemAlreadyExists(Constants.RESTAURANT_ALREADY_EXIST));

        var result = await _controller.CreateRestaurant(create);
        var bad=Assert.IsType<BadRequestObjectResult>(result.Result);

        Assert.Equal(400,bad.StatusCode);
        Assert.Equal(Constants.RESTAURANT_ALREADY_EXIST, bad.Value);

    }

    [Fact]
    public async Task Create_ValidData()
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

        _command.Setup(repo => repo.CreateRestaurant(create)).ReturnsAsync(restaurant);

        var result = await _controller.CreateRestaurant(create);

        var okResult= Assert.IsType<CreatedResult>(result.Result);

        Assert.Equal(okResult.StatusCode, 201);
        Assert.Equal(restaurant, okResult.Value);

    }

    [Fact]
    public async Task Update_InvalidDate()
    {

        var update = new UpdateRestaurantRequest()
        {
            Name="Test",
            Location= "test",
            Rating= 0
        };

        _command.Setup(repo => repo.UpdateRestaurant(11, update)).ThrowsAsync(new ItemDoesNotExist(Constants.RESTAURANT_DOES_NOT_EXIST));

        var result = await _controller.UpdateRestaurant(11, update);

        var bad = Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Equal(bad.StatusCode, 404);
        Assert.Equal(bad.Value, Constants.RESTAURANT_DOES_NOT_EXIST);

    }
    
    [Fact]
    public async Task Update_ValidData()
    {

        var update = new UpdateRestaurantRequest()
        {
            Name="Test",
            Location= "test",
            Rating= 0
        };

        var restaurant = TestRestaurantFactory.CreateRestaurant(2);
        restaurant.Name=update.Name;
        restaurant.Location=update.Location;
        restaurant.Rating=update.Rating.Value;

        _command.Setup(repo=>repo.UpdateRestaurant(5,update)).ReturnsAsync(restaurant);

        var result = await _controller.UpdateRestaurant(5, update);

        var okResult=Assert.IsType<OkObjectResult>(result.Result);

        Assert.Equal(okResult.StatusCode, 200);
        Assert.Equal(okResult.Value, restaurant);

    }


    [Fact]
    public async Task Delete_ItemDoesNotExist()
    {

        _command.Setup(repo=>repo.DeleteRestaurant(2)).ThrowsAsync(new ItemDoesNotExist(Constants.RESTAURANT_DOES_NOT_EXIST));

        var result= await _controller.DeleteRestaurant(2);

        var notfound= Assert.IsType<NotFoundObjectResult>(result.Result);

        Assert.Equal(notfound.StatusCode, 404);
        Assert.Equal(notfound.Value, Constants.RESTAURANT_DOES_NOT_EXIST);

    }

    [Fact]
    public async Task Delete_ValidData()
    {
        var restaurant = TestRestaurantFactory.CreateRestaurant(9);

        _command.Setup(repo => repo.DeleteRestaurant(9)).ReturnsAsync(restaurant);

        var result = await _controller.DeleteRestaurant(9);

        var okResult=Assert.IsType<AcceptedResult>(result.Result);

        Assert.Equal(202, okResult.StatusCode);
        Assert.Equal(restaurant, okResult.Value);

    }
    
    
}