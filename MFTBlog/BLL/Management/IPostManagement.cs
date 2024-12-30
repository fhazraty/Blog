using BLL.Model;

namespace BLL.Management
{
	public interface IPostManagement
	{
		Task<ResultViewModel> AddPost(PostViewModel postViewModel);
	}
}
