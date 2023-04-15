using IbiMovie.Application.Interfaces;
using IbiMovie.Core;
using IbiMovie.Infra.Data;
using IbiMovie.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IbiMovie.Tests
{
    public class MovieServiceTests
    {
        private IMovieService _MovieService;
        private AppDbContext _dbContext;

        public MovieServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "ibiMovieAppTest")
                .Options;

            _dbContext = new AppDbContext(options);
            _MovieService = new MovieService(_dbContext);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllMovies()
        {
            _dbContext.Movies.RemoveRange(await _MovieService.GetAllAsync());
            // Arrange
            var Movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "Movie 1" },
            new Movie { Id = 2, Title = "Movie 2" }
        };

            _dbContext.Movies.AddRange(Movies);
            _dbContext.SaveChanges();

            // Act
            var result = await _MovieService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            _dbContext.Movies.RemoveRange(Movies);
        }

    }
}
