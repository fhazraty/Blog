using BLL.Model;

namespace BLL.Management
{
	/// <summary>
	/// Defines a contract for managing category operations in the application.
	/// قراردادی برای مدیریت عملیات مرتبط با دسته‌بندی‌ها در برنامه تعریف می‌کند.
	/// </summary>
	public interface ICategoryManagement
	{
		/// <summary>
		/// Retrieves a list of all categories.
		/// لیستی از تمامی دسته‌بندی‌ها را بازیابی می‌کند.
		/// </summary>
		/// <returns>
		/// An enumerable collection of category view models. / یک مجموعه قابل شمارش از مدل‌های نمایشی دسته‌بندی.
		/// </returns>
		Task<IEnumerable<CategoryViewModel>> ListAllCategoriesAsync();

		/// <summary>
		/// Deletes a category by its ID.
		/// یک دسته‌بندی را بر اساس شناسه آن حذف می‌کند.
		/// </summary>
		/// <param name="categoryId">
		/// The ID of the category to delete. / شناسه دسته‌بندی که باید حذف شود.
		/// </param>
		/// <returns>
		/// A result indicating success or failure of the operation, including the deleted category ID. / نتیجه‌ای که موفقیت یا شکست عملیات را نشان می‌دهد، شامل شناسه دسته‌بندی حذف شده.
		/// </returns>
		Task<ResultEntityViewModel<int>> DeleteCategoryAsync(int categoryId);

		/// <summary>
		/// Adds a new category to the system.
		/// یک دسته‌بندی جدید به سیستم اضافه می‌کند.
		/// </summary>
		/// <param name="categoryViewModel">
		/// The details of the category to add. / جزئیات دسته‌بندی که باید اضافه شود.
		/// </param>
		/// <returns>
		/// A result indicating success or failure of the operation, including the ID of the newly added category. / نتیجه‌ای که موفقیت یا شکست عملیات را نشان می‌دهد، شامل شناسه دسته‌بندی اضافه شده.
		/// </returns>
		Task<ResultEntityViewModel<int>> AddNewCategoryAsync(CategoryViewModel categoryViewModel);

		/// <summary>
		/// Updates an existing category in the system.
		/// یک دسته‌بندی موجود در سیستم را به‌روزرسانی می‌کند.
		/// </summary>
		/// <param name="categoryViewModel">
		/// The updated details of the category. / جزئیات به‌روزرسانی‌شده دسته‌بندی.
		/// </param>
		/// <returns>
		/// A result indicating success or failure of the operation, including the ID of the updated category. / نتیجه‌ای که موفقیت یا شکست عملیات را نشان می‌دهد، شامل شناسه دسته‌بندی به‌روزرسانی‌شده.
		/// </returns>
		Task<ResultEntityViewModel<int>> UpdateCategoryAsync(CategoryViewModel categoryViewModel);
	}
}
