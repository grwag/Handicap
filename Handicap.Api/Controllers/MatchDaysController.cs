using AutoMapper;
using Handicap.Application.Services;
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

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var matchDay = await _matchDayService.CreateMatchDay();

            return Ok(_mapper.Map<MatchDayResponse>(matchDay));
        }

        [HttpPost("{id}/players")]
        public async Task<IActionResult> AddPlayer(string id, [FromBody]AddPlayerToMatchDayRequest addPlayerRequest)
        {
            //var player = await _playerService.GetById(addPlayerRequest.PlayerId);
            var matchDay = await _matchDayService.GetById(id);

            //await _matchDayService.AddPlayer(matchDay, player);

            return Ok(_mapper.Map<MatchDayResponse>(matchDay));
        }
    }
}
