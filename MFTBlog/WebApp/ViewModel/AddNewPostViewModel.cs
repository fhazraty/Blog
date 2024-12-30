using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModel
{
	public class AddNewPostViewModel
	{
		[Required(ErrorMessage = "عنوان پست اجباری است")]
		public string Title { get; set; }
		[Required(ErrorMessage = "محتوای HTML پست اجباری است")]
		public string HtmlContent { get; set; }
		public int CategoryId { get; set; }
		public List<int> TagIdList { get; set; }
	}
}
