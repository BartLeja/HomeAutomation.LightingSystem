using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LightingSystem.API.Commands;
using LightingSystem.API.Queries;
using LightingSystem.Data.Dtos;
using LightingSystem.Domain.HomeLightSystem;
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
        private readonly IMapper _mapper;

        public HomeLightSystemController(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
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
        public async Task<HomeLightSystemDto> Get(Guid id)
        {
            var test = await _mediator.Send(new GetLightingSystemQuery(id));
            return test;
        }

        // POST: api/LightSystem
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HomeLightSystemCommandDto request)
        {
            var homeLightSystem = await _mediator.Send(new AddLightSystemCommand(request.UserName));
            return Created(string.Empty, homeLightSystem);
        }

        // POST: api/LightSystem/LightPoint
        [HttpPost("{homeLightSystemId}")]
        public async Task<IActionResult> PostLightPoint(Guid homeLightSystemId, [FromBody] LightPointDto request)
        {
            List<Bulb> bulbs = new List<Bulb>();

            //foreach (var lightBulb in request.LightBulbs)
            //{
            //    bulbs.Add(_mapper.Map<Bulb>(lightBulb));
            //}
            bulbs = _mapper.Map<List<Bulb>>(request.LightBulbs);
            var lightPoint = await _mediator.Send(
                new AddLightPointCommand(
                    request.LightBulbsCount,
                    request.MqttId,
                    request.CustomName,
                    homeLightSystemId,
                    bulbs));
            // TODO mapper
            return Created(string.Empty, lightPoint);
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
