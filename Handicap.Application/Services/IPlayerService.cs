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
        Task<Player> GetById(Guid id);
        Task Delete(Guid id);
        Task<PagedList<Player>> All(PagingParameters pagingParameters);
    }
}