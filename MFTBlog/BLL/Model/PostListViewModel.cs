namespace BLL.Model
{
	public class PostListViewModel
	{
		public int Id { get; set; }
		public int RowIndex { get; set; }
		public string Title { get; set; }
		public DateTime InsertationDateTime { get; set; }
		public string PersianInsertationDateTime { get; set; }
		public string AuthorName { get; set; }
		public string CategoryName { get; set; }
	}
}
