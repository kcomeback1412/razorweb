using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CS58_Razor09EF.Models;

namespace CS58_Razor09EF.Pages_Blog
{
    public class IndexModel : PageModel
    {
        private readonly CS58_Razor09EF.Models.MyBlogContext _context;

        public IndexModel(CS58_Razor09EF.Models.MyBlogContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;

        public const int ITEM_PER_PAGE = 10;
        [BindProperty(SupportsGet = true, Name = "p")]
        public int currentPage { get; set; }
        public int countPages { get; set; }

        public async Task OnGetAsync(string SearchString)
        {

            int totalArticles = await _context.Articles.CountAsync();
            countPages = (int)Math.Ceiling((double)totalArticles/ITEM_PER_PAGE);
            if(currentPage < 1)
            {
                currentPage = 1;
            }

            if (currentPage > countPages)
            {
                currentPage = countPages;
            }


            //Article = await _context.Articles.ToListAsync();
            var qr = (from a in _context.Articles
                     orderby a.PublishDate descending
                     select a)
                     .Skip((currentPage - 1) * 10)
                     .Take(ITEM_PER_PAGE);

            if (!string.IsNullOrEmpty(SearchString)) {
               Article = qr.Where(a => a.Title.Contains(SearchString)).ToList();

            } else
            {

            Article = await qr.ToListAsync();
            }
        }
    }
}
