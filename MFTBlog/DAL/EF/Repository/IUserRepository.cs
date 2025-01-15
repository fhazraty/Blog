using DAL.EF.Model;

namespace DAL.EF.Repository
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User?> FindByUsername(string username);
		Task<int> GetUsersCount();
		Task<List<User>> GetUsers(int page, int perPage);
	}
}


