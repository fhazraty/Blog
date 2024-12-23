using System.ComponentModel.DataAnnotations;

namespace DAL.EF.Model
{
	public class UploadedFile
	{
		[Key]
		public int Id { get; set; }

		[Required, MaxLength(200)]
		public string Title { get; set; }

		[Required]
		public byte[] Data { get; set; }

		public DateTime UploadedAt { get; set; }
	}
}
