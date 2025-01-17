using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		public CategoryRepository(BlogContext context) : base(context)
		{
		}
		public async Task<Category?> GetByIdAsync(int id)
		{
			return 
				await 
				_dbSet
				.Include(c => c.Posts)
				.Include(c => c.SubCategories)
				.FirstOrDefaultAsync(c => c.Id == id);
		}
	}
}
