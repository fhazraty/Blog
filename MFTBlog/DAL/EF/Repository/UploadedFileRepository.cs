using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
	/// <summary>
	/// Implements repository methods for managing UploadedFile entities.
	/// پیاده‌سازی روش‌های مخزن برای مدیریت موجودیت‌های فایل آپلود شده.
	/// </summary>
	public class UploadedFileRepository : Repository<UploadedFile>, IUploadedFileRepository
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UploadedFileRepository"/> class.
		/// یک نمونه جدید از کلاس UploadedFileRepository را مقداردهی می‌کند.
		/// </summary>
		/// <param name="context">The database context. / کانتکست پایگاه داده.</param>
		public UploadedFileRepository(BlogContext context) : base(context)
		{
		}

		/// <summary>
		/// Retrieves the total count of uploaded files in the database asynchronously.
		/// تعداد کل فایل‌های آپلود شده را به صورت غیرهمزمان از پایگاه داده بازیابی می‌کند.
		/// </summary>
		/// <returns>
		/// The total count of uploaded files. / تعداد کل فایل‌های آپلود شده.
		/// </returns>
		public async Task<int> GetFilesCount()
		{
			return await _dbSet.CountAsync();
		}

		/// <summary>
		/// Retrieves a paginated list of uploaded files from the database asynchronously.
		/// لیست صفحه‌بندی‌شده‌ای از فایل‌های آپلود شده را به صورت غیرهمزمان از پایگاه داده بازیابی می‌کند.
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
		public async Task<List<UploadedFile>> GetFiles(int page, int perPage)
		{
			return await _dbSet
				.OrderByDescending(p => p.UploadedAt) // Orders files by upload date (newest first). / فایل‌ها را بر اساس تاریخ آپلود (جدیدترین اول) مرتب می‌کند.
				.Skip((page - 1) * perPage) // Skips files for previous pages. / فایل‌های صفحات قبلی را نادیده می‌گیرد.
				.Take(perPage) // Takes the number of files for the current page. / تعداد فایل‌های مربوط به صفحه جاری را برمی‌دارد.
				.ToListAsync();
		}

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
		public async Task<UploadedFile?> GetByIdAsync(int id)
		{
			return await _dbSet.FirstOrDefaultAsync(p => p.Id == id);
		}
	}
}
