using BLL.CMS.Model;
using DAL.CMS.EF.Model;
using DAL.CMS.EF.Repository;

namespace BLL.CMS.Management
{
	/// <summary>
	/// Manages operations related to tags in the application.
	/// مدیریت عملیات مرتبط با تگ‌ها در برنامه.
	/// </summary>
	public class TagManagement : ITagManagement
	{
		/// <summary>
		/// The repository for managing tag data.
		/// مخزن مدیریت داده‌های تگ.
		/// </summary>
		public ITagRepository TagRepository { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TagManagement"/> class.
		/// یک نمونه جدید از کلاس TagManagement را مقداردهی می‌کند.
		/// </summary>
		/// <param name="tagRepository">The tag repository. / مخزن تگ.</param>
		public TagManagement(ITagRepository tagRepository)
		{
			TagRepository = tagRepository;
		}

		/// <summary>
		/// Adds a new tag to the system.
		/// یک تگ جدید به سیستم اضافه می‌کند.
		/// </summary>
		/// <param name="tagViewModel">The details of the tag to add. / جزئیات تگ برای اضافه کردن.</param>
		/// <returns>
		/// A result indicating the success or failure of the operation. / نتیجه‌ای که موفقیت یا شکست عملیات را نشان می‌دهد.
		/// </returns>
		public async Task<ResultViewModel> AddTag(TagViewModel tagViewModel)
		{
			try
			{
				// Create a new Tag entity from the view model.
				// یک موجودیت تگ جدید از ViewModel ایجاد کنید.
				var tag = new Tag
				{
					Name = tagViewModel.Name
				};

				// Add the tag to the repository.
				// تگ را به مخزن اضافه کنید.
				await TagRepository.AddAsync(tag);

				// Return success result.
				// نتیجه موفقیت بازگردانید.
				return new ResultEntityViewModel<Tag>()
				{
					Entity = tag,
					IsSuccessful = true
				};
			}
			catch (Exception ex)
			{
				// Return failure result in case of an error.
				// در صورت بروز خطا، نتیجه شکست بازگردانید.
				return new ResultEntityViewModel<Tag>()
				{
					Exception = ex,
					IsSuccessful = false
				};
			}
		}

		/// <summary>
		/// Retrieves all tags from the system.
		/// تمامی تگ‌ها را از سیستم بازیابی می‌کند.
		/// </summary>
		/// <returns>
		/// A list of tag view models. / لیستی از مدل‌های نمایشی تگ.
		/// </returns>
		public async Task<List<TagViewModel>> GetTags()
		{
			// Retrieve all tags from the repository.
			// تمامی تگ‌ها را از مخزن بازیابی کنید.
			var tags = await TagRepository.GetAllAsync();

			// Map the tags to view models.
			// تگ‌ها را به مدل‌های نمایشی نگاشت کنید.
			return tags.Select(tag => new TagViewModel
			{
				Id = tag.Id,
				Name = tag.Name
			}).ToList();
		}

		/// <summary>
		/// Deletes a tag by its ID.
		/// یک تگ را بر اساس شناسه آن حذف می‌کند.
		/// </summary>
		/// <param name="id">The ID of the tag to delete. / شناسه تگ برای حذف.</param>
		/// <returns>
		/// A result indicating the success or failure of the operation. / نتیجه‌ای که موفقیت یا شکست عملیات را نشان می‌دهد.
		/// </returns>
		public async Task<ResultViewModel> DeleteTag(int id)
		{
			try
			{
				// Retrieve the tag by ID.
				// تگ را بر اساس شناسه بازیابی کنید.
				var tag = await TagRepository.GetByIdAsync(id);
				if (tag == null)
				{
					// Return failure result if the tag is not found.
					// در صورت عدم یافتن تگ، نتیجه شکست بازگردانید.
					return new ResultEntityViewModel<int>()
					{
						Entity = id,
						IsSuccessful = false,
						Message = "کلید یافت نشد",
						Exception = new KeyNotFoundException()
					};
				}

				// Check if the tag has related posts.
				// بررسی کنید آیا تگ با پست‌های مرتبط وجود دارد یا خیر.
				if (tag.Posts != null && tag.Posts.Any())
				{
					// Return failure if the tag has related posts.
					// در صورت وجود پست‌های وابسته به تگ، نتیجه شکست بازگردانید.
					return new ResultEntityViewModel<int>()
					{
						Entity = id,
						IsSuccessful = false,
						Message = "یک یا چند پست وابسته به این تگ وجود دارد!",
						Exception = new Exception("Tag has related posts and cannot be deleted")
					};
				}

				// Delete the tag from the repository.
				// تگ را از مخزن حذف کنید.
				await TagRepository.DeleteAsync(id);

				// Return success result.
				// نتیجه موفقیت بازگردانید.
				return new ResultEntityViewModel<int>()
				{
					Entity = id,
					IsSuccessful = true
				};
			}
			catch (Exception ex)
			{
				// Return failure result in case of an error.
				// در صورت بروز خطا، نتیجه شکست بازگردانید.
				return new ResultEntityViewModel<int>()
				{
					Entity = id,
					Exception = ex,
					IsSuccessful = false
				};
			}
		}

		/// <summary>
		/// Updates an existing tag.
		/// یک تگ موجود را به‌روزرسانی می‌کند.
		/// </summary>
		/// <param name="tagViewModel">The updated details of the tag. / جزئیات به‌روزرسانی‌شده تگ.</param>
		/// <returns>
		/// A result indicating the success or failure of the operation. / نتیجه‌ای که موفقیت یا شکست عملیات را نشان می‌دهد.
		/// </returns>
		public async Task<ResultViewModel> UpdateTag(TagViewModel tagViewModel)
		{
			try
			{
				// Retrieve the tag by ID.
				// تگ را بر اساس شناسه بازیابی کنید.
				var tag = await TagRepository.GetByIdAsync(tagViewModel.Id);
				if (tag == null)
				{
					// Return failure if the tag is not found.
					// در صورت عدم یافتن تگ، نتیجه شکست بازگردانید.
					return new ResultEntityViewModel<int>()
					{
						Entity = tagViewModel.Id,
						IsSuccessful = false,
						Message = "کلید یافت نشد",
						Exception = new KeyNotFoundException()
					};
				}

				// Update the tag details.
				// جزئیات تگ را به‌روزرسانی کنید.
				tag.Name = tagViewModel.Name;

				// Save the changes to the repository.
				// تغییرات را در مخزن ذخیره کنید.
				await TagRepository.UpdateAsync(tag);

				// Return success result.
				// نتیجه موفقیت بازگردانید.
				return new ResultEntityViewModel<Tag>()
				{
					Entity = tag,
					IsSuccessful = true
				};
			}
			catch (Exception ex)
			{
				// Return failure result in case of an error.
				// در صورت بروز خطا، نتیجه شکست بازگردانید.
				return new ResultEntityViewModel<Tag>()
				{
					Exception = ex,
					IsSuccessful = false
				};
			}
		}
	}
}
