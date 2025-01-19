using System.ComponentModel.DataAnnotations;

namespace DAL.EF.Model
{
	/// <summary>
	/// Represents a tag entity in the system.
	/// نمایانگر موجودیت برچسب در سیستم.
	/// </summary>
	public class Tag
	{
		/// <summary>
		/// Gets or sets the unique identifier for the tag.
		/// کلید اصلی برای برچسب.
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the tag.
		/// نام برچسب.
		/// </summary>
		[Required, MaxLength(50)]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the collection of posts associated with this tag.
		/// مجموعه پست‌های مرتبط با این برچسب.
		/// </summary>
		public ICollection<Post> Posts { get; set; } = new List<Post>();
	}
}
