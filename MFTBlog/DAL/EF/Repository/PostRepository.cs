using Azure;
using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
	public class PostRepository : Repository<Post>, IPostRepository
	{
		public PostRepository(BlogContext context) : base(context)
		{
		}

		public async Task<List<Post>> GetPosts(int page, int perpage)
		{
			return await _dbSet
                .AsNoTracking()
                .Include(p => p.Author)
				.Include(p => p.Category)
				.Include(p => p.Tags)
				.OrderByDescending(p => p.CreatedAt)
				.Skip((page - 1) * perpage)
				.Take(perpage)
				.ToListAsync();
		}

		public async Task<int> GetPostsCount()
		{
			return await _dbSet.AsNoTracking().CountAsync();
		}

		public async Task<Post?> GetByIdAsync(int id)
		{
			return await _dbSet
				.Include(p => p.Tags)
				.Include(p => p.Category)
				.Include(p => p.Author)
				.FirstOrDefaultAsync(p => p.Id == id);
		}
	}
}
