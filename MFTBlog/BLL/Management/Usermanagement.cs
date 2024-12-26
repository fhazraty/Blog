using BLL.Model;
using BLL.Utility;
using DAL.EF;
using DAL.EF.Model;
using DAL.EF.Repository;
using Microsoft.EntityFrameworkCore;

namespace BLL.Management
{
	public class Usermanagement : IUserManagement
	{
		private DbContext _context;
		public IUserRepository UserRepository { get; set; }
		public IRoleRepository RoleRepository { get; set; }
		public Usermanagement()
		{
			_context = new BlogContext();

			this.UserRepository = new UserRepository(_context);
			this.RoleRepository = new RoleRepository(_context);
		}
		public async Task<ResultViewModel> AddUser(UserViewModel userViewModel)
		{
			try
			{
				string salt = Guid.NewGuid().ToString();

				var usr = new User
				{
					Username = userViewModel.Username,
					PasswordHash = Security.HashPasswordSHA3(userViewModel.Password, salt),
					Salt = salt,
					BirthDate = userViewModel.BirthDate,
					FirstName = userViewModel.FirstName,
					LastName = userViewModel.LastName,
					NationalCode = userViewModel.NationalCode,
					PasswordCreatedAt = DateTime.Now,
					PasswordUpdatedAt = DateTime.Now
				};

				foreach (var role in userViewModel.Roles)
				{
					var roleEntity = await this.RoleRepository.GetRoleByNameAsync(role.Name);
					usr.Roles.Add(roleEntity);
				}

				await this.UserRepository.AddAsync(usr);

				return new ResultEntityViewModel<User>
				{
					IsSuccessful = true,
					Entity = usr
				};
			}
			catch (Exception ex)
			{
				return new ResultEntityViewModel<Exception>
				{
					IsSuccessful = false,
					Exception = ex
				};
			}
		}

		public async Task<ResultViewModel> FindUser(UserViewModel userViewModel)
		{
			try
			{
				var user = await this.UserRepository
					.FindByUsername(userViewModel.Username);

				if (user == null)
				{
					return new ResultEntityViewModel<User>
					{
						IsSuccessful = false,
						Exception = new Exception("User not found")
					};
				}

				if (user.PasswordHash != Security.HashPasswordSHA3(userViewModel.Password, user.Salt))
				{
					return new ResultEntityViewModel<User>
					{
						IsSuccessful = false,
						Exception = new Exception("is incorrect")
					};
				}

				return new ResultEntityViewModel<User>
				{
					IsSuccessful = true,
					Entity = user
				};
			}
			catch (Exception ex)
			{
				return new ResultEntityViewModel<Exception>
				{
					IsSuccessful = false,
					Exception = ex
				};
			}
		}
	}
}
