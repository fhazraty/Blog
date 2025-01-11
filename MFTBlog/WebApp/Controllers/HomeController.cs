using System.Diagnostics;
using BLL.Management;
using DAL.EF.Repository;
using DAL.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class HomeController : BaseController
	{
        private readonly ILogger<HomeController> _logger;
		public IPostManagement PostManagement { get; set; }
		public ITagManagement TagManagement { get; set; }
		public ICategoryManagement CategoryManagement { get; set; }
		public HomeController(ILogger<HomeController> logger)
		{
			var context = new BlogContext();
			
			PostManagement =
				new PostManagement(
					new PostRepository(context),
					new PostRepository(new BlogContext()),
					new UserRepository(context),
					new CategoryRepository(context),
					new TagRepository(context));
			
			TagManagement = new TagManagement(new TagRepository(context));
			
			CategoryManagement = new CategoryManagement(new CategoryRepository(context));
			
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		[Route("Posts")]
		[AllowAnonymous]
		public async Task<IActionResult> PostsAsync(int page = 1, int perPage = 10)
		{
			var posts = await PostManagement.ListPost(page, perPage);

			var tasks = posts.Item1.Select(async post =>
			{
				post.PersianInsertationDateTime = await ConvertToPersianDateTime(post.InsertationDateTime);
				return post;
			}).ToList();

			await Task.WhenAll(tasks);

			// محاسبه تعداد کل پست‌ها
			int totalPostsCount = posts.Item2;

			// محاسبه تعداد صفحات
			int totalPages = (int)Math.Ceiling(totalPostsCount / (double)perPage);

			// پرکردن ViewModel نهایی
			var model = new ListPostsViewModel
			{
				CurrentPage = page,
				TotalPages = totalPages,
				Posts = posts.Item1.Select(post => new PostItemViewModel
				{
					RowIndex = post.RowIndex,
					Title = post.Title,
					AbstractContent = post.AbstractContent,
					AuthorName = post.AuthorName,
					CategoryName = post.CategoryName,
					PersianInsertationDateTime = post.PersianInsertationDateTime
				}).ToList()
			};

			// ارسال مدل به ویو
			return View(model);
		}

		[AllowAnonymous]
		public async Task<IActionResult> ListPostsData(int page, int perpage)
		{
			var posts = await PostManagement.ListPost(page, perpage);

			var tasks = posts.Item1.Select(async post =>
			{
				post.PersianInsertationDateTime = await ConvertToPersianDateTime(post.InsertationDateTime);
				return post;
			}).ToList();

			await Task.WhenAll(tasks);

			return Json(new { successful = true, posts = posts.Item1, postscount = posts.Item2 });
		}
	}
}
