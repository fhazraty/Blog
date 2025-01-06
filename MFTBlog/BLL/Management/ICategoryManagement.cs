using BLL.Model;

namespace BLL.Management
{
	public interface ICategoryManagement
	{
		Task<IEnumerable<CategoryViewModel>> ListAllCategoriesAsync();
		Task<ResultEntityViewModel<int>> DeleteCategoryAsync(int categoryId);
		Task<ResultEntityViewModel<int>> AddNewCategoryAsync(CategoryViewModel categoryViewModel);
	}
}
