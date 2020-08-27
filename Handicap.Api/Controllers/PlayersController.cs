using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Handicap.Api.Paging;
using Handicap.Application.Exceptions;
using Handicap.Application.Interfaces;
using Handicap.Domain.Models;
using Handicap.Dto.Request;
using Handicap.Dto.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Handicap.Api.Extensions;
using Handicap.Application.Services;
using Microsoft.AspNetCore.Http;

namespace Handicap.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize("read_write")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly IGameService _gameService;
        private readonly ILogger<PlayersController> _logger;
        private readonly IMapper _mapper;

        public PlayersController(
            IPlayerService playerService,
            IGameService gameService,
            ILogger<PlayersController> logger,
            IMapper mapper)
        {
            _playerService = playerService;
            _gameService = gameService;
            _logger = logger;
            _mapper = mapper;
        }

        #region CRUD
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery]string orderBy = "LastName",
            [FromQuery]bool desc = false,
            [FromQuery]int pageSize = 10,
            [FromQuery]int page = 0
            )
        {
            var tenantId = this.GetTenantId();

            var query = await _playerService.Find(p => p.TenantId == tenantId);

            //var responseQuery = query.ProjectTo<PlayerResponse>(_mapper.ConfigurationProvider);
            query = desc ?
                query.OrderByDescending(orderBy)
                : query.OrderBy(orderBy);

            var response = HandicapResponse<PlayerResponse, Player>.Create(query, null, page, pageSize, _mapper);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var tenantId = this.GetTenantId();

            var player = await _playerService.GetById(id, tenantId);

            var playerResponse = _mapper.Map<PlayerResponse>(player);

            return Ok(playerResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PlayerRequest playerRequest)
        {
            var tenantId = this.GetTenantId();

            var player = _mapper.Map<Player>(playerRequest);
            player.TenantId = tenantId;

            var playerResponse = await _playerService.AddOrUpdate(player);

            return CreatedAtAction(
                nameof(GetById),
                new { id = playerResponse.Id },
                playerResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]string id, [FromBody]PlayerRequest playerRequest)
        {
            var tenantId = this.GetTenantId();

            var existingPlayer = await _playerService.GetById(id, tenantId);

            existingPlayer.FirstName = playerRequest.FirstName;
            existingPlayer.LastName = playerRequest.LastName;
            existingPlayer.Handicap = playerRequest.Handicap >= 0 && playerRequest.Handicap <= 95 ?
                playerRequest.Handicap : existingPlayer.Handicap;

            existingPlayer = await _playerService.AddOrUpdate(existingPlayer);

            return Ok(_mapper.Map<PlayerResponse>(existingPlayer));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var tenantId = this.GetTenantId();
            await _playerService.Delete(id, tenantId);

            return NoContent();
        }
        #endregion

        [HttpGet("{id}/games")]
        public async Task<IActionResult> GetGamesFromPlayer(
            [FromRoute]string id,
            [FromQuery]string orderBy = "Date",
            [FromQuery]bool desc = false,
            [FromQuery]int pageSize = 10,
            [FromQuery]int page = 0)
        {
            var tenantId = this.GetTenantId();

            var games = await _gameService.Find(g => g.TenantId == tenantId,
                nameof(Game.PlayerOne),
                nameof(Game.PlayerTwo));

            games = games.Where(g => g.PlayerOne.Id == id || g.PlayerTwo.Id == id);
            games = desc ?
                games.OrderByDescending(orderBy)
                : games.OrderBy(orderBy);

            var response = HandicapResponse<GameResponse, Game>.Create(games, null, page, pageSize, _mapper);

            return Ok(response);
        }
    }
}