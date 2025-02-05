using DAL.CMS.EF;
using DAL.CMS.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.CMS.EF.Repository
{
	public class SpecialConfigurationRepository : Repository<SpecialConfiguration>, ISpecialConfigurationRepository
	{
		public SpecialConfigurationRepository(CMSContext context) : base(context)
		{
		}
	}
}
