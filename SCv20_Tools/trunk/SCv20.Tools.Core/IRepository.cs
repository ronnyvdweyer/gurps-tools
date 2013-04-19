using System;
using System.Linq;
using System.Linq.Expressions;
namespace SCv20.Tools.Core {
    public interface IRepository<T>where T : class {
        /// <summary>
        /// Gets an entity with the given primary key values. If no entity is found in the Repository or the Store, then null is returned.
        /// </summary>
        /// <param name="id">The value of the primary key for the entity to be found.</param>
        /// <returns>The entity found, or null.</returns>
        T GetById(object id);


        /// <summary>
        /// Returns an IQueryable instance for access to entities of the given type in the Repository.
        /// </summary>
        /// <returns>An IQueryable instance for the given entity type.</returns>
        IQueryable<T> FindAll();


        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns></returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);


        /// <summary>
        /// Create a new entity in Repository.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        T Create(T entity);


        /// <summary>
        /// Re-Atach an Un-Atached entiy to Repository.
        /// </summary>
        /// <param name="entity">The entity to be Re-Atached into the Repository</param>
        T Edit(T entity);


        /// <summary>
        /// Remove an entity from Repository.
        /// </summary>
        /// <param name="id">The value of the ID for the entity to be removed.</param>
        T Remove(int id);


        /// <summary>
        /// Commit all changes made into Repository.
        /// </summary>
        void Commit();
    }
}
