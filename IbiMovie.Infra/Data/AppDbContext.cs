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
                      
            // Seed Actors
            modelBuilder.Entity<Actor>().HasData(
                new Actor { Id = 1, Name = "Tom Hanks", Height = 183, Hair_Color = "Brown", Skin_Color = "Fair", Eye_Color = "Green", Birth_Year = "1956", Gender = "Male" },
                new Actor { Id = 2, Name = "Tom Cruise", Height = 170, Hair_Color = "Brown", Skin_Color = "Fair", Eye_Color = "Green", Birth_Year = "1962", Gender = "Male" },
                new Actor { Id = 3, Name = "Brad Pitt", Height = 180, Hair_Color = "Blond", Skin_Color = "Fair", Eye_Color = "Blue", Birth_Year = "1963", Gender = "Male" },
                new Actor { Id = 4, Name = "Morgan Freeman", Height = 188, Hair_Color = "Grey", Skin_Color = "Black", Eye_Color = "Brown", Birth_Year = "1937", Gender = "Male" },
                new Actor { Id = 5, Name = "Leonardo DiCaprio", Height = 183, Hair_Color = "Blond", Skin_Color = "Fair", Eye_Color = "Blue", Birth_Year = "1974", Gender = "Male" }
                );

            // Seed Movies
            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Forrest Gump", Director = "Robert Zemeckis", Producer = "Wendy Finerman, Steve Tisch, Steve Starkey", Released_Date = new DateTime(1994, 7, 6) },
                new Movie { Id = 2, Title = "Cast Away", Director = "Robert Zemeckis", Producer = "Tom Hanks, Steve Starkey, Robert Zemeckis", Released_Date = new DateTime(2000, 12, 22) },
                new Movie { Id = 3, Title = "Saving Private Ryan", Director = "Steven Spielberg", Producer = "Steven Spielberg, Ian Bryce, Mark Gordon, Gary Levinsohn", Released_Date = new DateTime(1998, 7, 24) },
                new Movie { Id = 4, Title = "Top Gun", Director = "Tony Scott", Producer = "Don Simpson, Jerry Bruckheimer", Released_Date = new DateTime(1986, 5, 16) },
                new Movie { Id = 5, Title = "Mission: Impossible", Director = "Brian De Palma", Producer = "Tom Cruise, Paula Wagner", Released_Date = new DateTime(1996, 5, 22) },
                new Movie { Id = 6, Title = "Jerry Maguire", Director = "Cameron Crowe", Producer = "James L. Brooks, Cameron Crowe, Richard Sakai", Released_Date = new DateTime(1996, 12, 13) },
                new Movie { Id = 7, Title = "Fight Club", Director = "David Fincher", Producer = "Art Linson, Cean Chaffin, Ross Grayson Bell", Released_Date = new DateTime(1999, 10, 15) },
                new Movie { Id = 8, Title = "Se7ven", Director = "David Fincher", Producer = "Arnold Kopelson, Phyllis Carlyle", Released_Date = new DateTime(1995, 9, 22) },
                new Movie { Id = 9, Title = "12 Years a Slave", Director = "Steve McQueen", Producer = "Brad Pitt, Dede Gardner, Jeremy Kleiner, Steve McQueen, Anthony Katagas", Released_Date = new DateTime(2013, 10, 18) },
                new Movie { Id = 10, Title = "The Shawshank Redemption", Director = "Frank Darabont", Producer = "Niki Marvin", Released_Date = new DateTime(1994, 9, 23) },
                new Movie { Id = 11, Title = "Million Dollar Baby", Director = "Clint Eastwood", Producer = "Clint Eastwood, Albert S. Ruddy, Tom Rosenberg", Released_Date = new DateTime(2004, 12, 15) },
                new Movie { Id = 12, Title = "Titanic", Director = "James Cameron", Producer = "James Cameron, Jon Landau", Released_Date = new DateTime(1997, 12, 19) },
                new Movie { Id = 13, Title = "Inception", Director = "Christopher Nolan", Producer = "Emma Thomas, Christopher Nolan", Released_Date = new DateTime(2010, 7, 16) },
                new Movie { Id = 14, Title = "The Revenant", Director = "Alejandro González Iñárritu", Producer = "Arnon Milchan, Steve Golin, Alejandro González Iñárritu, Mary Parent, Keith Redmon", Released_Date = new DateTime(2015, 12, 25) }
                );

            // Create associations
            modelBuilder.Entity<MovieActor>().HasData(
                new MovieActor { MovieId = 1, ActorId = 1 },
                new MovieActor { MovieId = 2, ActorId = 1 },
                new MovieActor { MovieId = 3, ActorId = 1 },
                new MovieActor { MovieId = 4, ActorId = 2 },
                new MovieActor { MovieId = 5, ActorId = 2 },
                new MovieActor { MovieId = 6, ActorId = 2 },
                new MovieActor { MovieId = 7, ActorId = 3 },
                new MovieActor { MovieId = 8, ActorId = 3 },
                new MovieActor { MovieId = 9, ActorId = 3 },
                new MovieActor { MovieId = 10, ActorId = 4 },
                new MovieActor { MovieId = 8, ActorId = 4 },
                new MovieActor { MovieId = 11, ActorId = 4 },
                new MovieActor { MovieId = 12, ActorId = 5 },
                new MovieActor { MovieId = 13, ActorId = 5 },
                new MovieActor { MovieId = 14, ActorId = 5 }
            );
        }
    }

}