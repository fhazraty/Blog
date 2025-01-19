using DAL.EF.Model;

namespace DAL.EF.Repository
{
	/// <summary>
	/// Defines a repository interface for managing Category entities.
	/// یک رابط مخزن برای مدیریت موجودیت‌های دسته‌بندی تعریف می‌کند.
	/// </summary>
	public interface ICategoryRepository : IRepository<Category>
	{
		// This interface inherits the generic repository methods from IRepository.
		// این رابط متدهای عمومی مخزن را از IRepository به ارث می‌برد.
	}
}
