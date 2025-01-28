using BLL.Model;
using DAL.EF.Model;

namespace BLL.Management
{
    public interface ISpecialConfigurationManagement
    {
        Task<ResultViewModel> UpdateConfig(SpecialConfigurationViewModel specialConfigurationViewModel);
        Task<ResultEntityViewModel<SpecialConfiguration>> GetConfigById(int id);
        Task<List<SpecialConfigurationViewModel>> ListAllSpecialConfigurations();
    }
}
