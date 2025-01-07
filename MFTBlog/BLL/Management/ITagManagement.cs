using BLL.Model;

namespace BLL.Management
{
	public interface ITagManagement
	{
		Task<ResultViewModel> AddTag(TagViewModel tagViewModel);
		Task<List<TagViewModel>> GetTags();
		Task<ResultViewModel> DeleteTag(int id);
	}
}
