using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightingSystem.Domain.Commands;
using LightingSystem.Domain.Dtos;
using LightingSystem.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LightingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeLightSystemController : Controller
    {
        private readonly IMediator _mediator;

        public HomeLightSystemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/LightSystem
        [HttpGet]
        public async Task<HomeLightSystemDto> Get()
        {
            var test = await _mediator.Send(new GetLightingSystemQuery(Guid.NewGuid()));
            return test;
        }

        // GET: api/LightSystem/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/LightSystem
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HomeLightSystemCommandDto request)
        {
            var homeLightSystem = await _mediator.Send(new AddLightSystemCommand(request.UserName));
            return Created(string.Empty, homeLightSystem);
        }

        // PUT: api/LightSystem/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
