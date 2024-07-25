using CS58_Razor09EF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CS58_Razor09EF.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly MyBlogContext myBlogContext;
		public IndexModel(ILogger<IndexModel> logger, MyBlogContext _myBlogContext)
		{
			_logger = logger;
			myBlogContext = _myBlogContext;
		}

		public void OnGet()
		{
			var posts = (from a in myBlogContext.Articles
					   orderby a.PublishDate descending
					   select a).ToList();
			ViewData["posts"] = posts;
		}
	}
}
