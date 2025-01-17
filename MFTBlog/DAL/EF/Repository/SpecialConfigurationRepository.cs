using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
    public class SpecialConfigurationRepository : Repository<SpecialConfiguration>, ISpecialConfigurationRepository
    {
        public SpecialConfigurationRepository(BlogContext context) : base(context)
        {
        }
    }
}
