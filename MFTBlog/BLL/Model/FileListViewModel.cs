using System.ComponentModel.DataAnnotations;

namespace BLL.Model
{
	public class FileListViewModel
	{
		[Key]
		public int Id { get; set; }

		[Required, MaxLength(200)]
		public string Title { get; set; }

		[Required]
		public byte[] Data { get; set; }
		public DateTime UploadedAt { get; set; }
		public string PersianUploadDate { get; set; }
		public int RowIndex { get; set; }
	}
}
