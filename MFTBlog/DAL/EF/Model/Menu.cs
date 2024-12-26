using System.ComponentModel.DataAnnotations;

namespace DAL.EF.Model
{
	public class Menu
	{
		[Key]
		public int Id { get; set; }
		[Required, MaxLength(100)]
		public string Title { get; set; }
		public string Url { get; set; }
		public int? ParentMenuId { get; set; }
		public Menu ParentMenu { get; set; }
		public ICollection<Menu> SubMenus { get; set; } = new List<Menu>();
	}
}
