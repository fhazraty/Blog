using System.ComponentModel.DataAnnotations;

namespace DAL.CMS.EF.Model
{
	/// <summary>
	/// Represents a post entity in the system.
	/// نمایانگر موجودیت پست در سیستم.
	/// </summary>
	public class Post
	{
		/// <summary>
		/// Gets or sets the unique identifier for the post.
		/// کلید اصلی برای پست.
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the title of the post.
		/// عنوان پست.
		/// </summary>
		[Required, MaxLength(200)]
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the abstract content of the post.
		/// محتوای خلاصه پست.
		/// </summary>
		[Required]
		public string AbstractContent { get; set; }

		/// <summary>
		/// Gets or sets the HTML content of the post.
		/// محتوای HTML پست.
		/// </summary>
		[Required]
		public string HtmlContent { get; set; }

		/// <summary>
		/// Gets or sets the creation date of the post.
		/// تاریخ ایجاد پست.
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Gets or sets the collection of tags associated with the post.
		/// مجموعه برچسب‌های مرتبط با پست.
		/// </summary>
		public ICollection<Tag> Tags { get; set; } = new List<Tag>();

		/// <summary>
		/// Gets or sets the category of the post.
		/// دسته‌بندی پست.
		/// </summary>
		public Category Category { get; set; }

		/// <summary>
		/// Gets or sets the category ID of the post.
		/// شناسه دسته‌بندی پست.
		/// </summary>
		public int? CategoryId { get; set; }

		/// <summary>
		/// Gets or sets the author of the post.
		/// نویسنده پست.
		/// </summary>
		public User Author { get; set; }

		/// <summary>
		/// Gets or sets the author ID of the post.
		/// شناسه نویسنده پست.
		/// </summary>
		public int AuthorId { get; set; }
	}
}
