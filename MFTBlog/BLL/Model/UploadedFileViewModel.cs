using System.ComponentModel.DataAnnotations;

namespace BLL.Model
{
	public class UploadedFileViewModel
	{
		[Required, MaxLength(200)]
		public string Title { get; set; }

		[Required]
		public byte[] Data { get; set; }

		public DateTime UploadedAt { get; set; }
	}
}
