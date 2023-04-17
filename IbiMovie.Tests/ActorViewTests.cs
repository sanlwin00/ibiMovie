using IbiMovie.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IbiMovie.Tests
{
    public class ActorViewTests : IClassFixture<WebApplicationFactory<HomeController>>
    {
        private readonly WebApplicationFactory<HomeController> _factory;
        private readonly HttpClient _client;
        public ActorViewTests(WebApplicationFactory<HomeController> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_ActorPage_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();

            // ACT
            var response = await client.GetAsync("/Actor");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("Tom Hanks")]
        [InlineData("Tom Cruise")]
        [InlineData("Morgan Freeman")]
        [InlineData("Leonardo Dicaprio")]
        [InlineData("Brad Pitt")]
        public async Task Get_ActorPage_ShouldFilterActors(string actorName)
        {
            // Arrange
            var client = _factory.CreateClient();
            
            // Act
            var response = await client.GetAsync($"Actor/FilterByActorName?searchTerm={actorName}");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Contains($"<div class=\"card-header\">{actorName}</div>", content, StringComparison.OrdinalIgnoreCase);            

        }


    }
}