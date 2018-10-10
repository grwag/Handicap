using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Handicap.Data.Paging;
using Handicap.Domain.Models;
using Handicap.Dto.Response.Paging;

namespace Handicap.Data.Repo
{
    public interface IPlayerRepository
    {
        Task<PagedList<Player>> FindAsync<P>(Expression<Func<Player, bool>> expression, Expression<Func<Player, P>> expressionProperty, PagingParameters pagingParameters, bool desc = true, params string[] navigationProperties);
        Task<Player> Insert(Player player);
        Task SaveChangesAsync();
    }
}