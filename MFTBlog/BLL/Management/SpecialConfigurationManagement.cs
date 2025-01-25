using BLL.Model;
using DAL.EF.Model;
using DAL.EF.Repository;

namespace BLL.Management
{
    public class SpecialConfigurationManagement : ISpecialConfigurationManagement
    {
        public ISpecialConfigurationRepository SpecialConfigurationRepository { get; set; }
        public SpecialConfigurationManagement(ISpecialConfigurationRepository specialConfigurationRepository)
        {
                this.SpecialConfigurationRepository = specialConfigurationRepository;
        }
        public async Task<ResultViewModel> UpdateConfig(SpecialConfigurationViewModel specialConfigurationViewModel)
        {
            try
            {
                var config = await SpecialConfigurationRepository.GetByIdAsync(specialConfigurationViewModel.Id);

                if (config == null)
                {
                    return new ResultEntityViewModel<SpecialConfiguration>()
                    {
                        Exception = new KeyNotFoundException(),
                        IsSuccessful = false,
                        Message = "تنظیم یافت نشد!"
                    };
                }

                config.Value = specialConfigurationViewModel.Value;

                await this.SpecialConfigurationRepository.UpdateAsync(config);

                return new ResultEntityViewModel<SpecialConfiguration>()
                {
                    Entity = config,
                    IsSuccessful = true,
                    Message = "بروز رسانی با موفقیت انجام شد!"
                };
            }
            catch (Exception ex)
            {
                return new ResultEntityViewModel<SpecialConfiguration>()
                {
                    Exception = ex,
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }
		public async Task<ResultEntityViewModel<SpecialConfiguration>> GetConfigById(int id)
		{
			try
			{
				var config = await SpecialConfigurationRepository.GetByIdAsync(id);

				if (config == null)
				{
					return new ResultEntityViewModel<SpecialConfiguration>()
					{
						Exception = new KeyNotFoundException(),
						IsSuccessful = false,
						Message = "تنظیم یافت نشد!"
					};
				}

				return new ResultEntityViewModel<SpecialConfiguration>()
				{
					Entity = config,
					IsSuccessful = true
				};
			}
			catch (Exception ex)
			{
				return new ResultEntityViewModel<SpecialConfiguration>()
				{
					Exception = ex,
					IsSuccessful = false,
					Message = ex.Message
				};
			}
		}

    }
}
