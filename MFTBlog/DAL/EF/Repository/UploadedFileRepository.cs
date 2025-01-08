using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
	public class UploadedFileRepository : Repository<UploadedFile>, IUploadedFileRepository
	{
		public UploadedFileRepository(DbContext context) : base(context)
		{
		}
		public async Task<int> GetFilesCount()
		{
			return await _dbSet.CountAsync();
		}
		public async Task<List<UploadedFile>> GetFiles(int page, int perPage)
		{
			return await _dbSet
				.OrderByDescending(p => p.UploadedAt)
				.Skip((page - 1) * perPage)
				.Take(perPage)
				.ToListAsync();
		}
		public async Task<UploadedFile?> GetByIdAsync(int id)
		{
			return await _dbSet.FirstOrDefaultAsync(p => p.Id == id);
		}
	}
}
