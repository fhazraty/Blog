using BLL.Model;
using DAL.EF.Model;
using DAL.EF.Repository;

namespace BLL.Management
{
	/// <summary>
	/// Manages operations related to categories in the application.
	/// مدیریت عملیات مرتبط با دسته‌بندی‌ها در برنامه.
	/// </summary>
	public class CategoryManagement : ICategoryManagement
	{
		/// <summary>
		/// The repository for managing category data.
		/// مخزن مدیریت داده‌های دسته‌بندی.
		/// </summary>
		public ICategoryRepository CategoryRepository { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CategoryManagement"/> class.
		/// یک نمونه جدید از کلاس CategoryManagement مقداردهی می‌کند.
		/// </summary>
		/// <param name="categoryRepository">The category repository. / مخزن دسته‌بندی.</param>
		public CategoryManagement(ICategoryRepository categoryRepository)
		{
			this.CategoryRepository = categoryRepository;
		}

		/// <summary>
		/// Retrieves a list of all categories.
		/// لیستی از تمامی دسته‌بندی‌ها را بازیابی می‌کند.
		/// </summary>
		/// <returns>
		/// A collection of category view models. / یک مجموعه از مدل‌های نمایشی دسته‌بندی.
		/// </returns>
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

		/// <summary>
		/// Deletes a category by its ID.
		/// یک دسته‌بندی را بر اساس شناسه آن حذف می‌کند.
		/// </summary>
		/// <param name="categoryId">The ID of the category to delete. / شناسه دسته‌بندی که باید حذف شود.</param>
		/// <returns>
		/// A result indicating success or failure, including the deleted category ID. / نتیجه‌ای که موفقیت یا شکست را نشان می‌دهد، شامل شناسه دسته‌بندی حذف شده.
		/// </returns>
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

			// Check if the category has related posts.
			// بررسی کنید آیا دسته‌بندی دارای پست‌های مرتبط است یا خیر.
			if (category.Posts != null && category.Posts.Any())
			{
				return new ResultEntityViewModel<int>()
				{
					Entity = categoryId,
					IsSuccessful = false,
					Message = "این دسته بندی دارای پست های مرتبط است و نمی توان آن را حذف کرد!"
				};
			}

			// Check if the category has subcategories.
			// بررسی کنید آیا دسته‌بندی دارای زیر دسته‌بندی است یا خیر.
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

		/// <summary>
		/// Adds a new category to the system.
		/// یک دسته‌بندی جدید به سیستم اضافه می‌کند.
		/// </summary>
		/// <param name="categoryViewModel">The details of the category to add. / جزئیات دسته‌بندی که باید اضافه شود.</param>
		/// <returns>
		/// A result indicating success or failure, including the added category ID. / نتیجه‌ای که موفقیت یا شکست را نشان می‌دهد، شامل شناسه دسته‌بندی اضافه شده.
		/// </returns>
		public async Task<ResultEntityViewModel<int>> AddNewCategoryAsync(CategoryViewModel categoryViewModel)
		{
			try
			{
				// Validate the parent category if specified.
				// بررسی معتبر بودن دسته‌بندی والد در صورت مشخص شدن.
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

				// Create a new category entity.
				// ایجاد یک موجودیت دسته‌بندی جدید.
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

		/// <summary>
		/// Updates an existing category.
		/// یک دسته‌بندی موجود را به‌روزرسانی می‌کند.
		/// </summary>
		/// <param name="categoryViewModel">The updated details of the category. / جزئیات به‌روزرسانی‌شده دسته‌بندی.</param>
		/// <returns>
		/// A result indicating success or failure, including the updated category ID. / نتیجه‌ای که موفقیت یا شکست را نشان می‌دهد، شامل شناسه دسته‌بندی به‌روزرسانی‌شده.
		/// </returns>
		public async Task<ResultEntityViewModel<int>> UpdateCategoryAsync(CategoryViewModel categoryViewModel)
		{
			try
			{
				// Retrieve the category by ID.
				// دسته‌بندی را بر اساس شناسه بازیابی کنید.
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

				// Validate the parent category if specified.
				// بررسی معتبر بودن دسته‌بندی والد در صورت مشخص شدن.
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

				// Update the category details.
				// جزئیات دسته‌بندی را به‌روزرسانی کنید.
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
