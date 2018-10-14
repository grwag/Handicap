using Handicap.Data.Repo;
using Handicap.Domain.Models;
using System.Threading.Tasks;
using Handicap.Data.Paging;
using Handicap.Data.Exceptions;
using System.Linq;
using System;
using Handicap.Dto.Response.Paging;
using System.Linq.Expressions;
using AutoMapper;
using Handicap.Dbo;
using System.Collections.Generic;

namespace Handicap.Application.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public PlayerService(IPlayerRepository playerRepository,
            IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        public async Task<Player> InsertPlayer(Player player)
        {
            var playerDbo = _mapper.Map<PlayerDbo>(player);

            await _playerRepository.Insert(playerDbo);

            return _mapper.Map<Player>(playerDbo);
        }

        public async Task<Player> GetById(Guid id)
        {
            var playerDbo = await _playerRepository.GetById(id);

            if (playerDbo == null)
            {
                throw new EntityNotFoundException($"Player with id {id} does not exist.");
            }

            return _mapper.Map<Player>(playerDbo);
        }

        public async Task Delete(Guid id)
        {
            var playerDbo = await _playerRepository.GetById(id);

            if (playerDbo == null)
            {
                throw new EntityNotFoundException($"Player with id {id} does not exist.");
            }

            _playerRepository.Delete(playerDbo);
            await _playerRepository.SaveChangesAsync();
        }

        public async Task<PagedList<Player>> All(PagingParameters pagingParameters)
        {
            var result = await _playerRepository.All(
                pagingParameters,
                false);

            var query = _mapper.Map<IEnumerable<Player>>(result).AsQueryable();
            return PagedList<Player>.Create(query, pagingParameters.PageNumber, pagingParameters.PageSize);
        }
    }
}
