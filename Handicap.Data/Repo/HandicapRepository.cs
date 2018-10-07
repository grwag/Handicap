using Handicap.Data.Exceptions;
using Handicap.Data.Infrastructure;
using Handicap.Data.Paging;
using Handicap.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Handicap.Data.Repo
{
    public class HandicapRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly HandicapContext _context;
        private readonly DbSet<T> _entities;

        public HandicapRepository(HandicapContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IList<T>> GetAllAsync(params string[] navigationProperties)
        {
            var query = _entities.AsQueryable();

            foreach (string navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);

            return await query
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> GetById(Guid id, params string[] navigationProperties)
        {
            //var query = _entities.AsQueryable();

            //foreach (string navigationProperty in navigationProperties)
            //    query = query.Include(navigationProperty);

            //var entity = await query.FirstOrDefaultAsync(e => e.Id == id);
            //if (entity == null)
            //{
            //    throw new EntityNotFoundException($"Entity with id: '{id}' not found.");
            //}

            //return entity;
            throw new NotImplementedException();
        }

        public T Insert(T entity)
        {
            if (_entities.Find(entity.Id) != null)
            {
                throw new EntityAlreadyExistsException($"Entity '{entity}' already exists.");
            }

            entity.DateCreated = DateTimeOffset.Now;
            _entities.Add(entity);
            return entity;
        }

        public void Update(T item)
        {
            //var entity = _entities.Find(item.Id);
            //if (entity == null)
            //{
            //    throw new EntityNotFoundException($"Entity with id: '{item.Id}' not found.");
            //}

            //_context.Entry(entity).CurrentValues.SetValues(item);
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            //if (_entities.Find(entity.Id) == null)
            //{
            //    throw new EntityNotFoundException($"Entity with id: '{entity.Id}' not found.");
            //}

            //_entities.Remove(entity);
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<T>> FindAsync<P>(
            Expression<Func<T, bool>> expression,
            Expression<Func<T, P>> expressionProperty,
            PagingParameters pagingParameters,
            bool desc = true,
            params string[] navigationProperties)
        {
            //var query = _entities.AsQueryable();
            //foreach (string navigationProperty in navigationProperties)
            //    query = query.Include(navigationProperty);

            //if (expression != null)
            //{
            //    query = query.Where(expression);
            //}

            //if (desc)
            //{
            //    query = query.OrderByDescending(expressionProperty);
            //}
            //else
            //{
            //    query = query.OrderBy(expressionProperty);
            //}

            //return PagedList<T>.Create(query, pagingParameters.PageNumber, pagingParameters.PageSize);
            throw new NotImplementedException();
        }

        Task<ICollection<T>> IRepository<T>.GetAllAsync(params string[] navigationProperties)
        {
            throw new NotImplementedException();
        }
    }
}
