using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviePage.Models;

namespace MoviePage.Data
{
    public class MoviePageContext : DbContext
    {
        public MoviePageContext (DbContextOptions<MoviePageContext> options)
            : base(options)
        {
        }

        public DbSet<MoviePage.Models.Movie> Movie { get; set; }

        public DbSet<MoviePage.Models.Category> Category { get; set; }
    }
}
