using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
	public class MenuRepository : Repository<Menu>, IMenuRepository
	{
		public MenuRepository(BlogContext context) : base(context)
		{
		}
	}
}
