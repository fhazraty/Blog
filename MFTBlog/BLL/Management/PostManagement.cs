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
		public async Task<(List<PostListViewModel>,int)> ListPost(int page, int perPage)
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
				AuthorName = p.Author?.FirstName + " " + p.Author?.LastName,
				CategoryName = p.Category?.Name,
				InsertationDateTime = p.CreatedAt,
				RowIndex = ((page-1) * perPage) + index + 1
			}).ToList(), pageCount);
		}
	}
}
