using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestaurantCrudApi.Dto;
using RestaurantCrudApi.Restaurants.Model;
using tests.Infrastructure;
using Xunit;

namespace tests.IntegrationTests;

public class RestaurantIntegrationTests:IClassFixture<ApiWebApplicationFactory>
{
    
    private readonly HttpClient _client;

    public RestaurantIntegrationTests(ApiWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/Restaurant/create";
        var restaurant = new CreateRestaurantRequest() { Name = "new name", Location = "new location", Rating = 4 };
        var content = new StringContent(JsonConvert.SerializeObject(restaurant), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Restaurant>(responseString);

        Assert.NotNull(result);
        Assert.Equal(restaurant.Name, result.Name);
        Assert.Equal(restaurant.Location, result.Location);
        Assert.Equal(restaurant.Rating, result.Rating);
        
    }
    
    [Fact]
    public async Task Post_Create_RestaurantAlreadyExists_ReturnsBadRequestStatusCode()
    {
        var request = "/api/v1/Restaurant/create";
        var restaurant = new CreateRestaurantRequest() { Name = "new name", Location = "new location", Rating = 4 };
        var content = new StringContent(JsonConvert.SerializeObject(restaurant), Encoding.UTF8, "application/json");

        await _client.PostAsync(request, content);
        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        
    }
    
     [Fact]
    public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/Restaurant/create";
        var restaurant = new CreateRestaurantRequest() { Name = "new name", Location = "new location", Rating = 4 };
        var content = new StringContent(JsonConvert.SerializeObject(restaurant), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Restaurant>(responseString)!;

        request = "/api/v1/Restaurant/update/"+result.Id;
        var updateRestaurant = new UpdateRestaurantRequest{  Name = "updated name", Location = "updated location", Rating = 4 };
        content = new StringContent(JsonConvert.SerializeObject(updateRestaurant), Encoding.UTF8, "application/json");

        response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        responseString = await response.Content.ReadAsStringAsync();
        result = JsonConvert.DeserializeObject<Restaurant>(responseString)!;

        Assert.Equal(updateRestaurant.Name, result.Name);
        Assert.Equal(updateRestaurant.Location, result.Location);
        Assert.Equal(updateRestaurant.Rating, result.Rating);
    }

    [Fact]
    public async Task Put_Update_RestaurantDoesNotExists_ReturnsNotFoundStatusCode()
    {
        
        var request = "/api/v1/Restaurant/update/1";
        var updateRestaurant = new UpdateRestaurantRequest { Name = "updated name", Location = "updated location", Rating = 4 };
        var content = new StringContent(JsonConvert.SerializeObject(updateRestaurant), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
    }

    [Fact]
    public async Task Delete_Delete_RestaurantExists_ReturnsDeletedRestaurant()
    {

        var request = "/api/v1/Restaurant/create";
        var restaurant = new CreateRestaurantRequest() { Name = "new name", Location = "new location", Rating = 4 };
        var content = new StringContent(JsonConvert.SerializeObject(restaurant), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Restaurant>(responseString)!;

        request = "/api/v1/Restaurant/delete/" + result.Id;

        response = await _client.DeleteAsync(request);
        
        Assert.Equal(HttpStatusCode.Accepted,response.StatusCode);
    }

    [Fact]
    public async Task Delete_Delete_RestaurantDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Restaurant/delete/15";

        var response = await _client.DeleteAsync(request);

        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
        
    }

    [Fact]
    public async Task Get_GetById_ValidRequest_ReturnsOKStatusCode()
    {
        var request = "/api/v1/Restaurant/create";
        var restaurant = new CreateRestaurantRequest() { Name = "new name", Location = "new location", Rating = 4 };
        var content = new StringContent(JsonConvert.SerializeObject(restaurant), Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Restaurant>(responseString)!;

        request = "/api/v1/Restaurant/id/" + result.Id;
        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
        
    }
    
    [Fact]
    public async Task Get_GetById_RestaurantDoesExists_ReturnsNotFoundStatusCode()
    {
        
        var request = "/api/v1/Restaurant/id/1";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
    }
    
    [Fact]
    public async Task Get_GetByName_ValidRequest_ReturnsOKStatusCode()
    {

        var request = "/api/v1/Restaurant/create";
        var restaurant = new CreateRestaurantRequest() { Name = "new name", Location = "new location", Rating = 4 };
        var content = new StringContent(JsonConvert.SerializeObject(restaurant), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Restaurant>(responseString)!;

        request = "/api/v1/Restaurant/name/" + result.Name;

        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
        
    }

    [Fact]
    public async Task Get_GetByName_RestaurantDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Restaurant/name/test";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

    }
    
    [Fact]
    public async Task Get_GetByLocation_ValidRequest_ReturnsOKStatusCode()
    {

        var request = "/api/v1/Restaurant/create";
        var restaurant = new CreateRestaurantRequest() { Name = "new name", Location = "new location", Rating = 4 };
        var content = new StringContent(JsonConvert.SerializeObject(restaurant), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Restaurant>(responseString)!;

        request = "/api/v1/Restaurant/location/" + result.Location;
        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
        
    }

    [Fact]
    public async Task Get_GetByLocation_RestaurantDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Restaurant/location/test";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

    }
    
    [Fact]
    public async Task Get_GetByRating_ValidRequest_ReturnsOKStatusCode()
    {

        var request = "/api/v1/Restaurant/create";
        var restaurant = new CreateRestaurantRequest() { Name = "new name", Location = "new location", Rating = 4 };
        var content = new StringContent(JsonConvert.SerializeObject(restaurant), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Restaurant>(responseString)!;

        request = "/api/v1/Restaurant/rating/" + result.Rating;
        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
        
    }

    [Fact]
    public async Task Get_GetByRating_RestaurantDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Restaurant/rating/3";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

    }
    
    
    
}