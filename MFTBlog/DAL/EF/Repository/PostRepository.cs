using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
	/// <summary>
	/// Implements repository methods for managing Post entities.
	/// پیاده‌سازی روش‌های مخزن برای مدیریت موجودیت‌های پست.
	/// </summary>
	public class PostRepository : Repository<Post>, IPostRepository
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PostRepository"/> class.
		/// یک نمونه جدید از کلاس PostRepository را مقداردهی می‌کند.
		/// </summary>
		/// <param name="context">The database context. / کانتکست پایگاه داده.</param>
		public PostRepository(BlogContext context) : base(context)
		{
		}

		/// <summary>
		/// Retrieves a paginated list of posts from the database asynchronously.
		/// لیست صفحه‌بندی‌شده‌ای از پست‌ها را به صورت غیرهمزمان از پایگاه داده بازیابی می‌کند.
		/// </summary>
		/// <param name="page">
		/// The page number to retrieve. / شماره صفحه برای بازیابی.
		/// </param>
		/// <param name="perpage">
		/// The number of posts per page. / تعداد پست‌ها در هر صفحه.
		/// </param>
		/// <returns>
		/// A list of posts for the specified page. / لیستی از پست‌ها برای صفحه مشخص‌شده.
		/// </returns>
		public async Task<List<Post>> GetPosts(int page, int perpage)
		{
			return await _dbSet
				.AsNoTracking()
				.Include(p => p.Author)
				.Include(p => p.Category)
				.Include(p => p.Tags)
				.OrderByDescending(p => p.CreatedAt)
				.Skip((page - 1) * perpage)
				.Take(perpage)
				.ToListAsync();
		}

		/// <summary>
		/// Retrieves the total count of posts in the database asynchronously.
		/// تعداد کل پست‌ها را به صورت غیرهمزمان از پایگاه داده بازیابی می‌کند.
		/// </summary>
		/// <returns>
		/// The total count of posts. / تعداد کل پست‌ها.
		/// </returns>
		public async Task<int> GetPostsCount()
		{
			return await _dbSet.AsNoTracking().CountAsync();
		}

		/// <summary>
		/// Retrieves a specific post by its ID asynchronously, including related data.
		/// یک پست خاص را بر اساس شناسه آن به صورت غیرهمزمان بازیابی می‌کند، شامل داده‌های مرتبط.
		/// </summary>
		/// <param name="id">The ID of the post to retrieve. / شناسه پست برای بازیابی.</param>
		/// <returns>
		/// The post if found; otherwise, null. / پست در صورت یافتن؛ در غیر این صورت null.
		/// </returns>
		public async Task<Post?> GetByIdAsync(int id)
		{
			return await _dbSet
				.Include(p => p.Tags)
				.Include(p => p.Category)
				.Include(p => p.Author)
				.FirstOrDefaultAsync(p => p.Id == id);
		}
	}
}
