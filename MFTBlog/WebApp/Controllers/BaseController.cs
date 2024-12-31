using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
	public class BaseController : Controller
	{
		public int UserId => int.Parse(User.Claims.FirstOrDefault(c => c.Type == "usrId").Value);

		protected async Task<string> ConvertToPersianDateTime(DateTime dateTime)
		{
			var persianCalendar = new System.Globalization.PersianCalendar();
			var persianDate = $"{persianCalendar.GetYear(dateTime)}/{persianCalendar.GetMonth(dateTime):00}/{persianCalendar.GetDayOfMonth(dateTime):00} {persianCalendar.GetHour(dateTime):00}:{persianCalendar.GetMinute(dateTime):00}:{persianCalendar.GetSecond(dateTime):00}";
			return await Task.FromResult(persianDate);
		}
	}
}
