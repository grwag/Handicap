using AutoMapper;
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
            var matchDayQuery = await _matchDayService.Find(m => m.TenantId == tenantId,
                $"{nameof(MatchDay.Games)}")
                //$"{nameof(MatchDay.Players)}")
                ;

            var response = new HandicapResponse<MatchDay>(matchDayQuery, null, page, pageSize);
            
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
            var matchDay = await _matchDayService.GetById(id);
            matchDay = await _matchDayService.AddPlayers(matchDay, addPlayerRequest.PlayerIds);

            return Ok(_mapper.Map<MatchDayResponse>(matchDay));
        }

        [HttpPost("{id}/games/{gameId}")]
        public async Task<IActionResult> AddGame([FromRoute]string id,[FromRoute]string gameId)
        {
            var tenantId = this.GetTenantId();
            //var game = await _gameService.CreateGame(
            //    tenantId,
            //    gameRequest.PlayerOneId,
            //    gameRequest.PlayerTwoId,
            //    id);

            var matchDay = await _matchDayService.AddGame(id, gameId);
            return Ok(_mapper.Map<MatchDayResponse>(matchDay));
        }
    }
}
