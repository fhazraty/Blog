using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
	public class BaseController : Controller
	{
		public int UserId => int.Parse(User.Claims.FirstOrDefault(c => c.Type == "usrId").Value);

		protected async Task<string> ConvertToPersianDateTime(DateTime dateTime)
		{
			var persianCalendar = new System.Globalization.PersianCalendar();
			var persianDate = $"{persianCalendar.GetYear(dateTime)}/{persianCalendar.GetMonth(dateTime):00}/{persianCalendar.GetDayOfMonth(dateTime):00}";
			return await Task.FromResult(persianDate);
		}

        protected async Task<DateTime> ConvertToGregorianDateTime(string persianDateTime)
        {
            try
            {
                var dateParts = persianDateTime.Split('/');

                // استخراج سال، ماه، روز، ساعت، دقیقه و ثانیه
                int year = int.Parse(dateParts[0]);
                int month = int.Parse(dateParts[1]);
                int day = int.Parse(dateParts[2]);
                
                // تبدیل تاریخ شمسی به میلادی
                var persianCalendar = new System.Globalization.PersianCalendar();
                var gregorianDateTime = persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);

                return await Task.FromResult(gregorianDateTime);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid Persian date format.", ex);
            }
        }
    }
}
