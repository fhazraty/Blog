using System.ComponentModel.DataAnnotations;

namespace DAL.EF.Model
{
	/// <summary>
	/// Represents a category entity in the system.
	/// نمایانگر موجودیت دسته‌بندی در سیستم.
	/// </summary>
	public class Category
	{
		/// <summary>
		/// Gets or sets the unique identifier for the category.
		/// کلید اصلی برای دسته‌بندی.
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the category.
		/// نام دسته‌بندی.
		/// </summary>
		[Required, MaxLength(100)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the ID of the parent category, if any.
		/// شناسه دسته‌بندی والد (در صورت وجود).
		/// </summary>
		public int? ParentCategoryId { get; set; }

		/// <summary>
		/// Gets or sets the parent category of this category.
		/// دسته‌بندی والد این دسته‌بندی.
		/// </summary>
		public Category ParentCategory { get; set; }

		/// <summary>
		/// Gets or sets the collection of subcategories under this category.
		/// مجموعه زیرمجموعه‌های این دسته‌بندی.
		/// </summary>
		public ICollection<Category> SubCategories { get; set; } = new List<Category>();

		/// <summary>
		/// Gets or sets the collection of posts associated with this category.
		/// مجموعه پست‌های مرتبط با این دسته‌بندی.
		/// </summary>
		public ICollection<Post> Posts { get; set; } = new List<Post>();
	}

}
