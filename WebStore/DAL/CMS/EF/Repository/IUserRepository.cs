using DAL.CMS.EF.Model;

namespace DAL.CMS.EF.Repository
{
	/// <summary>
	/// Defines a repository interface for managing User entities.
	/// یک رابط مخزن برای مدیریت موجودیت‌های کاربر تعریف می‌کند.
	/// </summary>
	public interface IUserRepository : IRepository<User>
	{
		/// <summary>
		/// Finds a user by their username asynchronously.
		/// یک کاربر را بر اساس نام کاربری به صورت غیرهمزمان پیدا می‌کند.
		/// </summary>
		/// <param name="username">
		/// The username of the user to find. / نام کاربری کاربر برای پیدا کردن.
		/// </param>
		/// <returns>
		/// The user if found; otherwise, null. / کاربر در صورت یافتن؛ در غیر این صورت null.
		/// </returns>
		Task<User?> FindByUsername(string username);

		/// <summary>
		/// Retrieves the total count of users in the database asynchronously.
		/// تعداد کل کاربران را به صورت غیرهمزمان از پایگاه داده بازیابی می‌کند.
		/// </summary>
		/// <returns>
		/// The total count of users. / تعداد کل کاربران.
		/// </returns>
		Task<int> GetUsersCount();

		/// <summary>
		/// Retrieves a paginated list of users from the database asynchronously.
		/// لیست صفحه‌بندی‌شده‌ای از کاربران را به صورت غیرهمزمان از پایگاه داده بازیابی می‌کند.
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
		Task<List<User>> GetUsers(int page, int perPage);
	}
}
