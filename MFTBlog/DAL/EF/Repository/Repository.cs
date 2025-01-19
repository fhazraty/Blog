using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repository
{
	/// <summary>
	/// A generic repository that provides CRUD operations for entities.
	/// یک مخزن عمومی که عملیات CRUD را برای موجودیت‌ها فراهم می‌کند.
	/// </summary>
	/// <typeparam name="T">
	/// The type of the entity. / نوع موجودیت.
	/// </typeparam>
	public abstract class Repository<T> : IRepository<T> where T : class
	{
		/// <summary>
		/// The database context used by the repository.
		/// کانتکست پایگاه داده که توسط مخزن استفاده می‌شود.
		/// </summary>
		protected readonly DbContext _context;

		/// <summary>
		/// The DbSet representing the collection of the entity.
		/// مجموعه DbSet که نشان‌دهنده مجموعه‌ای از موجودیت‌ها است.
		/// </summary>
		protected readonly DbSet<T> _dbSet;

		/// <summary>
		/// Initializes a new instance of the <see cref="Repository{T}"/> class.
		/// یک نمونه جدید از کلاس Repository را مقداردهی می‌کند.
		/// </summary>
		/// <param name="context">The database context. / کانتکست پایگاه داده.</param>
		public Repository(DbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		/// <summary>
		/// Retrieves all entities from the database asynchronously.
		/// همه موجودیت‌ها را به صورت غیرهمزمان از پایگاه داده بازیابی می‌کند.
		/// </summary>
		/// <returns>
		/// A collection of entities. / مجموعه‌ای از موجودیت‌ها.
		/// </returns>
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		/// <summary>
		/// Retrieves a specific entity by its ID asynchronously.
		/// یک موجودیت خاص را بر اساس شناسه آن به صورت غیرهمزمان بازیابی می‌کند.
		/// </summary>
		/// <param name="id">The ID of the entity. / شناسه موجودیت.</param>
		/// <returns>
		/// The entity if found; otherwise, null. / موجودیت در صورت یافتن؛ در غیر این صورت null.
		/// </returns>
		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		/// <summary>
		/// Adds a new entity to the database asynchronously.
		/// یک موجودیت جدید را به صورت غیرهمزمان به پایگاه داده اضافه می‌کند.
		/// </summary>
		/// <param name="entity">The entity to add. / موجودیت برای افزودن.</param>
		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Updates an existing entity in the database asynchronously.
		/// یک موجودیت موجود را به صورت غیرهمزمان در پایگاه داده به‌روزرسانی می‌کند.
		/// </summary>
		/// <param name="entity">The entity to update. / موجودیت برای به‌روزرسانی.</param>
		public async Task UpdateAsync(T entity)
		{
			_dbSet.Update(entity);
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// Deletes an entity by its ID asynchronously.
		/// یک موجودیت را بر اساس شناسه آن به صورت غیرهمزمان حذف می‌کند.
		/// </summary>
		/// <param name="id">The ID of the entity to delete. / شناسه موجودیت برای حذف.</param>
		public async Task DeleteAsync(int id)
		{
			var entity = await GetByIdAsync(id);
			if (entity != null)
			{
				_dbSet.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}
	}
}
