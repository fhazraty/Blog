using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
