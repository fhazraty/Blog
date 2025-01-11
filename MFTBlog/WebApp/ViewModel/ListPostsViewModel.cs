namespace WebApp.ViewModel
{
	public class ListPostsViewModel
	{
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }

		public List<PostItemViewModel> Posts { get; set; }
	}
}
