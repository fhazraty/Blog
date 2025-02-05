using DAL.CMS.EF.Model;

namespace DAL.CMS.EF.Repository
{
	/// <summary>
	/// Defines a repository interface for managing UploadedFile entities.
	/// یک رابط مخزن برای مدیریت موجودیت‌های فایل آپلود شده تعریف می‌کند.
	/// </summary>
	public interface IUploadedFileRepository : IRepository<UploadedFile>
	{
		/// <summary>
		/// Retrieves the total count of uploaded files in the database asynchronously.
		/// تعداد کل فایل‌های آپلود شده را به صورت غیرهمزمان از پایگاه داده بازیابی می‌کند.
		/// </summary>
		/// <returns>
		/// The total count of uploaded files. / تعداد کل فایل‌های آپلود شده.
		/// </returns>
		Task<int> GetFilesCount();

		/// <summary>
		/// Retrieves a paginated list of uploaded files from the database asynchronously.
		/// یک لیست صفحه‌بندی‌شده از فایل‌های آپلود شده را به صورت غیرهمزمان از پایگاه داده بازیابی می‌کند.
		/// </summary>
		/// <param name="page">
		/// The page number to retrieve. / شماره صفحه برای بازیابی.
		/// </param>
		/// <param name="perPage">
		/// The number of files per page. / تعداد فایل‌ها در هر صفحه.
		/// </param>
		/// <returns>
		/// A list of uploaded files for the specified page. / لیستی از فایل‌های آپلود شده برای صفحه مشخص‌شده.
		/// </returns>
		Task<List<UploadedFile>> GetFiles(int page, int perPage);

		/// <summary>
		/// Retrieves a specific uploaded file by its ID asynchronously.
		/// یک فایل آپلود شده خاص را بر اساس شناسه آن به صورت غیرهمزمان بازیابی می‌کند.
		/// </summary>
		/// <param name="id">
		/// The ID of the uploaded file to retrieve. / شناسه فایل آپلود شده برای بازیابی.
		/// </param>
		/// <returns>
		/// The uploaded file if found; otherwise, null. / فایل آپلود شده در صورت یافتن؛ در غیر این صورت null.
		/// </returns>
		Task<UploadedFile?> GetByIdAsync(int id);
	}
}
