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
        Task<IQueryable<PlayerDbo>> All(
            PagingParameters pagingParameters,
            bool desc = true,
            params string[] navigationProperties);

        Task Insert(PlayerDbo playerDbo);
        void Delete(PlayerDbo playerDbo);
        Task<PlayerDbo> GetById(Guid id);
        Task SaveChangesAsync();
    }
}