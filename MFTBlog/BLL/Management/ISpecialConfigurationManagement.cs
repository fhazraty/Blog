using BLL.Model;

namespace BLL.Management
{
    public interface ISpecialConfigurationManagement
    {
        Task<ResultViewModel> UpdateConfig(SpecialConfigurationViewModel specialConfigurationViewModel);
    }
}
