using System.ComponentModel.DataAnnotations;

namespace BLL.Model
{
	public class UploadedFileViewModel
	{
		public string Title { get; set; }
		[Required, MaxLength(200)]
		public string FileName { get; set; }
		[Required, MaxLength(200)]
		public string ContentType { get; set; }
		[Required]
		public byte[] Data { get; set; }
		public DateTime UploadedAt { get; set; }
	}
}
