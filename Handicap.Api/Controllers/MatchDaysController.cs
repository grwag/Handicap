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
                .Find(m => m.TenantId == tenantId,
                nameof(MatchDay.Games),
                nameof(MatchDay.MatchDayPlayers));

            var response = HandicapResponse<MatchDayResponse, MatchDay>.Create(matchDayQuery, null, page, pageSize, _mapper);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id){
            var tenantId = this.GetTenantId();

            var matchDay = await _matchDayService.GetById(id);

            return Ok(_mapper.Map<MatchDayResponse>(matchDay));
        }

        [HttpGet("open")]
        public async Task<IActionResult> GetOpenMatchDays(
            [FromQuery]string orderBy = "Date",
            [FromQuery]bool desc = false,
            [FromQuery]int pageSize = 10,
            [FromQuery]int page = 0
        ){
            var tenantId = this.GetTenantId();
            var matchDayQuery = await _matchDayService.Find(m => m.TenantId == tenantId && !m.IsFinished);

            matchDayQuery = desc ?
                matchDayQuery.OrderByDescending(orderBy)
                : matchDayQuery.OrderBy(orderBy);

            var response = HandicapResponse<MatchDayResponse, MatchDay>.Create(matchDayQuery, null, page, pageSize, _mapper);

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
            var players = await _matchDayService.GetMatchDayPlayers(id, tenantId);

            var response = HandicapResponse<PlayerResponse, Player>.Create(players, null, page, pageSize, _mapper);

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
            var games = await _matchDayService.GetMatchDayGames(id, tenantId);

            var response = HandicapResponse<GameResponse, Game>.Create(games, null, page, pageSize, _mapper);

            return Ok(response);
        }

        [HttpPost("{id}/newGame")]
        public async Task<IActionResult> GetNewGame([FromRoute]string id)
        {
            var tenantId = this.GetTenantId();
            var newGame = await _gameService.CreateNewGameForMatchDay(id, tenantId);

            var gameResponse = _mapper.Map<GameResponse>(newGame);

            return CreatedAtRoute(
                routeName: "GetGameById",
                routeValues: new { id = newGame.Id },
                value: gameResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var tenantId = this.GetTenantId();
            var matchDay = await _matchDayService.CreateMatchDay(tenantId);
            var matchDayResponse = _mapper.Map<MatchDayResponse>(matchDay);

            return CreatedAtAction(
                nameof(Get),
                new { id = matchDay.Id },
                matchDayResponse);
        }

        [HttpPost("{id}/players")]
        public async Task<IActionResult> AddPlayer(string id, [FromBody]AddPlayerToMatchDayRequest addPlayerRequest)
        {
            var tenantId = this.GetTenantId();
            var matchDay = await _matchDayService.AddPlayers(id, addPlayerRequest.PlayerIds, tenantId);

            return Ok(_mapper.Map<MatchDayResponse>(matchDay));
        }

        [HttpDelete("{id}/players/{playerId}")]
        public async Task<IActionResult> RemovePlayer([FromRoute]string id, [FromRoute]string playerId)
        {
            var tenantId = this.GetTenantId();
            var matchDay = await _matchDayService.RemovePlayer(id, playerId, tenantId);

            return Ok(_mapper.Map<MatchDayResponse>(matchDay));
        }

        [HttpPost("{id}/games/{gameId}")]
        public async Task<IActionResult> AddGame([FromRoute]string id,[FromRoute]string gameId)
        {
            var tenantId = this.GetTenantId();

            var matchDay = await _matchDayService.AddGame(id, gameId, tenantId);
            return Ok(_mapper.Map<MatchDayResponse>(matchDay));
        }

        [HttpPost("{id}/finalize")]
        public async Task<IActionResult> FinalizeMatchDay([FromRoute]string id)
        {
            var tenantId = this.GetTenantId();
            var matchDay = await _matchDayService.FinalizeMatchDay(id, tenantId);

            return Ok(_mapper.Map<MatchDayResponse>(matchDay));
        }
    }
}
