using BLL.Model;
using DAL.EF.Model;
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
		public async Task<ResultEntityViewModel<int>> AddNewCategoryAsync(CategoryViewModel categoryViewModel)
		{
			try
			{
				if (categoryViewModel.ParentCategoryId.HasValue)
				{
					if(categoryViewModel.ParentCategoryId != 0)
					{
						var parentCategory = await CategoryRepository.GetByIdAsync(categoryViewModel.ParentCategoryId.Value);
						if (parentCategory == null)
						{
							return new ResultEntityViewModel<int>
							{
								IsSuccessful = false,
								Entity = categoryViewModel.ParentCategoryId.Value,
								Exception = new KeyNotFoundException(),
								Message = "دسته بندی والد یافت نشد!"
							};
						}


					}
					else
					{
						categoryViewModel.ParentCategoryId = null;
					}
				}

				var category = new Category
				{
					Name = categoryViewModel.Name,
					ParentCategoryId = categoryViewModel.ParentCategoryId
				};

				await CategoryRepository.AddAsync(category);

				return new ResultEntityViewModel<int>
				{
					Entity = category.Id,
					IsSuccessful = true,
					Message = "دسته بندی با موفقیت اضافه شد!"
				};
			}
			catch (Exception ex)
			{
				return new ResultEntityViewModel<int>
				{
					Exception = ex,
					IsSuccessful = false,
					Message = ex.Message
				};
			}
		}
		public async Task<ResultEntityViewModel<int>> UpdateCategoryAsync(CategoryViewModel categoryViewModel)
		{
			try
			{
				var category = await CategoryRepository.GetByIdAsync(categoryViewModel.Id);
				if (category == null)
				{
					return new ResultEntityViewModel<int>
					{
						IsSuccessful = false,
						Entity = categoryViewModel.Id,
						Exception = new KeyNotFoundException("دسته بندی یافت نشد!"),
						Message = "دسته بندی یافت نشد!"
					};
				}

				if (categoryViewModel.ParentCategoryId.HasValue)
				{
					if (categoryViewModel.ParentCategoryId != 0)
					{
						var parentCategory = await CategoryRepository.GetByIdAsync(categoryViewModel.ParentCategoryId.Value);
						if (parentCategory == null)
						{
							return new ResultEntityViewModel<int>
							{
								IsSuccessful = false,
								Entity = categoryViewModel.ParentCategoryId.Value,
								Exception = new KeyNotFoundException("دسته بندی والد یافت نشد!"),
								Message = "دسته بندی والد یافت نشد!"
							};
						}
					}
					else
					{
						categoryViewModel.ParentCategoryId = null;
					}
				}

				category.Name = categoryViewModel.Name;
				category.ParentCategoryId = categoryViewModel.ParentCategoryId;

				await CategoryRepository.UpdateAsync(category);

				return new ResultEntityViewModel<int>
				{
					Entity = category.Id,
					IsSuccessful = true,
					Message = "دسته بندی با موفقیت به روز شد!"
				};
			}
			catch (Exception ex)
			{
				return new ResultEntityViewModel<int>
				{
					Exception = ex,
					IsSuccessful = false,
					Message = ex.Message
				};
			}
		}

	}
}
