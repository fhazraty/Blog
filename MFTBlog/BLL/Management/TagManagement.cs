using BLL.Model;
using DAL.EF.Model;
using DAL.EF.Repository;

namespace BLL.Management
{
	public class TagManagement : ITagManagement
	{
		public ITagRepository TagRepository { get; set; }
		public TagManagement(ITagRepository tagRepository)
		{
			TagRepository = tagRepository;
		}
		public async Task<ResultViewModel> AddTag(TagViewModel tagViewModel)
		{
			try
			{
				var tag = new Tag
				{
					Name = tagViewModel.TagName
				};
				
				await TagRepository.AddAsync(tag);

				return new ResultEntityViewModel<Tag>()
				{
					Entity = tag,
					IsSuccessful = true
				};
			}
			catch (Exception ex)
			{
				return new ResultEntityViewModel<Tag>(){
					Exception = ex,
					IsSuccessful = false
				};
			}
		}

		public async Task<List<TagViewModel>> GetTags()
		{
			var tags = await TagRepository.GetAllAsync();
			return tags.Select(tag => new TagViewModel
			{
				TagName = tag.Name
			}).ToList();
		}
	}
}
