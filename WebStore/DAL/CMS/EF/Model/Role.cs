using System.ComponentModel.DataAnnotations;

namespace DAL.CMS.EF.Model
{
	/// <summary>
	/// Represents a role entity in the system.
	/// نمایانگر موجودیت نقش (Role) در سیستم.
	/// </summary>
	public class Role
	{
		/// <summary>
		/// Gets or sets the unique identifier for the role.
		/// کلید اصلی منحصر به فرد برای موجودیت نقش.
		/// </summary>
		/// <value>
		/// The unique identifier for the role.
		/// مقدار منحصر به فرد برای نقش.
		/// </value>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the role.
		/// نام نقش را تنظیم یا بازیابی می‌کند.
		/// </summary>
		/// <value>
		/// The name of the role.
		/// نام نقش.
		/// </value>
		[Required, MaxLength(50)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the collection of users associated with this role.
		/// مجموعه‌ای از کاربران مرتبط با این نقش را تنظیم یا بازیابی می‌کند.
		/// </summary>
		/// <value>
		/// The collection of users associated with this role.
		/// مجموعه کاربران مرتبط با این نقش.
		/// </value>
		public ICollection<UserRole> UserRoles { get; set; }
	}
}
