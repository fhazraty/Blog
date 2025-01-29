using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
	/// <summary>
	/// Implements repository methods for managing Category entities.
	/// پیاده‌سازی روش‌های مخزن برای مدیریت موجودیت‌های دسته‌بندی.
	/// </summary>
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CategoryRepository"/> class.
		/// یک نمونه جدید از کلاس CategoryRepository را مقداردهی می‌کند.
		/// </summary>
		/// <param name="context">The database context. / کانتکست پایگاه داده.</param>
		public CategoryRepository(BlogContext context) : base(context)
		{
		}

		/// <summary>
		/// Retrieves a category by its ID asynchronously, including related posts and subcategories.
		/// یک دسته‌بندی را بر اساس شناسه آن به صورت غیرهمزمان بازیابی می‌کند، شامل پست‌ها و زیرمجموعه‌های مرتبط.
		/// </summary>
		/// <param name="id">
		/// The ID of the category to retrieve. / شناسه دسته‌بندی برای بازیابی.
		/// </param>
		/// <returns>
		/// The category if found; otherwise, null. / دسته‌بندی در صورت یافتن؛ در غیر این صورت null.
		/// </returns>
		public async Task<Category?> GetByIdAsync(int id)
		{
			return await _dbSet
				.Include(c => c.Posts) // Includes related posts. / شامل پست‌های مرتبط.
				.Include(c => c.SubCategories) // Includes related subcategories. / شامل زیرمجموعه‌های مرتبط.
				.FirstOrDefaultAsync(c => c.Id == id);
		}
	}
}
