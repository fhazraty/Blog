using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
	/// <summary>
	/// Implements repository methods for managing Tag entities.
	/// پیاده‌سازی روش‌های مخزن برای مدیریت موجودیت‌های برچسب.
	/// </summary>
	public class TagRepository : Repository<Tag>, ITagRepository
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TagRepository"/> class.
		/// یک نمونه جدید از کلاس TagRepository را مقداردهی می‌کند.
		/// </summary>
		/// <param name="context">The database context. / کانتکست پایگاه داده.</param>
		public TagRepository(BlogContext context) : base(context)
		{
		}

		/// <summary>
		/// Retrieves a list of tags by their IDs asynchronously.
		/// لیستی از برچسب‌ها را بر اساس شناسه‌های آن‌ها به صورت غیرهمزمان بازیابی می‌کند.
		/// </summary>
		/// <param name="listId">
		/// The list of tag IDs to retrieve. / لیست شناسه‌های برچسب برای بازیابی.
		/// </param>
		/// <returns>
		/// A list of tags matching the specified IDs. / لیستی از برچسب‌ها که با شناسه‌های مشخص‌شده تطابق دارند.
		/// </returns>
		public async Task<List<Tag>> GetAllByIdList(List<int> listId)
		{
			return await this._dbSet.Where(t => listId.Contains(t.Id)).ToListAsync();
		}

		/// <summary>
		/// Retrieves a specific tag by its ID asynchronously, including related posts.
		/// یک برچسب خاص را بر اساس شناسه آن به همراه پست‌های مرتبط به صورت غیرهمزمان بازیابی می‌کند.
		/// </summary>
		/// <param name="id">
		/// The ID of the tag to retrieve. / شناسه برچسب برای بازیابی.
		/// </param>
		/// <returns>
		/// The tag if found; otherwise, null. / برچسب در صورت یافتن؛ در غیر این صورت null.
		/// </returns>
		public async Task<Tag?> GetByIdAsync(int id)
		{
			return await _dbSet
				.Include(t => t.Posts) // Includes related posts. / شامل پست‌های مرتبط.
				.FirstOrDefaultAsync(t => t.Id == id);
		}
	}
}
