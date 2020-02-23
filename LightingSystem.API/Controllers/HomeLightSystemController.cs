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
            List<LightBulb> bulbs = new List<LightBulb>();

            bulbs = _mapper.Map<List<LightBulb>>(request.LightBulbs);
            var lightPoint = await _mediator.Send(
                new AddLightPointCommand(
                    request.MqttId,
                    request.CustomName,
                    homeLightSystemId,
                    bulbs));
            // TODO mapper
            return Created(string.Empty, lightPoint);
        }

        // POST: api/LightSystem/disableLightPoint/4a3b39c7-ac56-4853-900f-b776c10cc2e3
        [HttpPost("disableLightPoint/{lightPointId}")]
        public async Task<IActionResult> DisableLightPoint( Guid lightPointId)
        {
            await _mediator.Send(new DisableLighPointCommand(lightPointId));
            return Ok(lightPointId);
        }

        // POST: api/LightSystem/enableLightPoint/4a3b39c7-ac56-4853-900f-b776c10cc2e3
        [HttpPost("enableLightPoint/{lightPointId}")]
        public async Task<IActionResult> EnableLightPoint( Guid lightPointId)
        {
            await _mediator.Send(new EnableLightPointCommand(lightPointId));
            return Ok(lightPointId);
        }

        // POST: api/LightSystem/disableAllLightPoints/4a3b39c7-ac56-4853-900f-b776c10cc2e3
        [HttpPost("disableAllLightPoints/{homeLightSystemid}")]
        public async Task<IActionResult> DisableAllLightPoints(Guid homeLightSystemId)
        {
            await _mediator.Send(new DisableAllLightPointsCommand(homeLightSystemId));
            return Ok(homeLightSystemId);
        }

        // POST: api/LightSystem/enableAllLightPoints/4a3b39c7-ac56-4853-900f-b776c10cc2e3
        [HttpPost("enableAllLightPoints/{homeLightSystemid}")]
        public async Task<IActionResult> EnableAllLightPoints(Guid homeLightSystemId)
        {
            await _mediator.Send(new EnableAllLightPointsCommand(homeLightSystemId));
            return Ok(homeLightSystemId);
        }

        // POST: api/changeLightBulbStatus/lightBulbId/4a3b39c7-ac56-4853-900f-b776c10cc2e3
        [HttpPost("changeLightBulbStatus/{lightBulbId}")]
        public async Task<IActionResult> ChangeLightBulbStatus(Guid lightBulbId, [FromBody] bool status)
        {
            await _mediator.Send(new ChangeLightBulbStatusCommand(lightBulbId, status));
            return Ok(lightBulbId);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
