using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.InteropServices;

namespace BLL.Model
{
	public class PostViewModel
	{
		public int Id { get; set; }
		public string? AuthorName { get; set; }
		public int? AuthorId { get; set; }
		public string Title { get; set; }
		public DateTime? CreatedAt { get; set; }
		public string? PersianInsertationDateTime { get; set; }
		public string AbstractContent { get; set; }
		public string HtmlContent { get; set; }
		public List<int>? TagIdList { get; set; }
		public List<string>? TagTextList { get; set; }
		public int? CategoryId { get; set; }
		public string? CategoryName { get; set; }
	}
}
