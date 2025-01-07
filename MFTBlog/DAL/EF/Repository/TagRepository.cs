using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
	public class TagRepository : Repository<Tag>, ITagRepository
	{
		public TagRepository(DbContext context) : base(context)
		{
		}

		public async Task<List<Tag>> GetAllByIdList(List<int> listId)
		{
			return await this._dbSet.Where(t => listId.Contains(t.Id)).ToListAsync();
		}

		public async Task<Tag?> GetByIdAsync(int id)
		{
			return _dbSet.
				Include(t => t.Posts)
				.FirstOrDefault(t => t.Id == id);
		}
	}
}
