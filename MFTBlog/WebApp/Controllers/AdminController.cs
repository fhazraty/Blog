﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
	public class AdminController : BaseController
	{
		[Authorize(Roles = "Admin")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
