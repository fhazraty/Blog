using DAL.CMS.EF.Model;

namespace DAL.CMS.EF.Repository
{
	/// <summary>
	/// Defines a repository interface for managing Post entities.
	/// یک رابط مخزن برای مدیریت موجودیت‌های پست تعریف می‌کند.
	/// </summary>
	public interface IPostRepository : IRepository<Post>
	{
		/// <summary>
		/// Retrieves a paginated list of posts from the database asynchronously.
		/// یک لیست صفحه‌بندی‌شده از پست‌ها را به صورت غیرهمزمان از پایگاه داده بازیابی می‌کند.
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
		Task<List<Post>> GetPosts(int page, int perpage);

		/// <summary>
		/// Retrieves the total count of posts in the database asynchronously.
		/// تعداد کل پست‌ها را به صورت غیرهمزمان از پایگاه داده بازیابی می‌کند.
		/// </summary>
		/// <returns>
		/// The total count of posts. / تعداد کل پست‌ها.
		/// </returns>
		Task<int> GetPostsCount();
	}
}
