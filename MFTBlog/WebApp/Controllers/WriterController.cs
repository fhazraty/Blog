using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
	public class WriterController : Controller
	{
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
		public IActionResult AddNewPost([FromBody] AddNewPostViewModel addNewPostViewModel)
		{
			return View();
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
