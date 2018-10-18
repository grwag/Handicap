using AutoMapper;
using Handicap.Application.Services;
using Handicap.Data.Paging;
using Handicap.Domain.Models;
using Handicap.Dto.Request;
using Handicap.Dto.Response;
using Handicap.Dto.Response.Paging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Handicap.Api.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;

        public GamesController(IGameService gameService,
            IPlayerService playerService,
            IMapper mapper)
        {
            _gameService = gameService;
            _playerService = playerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(PagingParameters pagingParameters)
        {
            var games = await _gameService.All();

            var pagedResponse = PagedList<GameResponse>.Create(
                _mapper.Map<IEnumerable<GameResponse>>(games).AsQueryable(),
                pagingParameters.PageNumber,
                pagingParameters.PageSize);

            return Ok(pagedResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody]GameRequest gameRequest)
        {
            var game = await _gameService.Insert(
                gameRequest.PlayerOneId,
                gameRequest.PlayerTwoId);

            var gameResponse = _mapper.Map<GameResponse>(game);

            return StatusCode((int)HttpStatusCode.Created, gameResponse);
        }
    }
}
