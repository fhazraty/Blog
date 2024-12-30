using BLL.Management;
using BLL.Model;
using DAL.EF;
using DAL.EF.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
	public class WriterController : BaseController
	{
		public IPostManagement PostManagement { get; set; }
		public ITagManagement TagManagement { get; set; }
		public ICategoryManagement CategoryManagement { get; set; }
		public WriterController()
		{
			var context = new BlogContext();
			PostManagement =
				new PostManagement(
					new PostRepository(context), 
					new UserRepository(context), 
					new CategoryRepository(context), 
					new TagRepository(context));
			TagManagement = new TagManagement(new TagRepository(context));
			CategoryManagement = new CategoryManagement(new CategoryRepository(context));
		}
		[HttpGet]
		[Authorize(Roles = "Writer")]
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		[Authorize(Roles = "Writer")]
		public IActionResult ListPosts()
		{
			return View();
		}
		[HttpGet]
		[Authorize(Roles = "Writer")]
		public IActionResult AddNewPost()
		{
			return View();
		}
		[HttpPost]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> AddNewPost([FromBody] AddNewPostViewModel addNewPostViewModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var postViewModel = new PostViewModel
			{
				Title = addNewPostViewModel.Title,
				HtmlContent = addNewPostViewModel.HtmlContent,
				CategoryId = addNewPostViewModel.CategoryId,
				TagIdList = addNewPostViewModel.TagIdList,
				AuthorId = UserId
			};

			var result = await PostManagement.AddPost(postViewModel);

			if (result.IsSuccessful)
			{
				return Json(new { successful = true });
			}

			var errorMessage = result is ResultEntityViewModel<Exception> exceptionResult
				? exceptionResult.Message
				: "An error occurred while adding the post.";

			return Json(new { successful = false, message = errorMessage });
		}

		[HttpGet]
		public async Task<IActionResult> GetTags()
		{
			var tags = await TagManagement.GetTags();
			return Json(tags);
		}

		[HttpPost]
		public IActionResult AddTag(TagViewModel tagViewModel)
		{
			TagManagement.AddTag(tagViewModel);

			return Ok();
		}


		[HttpGet]
		public async Task<IActionResult> GetCategories()
		{
			var categories = await CategoryManagement.ListAllCategoriesAsync();
			
			var categoryList = categories.Select(c => new
			{
				Id = c.Id,
				ParentCategoryId = c.ParentCategoryId,
				Name = c.Name,
				HasChildren = categories.Any(sub => sub.ParentCategoryId == c.Id)  // Check if has subcategories
			}).ToList();

			return Json(categoryList);
		}

		
		[HttpGet]
		[Authorize(Roles = "Writer")]
		public IActionResult ListCategories()
		{
			return View();
		}
		[HttpGet]
		[Authorize(Roles = "Writer")]
		public IActionResult ListTags()
		{
			return View();
		}
		[HttpGet]
		[Authorize(Roles = "Writer")]
		public IActionResult ListFiles()
		{
			return View();
		}
	}
}
