namespace WebApp.ViewModel
{
	public class PostDetailsViewModel
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string HtmlContent { get; set; }
		public string AuthorName { get; set; }
		public string CategoryName { get; set; }
		public string PersianInsertationDateTime { get; set; }
		public List<string> Tags { get; set; }
	}
}
