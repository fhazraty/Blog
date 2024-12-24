using System.ComponentModel.DataAnnotations;

namespace DAL.EF.Model
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		[Required, MaxLength(100)]
		public string Name { get; set; }
		public int? ParentCategoryId { get; set; }
		public Category ParentCategory { get; set; }
		public ICollection<Category> SubCategories { get; set; } = new List<Category>();
		public ICollection<Post> Posts { get; set; } = new List<Post>();
	}
}
