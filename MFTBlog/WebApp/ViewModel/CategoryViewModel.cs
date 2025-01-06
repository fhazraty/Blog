using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel
{
	public class CategoryViewModel
	{
		[Required]
		public string Name { get; set; }
		public int? ParentCategoryId { get; set; }
	}
}
