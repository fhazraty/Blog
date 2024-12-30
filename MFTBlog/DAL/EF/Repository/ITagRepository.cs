using DAL.EF.Model;

namespace DAL.EF.Repository
{
	public interface ITagRepository : IRepository<Tag>
	{
		Task<List<Tag>> GetAllByIdList(List<int> listId);
	}
}
