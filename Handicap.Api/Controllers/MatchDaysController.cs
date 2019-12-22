using AutoMapper;
using AutoMapper.QueryableExtensions;
using Handicap.Api.Extensions;
using Handicap.Api.Paging;
using Handicap.Application.Services;
using Handicap.Domain.Models;
using Handicap.Dto.Request;
using Handicap.Dto.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handicap.Api.Controllers
{
    [Route("api/[controller]")]
    public class MatchDaysController : ControllerBase
    {
        private readonly IMatchDayService _matchDayService;
        private readonly IPlayerService _playerService;
        private readonly IGameService _gameService;

        private readonly IMapper _mapper;
        private readonly ILogger<MatchDaysController> _logger;

        public MatchDaysController(IMatchDayService matchDayService,
            IPlayerService playerService,
            IGameService gameService,
            IMapper mapper,
            ILogger<MatchDaysController> logger)
        {
            _matchDayService = matchDayService;
            _playerService = playerService;
            _gameService = gameService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery]string orderBy = "Date",
            [FromQuery]bool desc = false,
            [FromQuery]int pageSize = 10,
            [FromQuery]int page = 0)
        {
            var tenantId = this.GetTenantId();
            var matchDayQuery = await _matchDayService
                .Find(m => m.TenantId == tenantId);

            var responseQuery = matchDayQuery.ProjectTo<MatchDayResponse>(_mapper.ConfigurationProvider);

            var response = new HandicapResponse<MatchDayResponse>(responseQuery, null, page, pageSize);
            
            return Ok(response);
        }

        [HttpGet("{id}/players")]
        public async Task<IActionResult> GetMatchDayPlayers(
            [FromRoute]string id,
            [FromQuery]string orderBy = "Date",
            [FromQuery]bool desc = false,
            [FromQuery]int pageSize = 10,
            [FromQuery]int page = 0)
        {
            var tenantId = this.GetTenantId();
            var players = await _matchDayService.GetMatchDayPlayers(id);

            var responseQuery = players.ProjectTo<PlayerResponse>(_mapper.ConfigurationProvider);

            var response = new HandicapResponse<PlayerResponse>(responseQuery, null, page, pageSize);

            return Ok(response);
        }

        [HttpGet("{id}/games")]
        public async Task<IActionResult> GetMatchDayGames(
            [FromRoute]string id,
            [FromQuery]string orderBy = "Date",
            [FromQuery]bool desc = false,
            [FromQuery]int pageSize = 10,
            [FromQuery]int page = 0)
        {
            var tenantId = this.GetTenantId();
            var games = await _matchDayService.GetMatchDayGames(id);

            // workaround
            var gameResponse = _mapper.Map<List<GameResponse>>(games.ToList());

            //var responseQuery = games.ProjectTo<GameResponse>(_mapper.ConfigurationProvider);

            var response = new HandicapResponse<GameResponse>(gameResponse.AsQueryable(), null, page, pageSize);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var tenantId = this.GetTenantId();
            var matchDay = await _matchDayService.CreateMatchDay(tenantId);

            return Ok(_mapper.Map<MatchDayResponse>(matchDay));
        }

        [HttpPost("{id}/players")]
        public async Task<IActionResult> AddPlayer(string id, [FromBody]AddPlayerToMatchDayRequest addPlayerRequest)
        {
            var matchDay = await _matchDayService.AddPlayers(id, addPlayerRequest.PlayerIds);

            return Ok(_mapper.Map<MatchDayResponse>(matchDay));
        }

        [HttpDelete("{id}/players/{playerId}")]
        public async Task<IActionResult> RemovePlayer([FromRoute]string id, [FromRoute]string playerId)
        {
            var matchDay = await _matchDayService.RemovePlayer(id, playerId);

            return Ok(_mapper.Map<MatchDayResponse>(matchDay));
        }

        [HttpPost("{id}/games/{gameId}")]
        public async Task<IActionResult> AddGame([FromRoute]string id,[FromRoute]string gameId)
        {
            var tenantId = this.GetTenantId();

            var matchDay = await _matchDayService.AddGame(id, gameId);
            return Ok(_mapper.Map<MatchDayResponse>(matchDay));
        }
    }
}
