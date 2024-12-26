using DAL.EF.Model;

namespace DAL.EF.Repository
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User?> FindByUsername(string username);
	}
}
