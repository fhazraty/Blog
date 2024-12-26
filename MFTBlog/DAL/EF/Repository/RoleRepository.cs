using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
	public class RoleRepository : Repository<Role>, IRoleRepository
	{
		public RoleRepository(DbContext context) : base(context)
		{
		}

		public async Task<Role> GetRoleByNameAsync(string name)
		{
			return await this._dbSet.FirstOrDefaultAsync(x => x.Name == name);
		}
	}
}
