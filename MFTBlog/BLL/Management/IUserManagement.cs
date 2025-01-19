using BLL.Model;

namespace BLL.Management
{
	/// <summary>
	/// Defines a contract for managing user-related operations.
	/// قراردادی برای مدیریت عملیات مرتبط با کاربران تعریف می‌کند.
	/// </summary>
	public interface IUserManagement
	{
		/// <summary>
		/// Adds a new user to the system.
		/// یک کاربر جدید به سیستم اضافه می‌کند.
		/// </summary>
		/// <param name="userViewModel">
		/// The user details to be added. / جزئیات کاربری که باید اضافه شود.
		/// </param>
		/// <returns>
		/// A result indicating the success or failure of the operation. / نتیجه‌ای که موفقیت یا شکست عملیات را نشان می‌دهد.
		/// </returns>
		Task<ResultViewModel> AddUser(UserViewModel userViewModel);

		/// <summary>
		/// Finds a user by their username and password.
		/// یک کاربر را بر اساس نام کاربری و رمز عبور پیدا می‌کند.
		/// </summary>
		/// <param name="userViewModel">
		/// The user details for authentication. / جزئیات کاربر برای احراز هویت.
		/// </param>
		/// <returns>
		/// A result containing the user if found or an error if not. / نتیجه‌ای که شامل کاربر در صورت یافتن یا خطا در غیر این صورت است.
		/// </returns>
		Task<ResultViewModel> FindUser(UserViewModel userViewModel);

		/// <summary>
		/// Retrieves a paginated list of users and the total count of users.
		/// یک لیست صفحه‌بندی‌شده از کاربران و تعداد کل آن‌ها را بازیابی می‌کند.
		/// </summary>
		/// <param name="page">
		/// The page number to retrieve. / شماره صفحه‌ای که باید بازیابی شود.
		/// </param>
		/// <param name="perPage">
		/// The number of users per page. / تعداد کاربران در هر صفحه.
		/// </param>
		/// <returns>
		/// A tuple containing the list of users and the total count. / یک تاپل که شامل لیست کاربران و تعداد کل است.
		/// </returns>
		Task<(List<UserListViewModel>, int)> ListUsers(int page, int perPage);

		/// <summary>
		/// Deletes a user by their ID.
		/// یک کاربر را بر اساس شناسه آن حذف می‌کند.
		/// </summary>
		/// <param name="userId">
		/// The ID of the user to be deleted. / شناسه کاربری که باید حذف شود.
		/// </param>
		/// <returns>
		/// A result indicating the success or failure of the operation. / نتیجه‌ای که موفقیت یا شکست عملیات را نشان می‌دهد.
		/// </returns>
		Task<ResultViewModel> DeleteUserById(int userId);
	}
}
