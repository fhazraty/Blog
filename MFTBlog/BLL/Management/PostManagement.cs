using BLL.Model;
using DAL.EF.Model;
using DAL.EF.Repository;

namespace BLL.Management
{
	public class PostManagement : IPostManagement
	{
		public IPostRepository PostRepository { get; set; }
		public IUserRepository UserRepository { get; set; }
		public ICategoryRepository CategoryRepository { get; set; }
		public ITagRepository TagRepository { get; set; }
		public PostManagement(IPostRepository postRepository,
			IUserRepository userRepository,
			ICategoryRepository categoryRepository,
			ITagRepository tagRepository)
		{
			this.PostRepository = postRepository;
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
					Author = await UserRepository.GetByIdAsync(postViewModel.AuthorId.Value),
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

				await this.PostRepository.AddAsync(post);

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
			int pageCount = await this.PostRepository.GetPostsCount();
            var posts = await this.PostRepository.GetPosts(page, perPage);

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
				var post = await this.PostRepository.GetByIdAsync(postId);
				if (post == null)
				{
					return new ResultEntityViewModel<string>
					{
						IsSuccessful = false,
						Entity = "پست یافت نشد!"
					};
				}

				await this.PostRepository.DeleteAsync(postId);

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
				var post = await this.PostRepository.GetByIdAsync(postId);
				if (post == null)
				{
					return new ResultEntityViewModel<Exception>
					{
						IsSuccessful = false,
						Exception = new KeyNotFoundException("پست یافت نشد!"),
						Message = "پست یافت نشد!"
					};
				}

				if(post.Category is null)
				{
					post.Category = new Category() { Name = "" };
				}


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
					CategoryName = post.Category.Name
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
				var post = await this.PostRepository.GetByIdAsync(postViewModel.Id);
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
				post.Tags = await TagRepository.GetAllByIdList(postViewModel.TagIdList);

				if (postViewModel.CategoryId.HasValue)
				{
					post.Category = await CategoryRepository.GetByIdAsync(postViewModel.CategoryId.Value);
				}
				else
				{
					post.Category = null;
				}

				await this.PostRepository.UpdateAsync(post);

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
