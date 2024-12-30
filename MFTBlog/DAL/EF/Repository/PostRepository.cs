using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
	public class PostRepository : Repository<Post>, IPostRepository
	{
		public PostRepository(DbContext context) : base(context)
		{
		}
	}
}
