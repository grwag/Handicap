using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Handicap.Data.Paging;
using Handicap.Domain.Models;
using Handicap.Dto.Response.Paging;

namespace Handicap.Application.Services
{
    public interface IPlayerService
    {
        Task<Player> InsertPlayer(Player player);
        Task<PagedList<Player>> FindAsync(Expression<Func<Player, bool>> expression, PagingParameters pagingParameters);
    }
}