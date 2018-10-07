using Handicap.Data.Paging;
using Handicap.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Handicap.Data.Repo
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<ICollection<T>> GetAllAsync(params string[] navigationProperties);
        Task<T> GetById(Guid id, params string[] navigationProperties);
        T Insert(T item);
        void Update(T item);
        void Delete(T item);
        Task SaveChangesAsync();
        Task<ICollection<T>> FindAsync<P>(
            Expression<Func<T, bool>> expression,
            Expression<Func<T, P>> expressionProperty,
            PagingParameters pagingParameters,
            bool desc = true,
            params string[] navigationProperties
            );
    }
}
