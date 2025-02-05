using BLL.CMS.Model;

namespace BLL.CMS.Management
{
	/// <summary>
	/// Defines a contract for managing post-related operations in the application.
	/// قراردادی برای مدیریت عملیات مرتبط با پست‌ها در برنامه تعریف می‌کند.
	/// </summary>
	public interface IPostManagement
	{
		/// <summary>
		/// Adds a new post to the system.
		/// یک پست جدید به سیستم اضافه می‌کند.
		/// </summary>
		/// <param name="postViewModel">
		/// The details of the post to add. / جزئیات پستی که باید اضافه شود.
		/// </param>
		/// <returns>
		/// A result indicating the success or failure of the operation. / نتیجه‌ای که موفقیت یا شکست عملیات را نشان می‌دهد.
		/// </returns>
		Task<ResultViewModel> AddPost(PostViewModel postViewModel);

		/// <summary>
		/// Retrieves a paginated list of posts and their total count.
		/// یک لیست صفحه‌بندی‌شده از پست‌ها و تعداد کل آن‌ها را بازیابی می‌کند.
		/// </summary>
		/// <param name="page">
		/// The page number to retrieve. / شماره صفحه‌ای که باید بازیابی شود.
		/// </param>
		/// <param name="perPage">
		/// The number of posts per page. / تعداد پست‌ها در هر صفحه.
		/// </param>
		/// <returns>
		/// A tuple containing the list of posts and their total count. / یک تاپل که شامل لیست پست‌ها و تعداد کل آن‌ها است.
		/// </returns>
		Task<(List<PostListViewModel>, int)> ListPost(int page, int perPage);

		/// <summary>
		/// Deletes a post by its ID.
		/// یک پست را بر اساس شناسه آن حذف می‌کند.
		/// </summary>
		/// <param name="postId">
		/// The ID of the post to delete. / شناسه پستی که باید حذف شود.
		/// </param>
		/// <returns>
		/// A result indicating the success or failure of the operation. / نتیجه‌ای که موفقیت یا شکست عملیات را نشان می‌دهد.
		/// </returns>
		Task<ResultViewModel> DeletePost(int postId);

		/// <summary>
		/// Retrieves a post by its ID.
		/// یک پست را بر اساس شناسه آن بازیابی می‌کند.
		/// </summary>
		/// <param name="postId">
		/// The ID of the post to retrieve. / شناسه پستی که باید بازیابی شود.
		/// </param>
		/// <returns>
		/// A result containing the post details or an error if not found. / نتیجه‌ای که شامل جزئیات پست یا خطا در صورت عدم وجود است.
		/// </returns>
		Task<ResultViewModel> GetPostById(int postId);

		/// <summary>
		/// Updates an existing post with new details.
		/// یک پست موجود را با جزئیات جدید به‌روزرسانی می‌کند.
		/// </summary>
		/// <param name="postViewModel">
		/// The updated details of the post. / جزئیات به‌روزرسانی‌شده پست.
		/// </param>
		/// <returns>
		/// A result indicating the success or failure of the operation. / نتیجه‌ای که موفقیت یا شکست عملیات را نشان می‌دهد.
		/// </returns>
		Task<ResultViewModel> UpdatePost(PostViewModel postViewModel);
	}
}
