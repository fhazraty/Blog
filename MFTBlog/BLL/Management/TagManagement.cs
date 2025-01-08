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
					Name = tagViewModel.Name
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
				Id = tag.Id,
				Name = tag.Name
			}).ToList();
		}
		public async Task<ResultViewModel> DeleteTag(int id)
		{
			try
			{
				var tag = await TagRepository.GetByIdAsync(id);
				if (tag == null)
				{
					return new ResultEntityViewModel<int>()
					{
						Entity = id,
						IsSuccessful = false,
						Message = "کلید یافت نشد",
						Exception = new KeyNotFoundException()
					};
				}

				if (tag.Posts != null && tag.Posts.Any())
				{
					return new ResultEntityViewModel<int>()
					{
						Entity = id,
						IsSuccessful = false,
						Message = "یک یا چند پست وابسته به این تگ وجود دارد!",
						Exception = new Exception("Tag has related posts and cannot be deleted")
					};
				}

				await TagRepository.DeleteAsync(id);
				return new ResultEntityViewModel<int>()
				{
					Entity = id,
					IsSuccessful = true
				};
			}
			catch (Exception ex)
			{
				return new ResultEntityViewModel<int>()
				{
					Entity = id,
					Exception = ex,
					IsSuccessful = false
				};
			}
		}
		public async Task<ResultViewModel> UpdateTag(TagViewModel tagViewModel)
		{
			try
			{
				var tag = await TagRepository.GetByIdAsync(tagViewModel.Id);
				if (tag == null)
				{
					return new ResultEntityViewModel<int>()
					{
						Entity = tagViewModel.Id,
						IsSuccessful = false,
						Message = "کلید یافت نشد",
						Exception = new KeyNotFoundException()
					};
				}

				tag.Name = tagViewModel.Name;
				await TagRepository.UpdateAsync(tag);

				return new ResultEntityViewModel<Tag>()
				{
					Entity = tag,
					IsSuccessful = true
				};
			}
			catch (Exception ex)
			{
				return new ResultEntityViewModel<Tag>()
				{
					Exception = ex,
					IsSuccessful = false
				};
			}
		}
	}
}
