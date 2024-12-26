using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "نام را وارد کنید")]
		[MaxLength(50, ErrorMessage = "طول نام نباید بیشتر از 50 کاراکتر باشد")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
		[MaxLength(50, ErrorMessage = "طول نام خانوادگی نباید بیشتر از 50 کاراکتر باشد")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "کد ملی را وارد کنید")]
		[MaxLength(10, ErrorMessage = "کد ملی باید 10 کاراکتر باشد")]
		public string NationalCode { get; set; }

		[Required(ErrorMessage = "نام کاربری را وارد کنید")]
		[MaxLength(50, ErrorMessage = "طول نام کاربری نباید بیشتر از 50 کاراکتر باشد")]
		public string Username { get; set; }

		[Required(ErrorMessage = "رمز عبور را وارد کنید")]
		[StringLength(20, MinimumLength = 8, ErrorMessage = "رمز عبور باید بین 8 تا 20 کاراکتر باشد")]
		public string Password { get; set; }

		[Required(ErrorMessage = "تاریخ تولد را وارد کنید")]
		public DateTime BirthDate { get; set; }
	}
}
