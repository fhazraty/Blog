using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
	public class WriterController : Controller
	{
		[Authorize(Roles = "Writer")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
