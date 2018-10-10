using AutoMapper;
using Handicap.Application.Services;
using Handicap.Data.Paging;
using Handicap.Domain.Models;
using Handicap.Dto.Request;
using Handicap.Dto.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Handicap.Api.Controllers
{
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;

        public PlayersController(IPlayerService playerService, IMapper mapper)
        {
            _playerService = playerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(PagingParameters pagingParameters)
        {
            var players = await _playerService.FindAsync(
                null,
                pagingParameters);

            return Ok(players);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody]PlayerRequest playerRequest)
        {
            var player = _mapper.Map<Player>(playerRequest);
            player = await _playerService.InsertPlayer(player);
            var playerResponse = _mapper.Map<PlayerResponse>(player);

            return Ok(playerResponse);
        }
    }
}
