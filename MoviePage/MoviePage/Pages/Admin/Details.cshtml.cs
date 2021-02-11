using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoviePage.Data;
using MoviePage.Models;

namespace MoviePage.Pages.Admin
{
    public class DetailsModel : PageModel
    {
        private readonly MoviePage.Data.MoviePageContext _context;

        public DetailsModel(MoviePage.Data.MoviePageContext context)
        {
            _context = context;
        }

        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie
                .Include(m => m.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
