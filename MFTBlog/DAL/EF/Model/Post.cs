using System.ComponentModel.DataAnnotations;

namespace DAL.EF.Model
{
	public class Post
	{
		[Key]
		public int Id { get; set; }

		[Required, MaxLength(200)]
		public string Title { get; set; }

		[Required]
		public string HtmlContent { get; set; }

		public DateTime CreatedAt { get; set; }

		public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

		public virtual Category Category { get; set; }

		public int? CategoryId { get; set; }

		public virtual User Author { get; set; }

		public int AuthorId { get; set; }
	}
}
