using BLL.Model;

namespace BLL.Management
{
	public interface IUserManagement
	{
		Task<ResultViewModel> AddUser(UserViewModel userViewModel);
		Task<ResultViewModel> FindUser(UserViewModel userViewModel);
		Task<(List<UserListViewModel>, int)> ListUsers(int page, int perPage);
	}
}
