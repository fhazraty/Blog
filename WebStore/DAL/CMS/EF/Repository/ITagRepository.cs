using DAL.CMS.EF.Model;

namespace DAL.CMS.EF.Repository
{
	/// <summary>
	/// Defines a repository interface for managing Tag entities.
	/// یک رابط مخزن برای مدیریت موجودیت‌های برچسب تعریف می‌کند.
	/// </summary>
	public interface ITagRepository : IRepository<Tag>
	{
		/// <summary>
		/// Retrieves a list of tags by their IDs asynchronously.
		/// لیستی از برچسب‌ها را بر اساس شناسه‌های آن‌ها به صورت غیرهمزمان بازیابی می‌کند.
		/// </summary>
		/// <param name="listId">
		/// The list of tag IDs to retrieve. / لیستی از شناسه‌های برچسب برای بازیابی.
		/// </param>
		/// <returns>
		/// A list of tags matching the specified IDs. / لیستی از برچسب‌ها که با شناسه‌های مشخص شده تطابق دارند.
		/// </returns>
		Task<List<Tag>> GetAllByIdList(List<int> listId);
	}
}
