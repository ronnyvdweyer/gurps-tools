using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SCv20_Tools.Core.Data {

    public class Repository<T> : IRepository<T>, IDisposable where T : class {
        private static Repository<T> _instance;
        private DataContext _context;

        /// <summary>
        /// Prevents a default instance of the <see cref="Repository"/> class from being created.
        /// </summary>
        private Repository() {
            _context = DataContext.BuildContext();
        }

        /// <summary>
        /// Gets a Singleton Instance if the given repository.
        /// </summary>
        /// <returns></returns>
        public static IRepository<T> GetInstance() {
            lock (typeof(T)) {
                if (_instance == null)
                    _instance = new Repository<T>();

                return _instance;
            }
        }

        /// <summary>
        /// Gets an entity with the given primary key values. If no entity is found in the Repository or the Store, then null is returned.
        /// </summary>
        /// <param name="id">The value of the primary key for the entity to be found.</param>
        /// <returns>The entity found, or null.</returns>
        public virtual T GetById(object id) {
            var entity = this._context.Set<T>().Find(id);
            //this._context.Entry(entity).State = EntityState.Detached; // TODO: Check if not Implies in Hierarquical Read.
            return entity;
        }

        /// <summary>
        /// Returns an IQueryable instance for access to entities of the given type in the Repository.
        /// </summary>
        /// <returns>An IQueryable instance for the given entity type.</returns>
        public virtual IQueryable<T> FindAll() {
            IQueryable<T> query = this._context.Set<T>();
            return query;
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns></returns>
        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate) {
            IQueryable<T> query = this._context.Set<T>().Where(predicate);

            return query;
        }

        /// <summary>
        /// Create a new entity in Repository.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        public virtual T Create(T entity) {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this._context.Set<T>().Add(entity);

            return entity;
        }

        /// <summary>
        /// Re-Atach an Un-Atached entiy to Repository.
        /// </summary>
        /// <param name="entity">The entity to be Re-Atached into the Repository</param>
        public virtual T Edit(T entity) {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this._context.Entry(entity).State = System.Data.EntityState.Modified;

            //  http://stackoverflow.com/questions/5672255/an-object-with-the-same-key-already-exists-in-the-objectstatemanager-the-object
            //  this._context.Entry(entity).CurrentValues.SetValues(EntityState.Modified);

            //this._context.Entry(entity).State = EntityState.Detached;
            //this._context.Set<T>().Attach(entity);
            //this._context.Entry(entity).CurrentValues.SetValues(EntityState.Modified);

            //this._context.Entry(entity).State = EntityState.Detached;
            //this._context.Set<T>().Attach(entity);
            //this._context.Entry(entity).CurrentValues.SetValues(entity);

            return entity;
        }

        /// <summary>
        /// Remove an entity from Repository.
        /// </summary>
        /// <param name="id">The value of the ID for the entity to be removed.</param>
        public virtual T Remove(int id) {
            if (id == 0)
                throw new ArgumentNullException("id");

            var e = GetById(id);
            this._context.Set<T>().Remove(e);

            return e;
        }

        /// <summary>
        /// Commit all changes made into Repository.
        /// </summary>
        public virtual void Commit() {
            try {
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException ex) {
                var msg = BuildValidationMessage(ex);
                throw new DbEntityValidationException("Entity Validation Failed - Errors Follow in " + msg);
            }
        }

        /// <summary>
        /// Builds a friendly message for the given Repository Validation Erros.
        /// </summary>
        /// <param name="ex">The exception to be parsed into a friendly message.</param>
        /// <returns>String containing the parsed exception messages.</returns>
        private string BuildValidationMessage(DbEntityValidationException ex) {
            StringBuilder sb = new StringBuilder();
            foreach (var failure in ex.EntityValidationErrors) {
                sb.AppendFormat("'{0}' failed validation.\n", failure.Entry.Entity.GetType());
                foreach (var error in failure.ValidationErrors) {
                    sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Disposes the Repository. The underlying System.Data.Objects.ObjectContext is also disposed if it was
        /// created is by this Repository.
        /// </summary>
        public void Dispose() {
            if (_context != null) {
                _context.Dispose();
                _context = null;
            }
        }
    }
}