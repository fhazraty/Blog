using System.ComponentModel.DataAnnotations;

namespace DAL.CMS.EF.Model
{
	/// <summary>
	/// Represents an uploaded file entity in the system.
	/// نمایانگر موجودیت فایل آپلود شده در سیستم.
	/// </summary>
	public class UploadedFile
	{
		/// <summary>
		/// Gets or sets the unique identifier for the uploaded file.
		/// کلید اصلی برای فایل آپلود شده.
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the title of the uploaded file.
		/// عنوان فایل آپلود شده.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the name of the uploaded file.
		/// نام فایل آپلود شده.
		/// </summary>
		[Required, MaxLength(200)]
		public string FileName { get; set; }

		/// <summary>
		/// Gets or sets the content type of the uploaded file.
		/// نوع محتوا (Content Type) فایل آپلود شده.
		/// </summary>
		[Required, MaxLength(200)]
		public string ContentType { get; set; }

		/// <summary>
		/// Gets or sets the binary data of the uploaded file.
		/// داده‌های باینری فایل آپلود شده.
		/// </summary>
		[Required]
		public byte[] Data { get; set; }

		/// <summary>
		/// Gets or sets the date and time when the file was uploaded.
		/// تاریخ و زمان آپلود فایل.
		/// </summary>
		public DateTime UploadedAt { get; set; }
	}
}
