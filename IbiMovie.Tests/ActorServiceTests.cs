using IbiMovie.Application.Interfaces;
using IbiMovie.Core;
using IbiMovie.Infra.Data;
using IbiMovie.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IbiMovie.Tests
{
    public class ActorServiceTests
    {
        // to isolate the unit tests, create InMemory DB for each test
        public (IActorService actorService, AppDbContext dbContext) CreateActorServiceWithInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var dbContext = new AppDbContext(options);
            var actorService = new ActorService(dbContext);

            return (actorService, dbContext);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllActors()
        {
            var (actorService, dbContext) = CreateActorServiceWithInMemoryDb();
            IActorService _actorService = actorService;
            AppDbContext _dbContext = dbContext;

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
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnFilteredActors()
        {
            var (actorService, dbContext) = CreateActorServiceWithInMemoryDb();
            IActorService _actorService = actorService;
            AppDbContext _dbContext = dbContext;

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
        }
    }

}