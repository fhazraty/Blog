using System.ComponentModel.DataAnnotations;

namespace DAL.EF.Model
{
	/// <summary>
	/// Represents a user entity in the system.
	/// نمایانگر موجودیت کاربر در سیستم.
	/// </summary>
	public class User
	{
		/// <summary>
		/// Gets or sets the unique identifier for the user.
		/// کلید اصلی منحصر به فرد برای کاربر.
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the first name of the user.
		/// نام کوچک کاربر.
		/// </summary>
		[Required, MaxLength(50)]
		public string FirstName { get; set; }

		/// <summary>
		/// Gets or sets the last name of the user.
		/// نام خانوادگی کاربر.
		/// </summary>
		[Required, MaxLength(50)]
		public string LastName { get; set; }

		/// <summary>
		/// Gets or sets the national code of the user.
		/// کد ملی کاربر.
		/// </summary>
		[Required, MaxLength(10)]
		public string NationalCode { get; set; }

		/// <summary>
		/// Gets or sets the username for the user.
		/// نام کاربری کاربر.
		/// </summary>
		[Required, MaxLength(50)]
		public string Username { get; set; }

		/// <summary>
		/// Gets or sets the hashed password for the user.
		/// رمز عبور هش‌شده کاربر.
		/// </summary>
		[Required]
		public string PasswordHash { get; set; }

		/// <summary>
		/// Gets or sets the salt used for password hashing.
		/// نمک استفاده‌شده برای هش کردن رمز عبور.
		/// </summary>
		[Required]
		public string Salt { get; set; }

		/// <summary>
		/// Gets or sets the birth date of the user.
		/// تاریخ تولد کاربر.
		/// </summary>
		public DateTime BirthDate { get; set; }

		/// <summary>
		/// Gets or sets the date when the password was created.
		/// تاریخ ایجاد رمز عبور.
		/// </summary>
		public DateTime PasswordCreatedAt { get; set; }

		/// <summary>
		/// Gets or sets the date when the password was last updated.
		/// تاریخ آخرین به‌روزرسانی رمز عبور.
		/// </summary>
		public DateTime PasswordUpdatedAt { get; set; }

		/// <summary>
		/// Gets or sets the roles associated with the user.
		/// نقش‌های مرتبط با کاربر.
		/// </summary>
		public ICollection<Role> Roles { get; set; } = new List<Role>();

		/// <summary>
		/// Gets or sets the posts authored by the user.
		/// پست‌هایی که توسط کاربر نوشته شده‌اند.
		/// </summary>
		public ICollection<Post> Posts { get; set; } = new List<Post>();
	}
}
