using DAL.EF.Model;

namespace DAL.EF.Repository
{
	public interface IUploadedFileRepository : IRepository<UploadedFile>
	{
		Task<int> GetFilesCount();
		Task<List<UploadedFile>> GetFiles(int page, int perPage);
		Task<UploadedFile?> GetByIdAsync(int id);
	}
}
