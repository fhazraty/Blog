using BLL.CMS.Model;

namespace BLL.CMS.Management
{
	/// <summary>
	/// Defines a contract for managing tag operations in the application.
	/// قراردادی برای مدیریت عملیات مرتبط با تگ‌ها در برنامه تعریف می‌کند.
	/// </summary>
	public interface ITagManagement
	{
		/// <summary>
		/// Adds a new tag to the system.
		/// یک تگ جدید به سیستم اضافه می‌کند.
		/// </summary>
		/// <param name="tagViewModel">
		/// The details of the tag to add. / جزئیات تگ برای اضافه کردن.
		/// </param>
		/// <returns>
		/// A result indicating success or failure of the operation. / نتیجه‌ای که موفقیت یا شکست عملیات را نشان می‌دهد.
		/// </returns>
		Task<ResultViewModel> AddTag(TagViewModel tagViewModel);

		/// <summary>
		/// Retrieves all tags from the system.
		/// تمامی تگ‌ها را از سیستم بازیابی می‌کند.
		/// </summary>
		/// <returns>
		/// A list of tag view models. / لیستی از مدل‌های نمایشی تگ.
		/// </returns>
		Task<List<TagViewModel>> GetTags();

		/// <summary>
		/// Deletes a tag by its ID.
		/// یک تگ را بر اساس شناسه آن حذف می‌کند.
		/// </summary>
		/// <param name="id">
		/// The ID of the tag to delete. / شناسه تگ برای حذف.
		/// </param>
		/// <returns>
		/// A result indicating success or failure of the operation. / نتیجه‌ای که موفقیت یا شکست عملیات را نشان می‌دهد.
		/// </returns>
		Task<ResultViewModel> DeleteTag(int id);

		/// <summary>
		/// Updates an existing tag in the system.
		/// یک تگ موجود در سیستم را به‌روزرسانی می‌کند.
		/// </summary>
		/// <param name="tagViewModel">
		/// The updated details of the tag. / جزئیات به‌روزرسانی‌شده تگ.
		/// </param>
		/// <returns>
		/// A result indicating success or failure of the operation. / نتیجه‌ای که موفقیت یا شکست عملیات را نشان می‌دهد.
		/// </returns>
		Task<ResultViewModel> UpdateTag(TagViewModel tagViewModel);
	}
}
