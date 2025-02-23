﻿using BLL.CMS.Model;
using BLL.Utility;
using DAL.CMS.EF.Model;
using DAL.CMS.EF.Repository;

namespace BLL.CMS.Management
{
	/// <summary>
	/// Provides management operations for user entities.
	/// عملیات مدیریتی برای موجودیت‌های کاربر را فراهم می‌کند.
	/// </summary>
	public class UserManagement : IUserManagement
	{
		/// <summary>
		/// Gets or sets the user repository.
		/// مخزن کاربران.
		/// </summary>
		public IUserRepository UserRepository { get; set; }
		/// <summary>
		/// Gets or sets the role repository.
		/// مخزن نقش‌ها.
		/// </summary>
		public IRoleRepository RoleRepository { get; set; }
		/// <summary>
		/// Initializes a new instance of the <see cref="UserManagement"/> class.
		/// یک نمونه جدید از کلاس UserManagement را مقداردهی می‌کند.
		/// </summary>
		/// <param name="userRepository">The user repository. / مخزن کاربران.</param>
		/// <param name="roleRepository">The role repository. / مخزن نقش‌ها.</param>
		public UserManagement(IUserRepository userRepository, IRoleRepository roleRepository)
		{
			UserRepository = userRepository;
			RoleRepository = roleRepository;
		}
		/// <summary>
		/// Adds a new user with the provided details.
		/// یک کاربر جدید را با جزئیات ارائه شده اضافه می‌کند.
		/// </summary>
		/// <param name="userViewModel">
		/// The user details to add. / جزئیات کاربر برای اضافه کردن.
		/// </param>
		/// <returns>
		/// A result indicating success or failure. / نتیجه‌ای که موفقیت یا شکست را نشان می‌دهد.
		/// </returns>
		public async Task<ResultViewModel> AddUser(UserViewModel userViewModel)
		{
			try
			{
				// Generate a salt for hashing the password.
				// ایجاد یک salt برای هش کردن رمز عبور.
				string salt = Guid.NewGuid().ToString();

				// Create a User instance from UserViewModel.
				// ایجاد یک نمونه کاربر از UserViewModel.
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
					PasswordUpdatedAt = DateTime.Now,
					UserRoles = new List<UserRole>()
				};

				// Add roles to the user.
				// افزودن نقش‌ها به کاربر.
				foreach (var role in userViewModel.Roles)
				{
					// Retrieve the role entity by name from the database.
					// بازیابی نقش از پایگاه داده بر اساس نام.
					var roleEntity = await RoleRepository.GetRoleByNameAsync(role.Name);

					// Add the role to the user's role collection.
					// افزودن نقش به لیست نقش‌های کاربر.
					usr.UserRoles.Add(new UserRole { User = usr, Role = roleEntity });
				}

				// Add the user to the database.
				// افزودن کاربر به پایگاه داده.
				await UserRepository.AddAsync(usr);

				// Return success result.
				// بازگرداندن نتیجه موفقیت.
				return new ResultEntityViewModel<User>
				{
					IsSuccessful = true,
					Entity = usr
				};
			}
			catch (Exception ex)
			{
				// Return failure result in case of an exception.
				// بازگرداندن نتیجه شکست در صورت بروز خطا.
				return new ResultEntityViewModel<Exception>
				{
					IsSuccessful = false,
					Exception = ex
				};
			}
		}
		/// <summary>
		/// Finds a user by their username and password.
		/// یک کاربر را بر اساس نام کاربری و رمز عبور پیدا می‌کند.
		/// </summary>
		/// <param name="userViewModel">
		/// The user details for authentication. / جزئیات کاربر برای احراز هویت.
		/// </param>
		/// <returns>
		/// A result indicating success or failure. / نتیجه‌ای که موفقیت یا شکست را نشان می‌دهد.
		/// </returns>
		public async Task<ResultViewModel> FindUser(UserViewModel userViewModel)
		{
			try
			{
				// Find the user by username.
				// جستجوی کاربر بر اساس نام کاربری.
				var user = await
					
					UserRepository
					.FindByUsername(userViewModel.Username);

				// Check if the user exists.
				// بررسی وجود کاربر.
				if (user == null)
				{
					// Return failure if the user is not found.
					// بازگرداندن نتیجه شکست در صورت عدم یافتن کاربر.
					return new ResultEntityViewModel<User>
					{
						IsSuccessful = false,
						Exception = new Exception("User not found")
					};
				}

				// Verify the password.
				// بررسی تطابق رمز عبور.
				if (user.PasswordHash != Security.HashPasswordSHA3(userViewModel.Password, user.Salt))
				{
					// Return failure if the password is incorrect.
					// بازگرداندن نتیجه شکست در صورت تطابق نداشتن رمز عبور.
					return new ResultEntityViewModel<User>
					{
						IsSuccessful = false,
						Exception = new Exception("Incorrect password")
					};
				}

				// Return success result with the user entity.
				// بازگرداندن نتیجه موفقیت به همراه کاربر.
				return new ResultEntityViewModel<User>
				{
					IsSuccessful = true,
					Entity = user
				};
			}
			catch (Exception ex)
			{
				// Return failure result in case of an exception.
				// بازگرداندن نتیجه شکست در صورت بروز خطا.
				return new ResultEntityViewModel<Exception>
				{
					IsSuccessful = false,
					Exception = ex
				};
			}
		}
		/// <summary>
		/// Retrieves a paginated list of users and the total count.
		/// یک لیست صفحه‌بندی‌شده از کاربران و تعداد کل آن‌ها را بازیابی می‌کند.
		/// </summary>
		/// <param name="page">The page number. / شماره صفحه.</param>
		/// <param name="perPage">The number of users per page. / تعداد کاربران در هر صفحه.</param>
		/// <returns>
		/// A tuple containing the list of users and the total count. / یک تاپل شامل لیست کاربران و تعداد کل.
		/// </returns>
		public async Task<(List<UserListViewModel>, int)> ListUsers(int page, int perPage)
		{
			// Retrieve the total count of users.
			// بازیابی تعداد کل کاربران.
			int pageCount = await UserRepository.GetUsersCount();

			// Retrieve users for the current page.
			// بازیابی کاربران برای صفحه جاری.
			var users = await UserRepository.GetUsers(page, perPage);

			// Convert users to UserListViewModel.
			// تبدیل کاربران به UserListViewModel.
			return (users.Select((u, index) => new UserListViewModel
			{
				Id = u.Id,
				Username = u.Username,
				FirstName = u.FirstName,
				LastName = u.LastName,
				NationalCode = u.NationalCode,
				BirthDate = u.BirthDate,
				RowIndex = (page - 1) * perPage + index + 1
			}).ToList(), pageCount);
		}
		/// <summary>
		/// Deletes a user by their ID.
		/// یک کاربر را بر اساس شناسه حذف می‌کند.
		/// </summary>
		/// <param name="userId">The ID of the user to delete. / شناسه کاربر برای حذف.</param>
		/// <returns>
		/// A result indicating success or failure. / نتیجه‌ای که موفقیت یا شکست را نشان می‌دهد.
		/// </returns>
		public async Task<ResultViewModel> DeleteUserById(int userId)
		{
			try
			{
				// Retrieve the user by ID.
				// جستجوی کاربر بر اساس شناسه.
				var user = await UserRepository.GetByIdAsync(userId);

				// Check if the user exists.
				// بررسی وجود کاربر.
				if (user == null)
				{
					// Return failure if the user is not found.
					// بازگرداندن نتیجه شکست در صورت عدم وجود کاربر.
					return new ResultEntityViewModel<User>
					{
						IsSuccessful = false,
						Exception = new Exception("User not found")
					};
				}

				// Delete the user.
				// حذف کاربر.
				await UserRepository.DeleteAsync(userId);

				// Return success result.
				// بازگرداندن نتیجه موفقیت.
				return new ResultEntityViewModel<int>
				{
					IsSuccessful = true,
					Entity = userId
				};
			}
			catch (Exception ex)
			{
				// Return failure result in case of an exception.
				// بازگرداندن نتیجه شکست در صورت بروز خطا.
				return new ResultEntityViewModel<Exception>
				{
					IsSuccessful = false,
					Exception = ex
				};
			}
		}
		/// <summary>
		/// Retrieves a user by their ID.
		/// یک کاربر را بر اساس شناسه بازیابی می‌کند.
		/// </summary>
		/// <param name="userId">The ID of the user to retrieve. / شناسه کاربر برای بازیابی.</param>
		/// <returns>
		/// A result containing the user if found or an error if not. / نتیجه‌ای که شامل کاربر در صورت یافتن یا خطا در غیر این صورت است.
		/// </returns>
		public async Task<ResultViewModel> GetUserById(int userId)
		{
			try
			{
				// Retrieve the user by ID.
				// جستجوی کاربر بر اساس شناسه.
				var user = await UserRepository.GetByIdAsync(userId);

				// Check if the user exists.
				// بررسی وجود کاربر.
				if (user == null)
				{
					// Return failure if the user is not found.
					// بازگرداندن نتیجه شکست در صورت عدم وجود کاربر.
					return new ResultEntityViewModel<User>
					{
						IsSuccessful = false,
						Exception = new Exception("User not found")
					};
				}

				// Return success result with the user entity.
				// بازگرداندن نتیجه موفقیت به همراه کاربر.
				return new ResultEntityViewModel<UserListViewModel>
				{
					IsSuccessful = true,
					Entity = new UserListViewModel()
					{
						Id = user.Id,
						FirstName = user.FirstName,
						LastName = user.LastName,
						NationalCode = user.NationalCode,
						Username = user.Username,
						Password = "",
						BirthDate = user.BirthDate,
						Roles = user.UserRoles.Select(ur => ur.Role).ToList()
					}
				};
			}
			catch (Exception ex)
			{
				// Return failure result in case of an exception.
				// بازگرداندن نتیجه شکست در صورت بروز خطا.
				return new ResultEntityViewModel<Exception>
				{
					IsSuccessful = false,
					Exception = ex
				};
			}
		}
		/// <summary>
		/// Updates an existing user with the provided details.
		/// یک کاربر موجود را با جزئیات ارائه شده به‌روزرسانی می‌کند.
		/// </summary>
		/// <param name="userViewModel">
		/// The user details to update. / جزئیات کاربر برای به‌روزرسانی.
		/// </param>
		/// <returns>
		/// A result indicating success or failure. / نتیجه‌ای که موفقیت یا شکست را نشان می‌دهد.
		/// </returns>
		public async Task<ResultViewModel> UpdateUser(UserViewModel userViewModel)
		{
			try
			{
				// Retrieve the user by ID.
				// جستجوی کاربر بر اساس شناسه.
				var user = await UserRepository.GetByIdAsync(userViewModel.Id);

				// Check if the user exists.
				// بررسی وجود کاربر.
				if (user == null)
				{
					// Return failure if the user is not found.
					// بازگرداندن نتیجه شکست در صورت عدم وجود کاربر.
					return new ResultEntityViewModel<User>
					{
						IsSuccessful = false,
						Exception = new Exception("User not found")
					};
				}

				// Update user details.
				// به‌روزرسانی جزئیات کاربر.
				user.FirstName = userViewModel.FirstName;
				user.LastName = userViewModel.LastName;
				user.NationalCode = userViewModel.NationalCode;
				user.Username = userViewModel.Username;
				user.BirthDate = userViewModel.BirthDate;

				// Update password if provided.
				// به‌روزرسانی رمز عبور در صورت ارائه.
				if (!string.IsNullOrEmpty(userViewModel.Password))
				{
					string salt = Guid.NewGuid().ToString();
					user.PasswordHash = Security.HashPasswordSHA3(userViewModel.Password, salt);
					user.Salt = salt;
					user.PasswordUpdatedAt = DateTime.Now;
				}

				// Update the user in the database.
				// به‌روزرسانی کاربر در پایگاه داده.
				await UserRepository.UpdateAsync(user);

				// Return success result.
				// بازگرداندن نتیجه موفقیت.
				return new ResultEntityViewModel<User>
				{
					IsSuccessful = true,
					Entity = user
				};
			}
			catch (Exception ex)
			{
				// Return failure result in case of an exception.
				// بازگرداندن نتیجه شکست در صورت بروز خطا.
				return new ResultEntityViewModel<Exception>
				{
					IsSuccessful = false,
					Exception = ex
				};
			}
		}
		/// <summary>
		/// Retrieves a list of all roles.
		/// یک لیست از تمام نقش‌ها را بازیابی می‌کند.
		/// </summary>
		/// <returns>
		/// A list of roles. / لیستی از نقش‌ها.
		/// </returns>
		public async Task<List<Role>> ListRoles()
		{
			try
			{
				// Retrieve all roles from the database.
				// بازیابی تمام نقش‌ها از پایگاه داده.
				var roles = await RoleRepository.GetAllAsync();

				// Return the list of roles.
				// بازگرداندن لیست نقش‌ها.
				return roles.ToList();
			}
			catch (Exception ex)
			{
				// Log the exception and rethrow it.
				// ثبت خطا و پرتاب مجدد آن.
				throw new Exception("An error occurred while retrieving roles.", ex);
			}
		}
		/// <summary>
		/// Updates the roles of an existing user.
		/// نقش‌های یک کاربر موجود را به‌روزرسانی می‌کند.
		/// </summary>
		/// <param name="userId">The ID of the user to update. / شناسه کاربر برای به‌روزرسانی.</param>
		/// <param name="roles">The new roles to assign to the user. / نقش‌های جدید برای اختصاص به کاربر.</param>
		/// <returns>
		/// A result indicating success or failure. / نتیجه‌ای که موفقیت یا شکست را نشان می‌دهد.
		/// </returns>
		public async Task<ResultViewModel> UpdateUserRole(int userId, List<int> roles)
		{
			try
			{
				// Retrieve the user by ID.
				// جستجوی کاربر بر اساس شناسه.
				var user = await UserRepository.GetByIdAsync(userId);

				// Check if the user exists.
				// بررسی وجود کاربر.
				if (user == null)
				{
					// Return failure if the user is not found.
					// بازگرداندن نتیجه شکست در صورت عدم وجود کاربر.
					return new ResultEntityViewModel<User>
					{
						IsSuccessful = false,
						Exception = new KeyNotFoundException(),
						Message = "کاربر یافت نشد!"
					};
				}

				// Clear existing roles.
				// پاک کردن نقش‌های موجود.
				user.UserRoles.Clear();

				// Add new roles to the user.
				// افزودن نقش‌های جدید به کاربر.
				foreach (var roleId in roles)
				{
					// Retrieve the role entity by name from the database.
					// بازیابی نقش از پایگاه داده بر اساس نام.
					var roleEntity = await RoleRepository.GetByIdAsync(roleId);

					// Add the role to the user's role collection.
					// افزودن نقش به لیست نقش‌های کاربر.
					user.UserRoles.Add(new UserRole { User = user, Role = roleEntity });
				}

				// Update the user in the database.
				// به‌روزرسانی کاربر در پایگاه داده.
				await UserRepository.UpdateAsync(user);

				// Return success result.
				// بازگرداندن نتیجه موفقیت.
				return new ResultEntityViewModel<User>
				{
					IsSuccessful = true,
					Entity = user
				};
			}
			catch (Exception ex)
			{
				// Return failure result in case of an exception.
				// بازگرداندن نتیجه شکست در صورت بروز خطا.
				return new ResultEntityViewModel<Exception>
				{
					IsSuccessful = false,
					Exception = ex
				};
			}
		}
	}
}
