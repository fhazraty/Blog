using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
	/// <summary>
	/// Implements repository methods for managing Role entities.
	/// پیاده‌سازی روش‌های مخزن برای مدیریت موجودیت‌های نقش.
	/// </summary>
	public class RoleRepository : Repository<Role>, IRoleRepository
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RoleRepository"/> class.
		/// یک نمونه جدید از کلاس RoleRepository را مقداردهی می‌کند.
		/// </summary>
		/// <param name="context">The database context. / کانتکست پایگاه داده.</param>
		public RoleRepository(BlogContext context) : base(context)
		{
		}

		/// <summary>
		/// Retrieves a role by its name asynchronously.
		/// یک نقش را بر اساس نام آن به صورت غیرهمزمان بازیابی می‌کند.
		/// </summary>
		/// <param name="name">
		/// The name of the role to retrieve. / نام نقش برای بازیابی.
		/// </param>
		/// <returns>
		/// The role if found; otherwise, null. / نقش در صورت یافتن؛ در غیر این صورت null.
		/// </returns>
		public async Task<Role> GetRoleByNameAsync(string name)
		{
			return await this._dbSet.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
		}
	}
}
