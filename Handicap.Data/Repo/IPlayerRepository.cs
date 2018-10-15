using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Handicap.Data.Paging;
using Handicap.Dbo;
using Handicap.Domain.Models;
using Handicap.Dto.Response.Paging;

namespace Handicap.Data.Repo
{
    public interface IPlayerRepository
    {
        Task<IQueryable<Player>> All(
            PagingParameters pagingParameters,
            bool desc = true,
            params string[] navigationProperties);

        Task Insert(Player player);
        void Delete(Player player);
        Task<Player> GetById(Guid id);
        Task SaveChangesAsync();
    }
}