using BLL.Management;
using BLL.Model;
using DAL.EF;
using DAL.EF.Model;
using DAL.EF.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
	public class AccountController : BaseController
	{
		public IUserManagement UserManagement { get; set; }
		public AccountController(IUserManagement userManagement)
		{
			this.UserManagement = userManagement;
		}
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> TryToLogin(LoginViewModel loginViewModel)
		{
			var usr = await UserManagement.FindUser(new UserViewModel()
			{
				Username = loginViewModel.Username,
				Password = loginViewModel.Password
			});
			if (!usr.IsSuccessful) { return RedirectToAction("ErrorInUsernameOrPassword", "Account"); }
			int usrId = ((ResultEntityViewModel<User>)usr).Entity.Id;
			var roles = ((ResultEntityViewModel<User>)usr).Entity.Roles.ToList();
			if (roles.Count == 0) { return RedirectToAction("ErrorInUsernameOrPassword", "Account"); }
			var role = roles[0].Name;
			if (role == "Writer" || role == "Admin")
			{
				List<Claim> claims = new List<Claim>
				{
					new Claim(ClaimTypes.Role, role),
					new Claim("Username", loginViewModel.Username),
					new Claim("usrId", usrId.ToString())
				};
				ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");
				ClaimsPrincipal principal = new ClaimsPrincipal(identity);
				await HttpContext.SignInAsync(
					scheme: "mft",
					principal: principal,
					properties: new AuthenticationProperties
					{
						ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
					});
				return RedirectToAction("Index", role);
			}
			return RedirectToAction("ErrorInUsernameOrPassword", "Account");
		}
		[HttpGet]
		public IActionResult ErrorInUsernameOrPassword()
		{
			return View();
		}
		[HttpGet]
		public IActionResult RegisterUserWriter()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var res = await UserManagement.AddUser(new UserViewModel()
				{
					Username = model.Username,
					Password = model.Password,
					FirstName = model.FirstName,
					LastName = model.LastName,
					NationalCode = model.NationalCode,
					BirthDate = await ConvertToGregorianDateTime(model.BirthDate),
					Roles = new List<Role>()
					{
						new Role()
						{
							Name = "Writer"
						}
					}
				});

				if (res.IsSuccessful)
				{
					return Json(new { success = true, message = "ثبت‌نام با موفقیت انجام شد" });
				}
				else
				{
					return Json(new { success = false, message = "خطا در ثبت‌نام، لطفا مجددا تلاش کنید" });
				}
			}

			var errors = ModelState
				.Where(x => x.Value.Errors.Any())
				.ToDictionary(k => k.Key, v => v.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault());

			return Json(new { success = false, errors });
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync("mft");
			return RedirectToAction("Index", "Account");
		}
	}
}
