using BLL.Model;
using DAL.EF.Model;
using DAL.EF.Repository;

namespace BLL.Management
{
	/// <summary>
	/// Manages operations related to posts in the application.
	/// مدیریت عملیات مرتبط با پست‌ها در برنامه.
	/// </summary>
	public class PostManagement : IPostManagement
	{
		/// <summary>
		/// The repository for managing post data.
		/// مخزن مدیریت داده‌های پست.
		/// </summary>
		public IPostRepository PostRepository { get; set; }

		/// <summary>
		/// The repository for managing user data.
		/// مخزن مدیریت داده‌های کاربران.
		/// </summary>
		public IUserRepository UserRepository { get; set; }

		/// <summary>
		/// The repository for managing category data.
		/// مخزن مدیریت داده‌های دسته‌بندی‌ها.
		/// </summary>
		public ICategoryRepository CategoryRepository { get; set; }

		/// <summary>
		/// The repository for managing tag data.
		/// مخزن مدیریت داده‌های برچسب‌ها.
		/// </summary>
		public ITagRepository TagRepository { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PostManagement"/> class.
		/// یک نمونه جدید از کلاس PostManagement مقداردهی می‌کند.
		/// </summary>
		public PostManagement(
			IPostRepository postRepository,
			IUserRepository userRepository,
			ICategoryRepository categoryRepository,
			ITagRepository tagRepository)
		{
			this.PostRepository = postRepository;
			this.UserRepository = userRepository;
			this.CategoryRepository = categoryRepository;
			this.TagRepository = tagRepository;
		}

		/// <summary>
		/// Adds a new post to the system.
		/// یک پست جدید به سیستم اضافه می‌کند.
		/// </summary>
		/// <param name="postViewModel">
		/// The details of the post to add. / جزئیات پستی که باید اضافه شود.
		/// </param>
		/// <returns>
		/// A result indicating success or failure. / نتیجه‌ای که موفقیت یا شکست را نشان می‌دهد.
		/// </returns>
		public async Task<ResultViewModel> AddPost(PostViewModel postViewModel)
		{
			try
			{
				// Create a new Post entity from the view model.
				// یک موجودیت پست جدید از ViewModel ایجاد کنید.
				var post = new Post()
				{
                    AuthorId = postViewModel.AuthorId,
					CreatedAt = DateTime.Now,
					AbstractContent = postViewModel.AbstractContent,
					HtmlContent = postViewModel.HtmlContent,
					Title = postViewModel.Title,
					Tags = await TagRepository.GetAllByIdList(postViewModel.TagIdList),
					CategoryId = postViewModel.CategoryId
				};

				// Add the post to the repository.
				// پست را به مخزن اضافه کنید.
				await this.PostRepository.AddAsync(post);

				// Return a success result with the created post.
				// نتیجه موفقیت به همراه پست ایجاد شده را بازگردانید.
				return new ResultEntityViewModel<Post>
				{
					IsSuccessful = true,
					Entity = post
				};
			}
			catch (Exception ex)
			{
				// Return a failure result in case of an error.
				// در صورت خطا، نتیجه شکست بازگردانده شود.
				return new ResultEntityViewModel<Exception>
				{
					IsSuccessful = false,
					Exception = ex
				};
			}
		}

		/// <summary>
		/// Retrieves a paginated list of posts and their total count.
		/// یک لیست صفحه‌بندی‌شده از پست‌ها و تعداد کل آن‌ها را بازیابی می‌کند.
		/// </summary>
		/// <param name="page">The page number to retrieve. / شماره صفحه.</param>
		/// <param name="perPage">The number of posts per page. / تعداد پست‌ها در هر صفحه.</param>
		/// <returns>
		/// A tuple containing the list of posts and their total count. / یک تاپل شامل لیست پست‌ها و تعداد کل آن‌ها.
		/// </returns>
		public async Task<(List<PostListViewModel>, int)> ListPost(int page, int perPage)
		{
			// Retrieve the total count of posts.
			// تعداد کل پست‌ها را بازیابی کنید.
			int pageCount = await this.PostRepository.GetPostsCount();

			// Retrieve the paginated posts.
			// پست‌های صفحه‌بندی‌شده را بازیابی کنید.
			var posts = await this.PostRepository.GetPosts(page, perPage);

			// Map the posts to PostListViewModel.
			// پست‌ها را به PostListViewModel نگاشت کنید.
			return (posts.Select((p, index) => new PostListViewModel
			{
				Id = p.Id,
				Title = p.Title,
				AbstractContent = p.AbstractContent,
				AuthorName = p.Author?.FirstName + " " + p.Author?.LastName,
				CategoryName = p.Category?.Name,
				InsertationDateTime = p.CreatedAt,
				RowIndex = ((page - 1) * perPage) + index + 1
			}).ToList(), pageCount);
		}

		/// <summary>
		/// Deletes a post by its ID.
		/// یک پست را بر اساس شناسه آن حذف می‌کند.
		/// </summary>
		/// <param name="postId">The ID of the post to delete. / شناسه پستی که باید حذف شود.</param>
		/// <returns>
		/// A result indicating success or failure. / نتیجه‌ای که موفقیت یا شکست را نشان می‌دهد.
		/// </returns>
		public async Task<ResultViewModel> DeletePost(int postId)
		{
			try
			{
				// Retrieve the post by ID.
				// پست را بر اساس شناسه بازیابی کنید.
				var post = await this.PostRepository.GetByIdAsync(postId);
				if (post == null)
				{
					// Return a failure result if the post is not found.
					// اگر پست یافت نشد، نتیجه شکست بازگردانید.
					return new ResultEntityViewModel<string>
					{
						IsSuccessful = false,
						Entity = "پست یافت نشد!"
					};
				}

				// Delete the post from the repository.
				// پست را از مخزن حذف کنید.
				await this.PostRepository.DeleteAsync(postId);

				// Return a success result with the deleted post ID.
				// نتیجه موفقیت به همراه شناسه پست حذف شده را بازگردانید.
				return new ResultEntityViewModel<int>
				{
					IsSuccessful = true,
					Entity = postId,
					Message = "پست با موفقیت حذف شد."
				};
			}
			catch (Exception ex)
			{
				// Return a failure result in case of an error.
				// در صورت خطا، نتیجه شکست بازگردانده شود.
				return new ResultEntityViewModel<Exception>
				{
					IsSuccessful = false,
					Exception = ex
				};
			}
		}

		/// <summary>
		/// Retrieves a post by its ID.
		/// یک پست را بر اساس شناسه آن بازیابی می‌کند.
		/// </summary>
		/// <param name="postId">The ID of the post to retrieve. / شناسه پستی که باید بازیابی شود.</param>
		/// <returns>
		/// A result containing the post details or an error if not found. / نتیجه‌ای شامل جزئیات پست یا خطا در صورت عدم وجود.
		/// </returns>
		public async Task<ResultViewModel> GetPostById(int postId)
		{
			try
			{
				// Retrieve the post by ID.
				// پست را بر اساس شناسه بازیابی کنید.
				var post = await this.PostRepository.GetByIdAsync(postId);
				if (post == null)
				{
					// Return a failure result if the post is not found.
					// اگر پست یافت نشد، نتیجه شکست بازگردانید.
					return new ResultEntityViewModel<Exception>
					{
						IsSuccessful = false,
						Exception = new KeyNotFoundException("پست یافت نشد!"),
						Message = "پست یافت نشد!"
					};
				}

				// Map the post to PostViewModel.
				// پست را به PostViewModel نگاشت کنید.
				var postViewModel = new PostViewModel
				{
					AuthorName = post.Author.FirstName + " " + post.Author.LastName,
					Title = post.Title,
					AbstractContent = post.AbstractContent,
					CreatedAt = post.CreatedAt,
					HtmlContent = post.HtmlContent,
					TagIdList = post.Tags.Select(t => t.Id).ToList(),
					TagTextList = post.Tags.Select(t => t.Name).ToList(),
					CategoryId = post.CategoryId,
					CategoryName = post.Category?.Name ?? ""
				};

				// Return a success result with the post details.
				// نتیجه موفقیت به همراه جزئیات پست بازگردانده شود.
				return new ResultEntityViewModel<PostViewModel>
				{
					IsSuccessful = true,
					Entity = postViewModel
				};
			}
			catch (Exception ex)
			{
				// Return a failure result in case of an error.
				// در صورت خطا، نتیجه شکست بازگردانده شود.
				return new ResultEntityViewModel<Exception>
				{
					IsSuccessful = false,
					Exception = ex
				};
			}
		}

		/// <summary>
		/// Updates an existing post.
		/// یک پست موجود را به‌روزرسانی می‌کند.
		/// </summary>
		/// <param name="postViewModel">The updated details of the post. / جزئیات به‌روزرسانی‌شده پست.</param>
		/// <returns>
		/// A result indicating success or failure. / نتیجه‌ای که موفقیت یا شکست را نشان می‌دهد.
		/// </returns>
		public async Task<ResultViewModel> UpdatePost(PostViewModel postViewModel)
		{
			try
			{
				// Retrieve the post by ID.
				// پست را بر اساس شناسه بازیابی کنید.
				var post = await this.PostRepository.GetByIdAsync(postViewModel.Id);
				if (post == null)
				{
					// Return a failure result if the post is not found.
					// اگر پست یافت نشد، نتیجه شکست بازگردانید.
					return new ResultEntityViewModel<string>
					{
						IsSuccessful = false,
						Entity = "پست یافت نشد!"
					};
				}

				// Update the post details.
				// جزئیات پست را به‌روزرسانی کنید.
				post.Title = postViewModel.Title;
				post.AbstractContent = postViewModel.AbstractContent;
				post.HtmlContent = postViewModel.HtmlContent;
				post.Tags = await TagRepository.GetAllByIdList(postViewModel.TagIdList);

				// Update category if provided.
				// دسته‌بندی را به‌روزرسانی کنید (در صورت وجود).
				if (postViewModel.CategoryId.HasValue)
				{
					post.Category = await CategoryRepository.GetByIdAsync(postViewModel.CategoryId.Value);
				}
				else
				{
					post.Category = null;
				}

				// Update the post in the repository.
				// پست را در مخزن به‌روزرسانی کنید.
				await this.PostRepository.UpdateAsync(post);

				// Return a success result with the updated post.
				// نتیجه موفقیت به همراه پست به‌روزرسانی‌شده بازگردانده شود.
				return new ResultEntityViewModel<Post>
				{
					IsSuccessful = true,
					Entity = post
				};
			}
			catch (Exception ex)
			{
				// Return a failure result in case of an error.
				// در صورت خطا، نتیجه شکست بازگردانده شود.
				return new ResultEntityViewModel<Exception>
				{
					IsSuccessful = false,
					Exception = ex
				};
			}
		}
	}
}
