using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
	public class MenuRepository : Repository<Menu>, IMenuRepository
	{
		public MenuRepository(DbContext context) : base(context)
		{
		}
	}
}
