/*
 * دستورات نصب اولیه EF Core
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.EntityFrameworkCore.Design
Install-package Microsoft.EntityFrameworkCore.Proxies

دستورات ساخت و اجرای Migration در Database
Add-Migration InitialCreate -Context "BlogContext"
Update-Database -Context "BlogContext"
Script-Migration -Context "BlogContext"
 */

using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
	/// <summary>
	/// Represents the database context for the blog system.
	/// نمایانگر کانتکست پایگاه داده برای سیستم وبلاگ.
	/// </summary>
	public class BlogContext : DbContext
	{
		/// <summary>
		/// Represents the users in the system. / کاربران سیستم.
		/// </summary>
		public DbSet<User> Users { get; set; }

		/// <summary>
		/// Represents the roles in the system. / نقش‌های سیستم.
		/// </summary>
		public DbSet<Role> Roles { get; set; }

		/// <summary>
		/// Represents the posts in the system. / پست‌های سیستم.
		/// </summary>
		public DbSet<Post> Posts { get; set; }

		/// <summary>
		/// Represents the tags in the system. / برچسب‌های سیستم.
		/// </summary>
		public DbSet<Tag> Tags { get; set; }

		/// <summary>
		/// Represents the categories in the system. / دسته‌بندی‌های سیستم.
		/// </summary>
		public DbSet<Category> Categories { get; set; }

		/// <summary>
		/// Represents the uploaded files in the system. / فایل‌های آپلود شده در سیستم.
		/// </summary>
		public DbSet<UploadedFile> UploadedFiles { get; set; }

		/// <summary>
		/// Represents the menus in the system. / منوهای سیستم.
		/// </summary>
		public DbSet<Menu> Menus { get; set; }

		/// <summary>
		/// Represents the special configurations in the system. / تنظیمات خاص سیستم.
		/// </summary>
		public DbSet<SpecialConfiguration> SpecialConfigurations { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="BlogContext"/> class. / سازنده پیش‌فرض کلاس BlogContext.
		/// </summary>
		public BlogContext() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="BlogContext"/> class with options. / سازنده کلاس BlogContext با گزینه‌ها.
		/// </summary>
		/// <param name="options">Options for configuring the context. / گزینه‌های تنظیم کانتکست.</param>
		public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

		/// <summary>
		/// Configures the database connection settings. / تنظیمات اتصال پایگاه داده.
		/// </summary>
		/// <param name="optionsBuilder">The options builder. / سازنده تنظیمات.</param>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=BlogDatabase;Integrated Security=True;Trust Server Certificate=True");
			base.OnConfiguring(optionsBuilder);
		}

		/// <summary>
		/// Configures the entity relationships and rules. / تنظیم روابط و قوانین موجودیت‌ها.
		/// </summary>
		/// <param name="modelBuilder">The model builder. / سازنده مدل‌ها.</param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// User and Roles: Many-to-Many relationship. / کاربر و نقش‌ها: رابطه چند-چند.
			modelBuilder.Entity<User>()
				.HasMany(u => u.Roles)
				.WithMany(r => r.Users);

			// Username must be unique. / نام کاربری باید یکتا باشد.
			modelBuilder.Entity<User>()
				.HasIndex(u => u.Username)
				.IsUnique();

			// Seeding roles. / مقداردهی اولیه نقش‌ها.
			modelBuilder.Entity<Role>().HasData(
				new Role() { Id = 1, Name = "Writer" },
				new Role() { Id = 2, Name = "Admin" }
			);

			// NationalCode must be unique. / کد ملی باید یکتا باشد.
			modelBuilder.Entity<User>()
				.HasIndex(u => u.NationalCode)
				.IsUnique();

			// Post and Author: One-to-Many relationship. / پست و نویسنده: رابطه یک-چند.
			modelBuilder.Entity<Post>()
				.HasOne(p => p.Author)
				.WithMany(u => u.Posts)
				.HasForeignKey(p => p.AuthorId);

			// Post and Category: One-to-Many relationship. / پست و دسته‌بندی: رابطه یک-چند.
			modelBuilder.Entity<Post>()
				.HasOne(p => p.Category)
				.WithMany(c => c.Posts)
				.HasForeignKey(p => p.CategoryId);

			// Post and Tags: Many-to-Many relationship. / پست و برچسب‌ها: رابطه چند-چند.
			modelBuilder.Entity<Post>()
				.HasMany(p => p.Tags)
				.WithMany(t => t.Posts);

			// Category and Subcategories: Parent-Child relationship. / دسته‌بندی و زیرمجموعه‌ها: رابطه والد-فرزند.
			modelBuilder.Entity<Category>()
				.HasOne(c => c.ParentCategory)
				.WithMany(c => c.SubCategories)
				.HasForeignKey(c => c.ParentCategoryId);

			// Menu and Submenus: Parent-Child relationship. / منو و زیرمنوها: رابطه والد-فرزند.
			modelBuilder.Entity<Menu>()
				.HasOne(m => m.ParentMenu)
				.WithMany(m => m.SubMenus)
				.HasForeignKey(m => m.ParentMenuId);

			// SpecialConfiguration: Primary key. / تنظیمات خاص سایت وبلاگ: کلید اصلی.
			modelBuilder.Entity<SpecialConfiguration>()
				.HasKey(sc => sc.Id);

			modelBuilder.Entity<SpecialConfiguration>().HasData(
				new SpecialConfiguration { Id = 1, Name = "تصویر صفحه اول شماره 1", Value = "../../Writer/ViewFile?id=1" },
				new SpecialConfiguration { Id = 2, Name = "تصویر صفحه اول شماره 2", Value = "../../Writer/ViewFile?id=2" },
				new SpecialConfiguration { Id = 3, Name = "تصویر صفحه اول شماره 3", Value = "../../Writer/ViewFile?id=3" },
				new SpecialConfiguration { Id = 4, Name = "اسم وبلاگ", Value = "وبلاگ من" },
				new SpecialConfiguration { Id = 5, Name = "متن وسط بالا", Value = "به وبلاگ من خوش آمدید" },
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
