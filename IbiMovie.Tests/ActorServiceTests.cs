using IbiMovie.Application.Interfaces;
using IbiMovie.Core;
using IbiMovie.Infra.Data;
using IbiMovie.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

public class ActorServiceTests
{
    private IActorService _actorService;
    private AppDbContext _dbContext;

    public ActorServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "ActorMovieAppTest")
            .Options;

        _dbContext = new AppDbContext(options);
        _actorService = new ActorService(_dbContext);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllActors()
    {
        _dbContext.Actors.RemoveRange(await _actorService.GetAllAsync());
        // Arrange
        var actors = new List<Actor>
        {
            new Actor { Id = 1, Name = "Actor 1" },
            new Actor { Id = 2, Name = "Actor 2" }
        };

        _dbContext.Actors.AddRange(actors);
        _dbContext.SaveChanges();

        // Act
        var result = await _actorService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        _dbContext.Actors.RemoveRange(actors);
    }

    [Fact]
    public async Task GetByNameAsync_ShouldReturnFilteredActors()
    {
        // Arrange
        var actors = new List<Actor>
        {
            new Actor { Id = 3, Name = "Actor 1" },
            new Actor { Id = 4, Name = "Actor 2" },
            new Actor { Id = 5, Name = "Another One" }
        };

        _dbContext.Actors.AddRange(actors);
        _dbContext.SaveChanges();

        // Act
        var result = await _actorService.GetByNameAsync("Actor");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.DoesNotContain(result, a => a.Name == "Another One");

        _dbContext.Actors.RemoveRange(actors);
    }
}

