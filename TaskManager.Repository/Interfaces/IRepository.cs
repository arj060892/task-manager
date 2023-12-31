﻿namespace TaskManager.Repository.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides a generic interface for repository operations.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>A list of entities.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Retrieves an entity by its identifier.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        /// <returns>The entity, if found; otherwise, null.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The added entity.</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The updated entity.</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>Boolean status</returns>
        Task<bool> DeleteAsync(T entity);
    }
}
