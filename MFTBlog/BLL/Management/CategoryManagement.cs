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
		public async Task<ResultEntityViewModel<int>> DeleteCategoryAsync(int categoryId)
		{
			var category = await CategoryRepository.GetByIdAsync(categoryId);
			if (category == null)
			{
				return new ResultEntityViewModel<int>()
				{
					Entity = categoryId,
					Exception = new KeyNotFoundException("آی دی یافت نشد!"),
					IsSuccessful = false,
					Message = "آی دی یافت نشد!"
				};
			}

			if (category.Posts != null && category.Posts.Any())
			{
				return new ResultEntityViewModel<int>()
				{
					Entity = categoryId,
					IsSuccessful = false,
					Message = "این دسته بندی دارای پست های مرتبط است و نمی توان آن را حذف کرد!"
				};
			}

			if (category.SubCategories != null && category.SubCategories.Any())
			{
				return new ResultEntityViewModel<int>()
				{
					Entity = categoryId,
					IsSuccessful = false,
					Message = "این دسته بندی دارای زیر دسته بندی های مرتبط است و نمی توان آن را حذف کرد!"
				};
			}

			await CategoryRepository.DeleteAsync(categoryId);

			return new ResultEntityViewModel<int>()
			{
				Entity = categoryId,
				IsSuccessful = true,
				Message = "حذف با موفقیت انجام شد!"
			};
		}
	}
}
