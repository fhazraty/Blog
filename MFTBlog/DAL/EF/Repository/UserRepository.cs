using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
	/// <summary>
	/// Implements repository methods for managing User entities.
	/// پیاده‌سازی روش‌های مخزن برای مدیریت موجودیت‌های کاربر.
	/// </summary>
	public class UserRepository : Repository<User>, IUserRepository
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserRepository"/> class.
		/// یک نمونه جدید از کلاس UserRepository را مقداردهی می‌کند.
		/// </summary>
		/// <param name="context">The database context. / کانتکست پایگاه داده.</param>
		public UserRepository(BlogContext context) : base(context)
		{
		}

		/// <summary>
		/// Finds a user by their username asynchronously, including their roles.
		/// یک کاربر را بر اساس نام کاربری به صورت غیرهمزمان پیدا می‌کند، شامل نقش‌های آن.
		/// </summary>
		/// <param name="username">
		/// The username of the user to find. / نام کاربری کاربر برای جستجو.
		/// </param>
		/// <returns>
		/// The user if found; otherwise, null. / کاربر در صورت یافتن؛ در غیر این صورت null.
		/// </returns>
		public async Task<User?> FindByUsername(string username)
		{
			return await this
				._dbSet
				.AsQueryable()
			    .AsNoTracking()
				.Include(u => u.Roles) // Includes related roles. / شامل نقش‌های مرتبط.
				.FirstOrDefaultAsync(u => u.Username == username);
		}

		/// <summary>
		/// Retrieves the total count of users in the database asynchronously.
		/// تعداد کل کاربران را به صورت غیرهمزمان از پایگاه داده بازیابی می‌کند.
		/// </summary>
		/// <returns>
		/// The total count of users. / تعداد کل کاربران.
		/// </returns>
		public async Task<int> GetUsersCount()
		{
			return 
				await this
				._dbSet
				.AsQueryable()
				.AsNoTracking()
				.CountAsync();
		}

		/// <summary>
		/// Retrieves a paginated list of users from the database asynchronously, including their roles.
		/// یک لیست صفحه‌بندی‌شده از کاربران را به صورت غیرهمزمان از پایگاه داده بازیابی می‌کند، شامل نقش‌های آن‌ها.
		/// </summary>
		/// <param name="page">
		/// The page number to retrieve. / شماره صفحه برای بازیابی.
		/// </param>
		/// <param name="perPage">
		/// The number of users per page. / تعداد کاربران در هر صفحه.
		/// </param>
		/// <returns>
		/// A list of users for the specified page. / لیستی از کاربران برای صفحه مشخص‌شده.
		/// </returns>
		public async Task<List<User>> GetUsers(int page, int perPage)
		{
			return await 
				this._dbSet
				.AsQueryable()
				.AsNoTracking()
				.Include(u => u.Roles) // Includes related roles. / شامل نقش‌های مرتبط.
				.Skip((page - 1) * perPage) // Skips users for previous pages. / کاربران صفحات قبلی را نادیده می‌گیرد.
				.Take(perPage) // Takes the number of users for the current page. / تعداد کاربران صفحه جاری را برمی‌دارد.
				.ToListAsync();
		}
	}
}
