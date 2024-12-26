using DAL.EF.Model;

namespace DAL.EF.Repository
{
	public interface IRoleRepository : IRepository<Role>
	{
		Task<Role> GetRoleByNameAsync(string name);
	}
}
