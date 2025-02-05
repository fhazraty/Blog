using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel
{
	public class UpdateCategoryViewModel
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public int? ParentCategoryId { get; set; }
		public bool HasChildren { get; set; }
	}
}
