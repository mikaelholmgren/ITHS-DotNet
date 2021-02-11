using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoviePage.Models;
using MoviePage.Data;
namespace MoviePage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MoviePageContext _context;

        public IndexModel(MoviePageContext ctx)
        {
            _context = ctx;
        }
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        [BindProperty(SupportsGet =true)]
        public int? CategoryId { get; set; }
        [BindProperty]
        public string SearchText { get; set; }
        public void OnGet()
        {
            var query = _context.Movie.Include(i => i.Category);
            if (CategoryId == null)
                Movies = query;
            else
                Movies = query.Where(c => c.Category.Id == CategoryId.Value);
            Categories = _context.Category;
        }
        public void OnPost()
        {
            var query = _context.Movie.Include(i => i.Category);

            if (!string.IsNullOrEmpty(SearchText))
            {
                // offline search..
                var list = query.ToList();
                Movies = list.Where(c => c.Title.Contains(SearchText));
            }
            else
                Movies = query;
            Categories = _context.Category;
        }
    }
}
