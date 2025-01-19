using DAL.EF.Model;

namespace DAL.EF.Repository
{
	/// <summary>
	/// Defines a repository interface for managing Role entities.
	/// یک رابط مخزن برای مدیریت موجودیت‌های نقش تعریف می‌کند.
	/// </summary>
	public interface IRoleRepository : IRepository<Role>
	{
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
		Task<Role> GetRoleByNameAsync(string name);
	}
}
