using System.Diagnostics;
using DAL.CMS.EF.Repository;
using DAL.CMS.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModel;
using Microsoft.EntityFrameworkCore;
using BLL.CMS.Model;
using BLL.CMS.Management;

namespace WebApp.Controllers
{
	public class HomeController : BaseController
	{
        private readonly ILogger<HomeController> _logger;
		public IPostManagement PostManagement { get; set; }
		public ITagManagement TagManagement { get; set; }
		public ICategoryManagement CategoryManagement { get; set; }
		public ISpecialConfigurationManagement SpecialConfigurationManagement { get; set; }
		public HomeController(ILogger<HomeController> logger,
			IPostManagement postManagement,
			ITagManagement tagManagement,
			ICategoryManagement categoryManagement,
			ISpecialConfigurationManagement specialConfigurationManagement)
		{
            this._logger = logger;
			this.PostManagement = postManagement;
			this.TagManagement = tagManagement;
			this.CategoryManagement = categoryManagement;
			this.SpecialConfigurationManagement = specialConfigurationManagement;
        }
        public async Task<IActionResult> IndexAsync()
        {
			ViewBag.Pic1 = (await SpecialConfigurationManagement.GetConfigById(1)).Entity.Value;
			ViewBag.Pic2 = (await SpecialConfigurationManagement.GetConfigById(2)).Entity.Value;
			ViewBag.Pic3 = (await SpecialConfigurationManagement.GetConfigById(3)).Entity.Value;

			ViewBag.CMSTitle = (await SpecialConfigurationManagement.GetConfigById(4)).Entity.Value;
			ViewBag.CMSMainHeader = (await SpecialConfigurationManagement.GetConfigById(5)).Entity.Value;
			ViewBag.CMSMainHeaderText = (await SpecialConfigurationManagement.GetConfigById(6)).Entity.Value;

			ViewBag.Pic1Title = (await SpecialConfigurationManagement.GetConfigById(7)).Entity.Value;
			ViewBag.Pic2Title = (await SpecialConfigurationManagement.GetConfigById(8)).Entity.Value;
			ViewBag.Pic3Title = (await SpecialConfigurationManagement.GetConfigById(9)).Entity.Value;

			ViewBag.Pic1TitleDesc = (await SpecialConfigurationManagement.GetConfigById(10)).Entity.Value;
			ViewBag.Pic2TitleDesc = (await SpecialConfigurationManagement.GetConfigById(11)).Entity.Value;
			ViewBag.Pic3TitleDesc = (await SpecialConfigurationManagement.GetConfigById(12)).Entity.Value;

			ViewBag.Pic1Link = (await SpecialConfigurationManagement.GetConfigById(13)).Entity.Value;
			ViewBag.Pic2Link = (await SpecialConfigurationManagement.GetConfigById(14)).Entity.Value;
			ViewBag.Pic3Link = (await SpecialConfigurationManagement.GetConfigById(15)).Entity.Value;

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
					Id = post.Id,
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
		[AllowAnonymous]
		public async Task<IActionResult> PostDetails(int id)
		{
			var postResult = await PostManagement.GetPostById(id);
			if (!postResult.IsSuccessful)
			{
				return NotFound();
			}

			var post = ((ResultEntityViewModel<PostViewModel>)postResult).Entity;
			post.PersianInsertationDateTime = await ConvertToPersianDateTime(post.CreatedAt.Value);

			var model = new PostDetailsViewModel
			{
				Id = post.Id,
				Title = post.Title,
				HtmlContent = post.HtmlContent,
				AuthorName = post.AuthorName,
				CategoryName = post.CategoryName,
				PersianInsertationDateTime = post.PersianInsertationDateTime,
				Tags = post.TagTextList
			};

			return View(model);
		}
	}
}
