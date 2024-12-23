/*
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.EntityFrameworkCore.Design

Add-Migration InitialCreate -Context "BlogContext"
Update-Database -Context "BlogContext"
Script-Migration -Context "BlogContext"
 */

using DAL.EF.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
	public class BlogContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<UploadedFile> UploadedFiles { get; set; }
		public DbSet<Menu> Menus { get; set; }
		public BlogContext()
		{
		}

		public BlogContext(DbContextOptions<BlogContext> options)
			: base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=BlogDatabase;Integrated Security=True;Trust Server Certificate=True");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasMany(u => u.Roles)
				.WithMany(r => r.Users);

			modelBuilder.Entity<Post>()
				.HasOne(p => p.Author)
				.WithMany(u => u.Posts)
				.HasForeignKey(p => p.AuthorId);

			modelBuilder.Entity<Post>()
				.HasOne(p => p.Category)
				.WithMany(c => c.Posts)
				.HasForeignKey(p => p.CategoryId);

			modelBuilder.Entity<Post>()
				.HasMany(p => p.Tags)
				.WithMany(t => t.Posts);

			modelBuilder.Entity<Category>()
				.HasOne(c => c.ParentCategory)
				.WithMany(c => c.SubCategories)
				.HasForeignKey(c => c.ParentCategoryId);

			modelBuilder.Entity<Menu>()
				.HasOne(m => m.ParentMenu)
				.WithMany(m => m.SubMenus)
				.HasForeignKey(m => m.ParentMenuId);

			base.OnModelCreating(modelBuilder);
		}
	}
}
