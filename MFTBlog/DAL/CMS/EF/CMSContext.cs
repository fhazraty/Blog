/*
 * Initial EF Core setup commands
 * دستورات نصب اولیه EF Core:
 * Install-Package Microsoft.EntityFrameworkCore.SqlServer
 * Install-Package Microsoft.EntityFrameworkCore.Tools
 * Install-Package Microsoft.EntityFrameworkCore.Design
 * Install-Package Microsoft.EntityFrameworkCore.Proxies
 *
 * Commands for creating and applying migrations:
 * دستورات ساخت و اجرای Migration در پایگاه داده:
 * Add-Migration InitialCreate -Context "CMSContext"
 * Update-Database -Context "CMSContext"
 * Script-Migration -Context "CMSContext"
 */

using DAL.CMS.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.CMS.EF
{
	/// <summary>
	/// Represents the database context for the CMS system.
	/// نمایانگر کانتکست پایگاه داده برای سیستم مدیریت محتوا.
	/// </summary>
	public class CMSContext : DbContext
	{
		/// <summary>
		/// Represents the users in the CMS system.
		/// نمایانگر کاربران موجود در سیستم مدیریت محتوا.
		/// </summary>
		public DbSet<User> Users { get; set; }

		/// <summary>
		/// Represents the roles in the CMS system.
		/// نمایانگر نقش‌ها در سیستم مدیریت محتوا.
		/// </summary>
		public DbSet<Role> Roles { get; set; }

		/// <summary>
		/// Represents the posts in the CMS system.
		/// نمایانگر پست‌ها در سیستم مدیریت محتوا.
		/// </summary>
		public DbSet<Post> Posts { get; set; }

		/// <summary>
		/// Represents the tags in the CMS system.
		/// نمایانگر برچسب‌ها در سیستم مدیریت محتوا.
		/// </summary>
		public DbSet<Tag> Tags { get; set; }

		/// <summary>
		/// Represents the categories in the CMS system.
		/// نمایانگر دسته‌بندی‌ها در سیستم مدیریت محتوا.
		/// </summary>
		public DbSet<Category> Categories { get; set; }

		/// <summary>
		/// Represents the uploaded files in the CMS system.
		/// نمایانگر فایل‌های آپلود شده در سیستم مدیریت محتوا.
		/// </summary>
		public DbSet<UploadedFile> UploadedFiles { get; set; }

		/// <summary>
		/// Represents the menus in the CMS system.
		/// نمایانگر منوها در سیستم مدیریت محتوا.
		/// </summary>
		public DbSet<Menu> Menus { get; set; }

		/// <summary>
		/// Represents the special configurations for the CMS system.
		/// نمایانگر تنظیمات خاص سیستم مدیریت محتوا.
		/// </summary>
		public DbSet<SpecialConfiguration> SpecialConfigurations { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CMSContext"/> class without options.
		/// سازنده پیش‌فرض کلاس CMSContext.
		/// </summary>
		public CMSContext() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="CMSContext"/> class with options.
		/// سازنده کلاس CMSContext با گزینه‌های تنظیمی.
		/// </summary>
		/// <param name="options">Options for configuring the database context.</param>
		/// <param name="options">گزینه‌های تنظیمی برای کانتکست پایگاه داده.</param>
		public CMSContext(DbContextOptions<CMSContext> options) : base(options) { }

		/// <summary>
		/// Configures the database connection settings.
		/// تنظیمات اتصال به پایگاه داده.
		/// </summary>
		/// <param name="optionsBuilder">The options builder for configuring the context.</param>
		/// <param name="optionsBuilder">سازنده گزینه‌ها برای تنظیم کانتکست.</param>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// Connection string to SQL Server database
			// رشته اتصال به پایگاه داده SQL Server
			optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=CMSDatabase;Integrated Security=True;Trust Server Certificate=True");
			base.OnConfiguring(optionsBuilder);
		}

		/// <summary>
		/// Configures the entity relationships, keys, and seed data for the database.
		/// تنظیم روابط، کلیدها و مقداردهی اولیه داده‌ها برای پایگاه داده.
		/// </summary>
		/// <param name="modelBuilder">The model builder for configuring entities.</param>
		/// <param name="modelBuilder">سازنده مدل برای تنظیم موجودیت‌ها.</param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// UserRole: Configuring the join table for User and Role
			// UserRole: تنظیم جدول واسط برای کاربران و نقش‌ها
			modelBuilder.Entity<UserRole>()
				.HasKey(ur => new { ur.UserId, ur.RoleId }); // Composite key / کلید ترکیبی

			modelBuilder.Entity<UserRole>()
				.HasOne(ur => ur.User)
				.WithMany(u => u.UserRoles)
				.HasForeignKey(ur => ur.UserId); // Foreign key to User / کلید خارجی به User

			modelBuilder.Entity<UserRole>()
				.HasOne(ur => ur.Role)
				.WithMany(r => r.UserRoles)
				.HasForeignKey(ur => ur.RoleId); // Foreign key to Role / کلید خارجی به Role

			// Unique constraint for Username
			// ایجاد محدودیت یکتا برای نام کاربری
			modelBuilder.Entity<User>()
				.HasIndex(u => u.Username)
				.IsUnique();

			// Seeding data for Role
			// مقداردهی اولیه داده‌ها برای نقش‌ها
			modelBuilder.Entity<Role>().HasData(
				new Role { Id = 1, Name = "Writer" },
				new Role { Id = 2, Name = "Admin" }
			);

			// Seeding data for User
			// مقداردهی اولیه داده‌ها برای کاربران
			modelBuilder.Entity<User>().HasData(
				new User
				{
					Id = 1,
					FirstName = "ادمین",
					LastName = "ادمین",
					Username = "admin",
					PasswordHash = "5fdf8fa7bf2a50747cb60d871a29cb9e0f6c3d60f34b94f2b4af3b41ee9a3fa2b3c68f30ec57a67e65d8b995b1366847cd5bb9246c788b4ae94db637660cc4d9",
					Salt = "c6173eda-beb4-4d4a-a719-b80e38ba3f06",
					NationalCode = "1234567890",
					BirthDate = new DateTime(1990, 1, 1),
					PasswordCreatedAt = new DateTime(2025, 1, 1),
					PasswordUpdatedAt = new DateTime(2025, 1, 1)
				}
			);

			// Seeding data for UserRole (join table)
			// مقداردهی اولیه داده‌ها برای جدول واسط UserRole
			modelBuilder.Entity<UserRole>().HasData(
				new UserRole { UserId = 1, RoleId = 2 } // Assigning Admin role to the admin user
														// انتساب نقش Admin به کاربر ادمین
			);

			// Unique constraint for NationalCode
			// ایجاد محدودیت یکتا برای کد ملی
			modelBuilder.Entity<User>()
				.HasIndex(u => u.NationalCode)
				.IsUnique();

			// Post and Author: One-to-Many relationship
			// رابطه یک-چند بین پست‌ها و نویسنده‌ها
			modelBuilder.Entity<Post>()
				.HasOne(p => p.Author)
				.WithMany(u => u.Posts)
				.HasForeignKey(p => p.AuthorId);

			// Post and Category: One-to-Many relationship
			// رابطه یک-چند بین پست‌ها و دسته‌بندی‌ها
			modelBuilder.Entity<Post>()
				.HasOne(p => p.Category)
				.WithMany(c => c.Posts)
				.HasForeignKey(p => p.CategoryId);

			// Post and Tags: Many-to-Many relationship
			// رابطه چند-چند بین پست‌ها و برچسب‌ها
			modelBuilder.Entity<Post>()
				.HasMany(p => p.Tags)
				.WithMany(t => t.Posts);

			// Category and Subcategories: Parent-Child relationship
			// رابطه والد-فرزند بین دسته‌بندی‌ها و زیرمجموعه‌ها
			modelBuilder.Entity<Category>()
				.HasOne(c => c.ParentCategory)
				.WithMany(c => c.SubCategories)
				.HasForeignKey(c => c.ParentCategoryId);

			// Menu and Submenus: Parent-Child relationship
			// رابطه والد-فرزند بین منوها و زیرمنوها
			modelBuilder.Entity<Menu>()
				.HasOne(m => m.ParentMenu)
				.WithMany(m => m.SubMenus)
				.HasForeignKey(m => m.ParentMenuId);

			// SpecialConfiguration: Seed data for configuration settings
			// مقداردهی اولیه برای تنظیمات خاص سیستم مدیریت محتوا
			modelBuilder.Entity<SpecialConfiguration>().HasData(
				new SpecialConfiguration { Id = 1, Name = "تصویر صفحه اول شماره 1", Value = "../../Writer/ViewFile?id=1" },
				new SpecialConfiguration { Id = 2, Name = "تصویر صفحه اول شماره 2", Value = "../../Writer/ViewFile?id=2" },
				new SpecialConfiguration { Id = 3, Name = "تصویر صفحه اول شماره 3", Value = "../../Writer/ViewFile?id=3" },
				new SpecialConfiguration { Id = 4, Name = "اسم سیستم مدیریت محتوا", Value = "سیستم مدیریت محتوا من" },
				new SpecialConfiguration { Id = 5, Name = "متن وسط بالا", Value = "به سیستم مدیریت محتوا من خوش آمدید" },
				new SpecialConfiguration { Id = 6, Name = "توضیحات متن وسط بالا زیر نوشته", Value = "محلی برای اشتراک افکار، ایده ها و داستانها." },
				new SpecialConfiguration { Id = 7, Name = "عنوان تصویر 1", Value = "عنوان تصویر 1" },
				new SpecialConfiguration { Id = 8, Name = "عنوان تصویر 2", Value = "عنوان تصویر 2" },
				new SpecialConfiguration { Id = 9, Name = "عنوان تصویر 3", Value = "عنوان تصویر 3" },
				new SpecialConfiguration { Id = 10, Name = "توضیح تصویر 1", Value = "توضیح تصویر 1" },
				new SpecialConfiguration { Id = 11, Name = "توضیح تصویر 2", Value = "توضیح تصویر 2" },
				new SpecialConfiguration { Id = 12, Name = "توضیح تصویر 3", Value = "توضیح تصویر 3" },
				new SpecialConfiguration { Id = 13, Name = "لینک تصویر1", Value = "#" },
				new SpecialConfiguration { Id = 14, Name = "لینک تصویر2", Value = "#" },
				new SpecialConfiguration { Id = 15, Name = "لینک تصویر3", Value = "#" }
			);

			base.OnModelCreating(modelBuilder);
		}
	}
}
