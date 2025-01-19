namespace DAL.EF.Repository
{
	/// <summary>
	/// Defines a generic repository interface for CRUD operations.
	/// یک رابط عمومی برای عملیات CRUD تعریف می‌کند.
	/// </summary>
	/// <typeparam name="T">
	/// The type of the entity. / نوع موجودیت.
	/// </typeparam>
	public interface IRepository<T> where T : class
	{
		/// <summary>
		/// Retrieves all entities from the database asynchronously.
		/// همه موجودیت‌ها را به صورت غیرهمزمان از پایگاه داده بازیابی می‌کند.
		/// </summary>
		/// <returns>
		/// A collection of entities. / مجموعه‌ای از موجودیت‌ها.
		/// </returns>
		Task<IEnumerable<T>> GetAllAsync();

		/// <summary>
		/// Retrieves a specific entity by its ID asynchronously.
		/// یک موجودیت خاص را بر اساس شناسه آن به صورت غیرهمزمان بازیابی می‌کند.
		/// </summary>
		/// <param name="id">The ID of the entity. / شناسه موجودیت.</param>
		/// <returns>
		/// The entity if found; otherwise, null. / موجودیت در صورت یافتن؛ در غیر این صورت null.
		/// </returns>
		Task<T> GetByIdAsync(int id);

		/// <summary>
		/// Adds a new entity to the database asynchronously.
		/// یک موجودیت جدید را به صورت غیرهمزمان به پایگاه داده اضافه می‌کند.
		/// </summary>
		/// <param name="entity">The entity to add. / موجودیت برای افزودن.</param>
		Task AddAsync(T entity);

		/// <summary>
		/// Updates an existing entity in the database asynchronously.
		/// یک موجودیت موجود را به صورت غیرهمزمان در پایگاه داده به‌روزرسانی می‌کند.
		/// </summary>
		/// <param name="entity">The entity to update. / موجودیت برای به‌روزرسانی.</param>
		Task UpdateAsync(T entity);

		/// <summary>
		/// Deletes an entity by its ID asynchronously.
		/// یک موجودیت را بر اساس شناسه آن به صورت غیرهمزمان حذف می‌کند.
		/// </summary>
		/// <param name="id">The ID of the entity to delete. / شناسه موجودیت برای حذف.</param>
		Task DeleteAsync(int id);
	}
}
