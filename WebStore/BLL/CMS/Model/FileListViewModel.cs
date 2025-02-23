﻿using System.ComponentModel.DataAnnotations;

namespace BLL.CMS.Model
{
	public class FileListViewModel
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		[Required, MaxLength(200)]
		public string FileName { get; set; }
		[Required, MaxLength(200)]
		public string ContentType { get; set; }
		[Required]
		public byte[] Data { get; set; }
		public DateTime UploadedAt { get; set; }
		public string PersianUploadDate { get; set; }
		public int RowIndex { get; set; }
	}
}
