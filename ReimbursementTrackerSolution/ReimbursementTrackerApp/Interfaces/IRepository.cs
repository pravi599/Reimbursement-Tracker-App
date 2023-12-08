namespace ReimbursementTrackerApp.Interfaces
{
    /// <summary>
    /// Generic repository interface for basic CRUD operations.
    /// </summary>
    /// <typeparam name="K">The type of the entity key.</typeparam>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public interface IRepository<K, T>
    {
        /// <summary>
        /// Gets an entity by its key.
        /// </summary>
        /// <param name="key">The key of the entity to retrieve.</param>
        /// <returns>The entity with the specified key, or null if not found.</returns>
        T GetById(K key);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>A list of all entities.</returns>
        IList<T> GetAll();

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The added entity.</returns>
        T Add(T entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The updated entity.</returns>
        T Update(T entity);

        /// <summary>
        /// Deletes an entity by its key.
        /// </summary>
        /// <param name="key">The key of the entity to delete.</param>
        /// <returns>The deleted entity.</returns>
        T Delete(K key);
    }
}