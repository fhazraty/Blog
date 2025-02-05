using BLL.CMS.Model;
using DAL.CMS.EF.Repository;
using DAL.CMS.EF.Model;

namespace BLL.CMS.Management
{
	/// <summary>
	/// Manages operations related to uploaded files.
	/// عملیات مدیریتی مرتبط با فایل‌های آپلود شده را مدیریت می‌کند.
	/// </summary>
	public class UploadedFileManagement : IUploadedFileManagement
	{
		/// <summary>
		/// The repository for managing uploaded files.
		/// مخزن مدیریت فایل‌های آپلود شده.
		/// </summary>
		public IUploadedFileRepository UploadedFileRepository { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="UploadedFileManagement"/> class.
		/// یک نمونه جدید از کلاس UploadedFileManagement را مقداردهی می‌کند.
		/// </summary>
		public UploadedFileManagement(IUploadedFileRepository uploadedFileRepository)
		{
			UploadedFileRepository = uploadedFileRepository;
		}

		/// <summary>
		/// Adds a new uploaded file to the system.
		/// یک فایل آپلود شده جدید به سیستم اضافه می‌کند.
		/// </summary>
		/// <param name="uploadedFileViewModel">
		/// The details of the uploaded file. / جزئیات فایل آپلود شده.
		/// </param>
		/// <returns>
		/// A result indicating success or failure. / نتیجه‌ای که موفقیت یا شکست را نشان می‌دهد.
		/// </returns>
		public async Task<ResultViewModel> AddFileAsync(UploadedFileViewModel uploadedFileViewModel)
		{
			try
			{
				// Check if the uploaded file view model is null.
				// بررسی اینکه آیا مدل داده فایل آپلود شده null است یا خیر.
				if (uploadedFileViewModel == null)
				{
					throw new ArgumentNullException(nameof(uploadedFileViewModel));
				}

				// Create a new file entity from the view model.
				// ایجاد یک موجودیت فایل جدید از مدل داده ViewModel.
				var uploadedFile = new UploadedFile
				{
					Title = uploadedFileViewModel.Title,
					FileName = uploadedFileViewModel.FileName,
					ContentType = uploadedFileViewModel.ContentType,
					Data = uploadedFileViewModel.Data,
					UploadedAt = DateTime.Now
				};

				// Add the file to the repository.
				// فایل را به مخزن اضافه می‌کند.
				await UploadedFileRepository.AddAsync(uploadedFile);

				// Return a success result.
				// نتیجه موفقیت را باز می‌گرداند.
				return new ResultEntityViewModel<int>()
				{
					Entity = uploadedFile.Id,
					IsSuccessful = true,
					Message = "ذخیره با موفقیت انجام شد."
				};
			}
			catch (Exception ex)
			{
				// Return a failure result in case of an error.
				// در صورت بروز خطا، نتیجه شکست را باز می‌گرداند.
				return new ResultEntityViewModel<int>()
				{
					IsSuccessful = false,
					Exception = ex,
					Message = "خطا در ذخیره اطلاعات"
				};
			}
		}

		/// <summary>
		/// Retrieves a paginated list of uploaded files.
		/// یک لیست صفحه‌بندی‌شده از فایل‌های آپلود شده بازیابی می‌کند.
		/// </summary>
		/// <param name="page">
		/// The page number to retrieve. / شماره صفحه‌ای که باید بازیابی شود.
		/// </param>
		/// <param name="perPage">
		/// The number of files per page. / تعداد فایل‌ها در هر صفحه.
		/// </param>
		/// <returns>
		/// A tuple containing the list of files and their total count. / یک تاپل که شامل لیست فایل‌ها و تعداد کل آن‌ها است.
		/// </returns>
		public async Task<(List<FileListViewModel>, int)> ListFiles(int page, int perPage)
		{
			// Get the total number of files.
			// تعداد کل فایل‌ها را بدست می‌آورد.
			int fileCount = await UploadedFileRepository.GetFilesCount();
			var files = await UploadedFileRepository.GetFiles(page, perPage);

			// Map the files to ViewModel.
			// فایل‌ها را به ViewModel نگاشت می‌کند.
			return (files.Select((f, index) => new FileListViewModel
			{
				Id = f.Id,
				Title = f.Title,
				FileName = f.FileName,
				ContentType = f.ContentType,
				UploadedAt = f.UploadedAt,
				RowIndex = (page - 1) * perPage + index + 1
			}).ToList(), fileCount);
		}

		/// <summary>
		/// Retrieves a file by its ID.
		/// یک فایل را بر اساس شناسه آن بازیابی می‌کند.
		/// </summary>
		/// <param name="id">
		/// The ID of the file to retrieve. / شناسه فایل برای بازیابی.
		/// </param>
		/// <returns>
		/// A FileListViewModel containing the file details or null if not found. / یک FileListViewModel شامل جزئیات فایل یا null در صورت عدم یافتن.
		/// </returns>
		public async Task<FileListViewModel?> GetFileByIdAsync(int id)
		{
			var file = await UploadedFileRepository.GetByIdAsync(id);

			if (file == null) return null;

			// Map the file to ViewModel.
			// فایل را به ViewModel نگاشت می‌کند.
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

		/// <summary>
		/// Deletes a file by its ID.
		/// یک فایل را بر اساس شناسه آن حذف می‌کند.
		/// </summary>
		/// <param name="id">
		/// The ID of the file to delete. / شناسه فایل برای حذف.
		/// </param>
		/// <returns>
		/// A result indicating success or failure. / نتیجه‌ای که موفقیت یا شکست را نشان می‌دهد.
		/// </returns>
		public async Task<ResultViewModel> DeleteFileAsync(int id)
		{
			try
			{
				// Retrieve the file by ID.
				// فایل را بر اساس شناسه بازیابی می‌کند.
				var file = await UploadedFileRepository.GetByIdAsync(id);
				if (file == null)
				{
					// Return failure result if file is not found.
					// در صورت عدم یافتن فایل، نتیجه شکست باز می‌گرداند.
					return new ResultEntityViewModel<int>()
					{
						Entity = id,
						Exception = new KeyNotFoundException(),
						IsSuccessful = false,
						Message = "فایل یافت نشد."
					};
				}

				// Delete the file from the repository.
				// فایل را از مخزن حذف می‌کند.
				await UploadedFileRepository.DeleteAsync(id);

				// Return success result.
				// نتیجه موفقیت را باز می‌گرداند.
				return new ResultEntityViewModel<int>()
				{
					Entity = id,
					IsSuccessful = true,
					Message = "فایل با موفقیت حذف شد."
				};
			}
			catch (Exception ex)
			{
				// Return failure result in case of an error.
				// در صورت بروز خطا، نتیجه شکست را باز می‌گرداند.
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
