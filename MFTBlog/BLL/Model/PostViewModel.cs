namespace BLL.Model
{
	public class PostViewModel
	{
		public int Id { get; set; }
		public int AuthorId { get; set; }
		public string Title { get; set; }
		public string HtmlContent { get; set; }
		public List<int> TagIdList { get; set; }
		public int? CategoryId { get; set; }
	}
}
