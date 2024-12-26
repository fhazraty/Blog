using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(DbContext context) : base(context)
		{
		}
		public async Task<User?> FindByUsername(string username)
		{
			return
				await
				this
					._dbSet
					.FirstOrDefaultAsync(u => u.Username == username);
		}
	}
}
