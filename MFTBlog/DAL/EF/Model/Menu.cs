using System.ComponentModel.DataAnnotations;

namespace DAL.EF.Model
{
	/// <summary>
	/// Represents a menu entity in the system.
	/// نمایانگر موجودیت منو در سیستم.
	/// </summary>
	public class Menu
	{
		/// <summary>
		/// Gets or sets the unique identifier for the menu.
		/// کلید اصلی برای منو.
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the title of the menu.
		/// عنوان منو.
		/// </summary>
		[Required, MaxLength(100)]
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the URL of the menu.
		/// آدرس URL منو.
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Gets or sets the ID of the parent menu, if any.
		/// شناسه منوی والد (در صورت وجود).
		/// </summary>
		public int? ParentMenuId { get; set; }

		/// <summary>
		/// Gets or sets the parent menu of this menu.
		/// منوی والد این منو.
		/// </summary>
		public Menu ParentMenu { get; set; }

		/// <summary>
		/// Gets or sets the collection of submenus under this menu.
		/// مجموعه زیرمنوهای این منو.
		/// </summary>
		public ICollection<Menu> SubMenus { get; set; } = new List<Menu>();
	}
}