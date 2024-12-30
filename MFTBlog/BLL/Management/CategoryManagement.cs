using BLL.Model;
using DAL.EF.Repository;

namespace BLL.Management
{
	public class CategoryManagement : ICategoryManagement
	{
		public ICategoryRepository CategoryRepository { get; set; }
		public CategoryManagement(ICategoryRepository categoryRepository)
		{
			this.CategoryRepository = categoryRepository;
		}

		public async Task<IEnumerable<CategoryViewModel>> ListAllCategoriesAsync()
		{
			var categories = await CategoryRepository.GetAllAsync();
			return categories.Select(c => new CategoryViewModel
			{
				Id = c.Id,
				Name = c.Name,
				ParentCategoryId = c.ParentCategoryId
			}).ToList();
		}
	}
}
