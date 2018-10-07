using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Handicap.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Handicap.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMatchDayService _matchDayService;

        public ValuesController(IMatchDayService matchDayService)
        {
            _matchDayService = matchDayService;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var md = await _matchDayService.CreateMatchDay(4);
            
            if(md != null)
            {
                return Ok(md);
            }

            return BadRequest();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
