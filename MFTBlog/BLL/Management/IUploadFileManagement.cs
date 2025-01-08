﻿using BLL.Model;

namespace BLL.Management
{
	public interface IUploadedFileManagement
	{
		Task<ResultViewModel> AddFileAsync(UploadedFileViewModel uploadedFileViewModel);
		Task<(List<FileListViewModel>, int)> ListFiles(int page, int perPage);
	}
}