namespace WebApp.ViewModel
{
	public class TagUpdateViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int? parentCategoryId { get; set; }
		public bool? hasChildren { get; set; }
	}
}
