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
					.Include(u => u.Roles)
					.FirstOrDefaultAsync(u => u.Username == username);
		}
		public async Task<int> GetUsersCount()
		{
			return await this._dbSet.CountAsync();
		}
		public async Task<List<User>> GetUsers(int page, int perPage)
		{
			return await this._dbSet
				.Include(u => u.Roles)
				.Skip((page - 1) * perPage)
				.Take(perPage)
				.ToListAsync();
		}

	}
}
