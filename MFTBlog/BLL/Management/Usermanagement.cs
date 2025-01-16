using BLL.Model;
using BLL.Utility;
using DAL.EF.Model;
using DAL.EF.Repository;
using Microsoft.EntityFrameworkCore;

namespace BLL.Management
{
	public class UserManagement : IUserManagement
	{
		private DbContext _context;
		public IUserRepository UserRepository1 { get; set; }
		public IUserRepository UserRepository2 { get; set; }
		public IRoleRepository RoleRepository { get; set; }
		public UserManagement(IUserRepository userRepository1, IUserRepository userRepository2, IRoleRepository roleRepository)
		{
			UserRepository1 = userRepository1;
			UserRepository2 = userRepository2;
			RoleRepository = roleRepository;
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

				await this.UserRepository1.AddAsync(usr);

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
				var user = await this.UserRepository1
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
		public async Task<(List<UserListViewModel>, int)> ListUsers(int page, int perPage)
		{
			var getPageCountTask = this.UserRepository1.GetUsersCount();
			var getUsersTask = this.UserRepository2.GetUsers(page, perPage);

			await Task.WhenAll(getPageCountTask, getUsersTask);

			int pageCount = await getPageCountTask;
			var users = await getUsersTask;

			return (users.Select((u, index) => new UserListViewModel
			{
				Id = u.Id,
				Username = u.Username,
				FirstName = u.FirstName,
				LastName = u.LastName,
				NationalCode = u.NationalCode,
				BirthDate = u.BirthDate,
				RowIndex = ((page - 1) * perPage) + index + 1
			}).ToList(), pageCount);
		}
		public async Task<ResultViewModel> DeleteUserById(int userId)
		{
			try
			{
				var user = await this.UserRepository1.GetByIdAsync(userId);
				if (user == null)
				{
					return new ResultEntityViewModel<User>
					{
						IsSuccessful = false,
						Exception = new Exception("User not found")
					};
				}

				await this.UserRepository1.DeleteAsync(userId);

				return new ResultEntityViewModel<int>
				{
					IsSuccessful = true,
					Entity = userId
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
