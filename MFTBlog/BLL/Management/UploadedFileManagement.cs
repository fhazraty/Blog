using BLL.Model;
using DAL.EF.Model;
using DAL.EF.Repository;

namespace BLL.Management
{
	public class UploadedFileManagement : IUploadedFileManagement
	{
		public IUploadedFileRepository UploadedFileRepository1 { get; set; }
		public IUploadedFileRepository UploadedFileRepository2 { get; set; }
		public UploadedFileManagement(
			IUploadedFileRepository uploadedFileRepository1, IUploadedFileRepository uploadedFileRepository2)
		{
			this.UploadedFileRepository1 = uploadedFileRepository1;
			this.UploadedFileRepository2 = uploadedFileRepository2;
		}
		public async Task<ResultViewModel> AddFileAsync(UploadedFileViewModel uploadedFileViewModel)
		{
			try
			{
				if (uploadedFileViewModel == null)
				{
					throw new ArgumentNullException(nameof(uploadedFileViewModel));
				}

				var uploadedFile = new UploadedFile
				{
					Title = uploadedFileViewModel.Title,
					FileName = uploadedFileViewModel.FileName,
					ContentType = uploadedFileViewModel.ContentType,
					Data = uploadedFileViewModel.Data,
					UploadedAt = DateTime.Now
				};

				await UploadedFileRepository1.AddAsync(uploadedFile);

				return new ResultEntityViewModel<int>()
				{
					Entity = uploadedFile.Id,
					IsSuccessful = true,
					Message = "ذخیره با موفقیت انجام شد."
				};
			}
			catch (Exception ex)
			{
				return new ResultEntityViewModel<int>()
				{
					IsSuccessful = false,
					Exception = ex,
					Message = "خطا در ذخیره اطلاعات"
				};
			}
		}
		public async Task<(List<FileListViewModel>, int)> ListFiles(int page, int perPage)
		{
			// دو وظیفه موازی: گرفتن تعداد کل فایل‌ها و فایل‌های صفحه موردنظر
			var getPageCountTask = this.UploadedFileRepository1.GetFilesCount();
			var getFilesTask = this.UploadedFileRepository2.GetFiles(page, perPage);

			// صبر برای اتمام وظایف
			await Task.WhenAll(getPageCountTask, getFilesTask);

			// دریافت نتایج
			int fileCount = await getPageCountTask;
			var files = await getFilesTask;

			// تبدیل نتایج به ViewModel
			return (files.Select((f, index) => new FileListViewModel
			{
				Id = f.Id,
				Title = f.Title,
				FileName = f.FileName,
				ContentType	= f.ContentType,
				UploadedAt = f.UploadedAt,
				RowIndex = ((page - 1) * perPage) + index + 1
			}).ToList(), fileCount);
		}
		public async Task<FileListViewModel?> GetFileByIdAsync(int id)
		{
			var file = await UploadedFileRepository1.GetByIdAsync(id);
			
			if (file == null) return null;

			return new FileListViewModel
			{
				Id = file.Id,
				Title = file.Title,
				FileName = file.FileName,
				ContentType = file.ContentType,
				Data = file.Data,
				UploadedAt = file.UploadedAt
			};
		}
		public async Task<ResultViewModel> DeleteFileAsync(int id)
		{
			try
			{
				var file = await UploadedFileRepository1.GetByIdAsync(id);
				if (file == null)
				{
					return new ResultEntityViewModel<int>()
					{
						Entity = id,
						Exception = new KeyNotFoundException(),
						IsSuccessful = false,
						Message = "فایل یافت نشد."
					};
				}

				await UploadedFileRepository1.DeleteAsync(id);

				return new ResultEntityViewModel<int>()
				{
					Entity = id,
					IsSuccessful = true,
					Message = "فایل با موفقیت حذف شد."
				};
			}
			catch (Exception ex)
			{
				return new ResultEntityViewModel<int>()
				{
					Entity = id,
					Exception = ex,
					IsSuccessful = false,
					Message = "خطا رخ داده است!"
				};
			}
		}
	}
}
