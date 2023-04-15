using IbiMovie.Core;
using Microsoft.EntityFrameworkCore;

namespace IbiMovie.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>()
            .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId);

            // dump the tables
            //modelBuilder.Entity<Movie>().HasData(new List<Movie>());
            //modelBuilder.Entity<Actor>().HasData(new List<Actor>());
            //modelBuilder.Entity<MovieActor>().HasData(new List<MovieActor>());

            //Seed Actors
            modelBuilder.Entity<Actor>().HasData(
                new Actor { Id = 1, Name = "Morgan Freeman" },
                new Actor { Id = 2, Name = "Tom Hanks" },
                new Actor { Id = 3, Name = "Brad Pitt" },
                new Actor { Id = 4, Name = "Keanu Reeves" },
                new Actor { Id = 5, Name = "Leonardo DiCaprio" }
            );

            // Seed Movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "The Shawshank Redemption", ReleasedYear = 1994 },
                new Movie { Id = 2, Title = "The Dark Knight", ReleasedYear = 2008 },
                new Movie { Id = 3, Title = "Pulp Fiction", ReleasedYear = 1994 },
                new Movie { Id = 4, Title = "Forrest Gump", ReleasedYear = 1994 },
                new Movie { Id = 5, Title = "Fight Club", ReleasedYear = 1999 },
                new Movie { Id = 6, Title = "Inception", ReleasedYear = 2010 },
                new Movie { Id = 7, Title = "The Matrix", ReleasedYear = 1999 },
                new Movie { Id = 8, Title = "The Lord of the Rings: The Fellowship of the Ring", ReleasedYear = 2001 },
                new Movie { Id = 9, Title = "The Godfather", ReleasedYear = 1972 },
                new Movie { Id = 10, Title = "The Silence of the Lambs", ReleasedYear = 1991 },
                new Movie { Id = 11, Title = "Goodfellas", ReleasedYear = 1990 },
                new Movie { Id = 12, Title = "The Sixth Sense", ReleasedYear = 1999 },
                new Movie { Id = 13, Title = "Saving Private Ryan", ReleasedYear = 1998 },
                new Movie { Id = 14, Title = "Gladiator", ReleasedYear = 2000 },
                new Movie { Id = 15, Title = "The Departed", ReleasedYear = 2006 }
            );

            // Seed MovieActors
            modelBuilder.Entity<MovieActor>().HasData(
                new MovieActor { MovieId = 1, ActorId = 1 }, // The Shawshank Redemption - Morgan Freeman
                new MovieActor { MovieId = 7, ActorId = 1 }, // The Matrix - Morgan Freeman
                new MovieActor { MovieId = 15, ActorId = 1 }, // The Departed - Morgan Freeman

                new MovieActor { MovieId = 4, ActorId = 2 }, // Forrest Gump - Tom Hanks
                new MovieActor { MovieId = 13, ActorId = 2 }, // Saving Private Ryan - Tom Hanks
                new MovieActor { MovieId = 6, ActorId = 2 }, // Inception - Tom Hanks

                new MovieActor { MovieId = 3, ActorId = 3 }, // Pulp Fiction - Brad Pitt
                new MovieActor { MovieId = 5, ActorId = 3 }, // Fight Club - Brad Pitt
                new MovieActor { MovieId = 12, ActorId = 3 }, // The Sixth Sense - Brad Pitt

                new MovieActor { MovieId = 7, ActorId = 4 }, // The Matrix - Keanu Reeves
                new MovieActor { MovieId = 8, ActorId = 4 }, // The Lord of the Rings: The Fellowship of the Ring - Keanu Reeves
                new MovieActor { MovieId = 10, ActorId = 4 }, // The Silence of the Lambs - Keanu Reeves

                new MovieActor { MovieId = 6, ActorId = 5 }, // Inception - Leonardo DiCaprio
                new MovieActor { MovieId = 15, ActorId = 5 }, // The Departed - Leonardo DiCaprio
                new MovieActor { MovieId = 2, ActorId = 5 } // The Dark Knight - Leonardo DiCaprio
            );
        }
    }


}
