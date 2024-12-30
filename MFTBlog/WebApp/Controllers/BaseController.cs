using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
	public class BaseController : Controller
	{
		public int UserId => int.Parse(User.Claims.FirstOrDefault(c => c.Type == "usrId").Value);
	}
}
