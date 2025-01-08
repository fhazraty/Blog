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
				Data = f.Data,
				UploadedAt = f.UploadedAt,
				PersianUploadDate = f.UploadedAt.ToString("yyyy/MM/dd HH:mm:ss", new System.Globalization.CultureInfo("fa-IR")),
				RowIndex = ((page - 1) * perPage) + index + 1
			}).ToList(), fileCount);
		}
	}
}
