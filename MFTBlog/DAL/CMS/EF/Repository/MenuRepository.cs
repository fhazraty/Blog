using DAL.CMS.EF;
using DAL.CMS.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.CMS.EF.Repository
{
	public class MenuRepository : Repository<Menu>, IMenuRepository
	{
		public MenuRepository(CMSContext context) : base(context)
		{
		}
	}
}
