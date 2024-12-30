using BLL.Model;

namespace BLL.Management
{
	public interface ICategoryManagement
	{
		Task<IEnumerable<CategoryViewModel>> ListAllCategoriesAsync();
	}
}
