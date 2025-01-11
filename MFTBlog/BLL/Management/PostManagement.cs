using BLL.Model;
using DAL.EF.Model;
using DAL.EF.Repository;

namespace BLL.Management
{
	public class PostManagement : IPostManagement
	{
		public IPostRepository PostRepository1 { get; set; }
		public IPostRepository PostRepository2 { get; set; }
		public IUserRepository UserRepository { get; set; }
		public ICategoryRepository CategoryRepository { get; set; }
		public ITagRepository TagRepository { get; set; }
		public PostManagement(IPostRepository postRepository1,
			IPostRepository postRepository2,
			IUserRepository userRepository,
			ICategoryRepository categoryRepository,
			ITagRepository tagRepository)
		{
			this.PostRepository1 = postRepository1;
			this.PostRepository2 = postRepository2;
			this.UserRepository = userRepository;
			this.CategoryRepository = categoryRepository;
			this.TagRepository = tagRepository;
		}
		public async Task<ResultViewModel> AddPost(PostViewModel postViewModel)
		{
			try
			{
				var post = new Post()
				{
					Author = await UserRepository.GetByIdAsync(postViewModel.AuthorId),
					CreatedAt = DateTime.Now,
					AbstractContent = postViewModel.AbstractContent,
					HtmlContent = postViewModel.HtmlContent,
					Title = postViewModel.Title,
					Tags = await TagRepository.GetAllByIdList(postViewModel.TagIdList)
				};

				if (postViewModel.CategoryId.HasValue)
				{
					post.Category = await CategoryRepository.GetByIdAsync(postViewModel.CategoryId.Value);
				}

				await this.PostRepository1.AddAsync(post);

				return new ResultEntityViewModel<Post>
				{
					IsSuccessful = true,
					Entity = post
				};
			}
			catch (Exception ex)
			{
				return new ResultEntityViewModel<Exception>
				{
					IsSuccessful = false,
					Exception = ex
				};
			}
		}
		public async Task<(List<PostListViewModel>, int)> ListPost(int page, int perPage)
		{
			var getPageCountTask = this.PostRepository1.GetPostsCount();
			var getPostsTask = this.PostRepository2.GetPosts(page, perPage);

			await Task.WhenAll(getPageCountTask, getPostsTask);

			int pageCount = await getPageCountTask;
			var posts = await getPostsTask;

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
		public async Task<ResultViewModel> DeletePost(int postId)
		{
			try
			{
				var post = await this.PostRepository1.GetByIdAsync(postId);
				if (post == null)
				{
					return new ResultEntityViewModel<string>
					{
						IsSuccessful = false,
						Entity = "پست یافت نشد!"
					};
				}

				await this.PostRepository1.DeleteAsync(postId);

				return new ResultEntityViewModel<int>
				{
					IsSuccessful = true,
					Entity = postId,
					Message = "پست با موفقیت حذف شد."
				};
			}
			catch (Exception ex)
			{
				return new ResultEntityViewModel<Exception>
				{
					IsSuccessful = false,
					Exception = ex
				};
			}
		}
		public async Task<ResultViewModel> GetPostById(int postId)
		{
			try
			{
				var post = await this.PostRepository1.GetByIdAsync(postId);
				if (post == null)
				{
					return new ResultEntityViewModel<Exception>
					{
						IsSuccessful = false,
						Exception = new KeyNotFoundException("پست یافت نشد!"),
						Message = "پست یافت نشد!"
					};
				}

				var postViewModel = new PostViewModel
				{
					AuthorId = post.AuthorId,
					Title = post.Title,
					AbstractContent = post.AbstractContent,	
					HtmlContent = post.HtmlContent,
					TagIdList = post.Tags.Select(t => t.Id).ToList(),
					CategoryId = post.CategoryId
				};

				return new ResultEntityViewModel<PostViewModel>
				{
					IsSuccessful = true,
					Entity = postViewModel
				};
			}
			catch (Exception ex)
			{
				return new ResultEntityViewModel<Exception>
				{
					IsSuccessful = false,
					Exception = ex
				};
			}
		}
		public async Task<ResultViewModel> UpdatePost(PostViewModel postViewModel)
		{
			try
			{
				var post = await this.PostRepository1.GetByIdAsync(postViewModel.Id);
				if (post == null)
				{
					return new ResultEntityViewModel<string>
					{
						IsSuccessful = false,
						Entity = "پست یافت نشد!"
					};
				}

				post.Title = postViewModel.Title;
				post.AbstractContent = postViewModel.AbstractContent;
				post.HtmlContent = postViewModel.HtmlContent;
				post.Author = await UserRepository.GetByIdAsync(postViewModel.AuthorId);
				post.Tags = await TagRepository.GetAllByIdList(postViewModel.TagIdList);

				if (postViewModel.CategoryId.HasValue)
				{
					post.Category = await CategoryRepository.GetByIdAsync(postViewModel.CategoryId.Value);
				}
				else
				{
					post.Category = null;
				}

				await this.PostRepository1.UpdateAsync(post);

				return new ResultEntityViewModel<Post>
				{
					IsSuccessful = true,
					Entity = post
				};
			}
			catch (Exception ex)
			{
				return new ResultEntityViewModel<Exception>
				{
					IsSuccessful = false,
					Exception = ex
				};
			}
		}
	}
}
