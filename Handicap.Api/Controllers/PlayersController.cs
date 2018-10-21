using AutoMapper;
using AutoMapper.QueryableExtensions;
using Handicap.Application.Services;
using Handicap.Data.Paging;
using Handicap.Domain.Models;
using Handicap.Dto.Request;
using Handicap.Dto.Response;
using Handicap.Dto.Response.Paging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Handicap.Api.Controllers
{
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public PlayersController(IPlayerService playerService,
            IGameService gameService,
            IMapper mapper)
        {
            _playerService = playerService;
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(PagingParameters pagingParameters)
        {
            var players = await _playerService.All();

            var pagedResponse = PagedList<PlayerResponse>.Create(
                _mapper.Map<IEnumerable<PlayerResponse>>(players).AsQueryable(),
                pagingParameters.PageNumber,
                pagingParameters.PageSize);

            return Ok(pagedResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var player = await _playerService.GetById(id);

            return Ok(_mapper.Map<PlayerResponse>(player));
        }

        [HttpGet("{id}/games")]
        public async Task<IActionResult> GetGamesForPlayer(Guid id, PagingParameters pagingParameters)
        {
            var games = await _gameService.GetGamesForPlayer(id);

            var pagedResponse = PagedList<GameResponse>.Create(
                _mapper.Map<IEnumerable<GameResponse>>(games).AsQueryable(),
                pagingParameters.PageNumber,
                pagingParameters.PageSize);

            return Ok(pagedResponse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _playerService.Delete(id);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody]PlayerRequest playerRequest)
        {
            var player = _mapper.Map<Player>(playerRequest);
            player = await _playerService.InsertPlayer(player);

            var playerResponse = _mapper.Map<PlayerResponse>(player);

            return CreatedAtAction(
                nameof(GetById),
                new { id = playerResponse.Id },
                playerResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(Guid id, [FromBody]PlayerRequest playerRequest)
        {
            var player = _mapper.Map<Player>(playerRequest);
            player.Id = id;

            player = await _playerService.Update(player);

            return Ok(_mapper.Map<PlayerResponse>(player));
        }
    }
}
