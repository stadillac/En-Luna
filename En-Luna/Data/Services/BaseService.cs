using En_Luna.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace En_Luna.Data.Services
{
    /// <inheritdoc />
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected ApplicationContext _context;

        public BaseService(ApplicationContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public virtual T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        /// <inheritdoc />
        public virtual T Delete(T entity)
        {
            entity.IsDeleted = true;
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return entity;
        }

        /// <inheritdoc />
        public virtual T? Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual ICollection<T> List()
        {
            return _context.Set<T>().ToList();
        }

        /// <inheritdoc />
        public virtual ICollection<T> List(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }

        /// <inheritdoc />
        public virtual T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
