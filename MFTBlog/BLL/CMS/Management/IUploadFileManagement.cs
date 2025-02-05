using BLL.CMS.Model;

namespace BLL.CMS.Management
{
	/// <summary>
	/// Defines a contract for managing uploaded file operations in the application.
	/// قراردادی برای مدیریت عملیات مرتبط با فایل‌های آپلود شده در برنامه تعریف می‌کند.
	/// </summary>
	public interface IUploadedFileManagement
	{
		/// <summary>
		/// Adds a new file to the system.
		/// یک فایل جدید به سیستم اضافه می‌کند.
		/// </summary>
		/// <param name="uploadedFileViewModel">
		/// The details of the uploaded file. / جزئیات فایل آپلود شده.
		/// </param>
		/// <returns>
		/// A result indicating success or failure of the operation. / نتیجه‌ای که موفقیت یا شکست عملیات را نشان می‌دهد.
		/// </returns>
		Task<ResultViewModel> AddFileAsync(UploadedFileViewModel uploadedFileViewModel);

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
		/// A tuple containing the list of files and the total count of files. / یک تاپل که شامل لیست فایل‌ها و تعداد کل فایل‌ها است.
		/// </returns>
		Task<(List<FileListViewModel>, int)> ListFiles(int page, int perPage);

		/// <summary>
		/// Retrieves a file by its ID.
		/// یک فایل را بر اساس شناسه آن بازیابی می‌کند.
		/// </summary>
		/// <param name="id">
		/// The ID of the file to retrieve. / شناسه فایل برای بازیابی.
		/// </param>
		/// <returns>
		/// A FileListViewModel containing the file details, or null if not found. / یک FileListViewModel شامل جزئیات فایل یا null در صورت عدم یافتن.
		/// </returns>
		Task<FileListViewModel?> GetFileByIdAsync(int id);

		/// <summary>
		/// Deletes a file by its ID.
		/// یک فایل را بر اساس شناسه آن حذف می‌کند.
		/// </summary>
		/// <param name="id">
		/// The ID of the file to delete. / شناسه فایل برای حذف.
		/// </param>
		/// <returns>
		/// A result indicating success or failure of the operation. / نتیجه‌ای که موفقیت یا شکست عملیات را نشان می‌دهد.
		/// </returns>
		Task<ResultViewModel> DeleteFileAsync(int id);
	}
}
