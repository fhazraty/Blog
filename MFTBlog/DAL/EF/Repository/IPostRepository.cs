using DAL.EF.Model;

namespace DAL.EF.Repository
{
	public interface IPostRepository : IRepository<Post>
	{
		Task<List<Post>> GetPosts(int page, int perpage);
		Task<int> GetPostsCount();
	}
}
