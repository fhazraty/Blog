using BLL.Model;

namespace BLL.Management
{
	public interface IPostManagement
	{
		Task<ResultViewModel> AddPost(PostViewModel postViewModel);
		Task<(List<PostListViewModel>, int)> ListPost(int page, int perPage);
		Task<ResultViewModel> DeletePost(int postId);
		Task<ResultViewModel> GetPostById(int postId);
		Task<ResultViewModel> UpdatePost(PostViewModel postViewModel);
	}
}
