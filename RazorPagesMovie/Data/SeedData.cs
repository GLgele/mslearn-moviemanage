using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;

namespace RazorPagesMovie.Models;

public static class SeedData
{
    private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new RazorPagesMovieContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<RazorPagesMovieContext>>()))
        {
            if (context == null || context.Movie == null)
            {
                logger.Error("Null RazorPagesMovieContext");
                throw new ArgumentNullException("Null RazorPagesMovieContext");
            }

            // Look for any movies.
            if (context.Movie.Any())
            {
                logger.Info("Database already have data.");
                return;   // DB has been seeded
            }

            context.Movie.AddRange(
                new Movie
                {
                    Title = "Place Holder",
                    ReleaseDate = DateTime.Now,
                    Genre = "Placeholder",
                    Price = 0.0M
                }
            );
            context.SaveChanges();
            logger.Warn("Empty Database! Database has been seeded.");
        }
    }
    public static void AddExample(IServiceProvider serviceProvider)
    {
        using (var context = new RazorPagesMovieContext(
        serviceProvider.GetRequiredService<
            DbContextOptions<RazorPagesMovieContext>>()))
        {
            if (context == null || context.Movie == null)
            {
                logger.Error("Null RazorPagesMovieContext");
                throw new ArgumentNullException("Null RazorPagesMovieContext");
            }
        context.Movie.AddRange(
        new Movie
        {
            Title = "When Harry Met Sally",
            ReleaseDate = DateTime.Parse("1989-2-12"),
            Genre = "Romantic Comedy",
            Price = 7.99M
        },

        new Movie
        {
            Title = "Ghostbusters ",
            ReleaseDate = DateTime.Parse("1984-3-13"),
            Genre = "Comedy",
            Price = 8.99M
        },

        new Movie
        {
            Title = "Ghostbusters 2",
            ReleaseDate = DateTime.Parse("1986-2-23"),
            Genre = "Comedy",
            Price = 9.99M
        },

        new Movie
        {
            Title = "Rio Bravo",
            ReleaseDate = DateTime.Parse("1959-4-15"),
            Genre = "Western",
            Price = 3.99M
        });
        context.SaveChanges();
        logger.Info("Example data inserted.");
        }
    }
}