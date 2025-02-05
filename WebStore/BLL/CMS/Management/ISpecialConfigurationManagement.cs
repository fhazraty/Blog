using BLL.CMS.Model;
using DAL.CMS.EF.Model;

namespace BLL.CMS.Management
{
	public interface ISpecialConfigurationManagement
	{
		Task<ResultViewModel> UpdateConfig(SpecialConfigurationViewModel specialConfigurationViewModel);
		Task<ResultEntityViewModel<SpecialConfiguration>> GetConfigById(int id);
		Task<List<SpecialConfigurationViewModel>> ListAllSpecialConfigurations();
	}
}
